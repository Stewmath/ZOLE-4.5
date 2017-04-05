using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmGaleSeed : Form
    {
        GBHL.GBFile gb;
        Image present;
        Image past;
        Program.GameTypes game;

        public frmGaleSeed(GBHL.GBFile g, Image pres, Image pas, Program.GameTypes game)
        {
            gb = g;
            InitializeComponent();
            present = pres;
            past = pas;
            this.game = game;
            if (game == Program.GameTypes.Seasons)
                rbPast.Visible = rbPresent.Visible = false;
            nIndex_ValueChanged(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nIndex_ValueChanged(object sender, EventArgs e)
        {
            if (rbPresent.Checked || game == Program.GameTypes.Seasons)
                gb.BufferLocation = (game == Program.GameTypes.Ages ? 0xAD03 : 0xAC4F) + (byte)nIndex.Value * 3;
            else
                gb.BufferLocation = 0xAD1E + (byte)nIndex.Value * 3;
            nMap.Value = gb.ReadByte();
            nPos.Value = gb.ReadByte();
            nUnknown.Value = gb.ReadByte();
        }

        private void rbPresent_CheckedChanged(object sender, EventArgs e)
        {
            nIndex_ValueChanged(null, null);
            pMap.Invalidate();
        }

        private void rbPast_CheckedChanged(object sender, EventArgs e)
        {
            nIndex_ValueChanged(null, null);
            pMap.Invalidate();
        }

        private void pMap_Paint(object sender, PaintEventArgs e)
        {
            if (rbPresent.Checked || game == Program.GameTypes.Seasons)
                gb.BufferLocation = (game == Program.GameTypes.Ages ? 0xAD03 : 0xAC4F);
            else
                gb.BufferLocation = 0xAD1E;

            for (int i = 0; i < 9; i++)
            {
                int yx = gb.ReadByte();
                gb.BufferLocation += 2;
                if (yx == 0)
                {
                    nIndex.Maximum = i - 1;
                    break;
                }
                int y = (yx >> 4) * 8;
                int x = (yx & 0xF) * 8;
                e.Graphics.DrawRectangle((nIndex.Value == i ? Pens.Red : Pens.White), x, y, 8, 8);
                e.Graphics.FillRectangle(Brushes.Black, x + 1, y + 1, 7, 7);
            }
        }

        private void frmGaleSeed_Load(object sender, EventArgs e)
        {

        }

        private void nMap_ValueChanged(object sender, EventArgs e)
        {
            if (rbPresent.Checked || game == Program.GameTypes.Seasons)
                gb.BufferLocation = (game == Program.GameTypes.Ages ? 0xAD03 : 0xAC4F) + (int)nIndex.Value * 3;
            else
                gb.BufferLocation = 0xAD1E + (byte)nIndex.Value * 3;
            gb.WriteByte((byte)nMap.Value);
            pMap.Invalidate();
        }

        private void nPos_ValueChanged(object sender, EventArgs e)
        {
            if (rbPresent.Checked || game == Program.GameTypes.Seasons)
                gb.BufferLocation = (game == Program.GameTypes.Ages ? 0xAD03 : 0xAC4F) + (int)nIndex.Value * 3;
            else
                gb.BufferLocation = 0xAD1E + (byte)nIndex.Value * 3;
            gb.BufferLocation++;
            gb.WriteByte((byte)nPos.Value);
        }

        private void nUnknown_ValueChanged(object sender, EventArgs e)
        {
            if (rbPresent.Checked || game == Program.GameTypes.Seasons)
                gb.BufferLocation = (game == Program.GameTypes.Ages ? 0xAD03 : 0xAC4F) + (int)nIndex.Value * 3;
            else
                gb.BufferLocation = 0xAD1E + (byte)nIndex.Value * 3;
            gb.BufferLocation += 2;
            gb.WriteByte((byte)nUnknown.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Just an indicator of what warp spot you're editing.", "Index Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the map that the warp is on.", "Map Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the position of Link on the destination map.  It is in YX format.",
                "YX Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Don't chnge or worry about this value.", "Unknown Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
