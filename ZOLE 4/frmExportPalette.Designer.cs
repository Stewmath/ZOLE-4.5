namespace ZOLE_4
{
    partial class frmExportPalette
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
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbRows = new System.Windows.Forms.RadioButton();
            this.nRow1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nRow2 = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nRow1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nRow2)).BeginInit();
            this.SuspendLayout();
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(12, 12);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(101, 17);
            this.rbAll.TabIndex = 0;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "Export All Colors";
            this.rbAll.UseVisualStyleBackColor = true;
            // 
            // rbRows
            // 
            this.rbRows.AutoSize = true;
            this.rbRows.Location = new System.Drawing.Point(12, 41);
            this.rbRows.Name = "rbRows";
            this.rbRows.Size = new System.Drawing.Size(85, 17);
            this.rbRows.TabIndex = 1;
            this.rbRows.Text = "Export Rows";
            this.rbRows.UseVisualStyleBackColor = true;
            this.rbRows.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // nRow1
            // 
            this.nRow1.Enabled = false;
            this.nRow1.Location = new System.Drawing.Point(130, 41);
            this.nRow1.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nRow1.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nRow1.Name = "nRow1";
            this.nRow1.Size = new System.Drawing.Size(46, 20);
            this.nRow1.TabIndex = 2;
            this.nRow1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nRow1.ValueChanged += new System.EventHandler(this.nRow1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(182, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "To";
            // 
            // nRow2
            // 
            this.nRow2.Enabled = false;
            this.nRow2.Location = new System.Drawing.Point(208, 41);
            this.nRow2.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nRow2.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nRow2.Name = "nRow2";
            this.nRow2.Size = new System.Drawing.Size(46, 20);
            this.nRow2.TabIndex = 4;
            this.nRow2.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nRow2.ValueChanged += new System.EventHandler(this.nRow2_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 78);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(130, 78);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmExportPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 113);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.nRow2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nRow1);
            this.Controls.Add(this.rbRows);
            this.Controls.Add(this.rbAll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmExportPalette";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Export Palette";
            ((System.ComponentModel.ISupportInitialize)(this.nRow1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nRow2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.RadioButton rbAll;
        public System.Windows.Forms.RadioButton rbRows;
        public System.Windows.Forms.NumericUpDown nRow1;
        public System.Windows.Forms.NumericUpDown nRow2;
    }
}