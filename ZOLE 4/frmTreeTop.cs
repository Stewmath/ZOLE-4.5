using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmTreeTop : Form
    {
        GBHL.GBFile gb;
        int map;
        int group;
        int treeLoc;
        TreeTopLoader tl;

        public frmTreeTop(GBHL.GBFile g, int grp, int mp, TreeTopLoader trl)
        {
            gb = g;
            map = mp;
            group = grp;
            tl = trl;
            InitializeComponent();
            int i = tl.getTreeTopLocation(group, map);
            if (i == -1)
            {
                panel2.Visible = true;
            }
            else
            {
                panel1.Visible = true;
                treeLoc = i;
                gb.BufferLocation = i + 1;
                nMap.Value = (byte)map;
                nTop.Value = gb.ReadByte();
                nX.Value = gb.ReadByte();
                nY.Value = gb.ReadByte();
            }
        }

        private void frmTreeTop_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i = tl.getTreeTopLocation(group, (int)nSearch.Value);
            if (i == -1)
            {
                MessageBox.Show("That map does not have a tree top.", "Error");
                return;
            }
            MessageBox.Show("Map swapped.", "Success");
            treeLoc = i;
            gb.BufferLocation = i;
            gb.WriteByte((byte)map);
            panel1.Visible = true;
            panel2.Visible = false;
            nMap.Value = (byte)map;
            nTop.Value = gb.ReadByte();
            nX.Value = gb.ReadByte();
            nY.Value = gb.ReadByte();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gb.BufferLocation = treeLoc;
            gb.WriteByte((byte)nMap.Value);
            gb.WriteByte((byte)nTop.Value);
            gb.WriteByte((byte)nX.Value);
            gb.WriteByte((byte)nY.Value);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
