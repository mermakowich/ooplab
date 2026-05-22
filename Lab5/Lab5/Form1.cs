using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        private List<GeometricFigure> figures = new List<GeometricFigure>();
        private Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            CreateFigures();
        }

        // Создание случайных фигур
        private void CreateFigures()
        {
            int rectCount = 5;
            int circleCount = 5;

            for (int i = 0; i < rectCount; i++)
            {
                int w = rnd.Next(40, 120);
                int h = rnd.Next(40, 120);
                int x = rnd.Next(w, panelDraw.Width - w);
                int y = rnd.Next(h, panelDraw.Height - h);
                Color c = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                figures.Add(new MyRectangle(x, y, c, Color.Black, 2, w, h));
            }

            for (int i = 0; i < circleCount; i++)
            {
                int r = rnd.Next(20, 60);
                int x = rnd.Next(r, panelDraw.Width - r);
                int y = rnd.Next(r, panelDraw.Height - r);
                Color c = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                figures.Add(new MyCircle(x, y, c, Color.Black, 2, r));
            }
        }

        private void panelDraw_Paint(object sender, PaintEventArgs e)
        {
            // Полиморфизм: метод Draw вызывается по типу объекта во время выполнения
            foreach (GeometricFigure f in figures)
            {
                f.Draw(e.Graphics);
            }
        }

        private void panelDraw_MouseClick(object sender, MouseEventArgs e)
        {
            // Идём с конца, чтобы выбрать "верхнюю" фигуру
            for (int i = figures.Count - 1; i >= 0; i--)
            {
                if (figures[i].ContainsPoint(e.X, e.Y))
                {
                    GeometricFigure f = figures[i];
                    f.LineThickness += 2;
                    Color newColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    f.ChangeColor(newColor);
                    textBoxInfo.Text = f.Name + ", площадь = " + f.GetArea().ToString("F2");
                    panelDraw.Invalidate();
                    return;
                }
            }
        }
    }
}
