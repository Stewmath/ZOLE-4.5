namespace ZOLE_4
{
    partial class frmEnemyEditor
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
            this.label2 = new System.Windows.Forms.Label();
            this.nMainID = new System.Windows.Forms.NumericUpDown();
            this.nSubID = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pnlItems = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.lblAIAddress = new System.Windows.Forms.Label();
            this.nAIAddress = new System.Windows.Forms.NumericUpDown();
            this.lblAIBank = new System.Windows.Forms.Label();
            this.nHealth = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nDamage = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.nCollisionHeight = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.nCollisionWidth = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.nPalette = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nFace = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nVulnerability = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nSprite = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nMainID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nSubID)).BeginInit();
            this.pnlItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nAIAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nHealth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nDamage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nCollisionHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nCollisionWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPalette)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nFace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nVulnerability)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nSprite)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Main ID:";
            // 
            // nMainID
            // 
            this.nMainID.Hexadecimal = true;
            this.nMainID.Location = new System.Drawing.Point(65, 12);
            this.nMainID.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nMainID.Name = "nMainID";
            this.nMainID.Size = new System.Drawing.Size(104, 20);
            this.nMainID.TabIndex = 2;
            // 
            // nSubID
            // 
            this.nSubID.Hexadecimal = true;
            this.nSubID.Location = new System.Drawing.Point(224, 12);
            this.nSubID.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nSubID.Name = "nSubID";
            this.nSubID.Size = new System.Drawing.Size(112, 20);
            this.nSubID.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(175, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Sub ID:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(295, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Load";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pnlItems
            // 
            this.pnlItems.Controls.Add(this.button2);
            this.pnlItems.Controls.Add(this.lblAIAddress);
            this.pnlItems.Controls.Add(this.nAIAddress);
            this.pnlItems.Controls.Add(this.lblAIBank);
            this.pnlItems.Controls.Add(this.nHealth);
            this.pnlItems.Controls.Add(this.label8);
            this.pnlItems.Controls.Add(this.nDamage);
            this.pnlItems.Controls.Add(this.label9);
            this.pnlItems.Controls.Add(this.nCollisionHeight);
            this.pnlItems.Controls.Add(this.label10);
            this.pnlItems.Controls.Add(this.nCollisionWidth);
            this.pnlItems.Controls.Add(this.label11);
            this.pnlItems.Controls.Add(this.nPalette);
            this.pnlItems.Controls.Add(this.label7);
            this.pnlItems.Controls.Add(this.nFace);
            this.pnlItems.Controls.Add(this.label6);
            this.pnlItems.Controls.Add(this.nVulnerability);
            this.pnlItems.Controls.Add(this.label5);
            this.pnlItems.Controls.Add(this.nSprite);
            this.pnlItems.Controls.Add(this.label4);
            this.pnlItems.Location = new System.Drawing.Point(12, 67);
            this.pnlItems.Name = "pnlItems";
            this.pnlItems.Size = new System.Drawing.Size(324, 162);
            this.pnlItems.TabIndex = 6;
            this.pnlItems.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 135);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(316, 23);
            this.button2.TabIndex = 21;
            this.button2.Text = "Save Enemy";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblAIAddress
            // 
            this.lblAIAddress.AutoSize = true;
            this.lblAIAddress.Location = new System.Drawing.Point(163, 111);
            this.lblAIAddress.Name = "lblAIAddress";
            this.lblAIAddress.Size = new System.Drawing.Size(24, 13);
            this.lblAIAddress.TabIndex = 20;
            this.lblAIAddress.Text = "0x0";
            // 
            // nAIAddress
            // 
            this.nAIAddress.Hexadecimal = true;
            this.nAIAddress.Location = new System.Drawing.Point(251, 109);
            this.nAIAddress.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.nAIAddress.Name = "nAIAddress";
            this.nAIAddress.Size = new System.Drawing.Size(65, 20);
            this.nAIAddress.TabIndex = 19;
            this.nAIAddress.ValueChanged += new System.EventHandler(this.nAIAddress_ValueChanged);
            // 
            // lblAIBank
            // 
            this.lblAIBank.AutoSize = true;
            this.lblAIBank.Location = new System.Drawing.Point(3, 111);
            this.lblAIBank.Name = "lblAIBank";
            this.lblAIBank.Size = new System.Drawing.Size(144, 13);
            this.lblAIBank.TabIndex = 18;
            this.lblAIBank.Text = "AI Memory Address (Bank 0):";
            // 
            // nHealth
            // 
            this.nHealth.Hexadecimal = true;
            this.nHealth.Location = new System.Drawing.Point(251, 81);
            this.nHealth.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nHealth.Name = "nHealth";
            this.nHealth.Size = new System.Drawing.Size(65, 20);
            this.nHealth.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(163, 83);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Health:";
            // 
            // nDamage
            // 
            this.nDamage.Hexadecimal = true;
            this.nDamage.Location = new System.Drawing.Point(251, 55);
            this.nDamage.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nDamage.Name = "nDamage";
            this.nDamage.Size = new System.Drawing.Size(65, 20);
            this.nDamage.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(163, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Damage Dealt:";
            // 
            // nCollisionHeight
            // 
            this.nCollisionHeight.Hexadecimal = true;
            this.nCollisionHeight.Location = new System.Drawing.Point(251, 29);
            this.nCollisionHeight.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nCollisionHeight.Name = "nCollisionHeight";
            this.nCollisionHeight.Size = new System.Drawing.Size(65, 20);
            this.nCollisionHeight.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(163, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Collision Height:";
            // 
            // nCollisionWidth
            // 
            this.nCollisionWidth.Hexadecimal = true;
            this.nCollisionWidth.Location = new System.Drawing.Point(251, 3);
            this.nCollisionWidth.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nCollisionWidth.Name = "nCollisionWidth";
            this.nCollisionWidth.Size = new System.Drawing.Size(65, 20);
            this.nCollisionWidth.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(163, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Collision Width:";
            // 
            // nPalette
            // 
            this.nPalette.Hexadecimal = true;
            this.nPalette.Location = new System.Drawing.Point(75, 81);
            this.nPalette.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nPalette.Name = "nPalette";
            this.nPalette.Size = new System.Drawing.Size(82, 20);
            this.nPalette.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Palette:";
            // 
            // nFace
            // 
            this.nFace.Hexadecimal = true;
            this.nFace.Location = new System.Drawing.Point(75, 55);
            this.nFace.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nFace.Name = "nFace";
            this.nFace.Size = new System.Drawing.Size(82, 20);
            this.nFace.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Face:";
            // 
            // nVulnerability
            // 
            this.nVulnerability.Hexadecimal = true;
            this.nVulnerability.Location = new System.Drawing.Point(75, 29);
            this.nVulnerability.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nVulnerability.Name = "nVulnerability";
            this.nVulnerability.Size = new System.Drawing.Size(82, 20);
            this.nVulnerability.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Vulnerability:";
            // 
            // nSprite
            // 
            this.nSprite.Hexadecimal = true;
            this.nSprite.Location = new System.Drawing.Point(75, 3);
            this.nSprite.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nSprite.Name = "nSprite";
            this.nSprite.Size = new System.Drawing.Size(82, 20);
            this.nSprite.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Sprite:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 235);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(316, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Close";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(313, 38);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(23, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "?";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // frmEnemyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 267);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.pnlItems);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.nSubID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nMainID);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmEnemyEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enemy Editor";
            ((System.ComponentModel.ISupportInitialize)(this.nMainID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nSubID)).EndInit();
            this.pnlItems.ResumeLayout(false);
            this.pnlItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nAIAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nHealth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nDamage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nCollisionHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nCollisionWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPalette)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nFace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nVulnerability)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nSprite)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nMainID;
        private System.Windows.Forms.NumericUpDown nSubID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel pnlItems;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nSprite;
        private System.Windows.Forms.NumericUpDown nVulnerability;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nPalette;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nFace;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nHealth;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nDamage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nCollisionHeight;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nCollisionWidth;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblAIAddress;
        private System.Windows.Forms.NumericUpDown nAIAddress;
        private System.Windows.Forms.Label lblAIBank;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}