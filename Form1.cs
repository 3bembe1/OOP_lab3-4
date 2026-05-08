namespace lab3_4
{
    public partial class Form1 : Form
    {
        private Picture picture = new Picture();

        public Form1()
        {
            InitializeComponent();

            picture.AddFigure(new Circle(50, 100, 100));
            picture.AddFigure(new Circle(30, 200, 150));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            picture.Draw(e.Graphics);
        }
    }
}
