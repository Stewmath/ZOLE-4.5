namespace ZOLE_4
{
    partial class frmStart
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nX = new System.Windows.Forms.NumericUpDown();
            this.nY = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.nMap = new System.Windows.Forms.NumericUpDown();
            this.nGroup = new System.Windows.Forms.NumericUpDown();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Starting Map:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Group:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Position:";
            // 
            // nX
            // 
            this.nX.Hexadecimal = true;
            this.nX.Location = new System.Drawing.Point(88, 65);
            this.nX.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nX.Name = "nX";
            this.nX.Size = new System.Drawing.Size(60, 20);
            this.nX.TabIndex = 5;
            // 
            // nY
            // 
            this.nY.Hexadecimal = true;
            this.nY.Location = new System.Drawing.Point(154, 65);
            this.nY.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nY.Name = "nY";
            this.nY.Size = new System.Drawing.Size(59, 20);
            this.nY.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(88, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 91);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // nMap
            // 
            this.nMap.Hexadecimal = true;
            this.nMap.Location = new System.Drawing.Point(88, 12);
            this.nMap.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nMap.Name = "nMap";
            this.nMap.Size = new System.Drawing.Size(125, 20);
            this.nMap.TabIndex = 1;
            // 
            // nGroup
            // 
            this.nGroup.Hexadecimal = true;
            this.nGroup.Location = new System.Drawing.Point(88, 38);
            this.nGroup.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nGroup.Name = "nGroup";
            this.nGroup.Size = new System.Drawing.Size(125, 20);
            this.nGroup.TabIndex = 9;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(196, 91);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(17, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "?";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // frmStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 123);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.nGroup);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.nY);
            this.Controls.Add(this.nX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nMap);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmStart";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Start Editor";
            this.Load += new System.EventHandler(this.frmStart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nGroup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nX;
        private System.Windows.Forms.NumericUpDown nY;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown nMap;
        private System.Windows.Forms.NumericUpDown nGroup;
        private System.Windows.Forms.Button button3;
    }
}