namespace CSharpGenerator
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonFile1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(this);
            GlobalVars.FilePath1 = openFileDialog1.FileName;
            GlobalVars.Size = (int)new FileInfo(GlobalVars.FilePath1).Length;
            GlobalVars.SideLength = (int)Math.Sqrt(GlobalVars.Size);
        }


        private void buttonBitmap_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = BitmapFunction.generateBitmap();
        }

        private void buttonPixelate_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = BitmapFunction.pixelateDrawing(pictureBox1.Image);
        }
    }
}
