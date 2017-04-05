using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmChest : Form
    {
        GBHL.GBFile gb;
        ChestLoader chest;
        int group, map;

        public frmChest(GBHL.GBFile g, int grp, int m, Program.GameTypes game)
        {
            gb = g;
            chest = new ChestLoader(gb, game);
            group = grp;
            map = m;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gb.BufferLocation = chest.chestLocation;
            if (chkMap.Checked)
            {
                gb.WriteByte((byte)map);
                gb.WriteByte((byte)nPos.Value);
            }
            else
            {
                gb.WriteByte((byte)nPos.Value);
                gb.WriteByte((byte)map);
            }
            gb.WriteWord((ushort)nID.Value);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmChest_Load(object sender, EventArgs e)
        {
            panel1.Visible = chest.loadChest(group, map);
            if (panel1.Visible)
            {
                nPos.Value = chest.yx;
                nID.Value = chest.id;
                chkMap.Checked = chest.mapFirst;
            }
            else
            {
                panel2.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (chest.loadChest(group, (int)nSwap.Value))
            {
                if (chest.mapFirst)
                    gb.WriteByte(chest.chestLocation, (byte)map);
                else
                    gb.WriteByte(chest.chestLocation + 1, (byte)map);

                nPos.Value = chest.yx;
                nID.Value = chest.id;

                MessageBox.Show("Map swapped.", "Success");
                panel1.Visible = true;
                panel2.Visible = false;
            }
            else
            {
                MessageBox.Show("Chest data does not exist on that map.", "Error");
            }
        }

        private void nID_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the position where you put the chest on the map.  It is in YX format.",
                "YX Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the Chest ID for the chest.  For a list of usable Chest IDs, see the Resources menu.",
                "ID Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
