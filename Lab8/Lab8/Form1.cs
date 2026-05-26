using System;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml;

namespace Lab8
{
    public partial class Form1 : Form
    {
        // Адрес RSS-ленты газеты "Коммерсантъ"
        private const string RSS_URL = "https://www.kommersant.ru/RSS/news.xml";

        // Сюда сохраняем скачанный XML-текст
        private string _rawXml = "";

        public Form1()
        {
            InitializeComponent();
        }

        // Кнопка "Загрузить RSS" — скачивает XML-текст ленты
        private void btnLoadRss_Click(object sender, EventArgs e)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RSS_URL);
                request.UserAgent = "Mozilla/5.0";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                _rawXml = reader.ReadToEnd();

                txtRaw.Text = _rawXml;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки: " + ex.Message);
            }
        }

        // Кнопка "Разобрать XML" — парсит XML и показывает новости
        private void btnParseXml_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_rawXml))
            {
                MessageBox.Show("Сначала загрузите RSS-ленту.");
                return;
            }

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(_rawXml);

                XmlNodeList items = xmlDoc.DocumentElement
                    .SelectSingleNode("channel")
                    .SelectNodes("item");

                txtParsed.Clear();
                foreach (XmlNode item in items)
                {
                    string title       = item.SelectSingleNode("title")?.InnerText       ?? "";
                    string link        = item.SelectSingleNode("link")?.InnerText        ?? "";
                    string description = item.SelectSingleNode("description")?.InnerText ?? "";
                    string pubDate     = item.SelectSingleNode("pubDate")?.InnerText     ?? "";

                    txtParsed.AppendText("Заголовок: " + title + "\r\n");
                    txtParsed.AppendText("Дата:       " + pubDate + "\r\n");
                    txtParsed.AppendText("Ссылка:     " + link + "\r\n");
                    txtParsed.AppendText("Аннотация:  " + description + "\r\n");
                    txtParsed.AppendText(new string('-', 70) + "\r\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка разбора XML: " + ex.Message);
            }
        }

        // Кнопка "Сохранить в БД" — удаляет старые новости и записывает новые
        private void btnSaveToDb_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_rawXml))
            {
                MessageBox.Show("Сначала загрузите RSS-ленту.");
                return;
            }

            try
            {
                SQLiteConnection db = new SQLiteConnection("Data Source=news.db;");
                db.Open();

                // Создаём таблицу, если её ещё нет
                SQLiteCommand command = new SQLiteCommand(
                    "PRAGMA synchronous = 1;" +
                    "CREATE TABLE IF NOT EXISTS News (" +
                    "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "Title TEXT, Link TEXT, Description TEXT, PubDate TEXT);",
                    db);
                command.ExecuteNonQuery();

                // Удаляем старые новости
                command = new SQLiteCommand("DELETE FROM News", db);
                command.ExecuteNonQuery();

                // Парсим XML и вставляем новые новости
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(_rawXml);

                XmlNodeList items = xmlDoc.DocumentElement
                    .SelectSingleNode("channel")
                    .SelectNodes("item");

                foreach (XmlNode item in items)
                {
                    string title       = item.SelectSingleNode("title")?.InnerText       ?? "";
                    string link        = item.SelectSingleNode("link")?.InnerText        ?? "";
                    string description = item.SelectSingleNode("description")?.InnerText ?? "";
                    string pubDate     = item.SelectSingleNode("pubDate")?.InnerText     ?? "";

                    command = new SQLiteCommand(
                        "INSERT INTO News(Title, Link, Description, PubDate) " +
                        "VALUES(@title, @link, @description, @pubDate)", db);
                    command.Parameters.AddWithValue("@title",       title);
                    command.Parameters.AddWithValue("@link",        link);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@pubDate",     pubDate);
                    command.ExecuteNonQuery();
                }

                db.Close();
                db.Dispose();

                MessageBox.Show("Новости успешно сохранены в базу данных.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения в БД: " + ex.Message);
            }
        }

        // Кнопка "Читать из БД" — считывает все записи из таблицы News
        private void btnReadFromDb_Click(object sender, EventArgs e)
        {
            try
            {
                SQLiteConnection db = new SQLiteConnection("Data Source=news.db;");
                db.Open();

                SQLiteCommand command = new SQLiteCommand("SELECT * FROM News", db);
                SQLiteDataReader reader = command.ExecuteReader();

                txtDb.Clear();
                foreach (DbDataRecord record in reader)
                {
                    txtDb.AppendText("ID:        " + record["Id"]          + "\r\n");
                    txtDb.AppendText("Заголовок: " + record["Title"]       + "\r\n");
                    txtDb.AppendText("Дата:      " + record["PubDate"]     + "\r\n");
                    txtDb.AppendText("Ссылка:    " + record["Link"]        + "\r\n");
                    txtDb.AppendText("Аннотация: " + record["Description"] + "\r\n");
                    txtDb.AppendText(new string('-', 70) + "\r\n");
                }

                db.Close();
                db.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения из БД: " + ex.Message);
            }
        }
    }
}
