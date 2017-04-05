namespace ZOLE_4
{
    partial class frmSign
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
            this.lblGroup = new System.Windows.Forms.Label();
            this.nSign = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nMap = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pMap = new ZOLE_4.GridBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.nText = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nSign)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMap)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nText)).BeginInit();
            this.SuspendLayout();
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(262, 9);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(72, 13);
            this.lblGroup.TabIndex = 1;
            this.lblGroup.Text = "Map Group: 0";
            // 
            // nSign
            // 
            this.nSign.Location = new System.Drawing.Point(328, 29);
            this.nSign.Name = "nSign";
            this.nSign.Size = new System.Drawing.Size(49, 20);
            this.nSign.TabIndex = 2;
            this.nSign.ValueChanged += new System.EventHandler(this.nSign_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(262, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sign Index:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(262, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Map:";
            // 
            // nMap
            // 
            this.nMap.Hexadecimal = true;
            this.nMap.Location = new System.Drawing.Point(328, 55);
            this.nMap.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nMap.Name = "nMap";
            this.nMap.Size = new System.Drawing.Size(49, 20);
            this.nMap.TabIndex = 4;
            this.nMap.ValueChanged += new System.EventHandler(this.nMap_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pMap);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 187);
            this.panel1.TabIndex = 6;
            // 
            // pMap
            // 
            this.pMap.AllowMultiSelection = false;
            this.pMap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pMap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pMap.BoxSize = new System.Drawing.Size(16, 16);
            this.pMap.CanvasSize = new System.Drawing.Size(160, 128);
            this.pMap.HoverBox = true;
            this.pMap.HoverColor = System.Drawing.Color.White;
            this.pMap.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.pMap.Location = new System.Drawing.Point(12, 3);
            this.pMap.Name = "pMap";
            this.pMap.Selectable = false;
            this.pMap.SelectedIndex = 0;
            this.pMap.SelectionColor = System.Drawing.Color.Red;
            this.pMap.SelectionRectangle = new System.Drawing.Rectangle(0, 0, 1, 1);
            this.pMap.Size = new System.Drawing.Size(244, 180);
            this.pMap.TabIndex = 20;
            this.pMap.TabStop = false;
            this.pMap.Paint += new System.Windows.Forms.PaintEventHandler(this.pMap_Paint);
            this.pMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pMap_MouseDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(265, 160);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(262, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Text:";
            // 
            // nText
            // 
            this.nText.Hexadecimal = true;
            this.nText.Location = new System.Drawing.Point(328, 82);
            this.nText.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nText.Name = "nText";
            this.nText.Size = new System.Drawing.Size(49, 20);
            this.nText.TabIndex = 8;
            this.nText.ValueChanged += new System.EventHandler(this.nText_ValueChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(383, 29);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(20, 20);
            this.button2.TabIndex = 10;
            this.button2.Text = "?";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(383, 57);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(20, 20);
            this.button3.TabIndex = 11;
            this.button3.Text = "?";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(383, 82);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(20, 20);
            this.button4.TabIndex = 12;
            this.button4.Text = "?";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // frmSign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 192);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nText);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nMap);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nSign);
            this.Controls.Add(this.lblGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmSign";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sign Editor";
            this.Load += new System.EventHandler(this.frmSign_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nSign)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMap)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nText)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.NumericUpDown nSign;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nMap;
        private System.Windows.Forms.Panel panel1;
        private GridBox pMap;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nText;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}