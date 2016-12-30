using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmDecompressTileset : Form
    {
        Form1 form1;
        bool lockAreaUpdates = true;
        public frmDecompressTileset(Form1 f, int area)
        {
            InitializeComponent();
            form1 = f;

            int indexBase = 0x10000 + 0x0F9C;
            AreaLoader.Area a = f.mapLoader.areaLoader.loadArea(indexBase + area * 8, area, Program.GameTypes.Ages);
            lockAreaUpdates = true;
            nVRAM.Value = a.vram;
            nUnique.Value = a.unique;
            nTileset.Value = a.tileset;
            nAnimation.Value = a.animation;
            nPalette.Value = a.palette;
            lockAreaUpdates = false;
            nVRAM_ValueChanged(null, null);
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

        private void nVRAM_ValueChanged(object sender, EventArgs e)
        {
            if (!lockAreaUpdates)
            {
                Bitmap tset = LoadTileset((int)nVRAM.Value, (int)nTileset.Value, (int)nUnique.Value, (int)nAnimation.Value, (int)nPalette.Value);
                pTileset.Image = tset;
            }
        }

        private Bitmap LoadTileset(int vram, int tileset, int unique, int animation, int palette)
        {
            return form1.loadTileset(vram, tileset, unique, animation, palette);
        }
    }
}
