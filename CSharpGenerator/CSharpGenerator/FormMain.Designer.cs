namespace CSharpGenerator
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonFile1 = new Button();
            openFileDialog1 = new OpenFileDialog();
            pictureBox1 = new PictureBox();
            textBoxSize = new TextBox();
            textBoxDimension = new TextBox();
            buttonBitmap = new Button();
            buttonPixelate = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // buttonFile1
            // 
            buttonFile1.Location = new Point(12, 12);
            buttonFile1.Name = "buttonFile1";
            buttonFile1.Size = new Size(75, 23);
            buttonFile1.TabIndex = 0;
            buttonFile1.Text = "File ...";
            buttonFile1.UseVisualStyleBackColor = true;
            buttonFile1.Click += buttonFile1_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(255, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1118, 720);
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // textBoxSize
            // 
            textBoxSize.Location = new Point(93, 738);
            textBoxSize.Name = "textBoxSize";
            textBoxSize.ReadOnly = true;
            textBoxSize.Size = new Size(75, 23);
            textBoxSize.TabIndex = 3;
            // 
            // textBoxDimension
            // 
            textBoxDimension.Location = new Point(174, 738);
            textBoxDimension.Name = "textBoxDimension";
            textBoxDimension.ReadOnly = true;
            textBoxDimension.Size = new Size(75, 23);
            textBoxDimension.TabIndex = 4;
            // 
            // buttonBitmap
            // 
            buttonBitmap.Location = new Point(12, 41);
            buttonBitmap.Name = "buttonBitmap";
            buttonBitmap.Size = new Size(75, 23);
            buttonBitmap.TabIndex = 9;
            buttonBitmap.Text = "Display!";
            buttonBitmap.UseVisualStyleBackColor = true;
            buttonBitmap.Click += buttonBitmap_Click;
            // 
            // buttonPixelate
            // 
            buttonPixelate.Location = new Point(12, 70);
            buttonPixelate.Name = "buttonPixelate";
            buttonPixelate.Size = new Size(75, 23);
            buttonPixelate.TabIndex = 35;
            buttonPixelate.Text = "Pixelate!";
            buttonPixelate.UseVisualStyleBackColor = true;
            buttonPixelate.Click += buttonPixelate_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1404, 766);
            Controls.Add(buttonPixelate);
            Controls.Add(buttonBitmap);
            Controls.Add(textBoxDimension);
            Controls.Add(textBoxSize);
            Controls.Add(pictureBox1);
            Controls.Add(buttonFile1);
            Name = "FormMain";
            Text = "File to Image Generator";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonFile1;
        private OpenFileDialog openFileDialog1;
        private PictureBox pictureBox1;
        private TextBox textBoxSize;
        private TextBox textBoxDimension;
        private Button buttonBitmap;
        private Button buttonPixelate;
    }
}
