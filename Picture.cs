using System;
using System.Collections.Generic;
using System.Text;

namespace lab3_4
{
    internal class Picture
    {
        // Координати верхнього лівого кута
        private double X;
        private double Y;

        private double Width = 0;
        private double Height = 0;

        private List<Figure> Figures = new List<Figure>();

        public void AddFigure(Figure figure)
        {
            Figures.Add(figure);

            // TODO: Оновлюємо координати картини, враховуючи позицію та розмір доданої фігури

            // Оновлюємо розміри зображення, враховуючи позицію та розмір фігури
            double figureRight = figure.X + figure.Radius; // Припускаємо, що фігура - це коло з радіусом
            double figureBottom = figure.Y + figure.Radius;
            
            if (figureRight > Width)
            {
                Width = figureRight; // Оновлюємо ширину картини
            }
            if (figureBottom > Height)
            {
                Height = figureBottom; // Оновлюємо висоту картини
            }
        }
        public void Draw(Graphics g)
        {
            foreach (var figure in Figures)
            {
                figure.Draw(g);
            }
        }
    }
}
