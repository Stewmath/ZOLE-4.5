using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmExportPalette : Form
    {
        public frmExportPalette()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRows.Checked)
                nRow1.Enabled = nRow2.Enabled = true;
            else
                nRow1.Enabled = nRow2.Enabled = false;
        }

        private void nRow1_ValueChanged(object sender, EventArgs e)
        {
            nRow2.Minimum = nRow1.Value;
        }

        private void nRow2_ValueChanged(object sender, EventArgs e)
        {
            nRow1.Maximum = nRow2.Value;
        }
    }
}
