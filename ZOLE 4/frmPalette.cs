using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmPalette : Form
    {
        PaletteLoader pLoader;
        Form1 form1;
        public Color[,] palette;
        Program.GameTypes g;

        public frmPalette(PaletteLoader p, Form1 f, int tileset, int pal, Program.GameTypes game)
        {
            g = game;
            pLoader = p;
            form1 = f; //To call the LoadTileset method

            InitializeComponent();
            nTileset.Enabled = nPalette.Enabled = false;

            nTileset.Value = tileset;
            nPalette.Value = pal;
            pLoader = new PaletteLoader(f.gb);

            palette = pLoader.LoadPalette((int)nPalette.Value, g);
            drawPalette();
            pTileset.Image = f.loadTileset(tileset);
        }

        private void frmPalette_Load(object sender, EventArgs e)
        {
            nTileset.Enabled = nPalette.Enabled = true;
        }

        private void drawPalette()
        {
            Bitmap b = new Bitmap(128, 16);
            Graphics g = Graphics.FromImage(b);
            for (int i = 0; i < 4; i++)
                g.FillRectangle(new SolidBrush(palette[0, i]), i * 32, 0, 32, 16);
            pSet1.Image = b;

            b = new Bitmap(128, 96);
            g = Graphics.FromImage(b);
            for (int k = 0; k < 6; k++)
                for (int i = 0; i < 4; i++)
                    g.FillRectangle(new SolidBrush(palette[k + 2, i]), i * 32, k * 16, 32, 16);
            pSet2.Image = b;
        }

        private void nPalette_ValueChanged(object sender, EventArgs e)
        {
            palette = pLoader.LoadPalette((int)nPalette.Value, g);
            drawPalette();
            pTileset.Image = form1.loadTileset((int)nTileset.Value, palette);
        }

        private void nTileset_ValueChanged(object sender, EventArgs e)
        {
            int i = 0;
            pTileset.Image = form1.loadTileset((int)nTileset.Value, ref i);
            nPalette.Value = i;
        }

        private void pSet1_MouseDown(object sender, MouseEventArgs e)
        {
            ColorDialog c = new ColorDialog();
            int i = pSet1.HoverIndex;
            c.Color = palette[0, i];
            if (c.ShowDialog() == DialogResult.OK)
            {
                int r = (int)(c.Color.R / 8 * 8);
                int g = (int)(c.Color.G / 8 * 8);
                int b = (int)(c.Color.B / 8 * 8);
                palette[0, i] = Color.FromArgb(r, g, b);
                drawPalette();
                pTileset.Image = form1.loadTileset((int)nTileset.Value, palette);
            }
        }

        private void pSet2_MouseDown(object sender, MouseEventArgs e)
        {
            ColorDialog c = new ColorDialog();
            int i = pSet2.HoverIndex;
            c.Color = palette[i / 4 + 2, i % 4];
            if (c.ShowDialog() == DialogResult.OK)
            {
                int r = (int)(c.Color.R / 8 * 8);
                int g = (int)(c.Color.G / 8 * 8);
                int b = (int)(c.Color.B / 8 * 8);
                palette[i / 4 + 2, i % 4] = Color.FromArgb(r, g, b);
                drawPalette();
                pTileset.Image = form1.loadTileset((int)nTileset.Value, palette);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void pSet1_Click(object sender, EventArgs e)
        {

        }
    }
}
