namespace lab3_4
{
    public partial class Form1 : Form
    {
        private Picture picture = new Picture();

        public Form1()
        {
            InitializeComponent();

            picture.AddFigure(new Circle(150, 100, 100));
            picture.AddFigure(new Circle(30, 200, 150));

            //picture.MoveFigureBy(1, 100, 100);

            Invalidate(); // Викликаємо перерисовку форми після зміни позиції фігури
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            picture.Draw(e.Graphics);
        }
    }
}
