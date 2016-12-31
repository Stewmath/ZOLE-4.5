using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmWarp : Form
    {
        string[] agesGroupNames = new string[] { "Present Overworld", "Past Overworld", "Present Underwater", "Past Underwater", "Present Maku Path", "Past Maku Path", "Level 1 1F", "Level 2 B1", "Level 2 1F", "Level 3 B1", "Level 3 1F", "Level 4 B1", "Level 4 1F", "Level 5 B1", "Level 5 1F", "Hero's Cave B1", "Hero's Cave 1F", "Black Tower", "Level 6 Present 1F", "Level 6 Past B1", "Level 6 Past 1F", "Level 7 1F", "Level 7 2F", "Level 7 3F", "Level 8 B3", "Level 8 B2", "Level 8 B1", "Level 8 1F", "Indoor Big", "Final Dungeon", "Unmapped", "Unmapped" };
        string[] seasonsGroupNames = new string[] { "Overworld Spring", "Overworld Summer", "Overworld Fall", "Overworld Winter", "Subrosia", "Hero's Cave", "Level 1 1F", "Level 2 1F", "Level 3 1F", "Level 3 2F", "Level 4 B2", "Level 4 B1", "Level 4 1F", "Level 5 1F", "Level 6 1F", "Level 6 2F", "Level 6 3F", "Level 6 4F", "Level 6 5F", "Hero's Cave", "Level 7 B2", "Level 7 B1", "Level 7 1F", "Level 8 B1", "Level 8 1F", "Final Dungeon", "Ganon's Dungeon", "Unmapped", "Unmapped" };
        Program.GameTypes game;
        GBHL.GBFile gb;
        WarpLoader warpLoader;
        MapLoader mapLoader;
        int lastAddress = -1;
        Warp loadedWarp = null;
        byte flag2;
        bool sideMap;
        frmWarpProps frmW = null;

        public frmWarp(Program.GameTypes g, GBHL.GBFile gbf, byte f)
        {
            InitializeComponent();
            game = g;
            if (g == Program.GameTypes.Ages)
                foreach (string s in agesGroupNames)
                    cboArea.Items.Add(s);
            else
                foreach (string s in seasonsGroupNames)
                    cboArea.Items.Add(s);

            cboArea.SelectedIndex = 0;
            flag2 = f;
            gb = gbf;
            warpLoader = new WarpLoader(gb);
            mapLoader = new MapLoader(gb);
        }

        public int getRealMapGroup(int groupIndex)
        {
            if (game == Program.GameTypes.Seasons)
            {
                if (groupIndex < 4)
                    return 0;
                if (groupIndex == 4)
                    return 1;
                if (groupIndex < 19)
                    return 4;
                if (groupIndex < 27)
                    return 5;
                if (groupIndex == 27)
                    return 4;
                return 5;
            }

            if (groupIndex < 4)
                return groupIndex;
            groupIndex -= 4;
            if (groupIndex >= 26)
                return 4 + (groupIndex & 1);
            if (groupIndex >= 14)
                return 5;
            else
                return 4;
        }

        private void frmWarp_Load(object sender, EventArgs e)
        {

        }

        private bool isSidemap(MapLoader.Room r)
        {
            int b = r.area.flags2;
            if ((b & 0x20) != 0)
                return true;
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool dungeon = new MinimapCreator(gb).dungeon(cboArea.SelectedIndex, game);
            mapLoader.loadMap((int)nMap.Value, new MinimapCreator(gb).getRealMapGroup(cboArea.SelectedIndex, game), 0, true, game);
            bool sideMap = isSidemap(mapLoader.room);
            this.sideMap = sideMap;
            if (!sideMap)
            {
                groupBox3.Visible = false;
                int address = warpLoader.getWarpHeaderAddress(getRealMapGroup(cboArea.SelectedIndex), (int)nMap.Value, new MinimapCreator(gb).dungeon(cboArea.SelectedIndex, game), flag2, game);
                lastAddress = address;
                if (address == -1)
                {
                    groupBox1.Visible = false;
                    groupBox2.Visible = false;
                    button5.Enabled = false;
                    if (!dungeon)
                        MessageBox.Show("No warp for that map found. To add one, find\nan existing warp on the same group\nand change the index.", "No Warp Found");
                    else
                        MessageBox.Show("No warp for that map found. Either find an\nexisting warp and replace it or have the warp\nlead directly to the underlying or above room.", "No Warp Found");
                    return;
                }

                Warp w = warpLoader.loadWarp(address);
                w.srcGroup = (byte)cboArea.SelectedIndex;
                button5.Enabled = true;
                loadedWarp = w;

                if ((w.opcode & 0x40) != 0)
                {
                    groupBox2.Text = "Warp Header - 0x" + address.ToString("X");
                    groupBox1.Visible = false;
                    groupBox2.Visible = true;
                    nFType.Value = w.opcode;
                    nFMap.Value = w.map;
                    nPointer.Value = 0x10000 + w.fpointer;
                }
                else
                {
                    groupBox1.Text = "Warp Header - 0x" + address.ToString("X");
                    groupBox2.Visible = false;
                    groupBox1.Visible = true;
                    nType.Value = w.opcode;
                    nSrcMap.Value = w.map;
                    nWarp.Value = w.index;
                    nDestGroup.Value = w.group;
                    nEntrance.Value = w.entrance;
                }
            }
            else
            {
                int address = warpLoader.getSideWarpHeaderAddress((new MinimapCreator(gb).getRealMapGroup(cboArea.SelectedIndex, game) == 4 ? 6 : 7), (int)nMap.Value, game);
                lastAddress = address;
                groupBox1.Visible = false;
                groupBox2.Visible = false;
                if (address == -1)
                {
                    groupBox3.Visible = false;
                    MessageBox.Show("No warp for that map found. To add one, find\nan existing warp on the same group\nand change the values to match the format similar\nto a regular side-map warp.", "No Warp Found");
                    button5.Enabled = false;
                    return;
                }
                button5.Enabled = true;
                groupBox3.Text = "Warp Header - 0x" + address.ToString("X");
                groupBox3.Visible = true;
                Warp w = warpLoader.loadSideWarp(address);
                w.srcGroup = (byte)cboArea.SelectedIndex;
                nX.Value = w.x;
                nSMap.Value = w.map;
                nSIndex.Value = w.index;
                nSGroup.Value = w.group;
                nSEntrance.Value = w.entrance;
                loadedWarp = w;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!groupBox3.Visible) //Not side room
            {
                if (gb.ReadByte(lastAddress + 4) == 0xFF)
                {
                    MessageBox.Show("Unable to continue. End of warp data for that group.", "Cannot Advance");
                    return;
                }
                lastAddress += 4;
                int address = lastAddress;
                Warp w = warpLoader.loadWarp(address);
                loadedWarp = w;

                if ((w.opcode & 0x40) != 0)
                {
                    groupBox2.Visible = true;
                    groupBox1.Visible = false;
                    groupBox2.Text = "Warp Header - 0x" + address.ToString("X");
                    nFType.Value = w.opcode;
                    nFMap.Value = w.map;
                    nMap.Value = w.map;
                    nPointer.Value = 0x10000 + w.fpointer;
                }
                else
                {
                    groupBox2.Visible = false;
                    groupBox1.Visible = true;
                    groupBox1.Text = "Warp Header - 0x" + address.ToString("X");
                    nType.Value = w.opcode;
                    nSrcMap.Value = w.map;
                    nMap.Value = w.map;
                    nWarp.Value = w.index;
                    nDestGroup.Value = w.group;
                    nEntrance.Value = w.entrance;
                }
            }
            else
            {
                if (gb.ReadByte(lastAddress + 4) == 0xFF)
                {
                    MessageBox.Show("Unable to continue. End of warp data for that group.", "Cannot Advance");
                    return;
                }
                lastAddress += 4;
                int address = lastAddress;
                groupBox3.Text = "Warp Header - 0x" + address.ToString("X");
                Warp w = warpLoader.loadSideWarp(address);
                loadedWarp = w;
                nX.Value = w.x;
                nSMap.Value = w.map;
                nMap.Value = w.map;
                nSIndex.Value = w.index;
                nSGroup.Value = w.group;
                nSEntrance.Value = w.entrance;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmW = new frmWarpProps(warpLoader, loadedWarp.srcGroup, loadedWarp.fpointer + 0x10000, game, gb);
            if (loadedWarp == null)
                return;
            Warp w;
            if (groupBox1.Visible)
            {
                w = warpLoader.loadWarpProps((int)nWarp.Value, (int)nDestGroup.Value, game);
                frmW.grp0.Visible = true;
                frmW.nMap0.Value = w.map;
                frmW.nPos0.Value = w.pos;
                frmW.nUnknown0.Value = w.unknown;
            }
            else if (groupBox2.Visible)
            {
                Warp m = warpLoader.loadWarp(0x10000 + loadedWarp.fpointer);
                frmW.grp1A.Visible = true;
                frmW.grp1B.Visible = true;
                frmW.fillWarp(m);
            }
            else
            {
                w = warpLoader.loadWarpProps((int)nSIndex.Value, (int)nSGroup.Value, game);
                frmW.grp0.Visible = true;
                frmW.nMap0.Value = w.map;
                frmW.nPos0.Value = w.pos;
                frmW.nUnknown0.Value = w.unknown;
            }

            if (frmW.ShowDialog() != DialogResult.OK)
            {
                loadedWarp.secondaryWarp = null;
                frmW = null;
                return;
            }

            loadedWarp.secondaryWarp = frmW.propWarp;
        }

        private void nX_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (groupBox1.Visible)
            {
                gb.BufferLocation = lastAddress + 1; //Skip the opcode
                gb.WriteByte((byte)nSrcMap.Value);
                gb.WriteByte((byte)nWarp.Value);
                byte b = (byte)(((byte)nDestGroup.Value << 4) + (byte)(nEntrance.Value));
                gb.WriteByte(b);

                if (frmW != null)
                {
                    gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x12F5B : 0x12D4E) + (int)nDestGroup.Value * 2;
                    gb.BufferLocation = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
                    gb.BufferLocation += (int)nWarp.Value * 3;
                    gb.WriteByte((byte)frmW.nMap0.Value);
                    gb.WriteByte((byte)frmW.nPos0.Value);
                    gb.WriteByte((byte)frmW.nUnknown0.Value);
                }
            }
            else if (groupBox2.Visible)
            {
                gb.BufferLocation = lastAddress + 1;
                gb.WriteByte((byte)nFMap.Value);
                int p = (int)nPointer.Value - 0x10000;
                gb.WriteBytes(new byte[] { (byte)(p & 0xFF), (byte)((p >> 8) + 0x40) });

                if (frmW != null)
                {
                    gb.BufferLocation = frmW.lastAddress;
                    if (frmW.nType.Value == 0 || ((byte)frmW.nType.Value & 0x80) != 0)
                        gb.WriteByte((byte)frmW.nType.Value);
                    else
                        gb.WriteByte(0);
                    gb.WriteByte((byte)frmW.nSrcX.Value);
                    gb.WriteByte((byte)frmW.nWarp.Value);
                    byte b = (byte)(((byte)frmW.nDestGroup.Value << 4) + (frmW.nEntrance.Value));
                    gb.WriteByte(b);

                    gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x12F5B : 0x12D4E) + (int)frmW.nDestGroup.Value * 2;
                    gb.BufferLocation = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
                    gb.BufferLocation += (int)frmW.nWarp.Value * 3;
                    gb.WriteByte((byte)frmW.nMap1.Value);
                    gb.WriteByte((byte)frmW.nPos1.Value);
                    gb.WriteByte((byte)frmW.nUnknown1.Value);
                }
            }
            else if (groupBox3.Visible)
            {
                gb.BufferLocation = lastAddress;
                gb.WriteByte((byte)nX.Value);
                gb.WriteByte((byte)nSMap.Value);
                gb.WriteByte((byte)nSIndex.Value);
                byte b = (byte)(((byte)nSGroup.Value << 4) + (nSEntrance.Value));
                gb.WriteByte(b);

                if (frmW != null)
                {
                    gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x12F5B : 0x12D4E) + (int)nSGroup.Value * 2;
                    gb.BufferLocation = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
                    gb.BufferLocation += (int)nSIndex.Value * 3;
                    gb.WriteByte((byte)frmW.nMap0.Value);
                    gb.WriteByte((byte)frmW.nPos0.Value);
                    gb.WriteByte((byte)frmW.nUnknown0.Value);
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void nWarp_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the type of warp.  0 is for stairs, 4 is for caves and doors, and 40 is for multiple warps on one map screen.\n\nDon't edit this.",
                "Warp Type Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Basically used to identify a specific warp.  Although it's editable, it's not recommended to edit it.",
                "Warp Index", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the map that the warp is on.  To change the map it's on, select the map you want here.",
                "Map Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the map group you will to travel to. Editing this is dangerous. Try to find a warp with the value you want already.\n\nThe Present Overworld is 0, Past Overworld is 1, Present Underwater is 2, Past Underwater is 3, Maku Path through Black Tower and the first Unmapped is 4, and Level 6 through Final Dungeon and the second Unmapped is 5.\n\nSee the wiki for more information.",
                "Map Group Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is just a value that determines what kind of warp it is.  2 is stairs, 3 is indoor exits and entrances, and 4 is outside entrances / exits and everything else.",
                "Entrance Type Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the type of warp.  0 is for stairs, 4 is for caves and doors, and 40 is for multiple warps on one map screen.\n\nDon't edit this.",
                "Warp Type Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the map that the warp is on.  To change the map it's on, select the map you want here.",
                "Map Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Although this can be edited, if you want the game to not crash, don't change it.",
                "Warp Pointer Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Basically used to identify a specific warp.  Although it's editable, it's not recommended to edit it.",
                "Warp Index", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the map that the warp is on.  To change the map it's on, select the map you want here.",
                "Map Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the location(s) on the side-scrolling map where you want the warp to be in effect.  Follow the list below:\n1 - Top Left Side\n2 - Top Right Side\n3 - Top Left and Right Sides\n4 - Bottom Left Side\n5 - Top Left and Bottom Left Sides\n6 - Top Right and Bottom Left Sides\n7 - Top Left, Top Right and Bottom Left Sides\n8 - Bottom Right Side\n9 - Top Left and Bottom Right Sides\nA - Top Right and Bottom Right Sides\nB - Top Left, Top Right and Bottom Right Sides\nC - Bottom Left and Bottom Right Sides\nD - Top Left, Bottom Left and Bottom Right Sides\nE - Top Right, Bottom Left and Bottom Right Sides\nF - All Sides",
                "Warp Location(s) Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the map group you want to travel to.  The Present Overworld is 0, Past Overworld is 1, Present Underwater is 2, Past Underwater is 3, Maku Path through Black Tower and the first Unmapped is 4, and Level 6 through Final Dungeon and the second Unmapped is 5.",
                "Map Group Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This could be one of three values.  If you want Link to enter facing upwards, enter 93.  If you want Link to enter facing downwards, enter F3.  If you want Link to fall from the ceiling on entry, enter 0B.",
                "Unknown Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}