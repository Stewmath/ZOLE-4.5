namespace ZOLE_4
{
    partial class frmChest
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
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkMap = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.nID = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.nPos = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.nSwap = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPos)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nSwap)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "No chest data found for this map.";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(87, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Swap";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.chkMap);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.nID);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.nPos);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(11, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(167, 86);
            this.panel1.TabIndex = 2;
            // 
            // chkMap
            // 
            this.chkMap.AutoSize = true;
            this.chkMap.Location = new System.Drawing.Point(35, 52);
            this.chkMap.Name = "chkMap";
            this.chkMap.Size = new System.Drawing.Size(69, 17);
            this.chkMap.TabIndex = 5;
            this.chkMap.Text = "Map First";
            this.chkMap.UseVisualStyleBackColor = true;
            this.chkMap.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(87, 53);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(79, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "OK";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // nID
            // 
            this.nID.Hexadecimal = true;
            this.nID.Location = new System.Drawing.Point(24, 27);
            this.nID.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nID.Name = "nID";
            this.nID.Size = new System.Drawing.Size(117, 20);
            this.nID.TabIndex = 3;
            this.nID.ValueChanged += new System.EventHandler(this.nID_ValueChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(2, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(79, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-3, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "ID:";
            // 
            // nPos
            // 
            this.nPos.Hexadecimal = true;
            this.nPos.Location = new System.Drawing.Point(24, 1);
            this.nPos.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nPos.Name = "nPos";
            this.nPos.Size = new System.Drawing.Size(117, 20);
            this.nPos.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "YX:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.nSwap);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(11, 9);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(167, 79);
            this.panel2.TabIndex = 5;
            this.panel2.Visible = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(8, 48);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(73, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "Cancel";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // nSwap
            // 
            this.nSwap.Hexadecimal = true;
            this.nSwap.Location = new System.Drawing.Point(79, 22);
            this.nSwap.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nSwap.Name = "nSwap";
            this.nSwap.Size = new System.Drawing.Size(79, 20);
            this.nSwap.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Search Map:";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(147, 1);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(19, 20);
            this.button5.TabIndex = 6;
            this.button5.Text = "?";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(147, 27);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(19, 20);
            this.button6.TabIndex = 7;
            this.button6.Text = "?";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // frmChest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(187, 101);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmChest";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chest Editor";
            this.Load += new System.EventHandler(this.frmChest_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPos)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nSwap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown nPos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.NumericUpDown nSwap;
        private System.Windows.Forms.CheckBox chkMap;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
    }
}