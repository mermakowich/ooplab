using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab7
{
    public partial class Form1 : Form
    {
        // Размер поля: 3x3, каждая клетка 150x150 пикселей
        const int N = 3;
        const int CELL = 150;

        int x0 = 50, y0 = 90;          // координаты левого верхнего угла поля
        int[,] table = new int[N, N];   // состояние поля: 0=пусто, 1=X, 2=O
        int hod = 0;                    // счётчик ходов (0..8)
        int win = 0;                    // 0=игра идёт, 1=победил X, 2=победил O
        int score1 = 0, score2 = 0;    // счёт по раундам

        // координаты выигрышной линии (в системе координат поля, после TranslateTransform)
        int winx0, winy0, winx1, winy1;

        public Form1()
        {
            InitializeComponent();
        }

        // ===== Обработчик клика мышью =====
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            // клик должен быть внутри поля
            if (e.X < x0 || e.X >= x0 + N * CELL) return;
            if (e.Y < y0 || e.Y >= y0 + N * CELL) return;

            // если победитель уже определён — ходить нельзя
            if (win != 0) return;

            int col = (e.X - x0) / CELL;   // столбец (0..2)
            int row = (e.Y - y0) / CELL;   // строка (0..2)

            if (table[col, row] != 0) return;   // клетка занята

            // чётный ход — крестик (1), нечётный — нолик (2)
            table[col, row] = (hod % 2 == 0) ? 1 : 2;
            hod++;

            CheckWin();     // проверяем 8 комбинаций

            Invalidate();   // перерисовываем форму (вызовет Paint)

            // показываем результат раунда
            if (win != 0)
            {
                string name = (win == 1) ? "Игрок 1" : "Игрок 2";
                MessageBox.Show(name + " победил!", "Конец раунда",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (hod == N * N)
            {
                MessageBox.Show("Ничья!", "Конец раунда",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ===== Проверка всех 8 выигрышных комбинаций =====
        void CheckWin()
        {
            // 3 строки
            for (int j = 0; j < N; j++)
            {
                if (table[0, j] != 0 && table[0, j] == table[1, j] && table[1, j] == table[2, j])
                {
                    SetWin(table[0, j], 10, j * CELL + 75, 440, j * CELL + 75);
                    return;
                }
            }

            // 3 столбца
            for (int i = 0; i < N; i++)
            {
                if (table[i, 0] != 0 && table[i, 0] == table[i, 1] && table[i, 1] == table[i, 2])
                {
                    SetWin(table[i, 0], i * CELL + 75, 10, i * CELL + 75, 440);
                    return;
                }
            }

            // главная диагональ (↘)
            if (table[0, 0] != 0 && table[0, 0] == table[1, 1] && table[1, 1] == table[2, 2])
            {
                SetWin(table[0, 0], 10, 10, 440, 440);
                return;
            }

            // побочная диагональ (↙)
            if (table[2, 0] != 0 && table[2, 0] == table[1, 1] && table[1, 1] == table[0, 2])
            {
                SetWin(table[2, 0], 440, 10, 10, 440);
            }
        }

        // Запоминаем победителя, его линию и обновляем счёт
        void SetWin(int player, int lx0, int ly0, int lx1, int ly1)
        {
            win = player;
            winx0 = lx0; winy0 = ly0; winx1 = lx1; winy1 = ly1;
            if (win == 1) score1++;
            else score2++;
        }

        // ===== Вся отрисовка — в обработчике Paint =====
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // сдвигаем начало координат в левый верхний угол поля
            g.TranslateTransform(x0, y0);

            // рисуем рамку поля
            g.DrawRectangle(new Pen(Color.Black, 3f), 0, 0, N * CELL, N * CELL);

            // внутренние линии сетки
            for (int i = 1; i < N; i++)
            {
                g.DrawLine(new Pen(Color.Gray, 1f), i * CELL, 0, i * CELL, N * CELL);
                g.DrawLine(new Pen(Color.Gray, 1f), 0, i * CELL, N * CELL, i * CELL);
            }

            // крестики и нолики
            int pad = 15;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    int left = i * CELL + pad;
                    int top  = j * CELL + pad;
                    int sz   = CELL - 2 * pad;

                    if (table[i, j] == 1)
                    {
                        // крестик — две линии
                        g.DrawLine(new Pen(Color.Red, 5f), left, top, left + sz, top + sz);
                        g.DrawLine(new Pen(Color.Red, 5f), left + sz, top, left, top + sz);
                    }
                    else if (table[i, j] == 2)
                    {
                        // нолик — эллипс
                        g.DrawEllipse(new Pen(Color.Blue, 5f), left, top, sz, sz);
                    }
                }
            }

            // выигрышная линия поверх фигур
            if (win != 0)
                g.DrawLine(new Pen(Color.Black, 6f), winx0, winy0, winx1, winy1);

            // обновляем цифры счёта
            labelScore1.Text = score1.ToString();
            labelScore2.Text = score2.ToString();

            // подсвечиваем, чья очередь ходить
            if (win == 0 && hod < N * N)
            {
                labelGamer1.BackColor = (hod % 2 == 0) ? Color.LightGreen : Color.Transparent;
                labelGamer2.BackColor = (hod % 2 == 1) ? Color.LightGreen : Color.Transparent;
            }
            else
            {
                labelGamer1.BackColor = Color.Transparent;
                labelGamer2.BackColor = Color.Transparent;
            }
        }

        // ===== Новая игра (очищаем поле, счёт не трогаем) =====
        void StartNew()
        {
            win = 0;
            hod = 0;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    table[i, j] = 0;
            Invalidate();
        }

        // ===== Сброс счёта + новая игра =====
        void ResetAll()
        {
            score1 = 0;
            score2 = 0;
            StartNew();
        }

        private void btnNew_Click(object sender, EventArgs e)   => StartNew();
        private void btnReset_Click(object sender, EventArgs e)  => ResetAll();
        private void menuNew_Click(object sender, EventArgs e)   => StartNew();
        private void menuReset_Click(object sender, EventArgs e) => ResetAll();
    }
}
