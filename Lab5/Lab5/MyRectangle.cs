using System.Drawing;

namespace Lab5
{
    // Класс "Прямоугольник" - наследник GeometricFigure
    public class MyRectangle : GeometricFigure
    {
        public int Width;
        public int Height;

        public MyRectangle(int x, int y, Color color, int thickness, int width, int height)
            : base(x, y, color, thickness)
        {
            Width = width;
            Height = height;
        }

        // Отрисовка прямоугольника
        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(FigureColor, LineThickness);
            int left = X - Width / 2;
            int top = Y - Height / 2;
            g.DrawRectangle(pen, left, top, Width, Height);
            pen.Dispose();
        }

        // Площадь прямоугольника
        public override double GetArea()
        {
            return Width * Height;
        }

        // Принадлежность точки прямоугольнику
        public override bool ContainsPoint(int px, int py)
        {
            int left = X - Width / 2;
            int right = X + Width / 2;
            int top = Y - Height / 2;
            int bottom = Y + Height / 2;
            return px >= left && px <= right && py >= top && py <= bottom;
        }

        public override string Name { get { return "Прямоугольник"; } }
    }
}
