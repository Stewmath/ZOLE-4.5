using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmWarpProps : Form
    {
        WarpLoader warpLoader;
        public Warp primaryWarp;
        public Warp propWarp;
        int group = 0;
        public int lastAddress = 0;
        GBHL.GBFile gb;
        Program.GameTypes game;

        public frmWarpProps(WarpLoader w, int g, int la, Program.GameTypes gm, GBHL.GBFile gbf)
        {
            game = gm;
            warpLoader = w;
            group = g;
            lastAddress = la;
            gb = gbf;
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

        public void fillWarp(Warp w)
        {
            primaryWarp = w;
            nType.Value = w.opcode;
            nSrcX.Value = w.map;
            nWarp.Value = w.index;
            if (w.index == 0)
                nWarp_ValueChanged(null, null);
            nDestGroup.Value = w.group;
            nEntrance.Value = w.entrance;
        }

        public void setPosWarp(int index)
        {
            Warp w = warpLoader.loadWarpProps(index, primaryWarp.group, game);
            propWarp = w;
            nMap1.Value = w.map;
            nPos1.Value = w.pos;
            nUnknown1.Value = w.unknown;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (gb.ReadByte(lastAddress + 4) == 0xFF)
            {
                MessageBox.Show("Unable to continue. End of warp data for that group.", "Cannot Advance");
                return;
            }
            if ((primaryWarp.opcode & 0x80) != 0)
            {
                if (MessageBox.Show("The next warp will not be read by the current map. Continue anyway?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }
            fillWarp(warpLoader.loadWarp(lastAddress + 4));
            lastAddress += 4;
        }

        private void nWarp_ValueChanged(object sender, EventArgs e)
        {
            setPosWarp((int)nWarp.Value);
        }

        private void frmWarpProps_Load(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the map that Link will go to after warping.",
                "Destination Map Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the position you want Link to appear on when he gets to the map.  The value is entered in YX format.  If you enter FF, Link will enter indoors and move up one tile - this is the common indoor entrance.  However, he will only enter this way if the \"Unknown\" box below is set to 93.",
                "Position Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This could be one of three values.  If you want Link to enter facing upwards, enter 93.  If you want Link to enter facing downwards, enter F3.  If you want Link to fall from the ceiling on entry, enter 0B.",
                "Unknown Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the type of warp.  0 is for stairs, 4 is for caves and doors, and 40 is for multiple warps on one map screen.",
                "Warp Type Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the position that the warp is located on the map.  It is in YX format.",
                "Y/X Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Basically used to identify a specific warp.  Although it's editable, it's not recommended to edit it.",
                "Warp Index", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the map group you want to travel to.  The Present Overworld is 0, Past Overworld is 1, Present Underwater is 2, Past Underwater is 3, Maku Path through Black Tower and the first Unmapped is 4, and Level 6 through Final Dungeon and the second Unmapped is 5.",
                "Map Group Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is just a value that determines what kind of warp it is.  2 is stairs, 3 is indoor exits and entrances, and 4 is outside entrances / exits and everything else.",
                "Entrance Type Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the map that Link will go to after warping.",
                "Destination Map Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the position you want Link to appear on when he gets to the map.  The value is entered in YX format.  If you enter FF, Link will enter indoors and move up one tile - this is the common indoor entrance.  However, he will only enter this way if the \"Unknown\" box below is set to 93.",
                "Position Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This could be one of three values.  If you want Link to enter facing upwards, enter 93.  If you want Link to enter facing downwards, enter F3.  If you want Link to fall from the ceiling on entry, enter 0B.",
                "Unknown Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}