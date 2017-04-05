using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GBHL;

namespace ZOLE_4
{
    public partial class frmDungeonMinimapEditor : Form
    {
        GBFile gb;
        byte[] minimapData;
        int group;
        byte activeFlag = 0;
        int location;

        public frmDungeonMinimapEditor(GBFile g, Image image, byte[] minimapData, int group)
        {
            InitializeComponent();
            gb = g;
            pMinimap.Image = image;
            this.minimapData = minimapData;
            this.group = group - 4;
            pMinimap.SelectedIndex = 0;
            pMinimap_MouseDown(null, null);
        }

        private void pMinimap_MouseDown(object sender, MouseEventArgs e)
        {
            gb.BufferLocation = 0x4DCE + (group * 0x100) + minimapData[pMinimap.SelectedIndex];
            location = gb.BufferLocation;
            nMap.Value = minimapData[pMinimap.SelectedIndex];
            byte b = gb.ReadByte();
            activeFlag = b;
            groupBox1.Text = "Flags - " + activeFlag.ToString("X");
            checkBox1.Checked = andMask(b, 1);
            checkBox2.Checked = andMask(b, 2);
            checkBox3.Checked = andMask(b, 4);
            checkBox4.Checked = andMask(b, 8);
            checkBox8.Checked = andMask(b, 0x10);
            checkBox5.Checked = andMask(b, 0x20);
            checkBox6.Checked = andMask(b, 0x40);
            checkBox7.Checked = andMask(b, 0x80);
        }

        private bool andMask(byte value, byte mask)
        {
            return ((value & mask) != 0);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
                activeFlag &= (0xFF ^ 1);
            else
                activeFlag |= 1;
            groupBox1.Text = "Flags - " + activeFlag.ToString("X");
            gb.WriteByte(location, activeFlag);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox2.Checked)
                activeFlag &= (0xFF ^ 2);
            else
                activeFlag |= 2;
            groupBox1.Text = "Flags - " + activeFlag.ToString("X");
            gb.WriteByte(location, activeFlag);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox3.Checked)
                activeFlag &= (0xFF ^ 4);
            else
                activeFlag |= 4;
            groupBox1.Text = "Flags - " + activeFlag.ToString("X");
            gb.WriteByte(location, activeFlag);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox4.Checked)
                activeFlag &= (0xFF ^ 8);
            else
                activeFlag |= 8;
            groupBox1.Text = "Flags - " + activeFlag.ToString("X");
            gb.WriteByte(location, activeFlag);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox5.Checked)
                activeFlag &= (0xFF ^ 0x20);
            else
                activeFlag |= 0x20;
            groupBox1.Text = "Flags - " + activeFlag.ToString("X");
            gb.WriteByte(location, activeFlag);
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox6.Checked)
                activeFlag &= (0xFF ^ 0x40);
            else
                activeFlag |= 0x40;
            groupBox1.Text = "Flags - " + activeFlag.ToString("X");
            gb.WriteByte(location, activeFlag);
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox7.Checked)
                activeFlag &= (0xFF ^ 0x80);
            else
                activeFlag |= 0x80;
            groupBox1.Text = "Flags - " + activeFlag.ToString("X");
            gb.WriteByte(location, activeFlag);
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox8.Checked)
                activeFlag &= (0xFF ^ 0x10);
            else
                activeFlag |= 0x10;
            groupBox1.Text = "Flags - " + activeFlag.ToString("X");
            gb.WriteByte(location, activeFlag);
        }

        private void nMap_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click on a map to view it.  You can then set flags on it, which are basically what shows up on the dungeon map in the game, such as room connections, chests (using the compass), and bosses (using the compass).  Check off all the options that apply.  Checking Chest and Boss at the same time makes for a hidden room.",
                "Dungeon Minimap Editing Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
