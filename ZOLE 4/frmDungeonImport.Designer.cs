namespace ZOLE_4
{
    partial class frmDungeonImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDungeonImport));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSelector = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rbLayout = new System.Windows.Forms.CheckBox();
            this.rbRooms = new System.Windows.Forms.CheckBox();
            this.rbIDs = new System.Windows.Forms.CheckBox();
            this.rbMusic = new System.Windows.Forms.CheckBox();
            this.rbOverride = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(141, 226);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(108, 23);
            this.buttonOK.TabIndex = 7;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(12, 226);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(108, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSelector
            // 
            this.buttonSelector.Location = new System.Drawing.Point(12, 82);
            this.buttonSelector.Name = "buttonSelector";
            this.buttonSelector.Size = new System.Drawing.Size(237, 23);
            this.buttonSelector.TabIndex = 14;
            this.buttonSelector.Text = "Select Dungeon File";
            this.buttonSelector.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 70);
            this.label1.TabIndex = 15;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // rbLayout
            // 
            this.rbLayout.AutoSize = true;
            this.rbLayout.Checked = true;
            this.rbLayout.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rbLayout.Location = new System.Drawing.Point(12, 111);
            this.rbLayout.Name = "rbLayout";
            this.rbLayout.Size = new System.Drawing.Size(137, 17);
            this.rbLayout.TabIndex = 22;
            this.rbLayout.Text = "Import Dungeon Layout";
            this.rbLayout.UseVisualStyleBackColor = true;
            // 
            // rbRooms
            // 
            this.rbRooms.AutoSize = true;
            this.rbRooms.Checked = true;
            this.rbRooms.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rbRooms.Location = new System.Drawing.Point(12, 134);
            this.rbRooms.Name = "rbRooms";
            this.rbRooms.Size = new System.Drawing.Size(112, 17);
            this.rbRooms.TabIndex = 23;
            this.rbRooms.Text = "Import Room Data";
            this.rbRooms.UseVisualStyleBackColor = true;
            // 
            // rbIDs
            // 
            this.rbIDs.AutoSize = true;
            this.rbIDs.Location = new System.Drawing.Point(12, 157);
            this.rbIDs.Name = "rbIDs";
            this.rbIDs.Size = new System.Drawing.Size(99, 17);
            this.rbIDs.TabIndex = 24;
            this.rbIDs.Text = "Import Area IDs";
            this.rbIDs.UseVisualStyleBackColor = true;
            // 
            // rbMusic
            // 
            this.rbMusic.AutoSize = true;
            this.rbMusic.Location = new System.Drawing.Point(12, 180);
            this.rbMusic.Name = "rbMusic";
            this.rbMusic.Size = new System.Drawing.Size(86, 17);
            this.rbMusic.TabIndex = 25;
            this.rbMusic.Text = "Import Music";
            this.rbMusic.UseVisualStyleBackColor = true;
            // 
            // rbOverride
            // 
            this.rbOverride.AutoSize = true;
            this.rbOverride.Location = new System.Drawing.Point(12, 203);
            this.rbOverride.Name = "rbOverride";
            this.rbOverride.Size = new System.Drawing.Size(200, 17);
            this.rbOverride.TabIndex = 26;
            this.rbOverride.Text = "Override Dungeon Mismatch Failsafe";
            this.rbOverride.UseVisualStyleBackColor = true;
            // 
            // frmDungeonImport
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(261, 261);
            this.Controls.Add(this.rbOverride);
            this.Controls.Add(this.rbMusic);
            this.Controls.Add(this.rbIDs);
            this.Controls.Add(this.rbRooms);
            this.Controls.Add(this.rbLayout);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSelector);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDungeonImport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dungeon Importer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSelector;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox rbLayout;
        public System.Windows.Forms.CheckBox rbRooms;
        public System.Windows.Forms.CheckBox rbIDs;
        public System.Windows.Forms.CheckBox rbMusic;
        public System.Windows.Forms.CheckBox rbOverride;
    }
}