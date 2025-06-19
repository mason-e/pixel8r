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
                Filter = "Supported Image Files|*.jpg;*.png;*.gif;*.bmp",
                Title = "Open image file"
            };
            openFileDialog1.ShowDialog(this);
            if (openFileDialog1.FileName != "")
            {
                GlobalVars.FilePath = openFileDialog1.FileName;
                labelFilePath.Text = $"Current file: {GlobalVars.FilePath}";
                pictureBoxImage.Image = BitmapFunction.generateBitmap();
                labelDimensions.Text = $"{GlobalVars.ImageSizeX}x{GlobalVars.ImageSizeY}";
                if (comboBoxPalette.SelectedIndex != -1 && comboBoxAlgorithm.SelectedIndex != -1)
                {
                    buttonPixelate.Enabled = true;
                }
                if (comboBoxAspectRatio.SelectedIndex != -1)
                {
                    checkBoxCrop.Enabled = true;
                    checkBoxCrop.Checked = false;
                }
            }
            // basically if user hits cancel on the dialog
            else
            {
                return;
            }
        }

        private void toolStripButtonReload_Click(object sender, EventArgs e)
        {
            pictureBoxImage.Image = BitmapFunction.generateBitmap();
            labelDimensions.Text = $"{GlobalVars.ImageSizeX}x{GlobalVars.ImageSizeY}";
        }

        private void buttonPixelate_Click(object sender, EventArgs e)
        {
            pictureBoxImage.Image = BitmapFunction.pixelateDrawing(pictureBoxImage.Image, comboBoxPalette.Text, comboBoxAlgorithm.Text);
        }

        private void comboBoxPalette_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBoxPalette.Image = BitmapFunction.drawPalette(comboBoxPalette.Text);
            if (comboBoxAlgorithm.SelectedIndex != -1 && GlobalVars.FilePath != null)
            {
                buttonPixelate.Enabled = true;
            }
        }

        private void comboBoxAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPalette.SelectedIndex != -1 && GlobalVars.FilePath != null)
            {
                buttonPixelate.Enabled = true;
            }
        }

        private void comboBoxAspectRatio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVars.FilePath != null)
            {
                checkBoxCrop.Enabled = true;
            }
        }

        int cursorX = -1;
        int cursorY = -1;
        int cropWidth = 0;
        int cropHeight = 0;

        private void pictureBoxImage_MouseMove(object sender, MouseEventArgs e)
        {
            cursorX = e.X;
            cursorY = e.Y;
            pictureBoxImage.Invalidate();
        }

        private void pictureBoxImage_Paint(object sender, PaintEventArgs e)
        {
            if (cursorX != -1 && cursorY != -1 && cropWidth != 0 && cropHeight != 0 && checkBoxCrop.Checked)
            {
                Graphics graphics = e.Graphics;
                Rectangle cursor = new Rectangle(cursorX, cursorY, cropWidth, cropHeight);
                graphics.DrawRectangle(new Pen(Color.LightBlue, 2), cursor);
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(127, 255, 255, 255)), cursor);
            }
        }

        private void pictureBoxImage_MouseLeave(object sender, EventArgs e)
        {
            cursorX = -1;
            cursorY = -1;
            pictureBoxImage.Invalidate();
        }

        private void checkBoxCrop_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCrop.Checked)
            {
                string[] aspectVals = comboBoxAspectRatio.Text.Split(":");
                int aspectWidth = Convert.ToInt32(aspectVals[0]);
                int aspectHeight = Convert.ToInt32(aspectVals[1].Split(" ")[0]);
                (cropWidth, cropHeight) = ResizeFunctions.getCropDimensions(aspectWidth, aspectHeight);
            }
        }
    }
}
