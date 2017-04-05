using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmDungeonPortal : Form
    {
        GBHL.GBFile gb;
        Program.GameTypes game;

        public frmDungeonPortal(GBHL.GBFile g, Program.GameTypes game)
        {
            gb = g;
            this.game = game;
            InitializeComponent();
            nDungeon_ValueChanged(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void nDungeon_ValueChanged(object sender, EventArgs e)
        {
            gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x28507 : 0x2491C) + ((byte)nDungeon.Value * 2);
            nMap1.Value = gb.ReadByte();
            nMap2.Value = gb.ReadByte();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmDungeonPortal_Load(object sender, EventArgs e)
        {

        }
    }
}
