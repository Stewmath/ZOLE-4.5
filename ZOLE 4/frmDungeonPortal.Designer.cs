namespace ZOLE_4
{
    partial class frmDungeonPortal
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
            this.nDungeon = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nMap1 = new System.Windows.Forms.NumericUpDown();
            this.nMap2 = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nDungeon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMap1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMap2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dungeon:";
            // 
            // nDungeon
            // 
            this.nDungeon.Location = new System.Drawing.Point(89, 12);
            this.nDungeon.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nDungeon.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nDungeon.Name = "nDungeon";
            this.nDungeon.Size = new System.Drawing.Size(93, 20);
            this.nDungeon.TabIndex = 1;
            this.nDungeon.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nDungeon.ValueChanged += new System.EventHandler(this.nDungeon_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Linked Maps:";
            // 
            // nMap1
            // 
            this.nMap1.Hexadecimal = true;
            this.nMap1.Location = new System.Drawing.Point(89, 38);
            this.nMap1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nMap1.Name = "nMap1";
            this.nMap1.Size = new System.Drawing.Size(43, 20);
            this.nMap1.TabIndex = 3;
            // 
            // nMap2
            // 
            this.nMap2.Hexadecimal = true;
            this.nMap2.Location = new System.Drawing.Point(139, 38);
            this.nMap2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nMap2.Name = "nMap2";
            this.nMap2.Size = new System.Drawing.Size(43, 20);
            this.nMap2.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(105, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 69);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmDungeonPortal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 104);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.nMap2);
            this.Controls.Add(this.nMap1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nDungeon);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmDungeonPortal";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Dungeon Portals";
            this.Load += new System.EventHandler(this.frmDungeonPortal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nDungeon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMap1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMap2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.NumericUpDown nDungeon;
        public System.Windows.Forms.NumericUpDown nMap1;
        public System.Windows.Forms.NumericUpDown nMap2;
    }
}