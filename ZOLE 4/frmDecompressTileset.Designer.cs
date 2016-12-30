namespace ZOLE_4
{
    partial class frmDecompressTileset
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nPalette = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nAnimation = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nUnique = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nTileset = new System.Windows.Forms.NumericUpDown();
            this.nVRAM = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pTileset = new ZOLE_4.GridBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nPalette)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nAnimation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUnique)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nTileset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nVRAM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTileset)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nPalette);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.nAnimation);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.nUnique);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.nTileset);
            this.groupBox1.Controls.Add(this.nVRAM);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 137);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Area Information";
            // 
            // nPalette
            // 
            this.nPalette.Location = new System.Drawing.Point(70, 111);
            this.nPalette.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nPalette.Name = "nPalette";
            this.nPalette.Size = new System.Drawing.Size(184, 20);
            this.nPalette.TabIndex = 23;
            this.nPalette.ValueChanged += new System.EventHandler(this.nVRAM_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Palette:";
            // 
            // nAnimation
            // 
            this.nAnimation.Location = new System.Drawing.Point(70, 88);
            this.nAnimation.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nAnimation.Name = "nAnimation";
            this.nAnimation.Size = new System.Drawing.Size(184, 20);
            this.nAnimation.TabIndex = 21;
            this.nAnimation.ValueChanged += new System.EventHandler(this.nVRAM_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Animation:";
            // 
            // nUnique
            // 
            this.nUnique.Location = new System.Drawing.Point(70, 65);
            this.nUnique.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nUnique.Name = "nUnique";
            this.nUnique.Size = new System.Drawing.Size(184, 20);
            this.nUnique.TabIndex = 19;
            this.nUnique.ValueChanged += new System.EventHandler(this.nVRAM_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Unique:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Base Tiles:";
            // 
            // nTileset
            // 
            this.nTileset.Location = new System.Drawing.Point(70, 42);
            this.nTileset.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.nTileset.Name = "nTileset";
            this.nTileset.Size = new System.Drawing.Size(184, 20);
            this.nTileset.TabIndex = 17;
            this.nTileset.ValueChanged += new System.EventHandler(this.nVRAM_ValueChanged);
            // 
            // nVRAM
            // 
            this.nVRAM.Location = new System.Drawing.Point(70, 19);
            this.nVRAM.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nVRAM.Name = "nVRAM";
            this.nVRAM.Size = new System.Drawing.Size(184, 20);
            this.nVRAM.TabIndex = 15;
            this.nVRAM.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.nVRAM.ValueChanged += new System.EventHandler(this.nVRAM_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Tileset:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(123, 422);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Decompress";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 422);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pTileset
            // 
            this.pTileset.AllowMultiSelection = true;
            this.pTileset.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pTileset.BoxSize = new System.Drawing.Size(16, 16);
            this.pTileset.CanvasSize = new System.Drawing.Size(256, 256);
            this.pTileset.HoverBox = true;
            this.pTileset.HoverColor = System.Drawing.Color.White;
            this.pTileset.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.pTileset.Location = new System.Drawing.Point(12, 156);
            this.pTileset.Name = "pTileset";
            this.pTileset.Selectable = true;
            this.pTileset.SelectedIndex = -2;
            this.pTileset.SelectionColor = System.Drawing.Color.Red;
            this.pTileset.SelectionRectangle = new System.Drawing.Rectangle(-2, 0, 1, 1);
            this.pTileset.Size = new System.Drawing.Size(260, 260);
            this.pTileset.TabIndex = 10;
            this.pTileset.TabStop = false;
            // 
            // frmDecompressTileset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 457);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pTileset);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmDecompressTileset";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Re-Decompress Tileset";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nPalette)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nAnimation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUnique)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nTileset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nVRAM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTileset)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.NumericUpDown nPalette;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private GridBox pTileset;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.NumericUpDown nAnimation;
        public System.Windows.Forms.NumericUpDown nUnique;
        public System.Windows.Forms.NumericUpDown nTileset;
        public System.Windows.Forms.NumericUpDown nVRAM;
    }
}