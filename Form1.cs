using System;
using System.Text;
namespace lab3_4
{
    public partial class Form1 : Form
    {
        private Picture picture = new Picture();

        public Form1()
        {
            InitializeComponent();

            picture.AddFigure(new Circle(50, 100, 100));
            picture.AddFigure(new Arc(50, 90, 300, 200));
            picture.AddFigure(new Sector(75, 120, 150, 250));
            picture.AddFigure(new Segment(100, 90, 250, 250));
            picture.AddFigure(new Sphere(75, 100, 100));
            picture.AddFigure(new SphericalSegment(100, 50, 200, 200));

            //picture.MoveFigureBy(1, 100, 100);

            Invalidate(); // Викликаємо перерисовку форми після зміни позиції фігури
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            picture.Draw(e.Graphics);

            // Собираем информацию о всех фигурах
            var info = new StringBuilder();
            for (int i = 0; i < picture.FiguresCount; i++)
            {
                info.AppendLine(picture.GetFigureInfo(i));
            }
            label1.Text = info.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            picture.MoveFigureBy(int.Parse(textBox1.Text), -50, 0);
            panel1.Invalidate(); // Оновлюємо саме ту панель, де малюємо
        }

        private void button2_Click(object sender, EventArgs e)
        {
            picture.MoveFigureBy(int.Parse(textBox1.Text), 50, 0);
            panel1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            picture.MoveFigureBy(int.Parse(textBox1.Text), 0, -50);
            panel1.Invalidate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            picture.MoveFigureBy(int.Parse(textBox1.Text), 0, 50);
            panel1.Invalidate();
        }

    }
}
