using System.Drawing;

namespace Lab5
{
    // Базовый класс "Геометрическая фигура"
    public class GeometricFigure
    {
        public int X;              // координата X центра
        public int Y;              // координата Y центра
        public Color FigureColor;  // цвет фигуры
        public int LineThickness;  // толщина линии контура

        public GeometricFigure(int x, int y, Color color, int thickness)
        {
            X = x;
            Y = y;
            FigureColor = color;
            LineThickness = thickness;
        }

        // Метод замены цвета
        public void ChangeColor(Color newColor)
        {
            FigureColor = newColor;
        }

        // Виртуальные методы (переопределяются в наследниках)
        public virtual void Draw(Graphics g) { }
        public virtual double GetArea() { return 0; }
        public virtual bool ContainsPoint(int px, int py) { return false; }
        public virtual string Name { get { return "Фигура"; } }
    }
}
