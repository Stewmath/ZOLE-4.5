using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmDungeonImport : Form
    {
        public string filename;

        public frmDungeonImport()
        {
            InitializeComponent();
            this.buttonSelector.Click += new System.EventHandler(this.buttonSelector_Click);
            this.buttonOK.Click += new System.EventHandler(this.buttonResult_Click);
            this.buttonOK.DialogResult = DialogResult.OK;
            this.buttonCancel.Click += new System.EventHandler(this.buttonResult_Click);
            this.buttonCancel.DialogResult = DialogResult.Cancel;
        }

        private void buttonSelector_Click(object sender, EventArgs e)
        {
            OpenFileDialog s = new OpenFileDialog();
            s.Title = "Import Dungeon Data";
            s.Filter = "Dungeon Data File (*.ZDD)|*.zdd";
            if (s.ShowDialog() != DialogResult.OK)
                return;
            filename = s.FileName;
        }

        private void buttonResult_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
