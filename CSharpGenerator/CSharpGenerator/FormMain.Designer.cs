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
            pictureBoxImage = new PictureBox();
            buttonPixelate = new Button();
            comboBoxPalette = new ComboBox();
            labelPalette = new Label();
            labelAlgorithm = new Label();
            comboBoxAlgorithm = new ComboBox();
            toolStripMenu = new ToolStrip();
            toolStripButtonOpen = new ToolStripButton();
            toolStripButtonReload = new ToolStripButton();
            toolStripButtonSave = new ToolStripButton();
            pictureBoxPalette = new PictureBox();
            labelPalettePreview = new Label();
            labelAspectRatio = new Label();
            comboBoxAspectRatio = new ComboBox();
            checkBoxCrop = new CheckBox();
            textBoxDimensions = new TextBox();
            textBoxFilePath = new TextBox();
            textBoxPendingEdit = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).BeginInit();
            toolStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPalette).BeginInit();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBoxImage
            // 
            pictureBoxImage.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxImage.Location = new Point(255, 33);
            pictureBoxImage.Name = "pictureBoxImage";
            pictureBoxImage.Size = new Size(1118, 720);
            pictureBoxImage.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBoxImage.TabIndex = 2;
            pictureBoxImage.TabStop = false;
            pictureBoxImage.Paint += pictureBoxImage_Paint;
            pictureBoxImage.MouseLeave += pictureBoxImage_MouseLeave;
            pictureBoxImage.MouseMove += pictureBoxImage_MouseMove;
            // 
            // buttonPixelate
            // 
            buttonPixelate.Enabled = false;
            buttonPixelate.Location = new Point(12, 369);
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
            comboBoxPalette.SelectedIndexChanged += comboBoxPalette_SelectedIndexChanged;
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
            // labelAlgorithm
            // 
            labelAlgorithm.AutoSize = true;
            labelAlgorithm.Location = new Point(12, 308);
            labelAlgorithm.Name = "labelAlgorithm";
            labelAlgorithm.Size = new Size(95, 15);
            labelAlgorithm.TabIndex = 39;
            labelAlgorithm.Text = "Select Algorithm";
            // 
            // comboBoxAlgorithm
            // 
            comboBoxAlgorithm.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAlgorithm.FormattingEnabled = true;
            comboBoxAlgorithm.Items.AddRange(new object[] { "RGB Lowest Combined Diff", "HSV Lowest Combined Diff" });
            comboBoxAlgorithm.Location = new Point(12, 326);
            comboBoxAlgorithm.Name = "comboBoxAlgorithm";
            comboBoxAlgorithm.Size = new Size(156, 23);
            comboBoxAlgorithm.TabIndex = 38;
            comboBoxAlgorithm.SelectedIndexChanged += comboBoxAlgorithm_SelectedIndexChanged;
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
            // pictureBoxPalette
            // 
            pictureBoxPalette.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxPalette.Location = new Point(9, 101);
            pictureBoxPalette.Name = "pictureBoxPalette";
            pictureBoxPalette.Size = new Size(240, 192);
            pictureBoxPalette.TabIndex = 41;
            pictureBoxPalette.TabStop = false;
            // 
            // labelPalettePreview
            // 
            labelPalettePreview.AutoSize = true;
            labelPalettePreview.Location = new Point(12, 83);
            labelPalettePreview.Name = "labelPalettePreview";
            labelPalettePreview.Size = new Size(87, 15);
            labelPalettePreview.TabIndex = 42;
            labelPalettePreview.Text = "Palette Preview";
            // 
            // labelAspectRatio
            // 
            labelAspectRatio.AutoSize = true;
            labelAspectRatio.Location = new Point(12, 436);
            labelAspectRatio.Name = "labelAspectRatio";
            labelAspectRatio.Size = new Size(116, 15);
            labelAspectRatio.TabIndex = 44;
            labelAspectRatio.Text = "Crop to Aspect Ratio";
            // 
            // comboBoxAspectRatio
            // 
            comboBoxAspectRatio.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAspectRatio.FormattingEnabled = true;
            comboBoxAspectRatio.Items.AddRange(new object[] { "16:9", "4:3", "16:15 (NES)", "8:7 (SNES)", "10:7 (Genesis)", "10:9 (GB, GG)", "3:2 (GBA)" });
            comboBoxAspectRatio.Location = new Point(12, 454);
            comboBoxAspectRatio.Name = "comboBoxAspectRatio";
            comboBoxAspectRatio.Size = new Size(121, 23);
            comboBoxAspectRatio.TabIndex = 43;
            comboBoxAspectRatio.SelectedIndexChanged += comboBoxAspectRatio_SelectedIndexChanged;
            // 
            // checkBoxCrop
            // 
            checkBoxCrop.Appearance = Appearance.Button;
            checkBoxCrop.AutoSize = true;
            checkBoxCrop.Enabled = false;
            checkBoxCrop.Location = new Point(175, 454);
            checkBoxCrop.Name = "checkBoxCrop";
            checkBoxCrop.Size = new Size(74, 25);
            checkBoxCrop.TabIndex = 47;
            checkBoxCrop.Text = "Preview ->";
            checkBoxCrop.UseVisualStyleBackColor = true;
            checkBoxCrop.CheckedChanged += checkBoxCrop_CheckedChanged;
            // 
            // textBoxDimensions
            // 
            textBoxDimensions.BackColor = SystemColors.Control;
            textBoxDimensions.BorderStyle = BorderStyle.None;
            textBoxDimensions.Location = new Point(1245, 11);
            textBoxDimensions.Name = "textBoxDimensions";
            textBoxDimensions.ReadOnly = true;
            textBoxDimensions.Size = new Size(128, 16);
            textBoxDimensions.TabIndex = 48;
            textBoxDimensions.TextAlign = HorizontalAlignment.Right;
            // 
            // textBoxFilePath
            // 
            textBoxFilePath.BackColor = SystemColors.Control;
            textBoxFilePath.BorderStyle = BorderStyle.None;
            textBoxFilePath.Location = new Point(255, 12);
            textBoxFilePath.Name = "textBoxFilePath";
            textBoxFilePath.ReadOnly = true;
            textBoxFilePath.Size = new Size(748, 16);
            textBoxFilePath.TabIndex = 49;
            textBoxFilePath.Text = "No file loaded. Select a file with the open button to enable more editing controls.";
            // 
            // textBoxPendingEdit
            // 
            textBoxPendingEdit.BackColor = SystemColors.Control;
            textBoxPendingEdit.BorderStyle = BorderStyle.None;
            textBoxPendingEdit.Location = new Point(255, 763);
            textBoxPendingEdit.Name = "textBoxPendingEdit";
            textBoxPendingEdit.ReadOnly = true;
            textBoxPendingEdit.Size = new Size(1118, 16);
            textBoxPendingEdit.TabIndex = 50;
            textBoxPendingEdit.TextAlign = HorizontalAlignment.Center;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1404, 791);
            Controls.Add(textBoxPendingEdit);
            Controls.Add(textBoxFilePath);
            Controls.Add(textBoxDimensions);
            Controls.Add(checkBoxCrop);
            Controls.Add(labelAspectRatio);
            Controls.Add(comboBoxAspectRatio);
            Controls.Add(labelPalettePreview);
            Controls.Add(pictureBoxPalette);
            Controls.Add(toolStripMenu);
            Controls.Add(labelAlgorithm);
            Controls.Add(comboBoxAlgorithm);
            Controls.Add(labelPalette);
            Controls.Add(comboBoxPalette);
            Controls.Add(buttonPixelate);
            Controls.Add(pictureBoxImage);
            KeyPreview = true;
            Name = "FormMain";
            Text = "File to Image Generator";
            KeyDown += FormMain_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).EndInit();
            toolStripMenu.ResumeLayout(false);
            toolStripMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPalette).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private OpenFileDialog openFileDialog1;
        private PictureBox pictureBoxImage;
        private Button buttonPixelate;
        private ComboBox comboBoxPalette;
        private Label labelPalette;
        private Label labelAlgorithm;
        private ComboBox comboBoxAlgorithm;
        private ToolStrip toolStripMenu;
        private ToolStripButton toolStripButtonOpen;
        private ToolStripButton toolStripButtonReload;
        private ToolStripButton toolStripButtonSave;
        private PictureBox pictureBoxPalette;
        private Label labelPalettePreview;
        private Label labelAspectRatio;
        private ComboBox comboBoxAspectRatio;
        private CheckBox checkBoxCrop;
        private TextBox textBoxDimensions;
        private TextBox textBoxFilePath;
        private TextBox textBoxPendingEdit;
    }
}
