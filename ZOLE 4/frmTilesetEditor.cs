using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmTilesetEditor : Form
    {
        GBHL.GBFile gb;
        Form1 form;
        byte[, ,] tiles;
        Bitmap tilesBmp;
        Color[,] palette;
        bool loaded = false;
        byte[] formation;
        byte[][] oldFormation;
        byte[, , ,] oldTiles;
        int history = 0;
        int maxHistory = 0;
        Point lastTilePoint = new Point(-1, -1);
        Point lastSetPoint = new Point(-1, -1);
        bool isPastTileset = false;
        int downIndex = -1;

        public frmTilesetEditor(GBHL.GBFile g, Form1 f)
        {
            InitializeComponent();
            tilesBmp = new Bitmap(128, 128);
            form = f;
            gb = g;
            nTileset.Value = f.nArea.Value;
            LoadTileset((int)nTileset.Value);
        }

        private void frmTilesetEditor_Load(object sender, EventArgs e)
        {
            oldFormation = new byte[1000][];
            oldTiles = new byte[1000, 256, 8, 8];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadTileset((int)nTileset.Value);
        }

        private void LoadTileset(int i)
        {
            tiles = new byte[256, 8, 8];
            //int indexBase = 0x10000 + (form.game == Program.GameTypes.Ages ? 0x0F9C : 0x0C84);
            //AreaLoader.Area a = form.mapLoader.areaLoader.loadArea(indexBase + (int)nTileset.Value * 8, (int)nTileset.Value, form.game);
            //form.decompressor.loadTileset(form.tilesetBuilder.getHeaderLocation(a.vram, form.game), a.unique, a.animation, form.game);
            byte[] buffer = new byte[4096];
            buffer = gb.ReadBytes(0x181000 + i * 0x1000, 4096);
            gb.ReadTiles(16, 16, buffer, ref tiles);
            LoadAll();
        }

        private void LoadAll()
        {
            loaded = true;
            LoadTilesetData();
            DrawPalette();
            DrawTiles();
            DrawTileset();
            DrawTile();
            DrawColors();
        }

        byte[] originalFormation;
        private void LoadTilesetData()
        {
            int indexBase = 0x10000 + (form.game == Program.GameTypes.Ages ? 0x0F9C : 0x0C84);
            AreaLoader.Area a = form.mapLoader.areaLoader.loadArea(indexBase + (int)nTileset.Value * 8, (int)nTileset.Value, form.game);
            isPastTileset = (a.tileset > 0xFF);
            gb.BufferLocation = gb.ReadByte(0x200000 + (a.tileset & 0x7F) * 3) * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            formation = gb.ReadBytes(0x800);
            originalFormation = new byte[0x800];
            Array.Copy(formation, originalFormation, 0x800);
            if (a.tileset > 0xFF)
                for (int i = 64; i < 128; i++)
                {
                    for (int y = 0; y < 2; y++)
                    {
                        for (int x = 0; x < 2; x++)
                        {
                            byte b = formation[i * 8 + 4 + x + (y * 2)];
                            if ((b & 7) == 6)
                            {
                                formation[i * 8 + 4 + x + (y * 2)] = (byte)((b & 0x78));
                            }
                        }
                    }
                }
        }

        private void DrawTileset()
        {
            Bitmap b = new Bitmap(256, 256);
            FastPixel fp = new FastPixel(b);
            fp.rgbValues = new byte[256 * 256 * 4];
            fp.Lock();
            for (int i = 0; i < 256; i++)
            {
                for (int y = 0; y < 2; y++)
                {
                    for (int x = 0; x < 2; x++)
                    {
                        byte by = formation[i * 8 + x + (y * 2)];
                        byte props = formation[i * 8 + 4 + x + (y * 2)];
                        byte pal = (byte)((props & 0x7));

                        by -= 0x80;
                        bool vflip = false;
                        bool hflip = false;
                        if (((props >> 4) & 2) != 0)
                            hflip = true;
                        if (((props >> 4) & 4) != 0)
                            vflip = true;
                        for (int yy = 0; yy < 8; yy++)
                        {
                            for (int xx = 0; xx < 8; xx++)
                            {
                                fp.SetPixel(((i % 16) * 16) + x * 8 + xx, ((i / 16) * 16) + y * 8 + yy, palette[pal, tiles[by, hflip ? 7 - xx : xx, vflip ? 7 - yy : yy]]);
                            }
                        }
                    }
                }
            }
            fp.Unlock(true);
            pTileset.Image = b;
        }

        private void DrawPalette()
        {
            int indexBase = 0x10000 + (form.game == Program.GameTypes.Ages ? 0x0F9C : 0x0C84);
            AreaLoader.Area a = form.mapLoader.areaLoader.loadArea(indexBase + (int)nTileset.Value * 8, (int)nTileset.Value, form.game);
            palette = form.paletteLoader.LoadPalette(a.palette, form.game);
            Bitmap pal = new Bitmap(256, 32);
            Graphics g = Graphics.FromImage(pal);
            for (int i = 0; i < 8; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    g.FillRectangle(new SolidBrush(palette[i, k]), i * 32, k * 8, 32, 8);
                }
            }
            pPalette.Image = pal;
        }

        private void DrawTiles()
        {
            FastPixel fp = new FastPixel(tilesBmp);
            fp.rgbValues = new byte[65536];
            fp.Lock();
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    for (int yy = 0; yy < 8; yy++)
                    {
                        for (int xx = 0; xx < 8; xx++)
                        {
                            fp.SetPixel(x * 8 + xx, y * 8 + yy, palette[pPalette.SelectedIndex, tiles[x + y * 16, xx, yy]]);
                        }
                    }
                }
            }
            fp.Unlock(true);
            pTiles.Image = tilesBmp;
        }

        private void DrawTile()
        {
            if (!loaded)
                return;
            Bitmap b = new Bitmap(64, 64);
            FastPixel fp = new FastPixel(b);
            fp.rgbValues = new byte[16384];
            fp.Lock();
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int xx = 0; xx < 8; xx++)
                    {
                        for (int yy = 0; yy < 8; yy++)
                        {
                            fp.SetPixel(x * 8 + xx, y * 8 + yy, palette[pPalette.SelectedIndex, tiles[pTiles.SelectedIndex, x, y]]);
                        }
                    }
                }
            }
            fp.Unlock(true);
            pTile.Image = b;
            DrawPreviewTile();
        }

        private void DrawPreviewTile()
        {
            Bitmap b = new Bitmap(8, 8);
            FastPixel fp = new FastPixel(b);
            fp.rgbValues = new byte[256];
            fp.Lock();
            bool hflip = chkHorizontal.Checked;
            bool vflip = chkVertical.Checked;
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    fp.SetPixel(x, y, palette[pPalette.SelectedIndex, tiles[pTiles.SelectedIndex, hflip ? 7 - x : x, vflip ? 7 - y : y]]);
                }
            }
            fp.Unlock(true);
            pPreviewTile.Image = b;
        }

        private void DrawColors()
        {
            Bitmap b = new Bitmap(184, 64);
            Graphics g = Graphics.FromImage(b);
            for (int i = 0; i < 4; i++)
                g.FillRectangle(new SolidBrush(palette[pPalette.SelectedIndex, i]), i * 46, 0, 46, 64);
            pColor.Image = b;
        }

        int lastIndex = 0;
        private void pPalette_MouseDown(object sender, MouseEventArgs e)
        {
            if (pPalette.SelectedIndex == 1)
            {
                pPalette.SelectedIndex = lastIndex;
                return;
            }
            if (!loaded)
                return;
            DrawTiles();
            DrawTile();
            DrawColors();
        }

        private void pPalette_MouseMove(object sender, MouseEventArgs e)
        {
            if (pPalette.SelectedIndex != 1)
                lastIndex = pPalette.SelectedIndex;
        }

        private void pTiles_MouseDown(object sender, MouseEventArgs e)
        {
            downIndex = -1;
            if (e.Button == MouseButtons.Left)
            {
                DrawTile();
            }
            else if (e.Button == MouseButtons.Right)
            {
                downIndex = e.X / 16 + (e.Y / 16) * 16;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            gb.BufferLocation = 0x181000 + (int)nTileset.Value * 0x1000;
            for (int i = 0; i < 256; i++)
            {
                for (int y = 0; y < 8; y++)
                {
                    byte b = (byte)(tiles[i, 7, y] & 1);
                    b |= (byte)((tiles[i, 6, y] & 1) << 1);
                    b |= (byte)((tiles[i, 5, y] & 1) << 2);
                    b |= (byte)((tiles[i, 4, y] & 1) << 3);
                    b |= (byte)((tiles[i, 3, y] & 1) << 4);
                    b |= (byte)((tiles[i, 2, y] & 1) << 5);
                    b |= (byte)((tiles[i, 1, y] & 1) << 6);
                    b |= (byte)((tiles[i, 0, y] & 1) << 7);
                    gb.WriteByte(b);
                    b = (byte)((tiles[i, 7, y] & 2) >> 1);
                    b |= (byte)((tiles[i, 6, y] & 2));
                    b |= (byte)((tiles[i, 5, y] & 2) << 1);
                    b |= (byte)((tiles[i, 4, y] & 2) << 2);
                    b |= (byte)((tiles[i, 3, y] & 2) << 3);
                    b |= (byte)((tiles[i, 2, y] & 2) << 4);
                    b |= (byte)((tiles[i, 1, y] & 2) << 5);
                    b |= (byte)((tiles[i, 0, y] & 2) << 6);
                    gb.WriteByte(b);
                }
            }
            int indexBase = 0x10000 + (form.game == Program.GameTypes.Ages ? 0x0F9C : 0x0C84);
            AreaLoader.Area a = form.mapLoader.areaLoader.loadArea(indexBase + (int)nTileset.Value * 8, (int)nTileset.Value, form.game);
            gb.BufferLocation = gb.ReadByte(0x200000 + (a.tileset & 0x7F) * 3) * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            gb.WriteBytes(formation);
            form.gb = gb;
        }

        private void pTile_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0 || e.X > 63 || e.Y > 63)
                return;
            if (e.Button == MouseButtons.Right)
            {
                pColor.SelectedIndex = tiles[pTiles.SelectedIndex, e.X / 8, e.Y / 8];
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                if (lastTilePoint.X == e.X / 8 * 8 && lastTilePoint.Y == e.Y / 8 * 8)
                    return;
                lastTilePoint = new Point(e.X / 8 * 8, e.Y / 8 * 8);
                if (history == 0)
                {
                    history = -1;
                    MakeHistory();
                }
                tiles[pTiles.SelectedIndex, e.X / 8, e.Y / 8] = (byte)pColor.SelectedIndex;
                DrawTile();//tileGraphics.FillRectangle(new SolidBrush(palette[pPalette.SelectedIndex, pColor.SelectedIndex]), e.X / 8 * 8, e.Y / 8 * 8, 8, 8);

                MakeHistory();
            }
        }

        private void pTile_MouseUp(object sender, MouseEventArgs e)
        {
            lastTilePoint = new Point(-1, -1);
            DrawTiles();
            DrawTileset();
        }

        private void pTileset_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0 || e.X > 511 || e.Y > 511)
                return;
            if (e.Button == MouseButtons.Left)
            {
                if (lastSetPoint.X == e.X / 16 * 16 && lastSetPoint.Y == e.Y / 16 * 16)
                    return;
                lastSetPoint = new Point(e.X / 16 * 16, e.Y / 16 * 16);
                if (history == 0)
                {
                    history = -1;
                    MakeHistory();
                }
                Bitmap b = (Bitmap)pTileset.Image;
                FastPixel fp = new FastPixel(b);
                fp.rgbValues = new byte[262144];
                fp.Lock();
                bool hflip = chkHorizontal.Checked;
                bool vflip = chkVertical.Checked;

                int dx = e.X / 32;
                int dy = e.Y / 32;
                int address = (dy * 16 + dx) * 8 + (e.X / 16 % 2) + (e.Y / 16 % 2) * 2;
                formation[address] = (byte)(pTiles.SelectedIndex + 0x80);
                int palIndex = pPalette.SelectedIndex;
                if ((originalFormation[address + 4] & 7) != 6 || !isPastTileset)
                {
                    byte p = (byte)(pPalette.SelectedIndex | 8);
                    if (chkHorizontal.Checked)
                        p |= 0x20;
                    if (chkVertical.Checked)
                        p |= 0x40;
                    formation[address + 4] = p;
                }
                else
                    palIndex = originalFormation[address + 4] & 7;

                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        fp.SetPixel(dx * 16 + (e.X / 16 % 2) * 8 + x, dy * 16 + (e.Y / 16 % 2) * 8 + y, palette[pPalette.SelectedIndex, tiles[pTiles.SelectedIndex, hflip ? 7 - x : x, vflip ? 7 - y : y]]);
                    }
                }
                fp.Unlock(true);
                pTileset.Image = b;
                MakeHistory();
            }
            if (e.Button == MouseButtons.Right)
            {
                int dx = e.X / 32;
                int dy = e.Y / 32;
                int address = (dy * 16 + dx) * 8 + (e.X / 16 % 2) + (e.Y / 16 % 2) * 2;
                chkHorizontal.Checked = (formation[address + 4] & 0x20) != 0;
                chkVertical.Checked = (formation[address + 4] & 0x40) != 0;
                pTiles.SelectedIndex = (byte)(formation[address] - 0x80);
                pPalette.SelectedIndex = formation[address + 4] & 0x7;
                DrawColors();
                DrawTiles();
                DrawTile();
                return;
            }
            if (e.Button == MouseButtons.Middle)
            {
                if (lastSetPoint.X == e.X / 16 * 16 && lastSetPoint.Y == e.Y / 16 * 16)
                    return;
                lastSetPoint = new Point(e.X / 16 * 16, e.Y / 16 * 16);
                int dx = e.X / 32;
                int dy = e.Y / 32;
                int address = (dy * 16 + dx) * 8 + (e.X / 16 % 2) + (e.Y / 16 % 2) * 2;
                if ((originalFormation[address + 4] & 7) == 6 && isPastTileset)
                    return;
                if (history == 0)
                {
                    history = -1;
                    MakeHistory();
                }
                bool hflip = (formation[address + 4] & 0x20) != 0;
                bool vflip = (formation[address + 4] & 0x40) != 0;
                byte p = (byte)(pPalette.SelectedIndex | 8);
                if (hflip)
                    p |= 0x20;
                if (vflip)
                    p |= 0x40;
                formation[address + 4] = p;

                Bitmap b = (Bitmap)pTileset.Image;
                FastPixel fp = new FastPixel(b);
                fp.rgbValues = new byte[262144];
                fp.Lock();
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        fp.SetPixel(dx * 16 + (e.X / 16 % 2) * 8 + x, dy * 16 + (e.Y / 16 % 2) * 8 + y, palette[pPalette.SelectedIndex, tiles[(byte)(formation[address] - 0x80), hflip ? 7 - x : x, vflip ? 7 - y : y]]);
                    }
                }
                fp.Unlock(true);
                pTileset.Image = b;
                MakeHistory();
            }
        }

        private void chkHorizontal_CheckedChanged(object sender, EventArgs e)
        {
            DrawPreviewTile();
        }

        private void chkVertical_CheckedChanged(object sender, EventArgs e)
        {
            DrawPreviewTile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void MakeHistory()
        {
            if (history == 99)
            {
                history = -1;
            }

            history++;
            oldFormation[history] = new byte[0x800];
            for (int i = 0; i < 0x800; i++)
                oldFormation[history][i] = formation[i];

            for (int i = 0; i < 256; i++)
                for (int y = 0; y < 8; y++)
                    for (int x = 0; x < 8; x++)
                        oldTiles[history, i, x, y] = tiles[i, x, y];
            maxHistory = history;
            button5.Enabled = false;
            if (history > 0)
                button4.Enabled = true;
            else
                button4.Enabled = false;
        }

        private void Undo()
        {
            if (history == 0)
                return;
            history--;
            button5.Enabled = true;
            if (history == 0)
                button4.Enabled = false;
            for (int i = 0; i < 0x800; i++)
                formation[i] = oldFormation[history][i];
            for (int i = 0; i < 256; i++)
                for (int y = 0; y < 8; y++)
                    for (int x = 0; x < 8; x++)
                        tiles[i, x, y] = oldTiles[history, i, x, y];
            DrawTiles();
            DrawTile();
            DrawTileset();
        }

        private void Redo()
        {
            if (history == maxHistory)
                return;
            history++;
            if (history == maxHistory)
                button5.Enabled = false;
            button4.Enabled = true;
            for (int i = 0; i < 0x800; i++)
                formation[i] = oldFormation[history][i];
            for (int i = 0; i < 256; i++)
                for (int y = 0; y < 8; y++)
                    for (int x = 0; x < 8; x++)
                        tiles[i, x, y] = oldTiles[history, i, x, y];
            DrawTiles();
            DrawTile();
            DrawTileset();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void pTileset_MouseUp(object sender, MouseEventArgs e)
        {
            lastSetPoint = new Point(-1, -1);
        }

        private void pTiles_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0 || e.X >= pTiles.Width - 4 || e.Y >= pTiles.Height - 4)
            {
                downIndex = -1;
                return;
            }
            if (downIndex != -1 && e.Button == MouseButtons.Right)
            {
                if (history == 0)
                {
                    history = -1;
                    MakeHistory();
                }

                int index = e.X / 16 + e.Y / 16 * 16;
                for (int y = 0; y < 8; y++)
                    for (int x = 0; x < 8; x++)
                        tiles[index, x, y] = tiles[downIndex, x, y];

                MakeHistory();
                DrawTiles();
                DrawTile();
                DrawTileset();
            }

            downIndex = -1;
        }
    }
}
