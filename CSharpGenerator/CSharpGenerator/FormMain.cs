namespace CSharpGenerator
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog()
            {
                FileName = "Select an image file",
                Filter = "Supported Image Files|*.jpg;*.png;*.gif;*.bmp",
                Title = "Open image file"
            };
            openFileDialog1.ShowDialog(this);
            GlobalVars.FilePath1 = openFileDialog1.FileName;
            pictureBox1.Image = BitmapFunction.generateBitmap();
        }

        private void toolStripButtonReload_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = BitmapFunction.generateBitmap();
        }

        private void buttonPixelate_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = BitmapFunction.pixelateDrawing(pictureBox1.Image, comboBoxPalette.Text, comboBoxAlgorithm.Text);
        }
    }
}
