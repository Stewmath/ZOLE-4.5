using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmAreaFlags : Form
    {
        GBHL.GBFile gb;
        Program.GameTypes game;
        AreaLoader areaLoader;
        public frmAreaFlags(GBHL.GBFile g, Program.GameTypes gm, AreaLoader a, int area)
        {
            InitializeComponent();
            gb = g;
            game = gm;
            areaLoader = a;
            nArea.Value = area;
            nArea_ValueChanged(null, null);
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

        private void nArea_ValueChanged(object sender, EventArgs e)
        {
            int indexBase = 0x10000 + (game == Program.GameTypes.Ages ? 0x0F9C : 0x0C84);
            if (gb.ReadByte(indexBase) == 0xFF)
            {
                indexBase = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            }
            AreaLoader.Area a = areaLoader.loadArea(indexBase + (int)nArea.Value * 8, (int)nArea.Value, game);
            nDungeon.Value = (byte)(a.flags1 & 0xF);
            int flag = a.flags2;
            checkBox1.Checked = (flag & 1) != 0;
            checkBox2.Checked = (flag & 2) != 0;
            checkBox3.Checked = (flag & 4) != 0;
            checkBox4.Checked = (flag & 8) != 0;
            checkBox5.Checked = (flag & 16) != 0;
            checkBox7.Checked = (flag & 32) != 0;
            checkBox8.Checked = (flag & 64) != 0;
            checkBox9.Checked = (flag & 128) != 0;
        }
    }
}
