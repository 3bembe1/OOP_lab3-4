using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace lab3_4
{
    // 1. Базовий клас
    internal abstract class Figure
    {

        // Додаємо координати на площині
        public double X { get; private set; }
        public double Y { get; private set; }

        // Усі фігури мають радіус
        public double Radius { get; protected set; }
        // Оновлюємо конструктор, щоб задавати початкові координати (за замовчуванням 0, 0)
        public Figure(double radius, double x = 0, double y = 0)
        {           
            Radius = radius;
            X = x;
            Y = y;

            RectangleF rect = GetBoundingBox();
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

        public abstract string PrintInfo(int index);

        public abstract RectangleF GetBoundingBox();
    }

    // 2. Проміжні класи для 2D та 3D
    internal abstract class Figure2D : Figure
    {
        public Figure2D(double radius, double x = 0, double y = 0) : base(radius, x, y) { }
        
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

    internal abstract class Figure3D : Figure
    {
        public Figure3D(double x = 0, double y = 0) : base(x, y) { }

        public abstract double GetVolume();
    }

    // 3. Плоскі фігури (Спадкують Figure2D)
    internal class Circle : Figure2D
    {
        public Circle(double radius, double x = 0, double y = 0) : base(radius, x, y) { }

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

        public override string PrintInfo(int index)
        {
            return $"Клас: {this.GetType().Name} | Індекс: {index} | Позиція: ({X}, {Y}) | Радіус: {Radius} | Периметр: {GetPerimeter():F2} | Площа: {GetArea():F2}";
        }

        public override RectangleF GetBoundingBox()
        {
            // Для кола, обмежувальний прямокутник - це квадрат, що вписує коло
            return new RectangleF((float)(X - Radius), (float)(Y - Radius), (float)(Radius * 2), (float)(Radius * 2));
        }
    }

    internal class Arc : Figure2D
    {
        public double Angle { get; private set; }

        public Arc(double radius, double angle, double x = 0, double y = 0) : base(radius, x, y)
        {
            Angle = angle;
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color.Blue, 2))
            {
                float r = (float)Radius;
                // 0 - початковий кут, Angle - кут розгортки
                g.DrawArc(pen, (float)X - r, (float)Y - r, r * 2, r * 2, 0, (float)Angle);
            }
        }

        public override double GetArea()
        {
            return 0;
        }

        public override double GetPerimeter()
        {
            return (Math.PI * Radius * Angle) / 180;
        }

        public override string PrintInfo(int index)
        {
            return $"Клас: {this.GetType().Name} | Індекс: {index} | Позиція: ({X}, {Y}) | Радіус: {Radius} | Кут: {Angle}° | Довжина дуги: {GetPerimeter():F2} | Площа: {GetArea():F2}";
        }

        public override RectangleF GetBoundingBox()
        {
            return new RectangleF((float)(X - Radius), (float)(Y - Radius), (float)(Radius * 2), (float)(Radius * 2));
        }
    }

    internal class Sector : Figure2D
    {
        public double Angle { get; private set; }

        public Sector(double radius, double angle, double x = 0, double y = 0) : base(radius, x, y)
        {
            Angle = angle;
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color.Green, 2))
            {
                float r = (float)Radius;
                g.DrawPie(pen, (float)X - r, (float)Y - r, r * 2, r * 2, 0, (float)Angle);
            }
        }

        public override double GetArea()
        {
            return (Math.PI * Math.Pow(Radius, 2) * Angle) / 360;
        }

        public override double GetPerimeter()
        {
            double arcLength = (Math.PI * Radius * Angle) / 180;
            return arcLength + (2 * Radius);
        }

        public override string PrintInfo(int index)
        {
            return $"Клас: {this.GetType().Name} | Індекс: {index} | Позиція: ({X}, {Y}) | Радіус: {Radius} | Кут: {Angle}° | Периметр: {GetPerimeter():F2} | Площа: {GetArea():F2}";
        }

        public override RectangleF GetBoundingBox()
        {
            return new RectangleF((float)(X - Radius), (float)(Y - Radius), (float)(Radius * 2), (float)(Radius * 2));
        }
    }

    internal class Segment : Figure2D
    {
        public double Angle { get; private set; }

        public Segment(double radius, double angle, double x = 0, double y = 0) : base(radius, x, y)
        {
            Angle = angle;
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color.Purple, 2))
            {
                float r = (float)Radius;
                // Малюємо дугу
                g.DrawArc(pen, (float)X - r, (float)Y - r, r * 2, r * 2, 0, (float)Angle);

                // Малюємо хорду (лінію, що з'єднує початок і кінець дуги)
                // Початок дуги при 0 градусах: (X + R, Y)
                float startX = (float)(X + Radius);
                float startY = (float)Y;

                // Кінець дуги при заданому куті (використовуємо синус і косинус)
                double radAngle = Angle * Math.PI / 180;
                float endX = (float)(X + Radius * Math.Cos(radAngle));
                float endY = (float)(Y + Radius * Math.Sin(radAngle));

                g.DrawLine(pen, startX, startY, endX, endY);
            }
        }

        public override double GetArea()
        {
            double radAngle = Angle * Math.PI / 180;
            return 0.5 * Math.Pow(Radius, 2) * (radAngle - Math.Sin(radAngle));
        }

        public override double GetPerimeter()
        {
            double arcLength = (Math.PI * Radius * Angle) / 180;
            double radAngle = Angle * Math.PI / 180;
            double chordLength = 2 * Radius * Math.Sin(radAngle / 2);
            return arcLength + chordLength;
        }

        public override string PrintInfo(int index)
        {
            return $"Клас: {this.GetType().Name} | Індекс: {index} | Позиція: ({X}, {Y}) | Радіус: {Radius} | Кут: {Angle}° | Периметр: {GetPerimeter():F2} | Площа: {GetArea():F2}";
        }

        public override RectangleF GetBoundingBox()
        {
            return new RectangleF((float)(X - Radius), (float)(Y - Radius), (float)(Radius * 2), (float)(Radius * 2));
        }
    }

    internal class Sphere : Figure3D
    {
        public Sphere(double radius, double x = 0, double y = 0) : base(x, y)
        {
            // Устанавливаем унаследованное свойство Radius (set — protected в Figure)
            Radius = radius;
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color.Red, 2))
            {
                float r = (float)Radius;
                // Малюємо зовнішній контур кулі (коло)
                g.DrawEllipse(pen, (float)X - r, (float)Y - r, r * 2, r * 2);

                // Малюємо "екватор" для ілюзії об'єму (сплюснутий по висоті еліпс)
                g.DrawEllipse(pen, (float)X - r, (float)Y - (r / 3), r * 2, (r * 2) / 3);
            }
        }

        public override void Scale(double factor)
        {
            if (factor <= 0) 
                throw new ArgumentException("Коефіцієнт масштабування має бути більшим за 0.");

            Radius *= factor;
        }

        public override double GetArea()
        {
            return 4 * Math.PI * Math.Pow(Radius, 2);
        }

        public override double GetVolume()
        {
            return (4.0 / 3.0) * Math.PI * Math.Pow(Radius, 3);
        }

        public override string PrintInfo(int index)
        {
            return $"Клас: {this.GetType().Name} | Індекс: {index} | Позиція центру: ({X}, {Y}) | Радіус: {Radius} | Площа поверхні: {GetArea():F2} | Об'єм: {GetVolume():F2}";
        }

        public override RectangleF GetBoundingBox()
        {
            return new RectangleF((float)(X - Radius), (float)(Y - Radius), (float)(Radius * 2), (float)(Radius * 2));
        }
    }

    internal class SphericalSegment : Figure3D
    {
        public double Height { get; private set; } = 0;

        public SphericalSegment(double Radius, double Height, double x = 0, double y = 0) : base(x, y) 
        {
            this.Height = Height;
        }

        public override void Draw(Graphics g)
        {
            float r = (float)Radius;
            float h = (float)Height;
            float cx = (float)X;
            float cy = (float)Y;

            float baseRadius = (float)Math.Sqrt(r * r - Math.Pow(r - h, 2));

            // Розрахунок кутів
            float cosAlpha = (r - h) / r;
            float alpha = (float)(Math.Acos(Math.Max(-1, Math.Min(1, cosAlpha))) * (180 / Math.PI));
            float startAngle = 270 - alpha;
            float sweepAngle = alpha * 2;

            // ВАЖЛИВО: Якщо ти хочеш, щоб (X, Y) був центром основи сегмента,
            // а не центром уявної сфери, треба додати зміщення (r - h) до всіх Y.
            float offsetV = (r - h);

            using (Pen penBlack = new Pen(Color.Black, 2))
            using (Pen penBlue = new Pen(Color.Blue, 2))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Малюємо дугу зі зміщенням вниз, щоб компенсувати "зліт"
                // Додаємо offsetV до координати Y
                g.DrawArc(penBlack, cx - r, cy - r + offsetV, r * 2, r * 2, startAngle, sweepAngle);

                // Основа тепер буде рівно в точці cy
                float ellipseHeight = baseRadius / 3;
                g.DrawEllipse(penBlue, cx - baseRadius, cy - ellipseHeight / 2, baseRadius * 2, ellipseHeight);
            }
        }

        public override void Scale(double factor)
        {
            Radius *= factor;
            Height *= factor;
        }

        public override double GetArea()
        {
            double rBaseSq = Radius * Radius - Math.Pow(Radius - Height, 2);
            return 2 * Math.PI * Radius * Height + Math.PI * rBaseSq;
        }

        public override double GetVolume()
        {
            return (Math.PI * Math.Pow(Height, 2) * (3 * Radius - Height)) / 3;
        }

        public override string PrintInfo(int index)
        {
            return $"Клас: {this.GetType().Name} | Індекс: {index} | Позиція центру: ({X}, {Y}) | Радіус: {Radius} | Висота: {Height} | Загальна площа поверхонь: {GetArea():F2} | Об'єм: {GetVolume():F2}";
        }

        public override RectangleF GetBoundingBox()
        {
            // Для спрощення габарити сегмента вписуємо в квадрат всієї кулі
            float d = (float)Radius * 2;
            return new RectangleF((float)X - (float)Radius, (float)Y - (float)Radius, d, d);
        }
    }
}
