using System;
using System.Drawing;

namespace Lab5
{
    // Класс "Круг" - наследник GeometricFigure
    public class MyCircle : GeometricFigure
    {
        public int Radius;

        public MyCircle(int x, int y, Color color, Color outlineColor, int thickness, int radius)
            : base(x, y, color, outlineColor, thickness)
        {
            Radius = radius;
        }

        // Отрисовка круга (заливка + контур)
        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(OutlineColor, LineThickness);
            SolidBrush brush = new SolidBrush(FigureColor);
            g.FillEllipse(brush, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            g.DrawEllipse(pen, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            pen.Dispose();
            brush.Dispose();
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
