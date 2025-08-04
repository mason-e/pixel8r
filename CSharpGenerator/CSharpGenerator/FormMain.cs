using System.Drawing;
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
                setParamsAfterImageLoad();
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
            setParamsAfterImageLoad();
        }

        private void buttonPixelate_Click(object sender, EventArgs e)
        {
            pictureBoxImage.Image = BitmapFunction.pixelateDrawing(pictureBoxImage.Image, comboBoxPalette.Text, comboBoxAlgorithm.Text);
            setParamsAfterImageLoad();
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
        int resizeLowerLimit = 0;
        int resizeUpperLimit = 0;

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
                // only need to change the width or height - lock the dimension that doesn't need to change
                if (resizeWidth == GlobalVars.ImageSizeX)
                {
                    cursorX = 0;

                }
                if (resizeHeight == GlobalVars.ImageSizeY)
                {
                    cursorY = 0;
                }
                Rectangle cursor = new Rectangle(cursorX, cursorY, resizeWidth, resizeHeight);
                graphics.DrawRectangle(new Pen(Color.LightBlue, 2), cursor);
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(127, 255, 255, 255)), cursor);
            }
        }

        private void pictureBoxImage_MouseClick(object sender, MouseEventArgs e)
        {
            if (cursorX != -1 && cursorY != -1 && resizeWidth != 0 && resizeHeight != 0 && checkBoxCrop.Checked)
            {
                bool canCrop = false;
                if (resizeWidth == GlobalVars.ImageSizeX)
                {
                    cursorX = 0;
                    if (cursorY >= 0 && cursorY + resizeHeight <= GlobalVars.ImageSizeY)
                    {
                        canCrop = true;
                    }
                }
                if (resizeHeight == GlobalVars.ImageSizeY)
                {
                    cursorY = 0;
                    if (cursorX >= 0 && cursorX + resizeWidth <= GlobalVars.ImageSizeX)
                    {
                        canCrop = true;
                    }
                }
                if (canCrop)
                {
                    pictureBoxImage.Image = ResizeFunctions.cropBitmap((Bitmap)pictureBoxImage.Image, cursorX, cursorY, resizeWidth, resizeHeight);
                    setParamsAfterImageLoad();
                }
                else
                {
                    textBoxPendingEdit.Text = "Crop attempt failed! Make sure that the crop boundary is not extending outside the image.";
                }
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
                if (GlobalVars.ImageSizeX == resizeWidth && GlobalVars.ImageSizeY == resizeHeight)
                {
                    textBoxPendingEdit.Text = "Image is already in desired aspect ratio";
                }
                else
                {
                    textBoxPendingEdit.Text = $"Crop will resize image to {resizeWidth}x{resizeHeight}. Click on image to crop. Click preview button or press ESC to cancel.";
                }
                buttonPreviewResize.Enabled = false;
                buttonSubmitResize.Enabled = false;
                trackBarResize.Enabled = false;
            }
            else
            {
                (resizeWidth, resizeHeight) = ResizeFunctions.getResizeDimensions(trackBarResize.Value);
                textBoxPendingEdit.Text = "";
                buttonPreviewResize.Enabled = true;
                buttonSubmitResize.Enabled = true;
                trackBarResize.Enabled = true;
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

        private void trackBarResize_Scroll(object sender, EventArgs e)
        {
            labelResize.Text = $"Resize to {trackBarResize.Value}%";
            if (GlobalVars.FilePath != null)
            {
                (resizeWidth, resizeHeight) = ResizeFunctions.getResizeDimensions(trackBarResize.Value);
                labelShrinkDimensions.Text = $"{resizeWidth}x{resizeHeight}";
            }
        }

        private void buttonPreviewResize_Click(object sender, EventArgs e)
        {
            // flash rectangle over image for about a second to show new size
            // needs to draw on the form if it'll be bigger and on the PictureBox if smaller
            //Graphics graphics = pictureBoxImage.CreateGraphics();
            if (trackBarResize.Value <= 100)
            {
                Graphics graphics = pictureBoxImage.CreateGraphics();
                for (int i = 0; i < 5; i++)
                {
                    // point to draw is based on 0,0 coordinate of the pictureBox
                    ResizeFunctions.drawCenterPointRectangle(graphics, resizeWidth, resizeHeight, GlobalVars.ImageSizeX / 2, GlobalVars.ImageSizeY / 2);
                    Thread.Sleep(100);
                    pictureBoxImage.Invalidate();
                    pictureBoxImage.Update();
                    Thread.Sleep(50);
                }
            }
            else
            {
                Graphics graphics = this.CreateGraphics();
                for (int i = 0; i < 5; i++)
                {
                    // point to draw is based on 0,0 coordinate of the form
                    ResizeFunctions.drawCenterPointRectangle(graphics, resizeWidth, resizeHeight, GlobalVars.pictureBoxCenterX, GlobalVars.pictureBoxCenterY);
                    Thread.Sleep(100);
                    this.Invalidate();
                    this.Update();
                    Thread.Sleep(50);
                }
            }

        }

        private void buttonSubmitResize_Click(object sender, EventArgs e)
        {
            pictureBoxImage.Image = BitmapFunction.resize((Bitmap)pictureBoxImage.Image, 100 / (float)trackBarResize.Value);
            setParamsAfterImageLoad();
        }

        private void setResizeOptions()
        {
            (resizeLowerLimit, resizeUpperLimit) = ResizeFunctions.getResizeBounds();
            trackBarResize.Maximum = resizeUpperLimit;
            trackBarResize.Minimum = resizeLowerLimit;
            buttonPreviewResize.Enabled = true;
            buttonSubmitResize.Enabled = true;
            trackBarResize.Enabled = true;
            trackBarResize.Value = resizeLowerLimit > 100 ? resizeLowerLimit : 100;
            trackBarResize.LargeChange = (resizeUpperLimit - resizeLowerLimit) / 20;
            labelResize.Text = $"Resize to {trackBarResize.Value}%";
        }

        private void pictureBoxImage_Resize(object sender, EventArgs e)
        {
            pictureBoxImage.Location = new Point(GlobalVars.pictureBoxCenterX - GlobalVars.ImageSizeX / 2, GlobalVars.pictureBoxCenterY - GlobalVars.ImageSizeY / 2);
        }

        private void setParamsAfterImageLoad()
        {
            textBoxDimensions.Text = $"{GlobalVars.ImageSizeX}x{GlobalVars.ImageSizeY}";
            setResizeOptions();
            labelShrinkDimensions.Text = $"{resizeWidth}x{resizeHeight}";
            checkBoxCrop.Checked = false;
        }
    }
}
