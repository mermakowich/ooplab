using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab9
{
    public partial class Form1 : Form
    {
        // Адрес multicast-группы и порт для рассылки
        const string HOST = "235.5.5.1";
        const int    PORT = 8001;
        const int    TTL  = 20;

        UdpClient client;
        IPAddress groupAddress;
        bool      alive = false;
        string    userName = "";

        public Form1()
        {
            InitializeComponent();
            groupAddress = IPAddress.Parse(HOST);

            // До входа активно только поле имени и кнопка "Войти"
            publishButton.Enabled = false;
            logoutButton.Enabled  = false;
        }

        // Кнопка "Войти" — подключение к multicast-группе
        private void loginButton_Click(object sender, EventArgs e)
        {
            userName = userNameTextBox.Text.Trim();
            if (userName == "")
            {
                MessageBox.Show("Введите имя пользователя.");
                return;
            }

            try
            {
                // Создаём сокет с ReuseAddress, чтобы можно было запустить
                // несколько экземпляров программы на одной машине.
                Socket socket = new Socket(AddressFamily.InterNetwork,
                                           SocketType.Dgram,
                                           ProtocolType.Udp);
                socket.SetSocketOption(SocketOptionLevel.Socket,
                                       SocketOptionName.ReuseAddress,
                                       true);
                socket.Bind(new IPEndPoint(IPAddress.Any, PORT));

                client = new UdpClient { Client = socket };
                client.JoinMulticastGroup(groupAddress, TTL);

                alive = true;
                Task.Run((Action)ReceiveMessages);

                // Рассылаем системное сообщение о входе
                SendPacket("SYS|" + userName + " вошёл на доску объявлений");

                // Переключаем доступность кнопок
                userNameTextBox.Enabled = false;
                loginButton.Enabled     = false;
                logoutButton.Enabled    = true;
                publishButton.Enabled   = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения: " + ex.Message);
            }
        }

        // Кнопка "Выйти" — отключение от multicast-группы
        private void logoutButton_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        // Кнопка "Опубликовать" — рассылка объявления
        private void publishButton_Click(object sender, EventArgs e)
        {
            string title = titleTextBox.Text.Trim();
            string price = priceTextBox.Text.Trim();
            if (title == "" || price == "")
            {
                MessageBox.Show("Заполните заголовок и цену.");
                return;
            }

            // Упаковываем поля в одну строку через разделитель '|'
            string packed = string.Join("|", "AD", userName, title, price);
            SendPacket(packed);

            titleTextBox.Clear();
            priceTextBox.Clear();
        }

        // Отправка одного UDP-пакета в multicast-группу
        private void SendPacket(string text)
        {
            byte[] data = Encoding.Unicode.GetBytes(text);
            client.Send(data, data.Length, HOST, PORT);
        }

        // Фоновый поток приёма сообщений
        private void ReceiveMessages()
        {
            try
            {
                while (alive)
                {
                    IPEndPoint remoteIp = null;
                    byte[] data   = client.Receive(ref remoteIp);
                    string packed = Encoding.Unicode.GetString(data);

                    string[] parts = packed.Split('|');
                    string type    = parts[0];
                    string display = "";

                    if (type == "AD" && parts.Length >= 4)
                    {
                        // AD|имя|заголовок|цена
                        string time = DateTime.Now.ToString("HH:mm:ss");
                        display = "[" + time + "] " + parts[1] +
                                  ": " + parts[2] + " – " + parts[3] + " руб.";
                    }
                    else if (type == "SYS" && parts.Length >= 2)
                    {
                        // SYS|текст
                        display = "→ " + parts[1];
                    }

                    if (display != "")
                    {
                        // Изменять элементы формы можно только из главного потока
                        this.Invoke(new MethodInvoker(() =>
                        {
                            adsListBox.Items.Insert(0, display);
                        }));
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                // Сокет закрыт намеренно — выходим тихо
                if (!alive) return;
                throw;
            }
        }

        // Выход из группы и закрытие сокета
        private void Disconnect()
        {
            if (!alive) return;

            try
            {
                // Сначала рассылаем уведомление, пока сокет ещё открыт
                SendPacket("SYS|" + userName + " покинул доску объявлений");

                alive = false;
                client.DropMulticastGroup(groupAddress);
                client.Close();
            }
            catch { }

            // Возвращаем форму в исходное состояние
            userNameTextBox.Enabled = true;
            loginButton.Enabled     = true;
            logoutButton.Enabled    = false;
            publishButton.Enabled   = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }
    }
}
