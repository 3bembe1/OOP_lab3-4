using System;
using System.Collections.Generic;
using System.Text;

namespace lab3_4
{
    internal class Picture
    {
        // Координати верхнього лівого кута
        private double X = double.MaxValue;
        private double Y = double.MaxValue;

        private double Width = 0;
        private double Height = 0;

        private List<Figure> Figures = new List<Figure>();

        public int FiguresCount => Figures.Count;

        public void MoveFigureTo(int index, double dx, double dy)
        {
            if (index >= 0 && index < Figures.Count)
            {
                Figures[index].MoveTo(dx, dy);
            }
        }

        public void MoveFigureBy(int index, double dx, double dy)
        {
            if (index >= 0 && index < Figures.Count)
            {
                Figures[index].MoveBy(dx, dy);
            }
        }

        public void AddFigure(Figure figure)
        {
            Figures.Add(figure);

            RectangleF rect = figure.GetBoundingBox();

            if (rect.Left < X)
                X = rect.Left;

            if (rect.Top < Y)
                Y = rect.Top;

            if (rect.Right -X > Width)
                Width = rect.Right - X;

            if (rect.Bottom - Y > Height)
                Height = rect.Bottom - Y;
        }
        public void Draw(Graphics g)
        {
            foreach (var figure in Figures)
            {
                figure.Draw(g);
            }
        }
        public string GetFigureInfo(int index)
        {
            if (index >= 0 && index < Figures.Count)
                return Figures[index].PrintInfo(index);
            return "Нет такой фигуры";
        }
    }
}
