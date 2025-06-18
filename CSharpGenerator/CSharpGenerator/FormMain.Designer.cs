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
            buttonLoadFile = new Button();
            openFileDialog1 = new OpenFileDialog();
            pictureBox1 = new PictureBox();
            buttonReload = new Button();
            buttonPixelate = new Button();
            comboBoxPalette = new ComboBox();
            labelPalette = new Label();
            label1 = new Label();
            comboBoxAlgorithm = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // buttonLoadFile
            // 
            buttonLoadFile.Location = new Point(12, 12);
            buttonLoadFile.Name = "buttonLoadFile";
            buttonLoadFile.Size = new Size(84, 23);
            buttonLoadFile.TabIndex = 0;
            buttonLoadFile.Text = "Load File...";
            buttonLoadFile.UseVisualStyleBackColor = true;
            buttonLoadFile.Click += buttonLoadFile_Click;
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
            // buttonReload
            // 
            buttonReload.Location = new Point(102, 12);
            buttonReload.Name = "buttonReload";
            buttonReload.Size = new Size(84, 23);
            buttonReload.TabIndex = 9;
            buttonReload.Text = "Reset Image";
            buttonReload.UseVisualStyleBackColor = true;
            buttonReload.Click += buttonReload_Click;
            // 
            // buttonPixelate
            // 
            buttonPixelate.Location = new Point(12, 214);
            buttonPixelate.Name = "buttonPixelate";
            buttonPixelate.Size = new Size(84, 23);
            buttonPixelate.TabIndex = 35;
            buttonPixelate.Text = "Pixelate!";
            buttonPixelate.UseVisualStyleBackColor = true;
            buttonPixelate.Click += buttonPixelate_Click;
            // 
            // comboBoxPalette
            // 
            comboBoxPalette.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPalette.FormattingEnabled = true;
            comboBoxPalette.Items.AddRange(new object[] { "Web Colors", "NES" });
            comboBoxPalette.Location = new Point(12, 104);
            comboBoxPalette.Name = "comboBoxPalette";
            comboBoxPalette.Size = new Size(121, 23);
            comboBoxPalette.TabIndex = 36;
            // 
            // labelPalette
            // 
            labelPalette.AutoSize = true;
            labelPalette.Location = new Point(12, 86);
            labelPalette.Name = "labelPalette";
            labelPalette.Size = new Size(77, 15);
            labelPalette.TabIndex = 37;
            labelPalette.Text = "Select Palette";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 142);
            label1.Name = "label1";
            label1.Size = new Size(95, 15);
            label1.TabIndex = 39;
            label1.Text = "Select Algorithm";
            // 
            // comboBoxAlgorithm
            // 
            comboBoxAlgorithm.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAlgorithm.FormattingEnabled = true;
            comboBoxAlgorithm.Items.AddRange(new object[] { "RGB Lowest Combined Diff", "HSV Lowest Combined Diff" });
            comboBoxAlgorithm.Location = new Point(12, 160);
            comboBoxAlgorithm.Name = "comboBoxAlgorithm";
            comboBoxAlgorithm.Size = new Size(156, 23);
            comboBoxAlgorithm.TabIndex = 38;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1404, 766);
            Controls.Add(label1);
            Controls.Add(comboBoxAlgorithm);
            Controls.Add(labelPalette);
            Controls.Add(comboBoxPalette);
            Controls.Add(buttonPixelate);
            Controls.Add(buttonReload);
            Controls.Add(pictureBox1);
            Controls.Add(buttonLoadFile);
            Name = "FormMain";
            Text = "File to Image Generator";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonLoadFile;
        private OpenFileDialog openFileDialog1;
        private PictureBox pictureBox1;
        private Button buttonReload;
        private Button buttonPixelate;
        private ComboBox comboBoxPalette;
        private Label labelPalette;
        private Label label1;
        private ComboBox comboBoxAlgorithm;
    }
}
