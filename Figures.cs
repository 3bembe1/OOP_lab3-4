using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace lab3_4
{
    // 1. Базовий клас
    internal abstract class Figure
    {
        protected string Name;

        // Додаємо координати на площині
        public double X { get; private set; }
        public double Y { get; private set; }

        // Усі фігури мають радіус
        public double Radius { get; protected set; }
        // Оновлюємо конструктор, щоб задавати початкові координати (за замовчуванням 0, 0)
        public Figure(string name, double radius, double x = 0, double y = 0)
        {
            Name = name;
            Radius = radius;
            X = x;
            Y = y;
        }

        // Метод 1: Переміщення в потрібну позицію (абсолютні координати)
        public void MoveTo(double newX, double newY)
        {
            X = newX;
            Y = newY;
        }

        // Метод 2: Переміщення на задану відстань (відносний зсув)
        public void MoveBy(double dx, double dy)
        {
            X += dx; // Додаємо зсув по X до поточної позиції
            Y += dy; // Додаємо зсув по Y до поточної позиції
        }

        // Додаємо абстрактний метод масштабування
        // factor > 1 (збільшення), factor < 1 (зменшення)
        public abstract void Scale(double factor);

        public abstract double GetArea();

        // Абстрактний метод малювання
        public abstract void Draw(Graphics g);

        public abstract string PrintInfo();
    }

    // 2. Проміжні класи для 2D та 3D
    internal abstract class Figure2D : Figure
    {
        public Figure2D(string name, double radius, double x = 0, double y = 0) : base(name, radius, x, y)
        {
        }

        // Реалізуємо масштабування для всіх 2D фігур
        public override void Scale(double factor)
        {
            if (factor <= 0)
            {
                throw new ArgumentException("Коефіцієнт масштабування має бути більшим за 0.");
            }

            Radius *= factor; // Множимо поточний радіус на коефіцієнт
        }

        // Периметр мають ТІЛЬКИ плоскі фігури
        public abstract double GetPerimeter();
    }

    // 3. Плоскі фігури (Спадкують Figure2D)
    internal class Circle : Figure2D
    {
        public Circle(double radius, double x = 0, double y = 0) : base("Коло", radius, x, y)
        {
            if (radius > x || radius > y)
            {
                throw new ArgumentException("Радіус не може перевищувати координати фігури.");
            }
        }

        public override void Draw(Graphics g)   
        {
            // Створюємо чорний пензлик товщиною 2 пікселі
            using (Pen pen = new Pen(Color.Black, 2))
            {
                // Малюємо еліпс, вписаний у квадрат (це і є коло)
                g.DrawEllipse(pen, (float)(X - Radius), (float)(Y - Radius), (float)Radius * 2, (float)Radius * 2);
            }
        }

        public override double GetArea()
        {
            return Math.PI * Math.Pow(Radius, 2);
        }

        public override double GetPerimeter()
        {
            return 2 * Math.PI * Radius;
        }

        public override string PrintInfo()
        {
            return $"Клас: {this.GetType().Name} | Назва: {Name} | Позиція: ({X}, {Y}) | Радіус: {Radius} | Периметр: {GetPerimeter():F2} | Площа: {GetArea():F2}";
        }
    }
}
