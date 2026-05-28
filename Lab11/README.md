# Лабораторная работа №11 — HTTP, REST API и асинхронность

## Цель

Познакомиться с протоколом HTTP, форматом JSON, авторизацией через токен и асинхронным программированием в C# на примере публичного API `restful-booker.herokuapp.com`.

---

## Теория

### REST API и HTTP

**REST** — архитектурный стиль взаимодействия клиента и сервера через HTTP. Каждый URL — это «адрес ресурса», а HTTP-метод — действие над ним.

| Метод | Действие | Пример URL |
|---|---|---|
| GET | Получить | `GET /booking` |
| POST | Создать | `POST /booking` |
| PUT | Обновить | `PUT /booking/5` |
| DELETE | Удалить | `DELETE /booking/5` |

Сервер отвечает кодом статуса: `200` (успех), `201` (создано), `400` (плохой запрос), `401` (нет авторизации), `404` (нет ресурса). API `restful-booker` иногда отвечает `418` — это специально оставленные «битые» ответы для тренировки обработки ошибок.

### JSON

Текстовый формат обмена данными. В C# работают через библиотеку `Newtonsoft.Json` (NuGet):

```csharp
string json = JsonConvert.SerializeObject(obj);          // объект → строка
Booking b   = JsonConvert.DeserializeObject<Booking>(json); // строка → объект
```

### HttpClient

Класс из `System.Net.Http`. Главный инструмент для HTTP в C#. Создаётся **один на всё приложение** (`static readonly`), иначе при частом пересоздании заканчиваются сетевые сокеты.

```csharp
static readonly HttpClient Http = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
```

Заголовок `Accept: application/json` важен — без него сервер `restful-booker` может вернуть XML вместо JSON.

### Авторизация через токен

Изменение данных через REST требует авторизации. Схема:

1. `POST /auth` с логином/паролем → сервер возвращает токен.
2. Дальше токен передаётся в заголовке `Cookie: token=...` при операциях `PUT` и `DELETE`.

В основном задании реализован только шаг 1 (получение токена) — токен выводится в статусной строке.

### async/await и Task.Run

HTTP-запрос — долгая операция. Если выполнять в главном потоке формы, окно зависнет. Решение — запускать в фоне через `Task.Run` и обновлять UI через `Invoke(MethodInvoker)`:

```csharp
Task.Run(async () =>
{
    string json = await Http.GetStringAsync(url);
    Invoke(new MethodInvoker(() => listView.Items.Add(...)));
});
```

### Task.WhenAll — параллельные запросы

Если нужно загрузить 100 записей по одной — это будет 100 × время одного запроса. С `Task.WhenAll` все запросы пускаются параллельно, и общее время ≈ времени самого медленного запроса:

```csharp
var tasks = ids.Select(id => FetchBooking(id)).ToList();
var results = await Task.WhenAll(tasks);
```

### Проверка статуса

`HttpClient` сам не бросает исключение при коде `4xx` — нужно проверять вручную:

```csharp
var resp = await Http.GetAsync(url);
if (!resp.IsSuccessStatusCode) return null;
string json = await resp.Content.ReadAsStringAsync();
if (json.TrimStart().StartsWith("<")) return null; // вдруг XML
```

---

## Структура проекта

```
Lab11/
  Lab11.sln
  README.md
  Lab11/
    Lab11.csproj         ← .NET Framework 4.7.2 + Newtonsoft.Json
    packages.config
    App.config
    Program.cs
    Models.cs            ← классы AuthResponse, BookingId, BookingDates, Booking
    Form1.cs             ← логика авторизации и загрузки
    Form1.Designer.cs    ← интерфейс формы
    Form1.resx
    Properties/AssemblyInfo.cs
```

## Используемые эндпоинты

| Операция | Метод | URL |
|---|---|---|
| Получить токен | POST | `/auth` |
| Список ID | GET | `/booking` |
| Детали записи | GET | `/booking/{id}` |

Базовый URL: `http://restful-booker.herokuapp.com`
Логин: `admin`, пароль: `password123`

---

## Как работает программа

### Авторизация (кнопка «Войти»)

```csharp
string body = JsonConvert.SerializeObject(new {
    username = loginBox.Text,
    password = passwordBox.Text
});
var content  = new StringContent(body, Encoding.UTF8, "application/json");
var response = Http.PostAsync(BASE + "/auth", content).Result;
string json  = response.Content.ReadAsStringAsync().Result;
var auth     = JsonConvert.DeserializeObject<AuthResponse>(json);
_token = auth.token;
```

Отправляем `POST /auth` с JSON-телом, получаем токен и сохраняем его в `_token`. После успешного входа поля логина/пароля блокируются, кнопка «Загрузить» становится активной.

### Загрузка (кнопка «Загрузить бронирования»)

1. **Запрос списка ID**: `GET /booking` → массив объектов вида `{ "bookingid": 123 }`.
2. **Загрузка деталей батчами по 20**: внутри батча 20 запросов отправляются параллельно через `Task.WhenAll`.
3. **Фильтрация**: если сервер ответил кодом ≠ 200 или вернул XML — запись пропускается.
4. **Остановка**: как только набирается **10 успешных** записей, цикл прерывается.
5. **Отображение**: каждая успешная запись добавляется строкой в `ListView` с колонками ID, Имя, Фамилия, Цена, Депозит, Заезд, Выезд, Доп. пожелания.

```csharp
for (int start = 0; start < total && loaded < 10; start += 20)
{
    var tasks = new List<Task<Booking>>();
    for (int i = start; i < Math.Min(start + 20, total); i++)
        tasks.Add(FetchBooking(ids[i].bookingid));

    var results = await Task.WhenAll(tasks);

    foreach (var b in results)
    {
        if (b == null) continue;
        loaded++;
        AddBookingToList(...);
        if (loaded == 10) break;
    }
}
```

### Безопасная загрузка одной записи

```csharp
async Task<Booking> FetchBooking(int id)
{
    try
    {
        var resp = await Http.GetAsync(BASE + "/booking/" + id);
        if (!resp.IsSuccessStatusCode) return null;
        string json = await resp.Content.ReadAsStringAsync();
        if (json.TrimStart().StartsWith("<")) return null;
        return JsonConvert.DeserializeObject<Booking>(json);
    }
    catch { return null; }
}
```

Возвращает `null` при любой ошибке — это сигнал «пропустить запись».

### Обновление UI из фона

Все изменения формы (статус, список) выполняются через `Invoke(new MethodInvoker(...))`, потому что обращаться к UI из фонового потока нельзя.

### Блокировка кнопок

| Состояние | loginButton | loadButton | login/password |
|---|---|---|---|
| Старт | активна | заблокирована | редактируемы |
| Авторизация в процессе | заблокирована | заблокирована | редактируемы |
| После входа | заблокирована | активна | заблокированы |
| Загрузка в процессе | заблокирована | заблокирована | заблокированы |
| После загрузки | заблокирована | активна | заблокированы |

---

## Порядок запуска

1. Открыть `Lab11.sln` в Visual Studio 2022.
2. Восстановить NuGet-пакеты: **Инструменты → Диспетчер пакетов NuGet → Восстановить пакеты** (скачает `Newtonsoft.Json`).
3. Запустить (`F5`).
4. Логин и пароль уже подставлены (`admin` / `password123`) — нажать **«Войти»**. В статусе появится токен.
5. Нажать **«Загрузить бронирования»** — таблица заполнится 10 записями.

---

## Особенности `restful-booker`

API публичный и специально «шумит» ошибками для тренировки: при загрузке некоторых ID он может вернуть `418 I'm a teapot` или XML. Поэтому в коде стоит двойная защита — проверка `IsSuccessStatusCode` и проверка первого символа ответа на `<`.
