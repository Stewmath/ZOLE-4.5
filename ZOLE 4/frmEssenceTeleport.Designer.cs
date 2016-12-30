namespace ZOLE_4
{
    partial class frmEssenceTeleport
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
            this.nGroup = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nMap = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nXY = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nStyle = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nDungeon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nXY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nStyle)).BeginInit();
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
            this.nDungeon.Hexadecimal = true;
            this.nDungeon.Location = new System.Drawing.Point(80, 12);
            this.nDungeon.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nDungeon.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nDungeon.Name = "nDungeon";
            this.nDungeon.Size = new System.Drawing.Size(128, 20);
            this.nDungeon.TabIndex = 1;
            this.nDungeon.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nDungeon.ValueChanged += new System.EventHandler(this.nDungeon_ValueChanged);
            // 
            // nGroup
            // 
            this.nGroup.Hexadecimal = true;
            this.nGroup.Location = new System.Drawing.Point(80, 38);
            this.nGroup.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nGroup.Name = "nGroup";
            this.nGroup.Size = new System.Drawing.Size(128, 20);
            this.nGroup.TabIndex = 3;
            this.nGroup.ValueChanged += new System.EventHandler(this.nGroup_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Map Group:";
            // 
            // nMap
            // 
            this.nMap.Hexadecimal = true;
            this.nMap.Location = new System.Drawing.Point(80, 64);
            this.nMap.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nMap.Name = "nMap";
            this.nMap.Size = new System.Drawing.Size(128, 20);
            this.nMap.TabIndex = 5;
            this.nMap.ValueChanged += new System.EventHandler(this.nMap_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Map:";
            // 
            // nXY
            // 
            this.nXY.Hexadecimal = true;
            this.nXY.Location = new System.Drawing.Point(80, 90);
            this.nXY.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nXY.Name = "nXY";
            this.nXY.Size = new System.Drawing.Size(128, 20);
            this.nXY.TabIndex = 7;
            this.nXY.ValueChanged += new System.EventHandler(this.nXY_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Y/X:";
            // 
            // nStyle
            // 
            this.nStyle.Hexadecimal = true;
            this.nStyle.Location = new System.Drawing.Point(80, 116);
            this.nStyle.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nStyle.Name = "nStyle";
            this.nStyle.Size = new System.Drawing.Size(128, 20);
            this.nStyle.TabIndex = 9;
            this.nStyle.ValueChanged += new System.EventHandler(this.nStyle_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Warp Style:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 142);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(81, 142);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmEssenceTeleport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(220, 177);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.nStyle);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nXY);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nMap);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nGroup);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nDungeon);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmEssenceTeleport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dungeon Essence Teleport Editor";
            this.Load += new System.EventHandler(this.frmEssenceTeleport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nDungeon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nXY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nStyle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nDungeon;
        private System.Windows.Forms.NumericUpDown nGroup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nMap;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nXY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nStyle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}