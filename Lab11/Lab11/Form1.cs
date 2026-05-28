using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Lab11
{
    public partial class Form1 : Form
    {
        // Базовый URL публичного API restful-booker
        const string BASE = "http://restful-booker.herokuapp.com";

        // Один общий HttpClient на всё приложение — так рекомендуется в .NET,
        // чтобы не плодить сокеты при каждом запросе.
        static readonly HttpClient Http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(15)
        };

        string _token = "";

        public Form1()
        {
            InitializeComponent();

            // Сообщаем серверу, что ждём ответ в формате JSON
            Http.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // До авторизации кнопка загрузки заблокирована
            loadButton.Enabled = false;
        }

        // ---------- АВТОРИЗАЦИЯ ----------

        private void loginButton_Click(object sender, EventArgs e)
        {
            loginButton.Enabled = false;
            statusLabel.Text = "Авторизация...";

            // Вся сетевая работа — в фоновом потоке
            Task.Run(() =>
            {
                try
                {
                    string body = JsonConvert.SerializeObject(new
                    {
                        username = loginBox.Text,
                        password = passwordBox.Text
                    });

                    var content  = new StringContent(body, Encoding.UTF8, "application/json");
                    var response = Http.PostAsync(BASE + "/auth", content).Result;
                    string json  = response.Content.ReadAsStringAsync().Result;

                    var auth = JsonConvert.DeserializeObject<AuthResponse>(json);

                    if (auth == null || auth.token == null || auth.token == "Bad credentials")
                        throw new Exception("Неверный логин или пароль");

                    _token = auth.token;

                    Invoke(new MethodInvoker(() =>
                    {
                        statusLabel.Text  = "Авторизован. Токен: " + _token;
                        loginBox.Enabled    = false;
                        passwordBox.Enabled = false;
                        loadButton.Enabled  = true;
                    }));
                }
                catch (Exception ex)
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        statusLabel.Text    = "Ошибка: " + ex.Message;
                        loginButton.Enabled = true;
                    }));
                }
            });
        }

        // ---------- ЗАГРУЗКА БРОНИРОВАНИЙ ----------

        private void loadButton_Click(object sender, EventArgs e)
        {
            loadButton.Enabled = false;
            listView.Items.Clear();

            Task.Run(async () =>
            {
                try
                {
                    // 1. Получаем все ID
                    string listJson = await Http.GetStringAsync(BASE + "/booking");
                    var ids = JsonConvert.DeserializeObject<List<BookingId>>(listJson);

                    Invoke(new MethodInvoker(() =>
                        statusLabel.Text = "Найдено " + ids.Count + " ID. Загружаю..."));

                    int loaded = 0;
                    int total  = ids.Count;
                    const int BATCH = 20;       // параллельных запросов в одном батче
                    const int NEED  = 10;       // нужно собрать 10 успешных

                    // 2. Идём батчами по 20, пока не наберём 10 успешных
                    for (int start = 0; start < total && loaded < NEED; start += BATCH)
                    {
                        int end = Math.Min(start + BATCH, total);

                        // Запускаем 20 запросов параллельно через Task.WhenAll
                        var tasks = new List<Task<Booking>>();
                        var batchIds = new List<int>();
                        for (int i = start; i < end; i++)
                        {
                            batchIds.Add(ids[i].bookingid);
                            tasks.Add(FetchBooking(ids[i].bookingid));
                        }
                        var results = await Task.WhenAll(tasks);

                        // 3. Обрабатываем результаты батча
                        for (int j = 0; j < results.Length && loaded < NEED; j++)
                        {
                            Booking b = results[j];
                            if (b == null) continue; // 418, 404 или XML — пропускаем

                            int id = batchIds[j];
                            loaded++;
                            AddBookingToList(id, b);
                        }
                    }

                    int finalLoaded = loaded;
                    int finalTotal  = total;
                    Invoke(new MethodInvoker(() =>
                    {
                        statusLabel.Text =
                            "Готово. Показано " + finalLoaded + " из " + finalTotal + " бронирований.";
                        loadButton.Enabled = true;
                    }));
                }
                catch (Exception ex)
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        statusLabel.Text   = "Ошибка загрузки: " + ex.Message;
                        loadButton.Enabled = true;
                    }));
                }
            });
        }

        // Запрос одного бронирования. Возвращает null, если ответ некорректный.
        async Task<Booking> FetchBooking(int id)
        {
            try
            {
                var resp = await Http.GetAsync(BASE + "/booking/" + id);
                if (!resp.IsSuccessStatusCode) return null;

                string json = await resp.Content.ReadAsStringAsync();

                // Сервер иногда возвращает XML вместо JSON — пропускаем такие ответы
                if (json.TrimStart().StartsWith("<")) return null;

                return JsonConvert.DeserializeObject<Booking>(json);
            }
            catch
            {
                return null;
            }
        }

        // Добавление строки в ListView — должно выполняться в главном потоке
        void AddBookingToList(int id, Booking b)
        {
            Invoke(new MethodInvoker(() =>
            {
                var item = new ListViewItem(id.ToString());
                item.SubItems.Add(b.firstname ?? "");
                item.SubItems.Add(b.lastname ?? "");
                item.SubItems.Add(b.totalprice + " руб.");
                item.SubItems.Add(b.depositpaid ? "Да" : "Нет");
                item.SubItems.Add(b.bookingdates != null ? b.bookingdates.checkin  : "");
                item.SubItems.Add(b.bookingdates != null ? b.bookingdates.checkout : "");
                item.SubItems.Add(b.additionalneeds ?? "");
                listView.Items.Add(item);
            }));
        }
    }
}
