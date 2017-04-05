namespace ZOLE_4
{
    partial class frmInteractions
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
            this.nType = new System.Windows.Forms.NumericUpDown();
            this.lblInteractionType = new System.Windows.Forms.Label();
            this.pInteractionColor = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pInteractionColor)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type:";
            // 
            // nType
            // 
            this.nType.Hexadecimal = true;
            this.nType.Location = new System.Drawing.Point(73, 6);
            this.nType.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nType.Name = "nType";
            this.nType.Size = new System.Drawing.Size(45, 20);
            this.nType.TabIndex = 1;
            this.nType.ValueChanged += new System.EventHandler(this.nType_ValueChanged);
            // 
            // lblInteractionType
            // 
            this.lblInteractionType.BackColor = System.Drawing.SystemColors.Control;
            this.lblInteractionType.Location = new System.Drawing.Point(9, 29);
            this.lblInteractionType.Name = "lblInteractionType";
            this.lblInteractionType.Size = new System.Drawing.Size(137, 17);
            this.lblInteractionType.TabIndex = 9;
            this.lblInteractionType.Text = "Type";
            // 
            // pInteractionColor
            // 
            this.pInteractionColor.Location = new System.Drawing.Point(124, 6);
            this.pInteractionColor.Name = "pInteractionColor";
            this.pInteractionColor.Size = new System.Drawing.Size(22, 20);
            this.pInteractionColor.TabIndex = 8;
            this.pInteractionColor.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(85, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblInteractionType);
            this.panel1.Controls.Add(this.nType);
            this.panel1.Controls.Add(this.pInteractionColor);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(153, 47);
            this.panel1.TabIndex = 11;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(38, 53);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(41, 23);
            this.button3.TabIndex = 15;
            this.button3.Text = "Close";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(20, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "?";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // frmInteractions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(153, 83);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmInteractions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Objects";
            this.Load += new System.EventHandler(this.frmInteractions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pInteractionColor)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nType;
        private System.Windows.Forms.Label lblInteractionType;
        private System.Windows.Forms.PictureBox pInteractionColor;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
    }
}