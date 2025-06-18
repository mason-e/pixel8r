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
            GlobalVars.FilePath = openFileDialog1.FileName;
            labelFilePath.Text = $"Current file: {GlobalVars.FilePath}";
            pictureBoxImage.Image = BitmapFunction.generateBitmap();
        }

        private void toolStripButtonReload_Click(object sender, EventArgs e)
        {
            pictureBoxImage.Image = BitmapFunction.generateBitmap();
        }

        private void buttonPixelate_Click(object sender, EventArgs e)
        {
            pictureBoxImage.Image = BitmapFunction.pixelateDrawing(pictureBoxImage.Image, comboBoxPalette.Text, comboBoxAlgorithm.Text);
        }

        private void comboBoxPalette_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBoxPalette.Image = BitmapFunction.drawPalette(comboBoxPalette.Text);
        }
    }
}
