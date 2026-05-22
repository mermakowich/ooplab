using System;
using System.Drawing;

namespace Lab5
{
    // Класс "Круг" - наследник GeometricFigure
    public class MyCircle : GeometricFigure
    {
        public int Radius;

        public MyCircle(int x, int y, Color color, int thickness, int radius)
            : base(x, y, color, thickness)
        {
            Radius = radius;
        }

        // Отрисовка круга
        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(FigureColor, LineThickness);
            g.DrawEllipse(pen, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            pen.Dispose();
        }

        // Площадь круга
        public override double GetArea()
        {
            return Math.PI * Radius * Radius;
        }

        // Принадлежность точки кругу
        public override bool ContainsPoint(int px, int py)
        {
            int dx = px - X;
            int dy = py - Y;
            return dx * dx + dy * dy <= Radius * Radius;
        }

        public override string Name { get { return "Круг"; } }
    }
}
