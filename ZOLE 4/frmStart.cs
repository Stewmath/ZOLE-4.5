using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmStart : Form
    {
        GBHL.GBFile gb;
        Program.GameTypes game;
        public frmStart(GBHL.GBFile g, Program.GameTypes gm)
        {
            gb = g;
            game = gm;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x1C19B : 0x1C193);
            gb.WriteByte((byte)nGroup.Value);
            gb.BufferLocation++;
            gb.WriteByte((byte)nMap.Value);
            gb.BufferLocation++;
            gb.WriteByte((byte)nY.Value);
            gb.BufferLocation++;
            gb.WriteByte((byte)nX.Value);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmStart_Load(object sender, EventArgs e)
        {
            gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x1C19B : 0x1C193);
            nGroup.Value = gb.ReadByte();
            gb.BufferLocation++;
            nMap.Value = gb.ReadByte();
            gb.BufferLocation++;
            nY.Value = gb.ReadByte();
            gb.BufferLocation++;
            nX.Value = gb.ReadByte();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The Starting Map is the map that Link will start on.  The Group is the map group - refer to the Resources section for a list of map groups.  The first box of the position is the X value and the second is the Y value.  Be sure to add the number 8 after the desired value for position.  If you want Link to start at (4, A), for example, type in the first box \"48\", and in the second \"A8\".",
                "Start Position Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
