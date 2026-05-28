using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab10
{
    // Вариант 8: найти количество натуральных чисел до 10 000 000,
    // которые сами являются палиндромами и чей квадрат — тоже палиндром.
    public partial class Form1 : Form
    {
        const int LIMIT = 10_000_000;

        // Список значений лимита потоков для замеров
        readonly int[] threadLimits = { 1, 2, 4, 8, 12, 16 };

        public Form1()
        {
            InitializeComponent();
        }

        // Проверка: является ли число палиндромом
        static bool IsPalindrome(long n)
        {
            string s = n.ToString();
            int i = 0;
            int j = s.Length - 1;
            while (i < j)
            {
                if (s[i] != s[j]) return false;
                i++;
                j--;
            }
            return true;
        }

        // Условие задачи: само число — палиндром И его квадрат — палиндром
        static bool TestNumber(int n)
        {
            if (!IsPalindrome(n)) return false;
            long sqr = (long)n * n;
            return IsPalindrome(sqr);
        }

        // Последовательный поиск (один поток)
        long SerialCount()
        {
            long count = 0;
            for (int i = 1; i <= LIMIT; i++)
            {
                if (TestNumber(i)) count++;
            }
            return count;
        }

        // Параллельный поиск с ограничением числа потоков
        long ParallelCount(int maxThreads)
        {
            long count = 0;
            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = maxThreads
            };

            // Используем шаблон TLS — у каждого потока свой локальный счётчик,
            // в финализаторе складываем их через Interlocked.
            Parallel.For(1, LIMIT + 1, options,
                () => 0L,
                (i, state, localSum) =>
                {
                    if (TestNumber(i)) localSum++;
                    return localSum;
                },
                localSum => System.Threading.Interlocked.Add(ref count, localSum)
            );

            return count;
        }

        // Нажатие кнопки "Запустить" — запускает все замеры в фоновом потоке
        private async void btnRun_Click(object sender, EventArgs e)
        {
            btnRun.Enabled = false;
            txtLog.Clear();
            chart.Series["Время"].Points.Clear();
            chart.Series["Идеал"].Points.Clear();

            Log("Лимит поиска: " + LIMIT.ToString("N0"));
            Log("Запуск замеров... подождите.");
            Log("");

            // Все тяжёлые вычисления — в фоне, чтобы форма не зависала
            await Task.Run(() =>
            {
                // Последовательный замер
                Stopwatch sw = Stopwatch.StartNew();
                long serialResult = SerialCount();
                sw.Stop();
                long serialTime = sw.ElapsedMilliseconds;

                Log("Последовательно:");
                Log("  Найдено чисел: " + serialResult);
                Log("  Время: " + serialTime + " мс");
                Log("");
                Log("Параллельно (Parallel.For):");
                Log(string.Format("  {0,-8} {1,-12} {2,-12} {3,-10}",
                    "Потоки", "Время, мс", "Идеал, мс", "Найдено"));

                long baseTime = 0; // время при 1 потоке — основа для идеального ускорения

                for (int k = 0; k < threadLimits.Length; k++)
                {
                    int threads = threadLimits[k];

                    sw.Restart();
                    long result = ParallelCount(threads);
                    sw.Stop();
                    long time = sw.ElapsedMilliseconds;

                    if (k == 0) baseTime = time;
                    long ideal = baseTime / threads;

                    Log(string.Format("  {0,-8} {1,-12} {2,-12} {3,-10}",
                        threads, time, ideal, result));

                    // Обновляем график из главного потока через Invoke
                    int t = threads;
                    long tm = time;
                    long id = ideal;
                    this.Invoke(new MethodInvoker(() =>
                    {
                        chart.Series["Время"].Points.AddXY(t, tm);
                        chart.Series["Идеал"].Points.AddXY(t, id);
                    }));
                }

                Log("");
                Log("Готово.");
            });

            btnRun.Enabled = true;
        }

        // Безопасный вывод текста в TextBox (можно вызывать из любого потока)
        void Log(string text)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new MethodInvoker(() => txtLog.AppendText(text + "\r\n")));
            }
            else
            {
                txtLog.AppendText(text + "\r\n");
            }
        }
    }
}
