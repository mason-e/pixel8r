using System.Windows.Forms;

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
                textBoxFilePath.Text = $"Current file: {GlobalVars.FilePath}";
                pictureBoxImage.Image = BitmapFunction.generateBitmap();
                textBoxDimensions.Text = $"{GlobalVars.ImageSizeX}x{GlobalVars.ImageSizeY}";
                buttonPreviewDownsize.Enabled = true;
                buttonSubmitDownsize.Enabled = true;
                (resizeWidth, resizeHeight) = ResizeFunctions.getResizeDimensions(trackBarResizeDown.Value);
                labelShrinkDimensions.Text = $"{resizeWidth}x{resizeHeight}";
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
            textBoxDimensions.Text = $"{GlobalVars.ImageSizeX}x{GlobalVars.ImageSizeY}";
            checkBoxCrop.Checked = false;
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
            if (checkBoxCrop.Checked)
            {
                string[] aspectVals = comboBoxAspectRatio.Text.Split(":");
                int aspectWidth = Convert.ToInt32(aspectVals[0]);
                int aspectHeight = Convert.ToInt32(aspectVals[1].Split(" ")[0]);
                (resizeWidth, resizeHeight) = ResizeFunctions.getCropDimensions(aspectWidth, aspectHeight);
                textBoxPendingEdit.Text = $"Crop will resize image to {resizeWidth}x{resizeHeight}. Click on image to crop. Click preview button or press ESC to cancel.";
            }
        }

        int cursorX = -1;
        int cursorY = -1;
        int resizeWidth = 0;
        int resizeHeight = 0;

        private void pictureBoxImage_MouseMove(object sender, MouseEventArgs e)
        {
            cursorX = e.X;
            cursorY = e.Y;
            pictureBoxImage.Invalidate();
        }

        private void pictureBoxImage_Paint(object sender, PaintEventArgs e)
        {
            if (cursorX != -1 && cursorY != -1 && resizeWidth != 0 && resizeHeight != 0 && checkBoxCrop.Checked)
            {
                Graphics graphics = e.Graphics;
                Rectangle cursor = new Rectangle(cursorX, cursorY, resizeWidth, resizeHeight);
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
                (resizeWidth, resizeHeight) = ResizeFunctions.getCropDimensions(aspectWidth, aspectHeight);
                textBoxPendingEdit.Text = $"Crop will resize image to {resizeWidth}x{resizeHeight}. Click on image to crop. Click preview button or press ESC to cancel.";
                buttonPreviewDownsize.Enabled = false;
                buttonSubmitDownsize.Enabled = false;
                trackBarResizeDown.Enabled = false;
            }
            else
            {
                (resizeWidth, resizeHeight) = ResizeFunctions.getResizeDimensions(trackBarResizeDown.Value);
                textBoxPendingEdit.Text = "";
                buttonPreviewDownsize.Enabled = true;
                buttonSubmitDownsize.Enabled = true;
                trackBarResizeDown.Enabled = true;
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                checkBoxCrop.Checked = false;
                pictureBoxImage.Invalidate();
            }
        }

        private void trackBarResizeDown_Scroll(object sender, EventArgs e)
        {
            labelResizeDown.Text = $"Resize down to {trackBarResizeDown.Value}%";
            if (GlobalVars.FilePath != null)
            {
                (resizeWidth, resizeHeight) = ResizeFunctions.getResizeDimensions(trackBarResizeDown.Value);
                labelShrinkDimensions.Text = $"{resizeWidth}x{resizeHeight}";
            }
        }

        private void buttonPreviewDownsize_Click(object sender, EventArgs e)
        {
            // flash rectangle over image for about a second to show new size
            Graphics graphics = pictureBoxImage.CreateGraphics();
            for (int i = 0; i < 5; i++)
            {
                ResizeFunctions.drawCenterPointRectangle(graphics, resizeWidth, resizeHeight);
                Thread.Sleep(100);
                pictureBoxImage.Invalidate();
                pictureBoxImage.Update();
                Thread.Sleep(50);
            }
        }

        private void buttonSubmitDownsize_Click(object sender, EventArgs e)
        {
            pictureBoxImage.Image = BitmapFunction.resize((Bitmap)pictureBoxImage.Image, 100 / (float)trackBarResizeDown.Value);
        }
    }
}
