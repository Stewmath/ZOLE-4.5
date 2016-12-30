namespace ZOLE_4
{
    partial class frmGaleSeed
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
            this.rbPresent = new System.Windows.Forms.RadioButton();
            this.rbPast = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.nIndex = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nMap = new System.Windows.Forms.NumericUpDown();
            this.nPos = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.nUnknown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.pMap = new ZOLE_4.GridBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUnknown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMap)).BeginInit();
            this.SuspendLayout();
            // 
            // rbPresent
            // 
            this.rbPresent.AutoSize = true;
            this.rbPresent.Checked = true;
            this.rbPresent.Location = new System.Drawing.Point(12, 150);
            this.rbPresent.Name = "rbPresent";
            this.rbPresent.Size = new System.Drawing.Size(61, 17);
            this.rbPresent.TabIndex = 1;
            this.rbPresent.TabStop = true;
            this.rbPresent.Text = "Present";
            this.rbPresent.UseVisualStyleBackColor = true;
            this.rbPresent.CheckedChanged += new System.EventHandler(this.rbPresent_CheckedChanged);
            // 
            // rbPast
            // 
            this.rbPast.AutoSize = true;
            this.rbPast.Location = new System.Drawing.Point(98, 150);
            this.rbPast.Name = "rbPast";
            this.rbPast.Size = new System.Drawing.Size(46, 17);
            this.rbPast.TabIndex = 2;
            this.rbPast.Text = "Past";
            this.rbPast.UseVisualStyleBackColor = true;
            this.rbPast.CheckedChanged += new System.EventHandler(this.rbPast_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Index:";
            // 
            // nIndex
            // 
            this.nIndex.Location = new System.Drawing.Point(71, 173);
            this.nIndex.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nIndex.Name = "nIndex";
            this.nIndex.Size = new System.Drawing.Size(52, 20);
            this.nIndex.TabIndex = 4;
            this.nIndex.ValueChanged += new System.EventHandler(this.nIndex_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 201);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Map:";
            // 
            // nMap
            // 
            this.nMap.Hexadecimal = true;
            this.nMap.Location = new System.Drawing.Point(71, 199);
            this.nMap.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nMap.Name = "nMap";
            this.nMap.Size = new System.Drawing.Size(52, 20);
            this.nMap.TabIndex = 6;
            this.nMap.ValueChanged += new System.EventHandler(this.nMap_ValueChanged);
            // 
            // nPos
            // 
            this.nPos.Hexadecimal = true;
            this.nPos.Location = new System.Drawing.Point(71, 225);
            this.nPos.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nPos.Name = "nPos";
            this.nPos.Size = new System.Drawing.Size(52, 20);
            this.nPos.TabIndex = 8;
            this.nPos.ValueChanged += new System.EventHandler(this.nPos_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 227);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Dest. YX:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 277);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // nUnknown
            // 
            this.nUnknown.Hexadecimal = true;
            this.nUnknown.Location = new System.Drawing.Point(71, 251);
            this.nUnknown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nUnknown.Name = "nUnknown";
            this.nUnknown.Size = new System.Drawing.Size(52, 20);
            this.nUnknown.TabIndex = 11;
            this.nUnknown.ValueChanged += new System.EventHandler(this.nUnknown_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 253);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Unknown:";
            // 
            // pMap
            // 
            this.pMap.AllowMultiSelection = false;
            this.pMap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pMap.BoxSize = new System.Drawing.Size(8, 8);
            this.pMap.CanvasSize = new System.Drawing.Size(128, 128);
            this.pMap.HoverBox = true;
            this.pMap.HoverColor = System.Drawing.Color.White;
            this.pMap.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.pMap.Location = new System.Drawing.Point(12, 12);
            this.pMap.Name = "pMap";
            this.pMap.Selectable = false;
            this.pMap.SelectedIndex = 0;
            this.pMap.SelectionColor = System.Drawing.Color.Red;
            this.pMap.SelectionRectangle = new System.Drawing.Rectangle(0, 0, 1, 1);
            this.pMap.Size = new System.Drawing.Size(132, 132);
            this.pMap.TabIndex = 12;
            this.pMap.TabStop = false;
            this.pMap.Paint += new System.Windows.Forms.PaintEventHandler(this.pMap_Paint);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(129, 173);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(15, 20);
            this.button2.TabIndex = 13;
            this.button2.Text = "?";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(129, 199);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(15, 20);
            this.button3.TabIndex = 14;
            this.button3.Text = "?";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(129, 225);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(15, 20);
            this.button4.TabIndex = 15;
            this.button4.Text = "?";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(129, 251);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(15, 20);
            this.button5.TabIndex = 16;
            this.button5.Text = "?";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // frmGaleSeed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(156, 312);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pMap);
            this.Controls.Add(this.nUnknown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.nPos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nMap);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nIndex);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbPast);
            this.Controls.Add(this.rbPresent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmGaleSeed";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gale Seed Warp Editor";
            this.Load += new System.EventHandler(this.frmGaleSeed_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUnknown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbPresent;
        private System.Windows.Forms.RadioButton rbPast;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nIndex;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nMap;
        private System.Windows.Forms.NumericUpDown nPos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown nUnknown;
        private System.Windows.Forms.Label label4;
        private GridBox pMap;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}