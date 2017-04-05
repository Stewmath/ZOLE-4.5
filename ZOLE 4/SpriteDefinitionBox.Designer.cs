namespace ZOLE_4
{
    partial class SpriteDefinitionBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.nID = new System.Windows.Forms.NumericUpDown();
            this.nValue1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nValue2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nValue3 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nValue4 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nValue1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nValue2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nValue3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nValue4)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // nID
            // 
            this.nID.Hexadecimal = true;
            this.nID.Location = new System.Drawing.Point(67, 10);
            this.nID.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nID.Name = "nID";
            this.nID.Size = new System.Drawing.Size(103, 20);
            this.nID.TabIndex = 1;
            this.nID.ValueChanged += new System.EventHandler(this.nID_ValueChanged);
            // 
            // nValue1
            // 
            this.nValue1.Hexadecimal = true;
            this.nValue1.Location = new System.Drawing.Point(67, 36);
            this.nValue1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nValue1.Name = "nValue1";
            this.nValue1.Size = new System.Drawing.Size(103, 20);
            this.nValue1.TabIndex = 3;
            this.nValue1.ValueChanged += new System.EventHandler(this.nID_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Value1:";
            // 
            // nValue2
            // 
            this.nValue2.Hexadecimal = true;
            this.nValue2.Location = new System.Drawing.Point(67, 62);
            this.nValue2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nValue2.Name = "nValue2";
            this.nValue2.Size = new System.Drawing.Size(103, 20);
            this.nValue2.TabIndex = 5;
            this.nValue2.ValueChanged += new System.EventHandler(this.nID_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Value2:";
            // 
            // nValue3
            // 
            this.nValue3.Hexadecimal = true;
            this.nValue3.Location = new System.Drawing.Point(67, 88);
            this.nValue3.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nValue3.Name = "nValue3";
            this.nValue3.Size = new System.Drawing.Size(103, 20);
            this.nValue3.TabIndex = 7;
            this.nValue3.ValueChanged += new System.EventHandler(this.nID_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Value3:";
            // 
            // nValue4
            // 
            this.nValue4.Hexadecimal = true;
            this.nValue4.Location = new System.Drawing.Point(67, 114);
            this.nValue4.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nValue4.Name = "nValue4";
            this.nValue4.Size = new System.Drawing.Size(103, 20);
            this.nValue4.TabIndex = 9;
            this.nValue4.ValueChanged += new System.EventHandler(this.nID_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Value4:";
            // 
            // SpriteDefinitionBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nValue4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nValue3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nValue2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nValue1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nID);
            this.Controls.Add(this.label1);
            this.Name = "SpriteDefinitionBox";
            this.Size = new System.Drawing.Size(176, 138);
            this.Load += new System.EventHandler(this.SpriteDefinitionBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nValue1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nValue2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nValue3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nValue4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nID;
        private System.Windows.Forms.NumericUpDown nValue1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nValue2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nValue3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nValue4;
        private System.Windows.Forms.Label label5;
    }
}
