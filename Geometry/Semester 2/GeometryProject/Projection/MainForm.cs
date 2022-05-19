namespace Projection
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();

            

        }

        private void DrawAxes()
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics graphic = CreateGraphics();
            var pen = new Pen(Color.Black, 4f);

            graphic.DrawEllipse(pen, 100, 100, 100, 100);


            var points = new Point[]
            {
                new Point(100, 100),
                new Point(200, 100),
                new Point(300, 100),
                new Point(400, 100),
                new Point(500, 100),
            };

            graphic.DrawLines(pen, points);
        }
    }
}