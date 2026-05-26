# Лабораторная работа №8 — RSS-лента + SQLite на C#

## Цель

Научиться получать данные из интернета (RSS-лента), разбирать XML-документ и сохранять данные в локальную базу данных SQLite.

---

## Теория

### XML и RSS

**XML** (eXtensible Markup Language) — язык разметки для хранения и передачи структурированных данных. Документ состоит из тегов-элементов: открывающего `<tag>`, закрывающего `</tag>` и вложенного содержимого. Есть один корневой элемент, внутри которого находятся все остальные.

**RSS** (Really Simple Syndication) — формат на основе XML для публикации новостных лент, анонсов и обновлений сайтов. RSS-документ имеет следующую структуру:

```xml
<rss version="2.0">
  <channel>
    <title>Название канала</title>
    <item>
      <title>Заголовок новости</title>
      <link>Ссылка на полную статью</link>
      <description>Краткое описание</description>
      <pubDate>Дата и время публикации</pubDate>
    </item>
    ...
  </channel>
</rss>
```

Корень — `<rss>`, внутри один `<channel>`, внутри него множество `<item>` — каждый item это одна новость.

### HttpWebRequest / HttpWebResponse

Классы из `System.Net`. Позволяют отправить HTTP-запрос на сервер и получить ответ. `HttpWebRequest` формирует запрос (адрес, заголовки), `HttpWebResponse` содержит ответ (статус, поток с данными). Данные читаются из потока через `StreamReader`.

### XmlDocument

Класс из `System.Xml`. Загружает весь XML-документ в память и представляет его как дерево узлов (`XmlNode`). Для поиска по дереву используются методы:
- `SelectSingleNode("имя")` — найти один узел по имени
- `SelectNodes("имя")` — получить список всех узлов с таким именем
- `.InnerText` — получить текстовое содержимое узла

### SQLite

**SQLite** — встраиваемая база данных: не нужен отдельный сервер, вся БД хранится в одном файле `.db`. Подключается через NuGet-пакет `System.Data.SQLite.Core`.

Типичный порядок работы:
1. Создать `SQLiteConnection` с путём к файлу БД и вызвать `.Open()`
2. Создать `SQLiteCommand` с SQL-запросом
3. Вызвать `.ExecuteNonQuery()` для команд без возврата данных (INSERT, DELETE, CREATE)
4. Вызвать `.ExecuteReader()` для SELECT — получить `SQLiteDataReader`
5. В конце `.Close()` и `.Dispose()`

Таблица `News` в проекте:

| Поле        | Тип     | Описание                        |
|-------------|---------|---------------------------------|
| Id          | INTEGER | Первичный ключ, автоинкремент   |
| Title       | TEXT    | Заголовок новости               |
| Link        | TEXT    | Ссылка на полную статью         |
| Description | TEXT    | Краткое описание                |
| PubDate     | TEXT    | Дата и время публикации         |

---

## Как работает программа

### Кнопка «Загрузить RSS»

```csharp
HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RSS_URL);
request.UserAgent = "Mozilla/5.0";
HttpWebResponse response = (HttpWebResponse)request.GetResponse();
Stream stream = response.GetResponseStream();
StreamReader reader = new StreamReader(stream);
_rawXml = reader.ReadToEnd();
txtRaw.Text = _rawXml;
```

Создаётся HTTP-запрос к адресу RSS-ленты Коммерсанта. Ответ читается как текст и отображается в первом поле. Текст сохраняется в поле `_rawXml` для последующей обработки.

### Кнопка «Разобрать XML»

```csharp
XmlDocument xmlDoc = new XmlDocument();
xmlDoc.LoadXml(_rawXml);

XmlNodeList items = xmlDoc.DocumentElement
    .SelectSingleNode("channel")
    .SelectNodes("item");

foreach (XmlNode item in items)
{
    string title       = item.SelectSingleNode("title")?.InnerText ?? "";
    string link        = item.SelectSingleNode("link")?.InnerText ?? "";
    string description = item.SelectSingleNode("description")?.InnerText ?? "";
    string pubDate     = item.SelectSingleNode("pubDate")?.InnerText ?? "";
    // вывод в txtParsed
}
```

Загружаем XML-текст в `XmlDocument`. Идём по цепочке: корень → `channel` → все `item`. Из каждого `item` извлекаем нужные поля и выводим во второе текстовое поле.

### Кнопка «Сохранить в БД»

```csharp
SQLiteConnection db = new SQLiteConnection("Data Source=news.db;");
db.Open();

// Создаём таблицу, если не существует
SQLiteCommand command = new SQLiteCommand("PRAGMA synchronous = 1;" +
    "CREATE TABLE IF NOT EXISTS News (Id INTEGER PRIMARY KEY AUTOINCREMENT," +
    "Title TEXT, Link TEXT, Description TEXT, PubDate TEXT);", db);
command.ExecuteNonQuery();

// Удаляем старые новости
command = new SQLiteCommand("DELETE FROM News", db);
command.ExecuteNonQuery();

// Вставляем новые через параметры (защита от SQL-инъекций)
command = new SQLiteCommand(
    "INSERT INTO News(Title, Link, Description, PubDate) " +
    "VALUES(@title, @link, @description, @pubDate)", db);
command.Parameters.AddWithValue("@title", title);
// ...
command.ExecuteNonQuery();
```

Создаётся (или открывается) файл `news.db`. Таблица создаётся при первом запуске. Перед вставкой старые записи удаляются — так в БД всегда только актуальные новости. Вставка идёт через именованные параметры (`@title` и т.д.), что безопаснее конкатенации строк.

### Кнопка «Читать из БД»

```csharp
SQLiteCommand command = new SQLiteCommand("SELECT * FROM News", db);
SQLiteDataReader reader = command.ExecuteReader();

foreach (DbDataRecord record in reader)
{
    string title = record["Title"].ToString();
    // ...
}
```

Запрос `SELECT * FROM News` читает все строки таблицы. `SQLiteDataReader` позволяет перебирать строки в цикле. Поля извлекаются по имени через индексатор.

---

## Порядок запуска

1. Открыть `Lab8.sln` в Visual Studio 2022
2. Восстановить NuGet-пакеты: меню **Инструменты → Диспетчер пакетов NuGet → Восстановить пакеты**
3. Запустить проект (F5)
4. Нажать **«Загрузить RSS»** — в первом поле появится XML-текст ленты
5. Нажать **«Разобрать XML»** — во втором поле появятся новости в читаемом виде
6. Нажать **«Сохранить в БД»** — новости запишутся в файл `news.db` (создаётся рядом с `.exe`)
7. Нажать **«Читать из БД»** — в третьем поле выведутся все записи из базы

---

## Структура БД

**Таблица: News**

```sql
CREATE TABLE IF NOT EXISTS News (
    Id          INTEGER PRIMARY KEY AUTOINCREMENT,
    Title       TEXT,
    Link        TEXT,
    Description TEXT,
    PubDate     TEXT
);
```

Файл базы данных: `news.db` (создаётся автоматически рядом с исполняемым файлом).
