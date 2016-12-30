namespace ZOLE_4
{
    partial class frmTilesetEditor
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nTileset = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.chkHorizontal = new System.Windows.Forms.CheckBox();
            this.chkVertical = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.pPreviewTile = new ZOLE_4.InterpolationPicturebox();
            this.pTileset = new ZOLE_4.GridBox();
            this.pColor = new ZOLE_4.GridBox();
            this.pTile = new ZOLE_4.GridBox();
            this.pPalette = new ZOLE_4.GridBox();
            this.pTiles = new ZOLE_4.GridBox();
            ((System.ComponentModel.ISupportInitialize)(this.nTileset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPreviewTile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTileset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPalette)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTiles)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(537, 505);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(534, 407);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Area ID:";
            // 
            // nTileset
            // 
            this.nTileset.Location = new System.Drawing.Point(586, 405);
            this.nTileset.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nTileset.Name = "nTileset";
            this.nTileset.Size = new System.Drawing.Size(114, 20);
            this.nTileset.TabIndex = 9;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(732, 403);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(62, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Load";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // chkHorizontal
            // 
            this.chkHorizontal.AutoSize = true;
            this.chkHorizontal.Location = new System.Drawing.Point(586, 431);
            this.chkHorizontal.Name = "chkHorizontal";
            this.chkHorizontal.Size = new System.Drawing.Size(92, 17);
            this.chkHorizontal.TabIndex = 20;
            this.chkHorizontal.Text = "Horizontal Flip";
            this.chkHorizontal.UseVisualStyleBackColor = true;
            this.chkHorizontal.CheckedChanged += new System.EventHandler(this.chkHorizontal_CheckedChanged);
            // 
            // chkVertical
            // 
            this.chkVertical.AutoSize = true;
            this.chkVertical.Location = new System.Drawing.Point(706, 431);
            this.chkVertical.Name = "chkVertical";
            this.chkVertical.Size = new System.Drawing.Size(80, 17);
            this.chkVertical.TabIndex = 21;
            this.chkVertical.Text = "Vertical Flip";
            this.chkVertical.UseVisualStyleBackColor = true;
            this.chkVertical.CheckedChanged += new System.EventHandler(this.chkVertical_CheckedChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(668, 505);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(126, 23);
            this.button3.TabIndex = 23;
            this.button3.Text = "Close";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(537, 476);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(125, 23);
            this.button4.TabIndex = 24;
            this.button4.Text = "Undo";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(669, 476);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(125, 23);
            this.button5.TabIndex = 25;
            this.button5.Text = "Redo";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // pPreviewTile
            // 
            this.pPreviewTile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pPreviewTile.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.pPreviewTile.Location = new System.Drawing.Point(706, 405);
            this.pPreviewTile.Name = "pPreviewTile";
            this.pPreviewTile.Size = new System.Drawing.Size(20, 20);
            this.pPreviewTile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pPreviewTile.TabIndex = 22;
            this.pPreviewTile.TabStop = false;
            // 
            // pTileset
            // 
            this.pTileset.AllowMultiSelection = false;
            this.pTileset.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pTileset.BoxSize = new System.Drawing.Size(16, 16);
            this.pTileset.CanvasSize = new System.Drawing.Size(512, 512);
            this.pTileset.HoverBox = true;
            this.pTileset.HoverColor = System.Drawing.Color.White;
            this.pTileset.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.pTileset.Location = new System.Drawing.Point(12, 12);
            this.pTileset.Name = "pTileset";
            this.pTileset.Selectable = false;
            this.pTileset.SelectedIndex = 0;
            this.pTileset.SelectionColor = System.Drawing.Color.Red;
            this.pTileset.SelectionRectangle = new System.Drawing.Rectangle(0, 0, 1, 1);
            this.pTileset.Size = new System.Drawing.Size(516, 516);
            this.pTileset.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pTileset.TabIndex = 19;
            this.pTileset.TabStop = false;
            this.pTileset.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pTileset_MouseDown);
            this.pTileset.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pTileset_MouseDown);
            this.pTileset.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pTileset_MouseUp);
            // 
            // pColor
            // 
            this.pColor.AllowMultiSelection = false;
            this.pColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pColor.BoxSize = new System.Drawing.Size(46, 64);
            this.pColor.CanvasSize = new System.Drawing.Size(184, 64);
            this.pColor.HoverBox = true;
            this.pColor.HoverColor = System.Drawing.Color.White;
            this.pColor.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.pColor.Location = new System.Drawing.Point(606, 320);
            this.pColor.Name = "pColor";
            this.pColor.Selectable = true;
            this.pColor.SelectedIndex = 0;
            this.pColor.SelectionColor = System.Drawing.Color.Red;
            this.pColor.SelectionRectangle = new System.Drawing.Rectangle(0, 0, 1, 1);
            this.pColor.Size = new System.Drawing.Size(188, 68);
            this.pColor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pColor.TabIndex = 18;
            this.pColor.TabStop = false;
            // 
            // pTile
            // 
            this.pTile.AllowMultiSelection = false;
            this.pTile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pTile.BoxSize = new System.Drawing.Size(8, 8);
            this.pTile.CanvasSize = new System.Drawing.Size(64, 64);
            this.pTile.HoverBox = true;
            this.pTile.HoverColor = System.Drawing.Color.White;
            this.pTile.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.pTile.Location = new System.Drawing.Point(534, 320);
            this.pTile.Name = "pTile";
            this.pTile.Selectable = false;
            this.pTile.SelectedIndex = 0;
            this.pTile.SelectionColor = System.Drawing.Color.Red;
            this.pTile.SelectionRectangle = new System.Drawing.Rectangle(0, 0, 1, 1);
            this.pTile.Size = new System.Drawing.Size(68, 68);
            this.pTile.TabIndex = 17;
            this.pTile.TabStop = false;
            this.pTile.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pTile_MouseDown);
            this.pTile.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pTile_MouseDown);
            this.pTile.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pTile_MouseUp);
            // 
            // pPalette
            // 
            this.pPalette.AllowMultiSelection = false;
            this.pPalette.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pPalette.BoxSize = new System.Drawing.Size(32, 32);
            this.pPalette.CanvasSize = new System.Drawing.Size(256, 32);
            this.pPalette.HoverBox = true;
            this.pPalette.HoverColor = System.Drawing.Color.White;
            this.pPalette.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.pPalette.Location = new System.Drawing.Point(534, 12);
            this.pPalette.Name = "pPalette";
            this.pPalette.Selectable = true;
            this.pPalette.SelectedIndex = 0;
            this.pPalette.SelectionColor = System.Drawing.Color.Red;
            this.pPalette.SelectionRectangle = new System.Drawing.Rectangle(0, 0, 1, 1);
            this.pPalette.Size = new System.Drawing.Size(260, 36);
            this.pPalette.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pPalette.TabIndex = 16;
            this.pPalette.TabStop = false;
            this.pPalette.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pPalette_MouseDown);
            this.pPalette.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pPalette_MouseMove);
            // 
            // pTiles
            // 
            this.pTiles.AllowMultiSelection = false;
            this.pTiles.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pTiles.BoxSize = new System.Drawing.Size(16, 16);
            this.pTiles.CanvasSize = new System.Drawing.Size(256, 256);
            this.pTiles.HoverBox = true;
            this.pTiles.HoverColor = System.Drawing.Color.White;
            this.pTiles.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.pTiles.Location = new System.Drawing.Point(534, 54);
            this.pTiles.Name = "pTiles";
            this.pTiles.Selectable = true;
            this.pTiles.SelectedIndex = 0;
            this.pTiles.SelectionColor = System.Drawing.Color.Red;
            this.pTiles.SelectionRectangle = new System.Drawing.Rectangle(0, 0, 1, 1);
            this.pTiles.Size = new System.Drawing.Size(260, 260);
            this.pTiles.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pTiles.TabIndex = 15;
            this.pTiles.TabStop = false;
            this.pTiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pTiles_MouseDown);
            this.pTiles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pTiles_MouseUp);
            // 
            // frmTilesetEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 540);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.pPreviewTile);
            this.Controls.Add(this.chkVertical);
            this.Controls.Add(this.chkHorizontal);
            this.Controls.Add(this.pTileset);
            this.Controls.Add(this.pColor);
            this.Controls.Add(this.pTile);
            this.Controls.Add(this.pPalette);
            this.Controls.Add(this.pTiles);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.nTileset);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmTilesetEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tileset Editor";
            this.Load += new System.EventHandler(this.frmTilesetEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nTileset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPreviewTile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTileset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPalette)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private GridBox pTiles;
        private GridBox pPalette;
        private GridBox pTile;
        private GridBox pColor;
        public System.Windows.Forms.NumericUpDown nTileset;
        private GridBox pTileset;
        private System.Windows.Forms.CheckBox chkHorizontal;
        private System.Windows.Forms.CheckBox chkVertical;
        private InterpolationPicturebox pPreviewTile;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}