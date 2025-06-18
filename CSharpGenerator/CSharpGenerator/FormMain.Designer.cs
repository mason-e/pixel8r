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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            openFileDialog1 = new OpenFileDialog();
            pictureBox1 = new PictureBox();
            buttonPixelate = new Button();
            comboBoxPalette = new ComboBox();
            labelPalette = new Label();
            label1 = new Label();
            comboBoxAlgorithm = new ComboBox();
            toolStripMenu = new ToolStrip();
            toolStripButtonOpen = new ToolStripButton();
            toolStripButtonReload = new ToolStripButton();
            toolStripButtonSave = new ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            toolStripMenu.SuspendLayout();
            SuspendLayout();
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
            // buttonPixelate
            // 
            buttonPixelate.Location = new Point(12, 150);
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
            comboBoxPalette.Location = new Point(12, 51);
            comboBoxPalette.Name = "comboBoxPalette";
            comboBoxPalette.Size = new Size(121, 23);
            comboBoxPalette.TabIndex = 36;
            // 
            // labelPalette
            // 
            labelPalette.AutoSize = true;
            labelPalette.Location = new Point(12, 33);
            labelPalette.Name = "labelPalette";
            labelPalette.Size = new Size(77, 15);
            labelPalette.TabIndex = 37;
            labelPalette.Text = "Select Palette";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 89);
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
            comboBoxAlgorithm.Location = new Point(12, 107);
            comboBoxAlgorithm.Name = "comboBoxAlgorithm";
            comboBoxAlgorithm.Size = new Size(156, 23);
            comboBoxAlgorithm.TabIndex = 38;
            // 
            // toolStripMenu
            // 
            toolStripMenu.AutoSize = false;
            toolStripMenu.Dock = DockStyle.None;
            toolStripMenu.ImageScalingSize = new Size(20, 20);
            toolStripMenu.Items.AddRange(new ToolStripItem[] { toolStripButtonOpen, toolStripButtonReload, toolStripButtonSave });
            toolStripMenu.Location = new Point(-1, 0);
            toolStripMenu.Name = "toolStripMenu";
            toolStripMenu.Size = new Size(253, 25);
            toolStripMenu.TabIndex = 0;
            // 
            // toolStripButtonOpen
            // 
            toolStripButtonOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonOpen.Image = (Image)resources.GetObject("toolStripButtonOpen.Image");
            toolStripButtonOpen.ImageTransparentColor = Color.Magenta;
            toolStripButtonOpen.Name = "toolStripButtonOpen";
            toolStripButtonOpen.Size = new Size(24, 22);
            toolStripButtonOpen.Text = "Open Image File";
            toolStripButtonOpen.Click += toolStripButtonOpen_Click;
            // 
            // toolStripButtonReload
            // 
            toolStripButtonReload.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonReload.Image = (Image)resources.GetObject("toolStripButtonReload.Image");
            toolStripButtonReload.ImageTransparentColor = Color.Magenta;
            toolStripButtonReload.Name = "toolStripButtonReload";
            toolStripButtonReload.Size = new Size(24, 22);
            toolStripButtonReload.Text = "Reload Original Image";
            toolStripButtonReload.Click += toolStripButtonReload_Click;
            // 
            // toolStripButtonSave
            // 
            toolStripButtonSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonSave.Image = (Image)resources.GetObject("toolStripButtonSave.Image");
            toolStripButtonSave.ImageTransparentColor = Color.Magenta;
            toolStripButtonSave.Name = "toolStripButtonSave";
            toolStripButtonSave.Size = new Size(24, 22);
            toolStripButtonSave.Text = "Save Current Image As";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1404, 766);
            Controls.Add(toolStripMenu);
            Controls.Add(label1);
            Controls.Add(comboBoxAlgorithm);
            Controls.Add(labelPalette);
            Controls.Add(comboBoxPalette);
            Controls.Add(buttonPixelate);
            Controls.Add(pictureBox1);
            Name = "FormMain";
            Text = "File to Image Generator";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            toolStripMenu.ResumeLayout(false);
            toolStripMenu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private OpenFileDialog openFileDialog1;
        private PictureBox pictureBox1;
        private Button buttonPixelate;
        private ComboBox comboBoxPalette;
        private Label labelPalette;
        private Label label1;
        private ComboBox comboBoxAlgorithm;
        private ToolStrip toolStripMenu;
        private ToolStripButton toolStripButtonOpen;
        private ToolStripButton toolStripButtonReload;
        private ToolStripButton toolStripButtonSave;
    }
}
