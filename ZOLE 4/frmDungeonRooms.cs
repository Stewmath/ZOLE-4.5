using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmDungeonRooms : Form
    {
        public byte[] mapOrder;
        byte[] baseMapOrder = new byte[64];
        GBHL.GBFile gb;

        public frmDungeonRooms(GBHL.GBFile g, byte[] m, Image i)
        {
            gb = g;
            mapOrder = m;
            Array.Copy(m, baseMapOrder, m.Length);
            InitializeComponent();
            pMinimap.Image = i;
        }

        private void gridBox1_MouseDown(object sender, MouseEventArgs e)
        {
            nMap.Value = mapOrder[pMinimap.SelectedIndex];
            if (baseMapOrder[pMinimap.SelectedIndex] != (byte)nMap.Value)
                pMinimap.SelectionColor = Color.Blue;
            else
                pMinimap.SelectionColor = Color.Red;
        }

        private void nMap_ValueChanged(object sender, EventArgs e)
        {
            mapOrder[pMinimap.SelectedIndex] = (byte)nMap.Value;
            if (baseMapOrder[pMinimap.SelectedIndex] != (byte)nMap.Value)
                pMinimap.SelectionColor = Color.Blue;
            else
                pMinimap.SelectionColor = Color.Red;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmDungeonRooms_Load(object sender, EventArgs e)
        {
            gridBox1_MouseDown(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 64; i++)
                mapOrder[i] = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This arranges the dungeon maps any way you want them to connect to each other and appear.  Hint:  Never click \"Clear All\".",
                "Arrange Maps Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
