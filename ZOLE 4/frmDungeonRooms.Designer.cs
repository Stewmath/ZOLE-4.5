namespace ZOLE_4
{
    partial class frmDungeonRooms
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
            this.nMap = new System.Windows.Forms.NumericUpDown();
            this.pMinimap = new ZOLE_4.GridBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMinimap)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Map:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 224);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(244, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // nMap
            // 
            this.nMap.Hexadecimal = true;
            this.nMap.Location = new System.Drawing.Point(49, 198);
            this.nMap.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nMap.Name = "nMap";
            this.nMap.Size = new System.Drawing.Size(109, 20);
            this.nMap.TabIndex = 3;
            this.nMap.ValueChanged += new System.EventHandler(this.nMap_ValueChanged);
            // 
            // pMinimap
            // 
            this.pMinimap.AllowMultiSelection = false;
            this.pMinimap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pMinimap.BoxSize = new System.Drawing.Size(30, 22);
            this.pMinimap.CanvasSize = new System.Drawing.Size(240, 176);
            this.pMinimap.HoverBox = true;
            this.pMinimap.HoverColor = System.Drawing.Color.White;
            this.pMinimap.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.pMinimap.Location = new System.Drawing.Point(12, 12);
            this.pMinimap.Name = "pMinimap";
            this.pMinimap.Selectable = true;
            this.pMinimap.SelectedIndex = 0;
            this.pMinimap.SelectionColor = System.Drawing.Color.Red;
            this.pMinimap.SelectionRectangle = new System.Drawing.Rectangle(0, 0, 1, 1);
            this.pMinimap.Size = new System.Drawing.Size(244, 180);
            this.pMinimap.TabIndex = 18;
            this.pMinimap.TabStop = false;
            this.pMinimap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridBox1_MouseDown);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(164, 198);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 20);
            this.button2.TabIndex = 19;
            this.button2.Text = "Clear All";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(235, 198);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(20, 20);
            this.button3.TabIndex = 20;
            this.button3.Text = "?";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // frmDungeonRooms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 257);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.nMap);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pMinimap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmDungeonRooms";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Arrange Rooms";
            this.Load += new System.EventHandler(this.frmDungeonRooms_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMinimap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown nMap;
        private GridBox pMinimap;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;


    }
}