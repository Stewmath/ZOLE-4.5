namespace ZOLE_4
{
    partial class frmPalette
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
            this.label1 = new System.Windows.Forms.Label();
            this.nPalette = new System.Windows.Forms.NumericUpDown();
            this.nTileset = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pSet1 = new ZOLE_4.GridBox();
            this.pSet2 = new ZOLE_4.GridBox();
            this.pTileset = new ZOLE_4.GridBox();
            ((System.ComponentModel.ISupportInitialize)(this.nPalette)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nTileset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTileset)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(146, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Palette:";
            // 
            // nPalette
            // 
            this.nPalette.Location = new System.Drawing.Point(195, 38);
            this.nPalette.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nPalette.Name = "nPalette";
            this.nPalette.Size = new System.Drawing.Size(77, 20);
            this.nPalette.TabIndex = 14;
            this.nPalette.ValueChanged += new System.EventHandler(this.nPalette_ValueChanged);
            // 
            // nTileset
            // 
            this.nTileset.Location = new System.Drawing.Point(195, 12);
            this.nTileset.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nTileset.Name = "nTileset";
            this.nTileset.Size = new System.Drawing.Size(77, 20);
            this.nTileset.TabIndex = 16;
            this.nTileset.ValueChanged += new System.EventHandler(this.nTileset_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Area ID:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(149, 78);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(149, 107);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 23);
            this.button2.TabIndex = 19;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pSet1
            // 
            this.pSet1.AllowMultiSelection = false;
            this.pSet1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pSet1.BoxSize = new System.Drawing.Size(32, 16);
            this.pSet1.CanvasSize = new System.Drawing.Size(128, 16);
            this.pSet1.HoverBox = true;
            this.pSet1.HoverColor = System.Drawing.Color.White;
            this.pSet1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.pSet1.Location = new System.Drawing.Point(12, 12);
            this.pSet1.Name = "pSet1";
            this.pSet1.Selectable = false;
            this.pSet1.SelectedIndex = 0;
            this.pSet1.SelectionColor = System.Drawing.Color.Red;
            this.pSet1.SelectionRectangle = new System.Drawing.Rectangle(0, 0, 1, 1);
            this.pSet1.Size = new System.Drawing.Size(128, 17);
            this.pSet1.TabIndex = 13;
            this.pSet1.TabStop = false;
            this.pSet1.Click += new System.EventHandler(this.pSet1_Click);
            this.pSet1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pSet1_MouseDown);
            // 
            // pSet2
            // 
            this.pSet2.AllowMultiSelection = false;
            this.pSet2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pSet2.BoxSize = new System.Drawing.Size(32, 16);
            this.pSet2.CanvasSize = new System.Drawing.Size(128, 96);
            this.pSet2.HoverBox = true;
            this.pSet2.HoverColor = System.Drawing.Color.White;
            this.pSet2.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.pSet2.Location = new System.Drawing.Point(12, 33);
            this.pSet2.Name = "pSet2";
            this.pSet2.Selectable = false;
            this.pSet2.SelectedIndex = 0;
            this.pSet2.SelectionColor = System.Drawing.Color.Red;
            this.pSet2.SelectionRectangle = new System.Drawing.Rectangle(0, 0, 1, 1);
            this.pSet2.Size = new System.Drawing.Size(128, 97);
            this.pSet2.TabIndex = 12;
            this.pSet2.TabStop = false;
            this.pSet2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pSet2_MouseDown);
            // 
            // pTileset
            // 
            this.pTileset.AllowMultiSelection = false;
            this.pTileset.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pTileset.BoxSize = new System.Drawing.Size(16, 16);
            this.pTileset.CanvasSize = new System.Drawing.Size(256, 256);
            this.pTileset.HoverBox = true;
            this.pTileset.HoverColor = System.Drawing.Color.White;
            this.pTileset.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.pTileset.Location = new System.Drawing.Point(12, 136);
            this.pTileset.Name = "pTileset";
            this.pTileset.Selectable = true;
            this.pTileset.SelectedIndex = -1;
            this.pTileset.SelectionColor = System.Drawing.Color.Red;
            this.pTileset.SelectionRectangle = new System.Drawing.Rectangle(-1, 0, 1, 1);
            this.pTileset.Size = new System.Drawing.Size(260, 260);
            this.pTileset.TabIndex = 10;
            this.pTileset.TabStop = false;
            // 
            // frmPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 410);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nTileset);
            this.Controls.Add(this.nPalette);
            this.Controls.Add(this.pSet1);
            this.Controls.Add(this.pSet2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pTileset);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmPalette";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Palette Editor";
            this.Load += new System.EventHandler(this.frmPalette_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nPalette)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nTileset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTileset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GridBox pTileset;
        private System.Windows.Forms.Label label1;
        private GridBox pSet2;
        private GridBox pSet1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown nPalette;
        public System.Windows.Forms.NumericUpDown nTileset;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}