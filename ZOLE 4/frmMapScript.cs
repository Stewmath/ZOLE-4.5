using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmMapScript : Form
    {
        GBHL.GBFile gb;
        MapScripts m;
        int grp;
        byte bm;

        public frmMapScript(GBHL.GBFile g, int map, int group)
        {
            gb = g;
            grp = group;
            bm = (byte)map;
            m = new MapScripts(gb);
            InitializeComponent();
            int b = m.loadScript(map, group);
            if (b == -1)
            {
                panel2.Visible = true;
                return;
            }
            panel1.Visible = true;
            nScript.Value = (byte)b;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void nScript_ValueChanged(object sender, EventArgs e)
        {
            gb.BufferLocation = 0x012437 + (byte)nScript.Value * 2;
            nMemory.Value = gb.ReadByte() + gb.ReadByte() * 0x100;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            gb.BufferLocation = 0x012437 + (byte)nScript.Value * 2;
            gb.WriteByte((byte)((int)nMemory.Value & 0xFF));
            gb.WriteByte((byte)((int)nMemory.Value >> 8));
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int b = m.loadScript((byte)nMap.Value, grp);
            if (b == -1)
            {
                MessageBox.Show("That map does not have a script.", "Error");
                return;
            }

            gb.BufferLocation = m.scriptLocation;
            gb.WriteByte(bm);
            MessageBox.Show("Map swapped.", "Success");
            panel2.Visible = false;
            panel1.Visible = true;
            nScript.Value = (byte)b;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void nMemory_ValueChanged(object sender, EventArgs e)
        {
            if (nMemory.Value < 0x4000 || nMemory.Value > 0x7FFF)
                lblPointer.Text = "0x" + ((int)nMemory.Value).ToString("X");
            else
            {
                ushort value = (ushort)(nMemory.Value);
                lblPointer.Text = "0x" + ((value & 0xFF) + (((value >> 8) - 0x40) * 0x100) + 0x10000).ToString("X");
            }
        }
    }
}
