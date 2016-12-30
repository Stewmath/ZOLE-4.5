using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmSecondaryScript : Form
    {
        GBHL.GBFile gb;
        MapScripts m;
        int group;
        int map;
        int sl = -1;

        public frmSecondaryScript(GBHL.GBFile g, MapScripts ms, int grp, int mp)
        {
            gb = g;
            m = ms;
            group = grp;
            map = mp;
            InitializeComponent();
            int i = m.loadSecondaryScriptRef(group, map);
            sl = i;
            if (i == -1)
            {
                panel2.Visible = true;
            }
            else
            {
                panel1.Visible = true;
                nScript.Value = gb.ReadByte(i + 1);
            }
        }

        private void frmSecondaryScript_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int b = m.loadSecondaryScriptRef((group), (byte)nMap.Value);
            if (b == -1)
            {
                MessageBox.Show("That map does not have a secondary script.", "Error");
                return;
            }

            sl = b;
            gb.BufferLocation = b;
            gb.WriteByte((byte)map);
            MessageBox.Show("Map swapped.", "Success");
            panel2.Visible = false;
            panel1.Visible = true;
            nScript.Value = gb.ReadByte();
        }

        private void nScript_ValueChanged(object sender, EventArgs e)
        {
            gb.BufferLocation = sl + 1;
            gb.WriteByte((byte)nScript.Value);
            gb.BufferLocation = 0xBA93 + (byte)nScript.Value * 2;
            nMemory.Value = gb.ReadByte() + (gb.ReadByte() << 8); //gb.BufferLocation = 0x8000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
        }

        private void nMemory_ValueChanged(object sender, EventArgs e)
        {
            gb.BufferLocation = 0xBA93 + (byte)nScript.Value * 2;
            gb.WriteByte((byte)((int)nMemory.Value & 0xFF));
            gb.WriteByte((byte)((int)nMemory.Value >> 8));
            if (nMemory.Value < 0x4000 || nMemory.Value > 0x7FFF)
            {
                lblPointer.Text = "0x" + ((int)nMemory.Value).ToString("X");
            }
            else
            {
                ushort value = (ushort)(nMemory.Value);
                lblPointer.Text = "0x" + ((value & 0xFF) + (((value >> 8) - 0x40) * 0x100) + 0x8000).ToString("X");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
