using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using System.Configuration;
using System.Collections.Specialized;

//using System.Runtime.InteropServices;

namespace ZOLE_4
{
    public partial class Form1 : Form
    {
        public Program.GameTypes game = Program.GameTypes.Ages;

        public MapLoader mapLoader;
        public GBHL.GBFile gb;
        public Decompressor decompressor;
        public TilesetBuilder tilesetBuilder;
        public PaletteLoader paletteLoader;
        MinimapCreator minimapCreator;
        public frmExportMaps frmExport;
        InteractionLoader interactionLoader;
        MapSaver mapSaver;
        StaticObjectLoader staticObjectLoader;
        TransitionLoader transitionLoader;
        EnemyLoader enemyLoader;

        bool mapZoom = false;
        int mouseX = 0;
        int mouseY = 0;
        string hexEditor;

        /*
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        */
        string filename = "";
        string exportToFilename;
        int exportGroupIndex;
        Bitmap[] cachedAreas;
        Bitmap[,] cachedSeasons;
        public Bitmap[] bigMaps;
        int firstMap;
        byte selectedTile = 0;
        bool lockAreaUpdates = true;
        SolidBrush[] specialBrushes = new SolidBrush[] { new SolidBrush(Color.FromArgb(100, 255, 0, 0)), new SolidBrush(Color.FromArgb(100, 0, 0, 255)) };
        Point lastMapHoverPoint = new Point(-1, -1);

        string[] agesGroupNames = new string[] { "Overworld Present", "Overworld Past", "Underwater Present", "Underwater Past", "Maku Path Present", "Maku Path Past", "Level 1 1F", "Level 2 B1", "Level 2 1F", "Level 3 B1", "Level 3 1F", "Level 4 B1", "Level 4 1F", "Level 5 B1", "Level 5 1F", "Hero's Cave B1", "Hero's Cave 1F", "Black Tower", "Level 6 Present 1F", "Level 6 Past B1", "Level 6 Past 1F", "Level 7 1F", "Level 7 2F", "Level 7 3F", "Level 8 B3", "Level 8 B2", "Level 8 B1", "Level 8 1F", "Indoor Big", "Ganon's Dungeon", "Unmapped", "Unmapped" };
        string[] seasonsGroupNames = new string[] { "Overworld Spring", "Overworld Summer", "Overworld Fall", "Overworld Winter", "Subrosia", "Hero's Cave", "Level 1 1F", "Level 2 1F", "Level 3 1F", "Level 3 2F", "Level 4 B2", "Level 4 B1", "Level 4 1F", "Level 5 1F", "Level 6 1F", "Level 6 2F", "Level 6 3F", "Level 6 4F", "Level 6 5F", "Hero's Cave", "Level 7 B2", "Level 7 B1", "Level 7 1F", "Level 8 B1", "Level 8 1F", "Final Dungeon", "Ganon's Dungeon", "Unmapped", "Unmapped" };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hexEditor = ConfigurationManager.AppSettings.Get("HexEditor");
        }

        private void openROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Title = "Open a Zelda Oracles ROM";
            o.Filter = "All Supported Types|*.gbc;*.bin";
            if (o.ShowDialog() != DialogResult.OK)
                return;
            LoadROM(o.FileName);
        }

        private void reloadROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filename == "")
                return;
            LoadROM(filename);

            
        }

        private void LoadROM(string file)
        {
            BinaryReader br = new BinaryReader(File.OpenRead(file));
            byte[] buffer = br.ReadBytes((int)br.BaseStream.Length);
            br.Close();
            gb = new GBHL.GBFile(buffer);
            filename = file;

            string romName = Program.GetROMHeader(gb);
            if (romName.Contains("DIN"))
                game = Program.GameTypes.Seasons;
            else
                game = Program.GameTypes.Ages;
            DoAgesSeasonsGUI();

            GBHL.GBFile dest = new GBHL.GBFile(gb.Buffer);
            mapSaver = new MapSaver(dest);
            mapLoader = new MapLoader(gb);
            ChestLoader cl = new ChestLoader(gb, game);
            if (game == Program.GameTypes.Ages)
            {
                if (gb.Buffer.Length == 0x100000)
                {
                    var response = MessageBox.Show(
                        "The ROM you selected will now be expanded for editing.",
                        "Patching ROM",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Information);

                    if (response != DialogResult.OK)
                        return;

                    mapSaver.expandROM();
                    //mapSaver.decompressGroup(0, mapLoader, game);
                    mapSaver.decompressAllGroups(mapLoader, game);
                    gb.Buffer = dest.Buffer;
                    cl.repointChests();
                    decompressor = new Decompressor(gb);
                    tilesetBuilder = new TilesetBuilder(gb);
                    decompressTiles();
                    writeFile();
                }
                else if (gb.Buffer.Length == 0x200000)
                {
                    mapSaver.expand2MROM();
                    gb.Buffer = dest.Buffer;
                    decompressor = new Decompressor(gb);
                    tilesetBuilder = new TilesetBuilder(gb);
                    decompressTiles();
                    writeFile();
                }
                else
                {
                    decompressor = new Decompressor(gb);
                    tilesetBuilder = new TilesetBuilder(gb);
                }
                if (gb.ReadByte(0x3F15) == 0x30)
                    gb.WriteByte(0x3F15, 0x2F);
                EnemyLoader el = new EnemyLoader(gb);
            }
            else
            {
                if (gb.Buffer.Length == 0x100000)
                {
                    var response = MessageBox.Show(
                        "The ROM you selected will now be expanded for editing.",
                        "Patching ROM",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Information);

                    if (response != DialogResult.OK)
                        return;

                    mapSaver.expandSeasonsROM();
                    //mapSaver.decompressGroup(0, mapLoader, game);
                    mapSaver.decompressAllGroups(mapLoader, game);
                    gb.Buffer = dest.Buffer;
                    writeFile();
                }
                decompressor = new Decompressor(gb);
                tilesetBuilder = new TilesetBuilder(gb);
            }

            paletteLoader = new PaletteLoader(gb);
            minimapCreator = new MinimapCreator(gb);
            interactionLoader = new InteractionLoader(gb, game);
            staticObjectLoader = new StaticObjectLoader(gb);
            transitionLoader = new TransitionLoader(gb);
            enemyLoader = new EnemyLoader(gb);

            cachedAreas = new Bitmap[256];
            cachedSeasons = new Bitmap[256, 4];
            bigMaps = new Bitmap[agesGroupNames.Length];

            cboArea.Items.Clear();
            if (game == Program.GameTypes.Ages)
                foreach (string s in agesGroupNames)
                    cboArea.Items.Add(s);
            else
            {
                foreach (string s in seasonsGroupNames)
                    cboArea.Items.Add(s);
                MessageBox.Show("Seasons isn't as well supported as Ages. There may be missing or unwanted features (bugs).");
            }

            cboArea.SelectedIndex = 0;

            LoadMap((int)nMap.Value, cboArea.SelectedIndex);
            selectTile(0);
        }

        private void DoAgesSeasonsGUI()
        {
            if (game == Program.GameTypes.Ages)
            {
                nVRAM.Enabled = false;
                nTileset.Enabled = false;
                nUnique.Enabled = false;
                toolAges.Visible = true;
                toolSeasons.Visible = false;
                groupBox4.Visible = true;
                menuToolsSeasons.Visible = false;
                toolsToolStripMenuItem.Visible = true;
                toolStripMenuItem21.Visible = true;
            }
            else
            {
                nVRAM.Enabled = true;
                nTileset.Enabled = true;
                nUnique.Enabled = true;
                toolAges.Visible = false;
                toolSeasons.Visible = true;
                groupBox4.Visible = false;
                menuToolsSeasons.Visible = true;
                toolsToolStripMenuItem.Visible = false;
                toolStripMenuItem21.Visible = false;
            }
        }

        private void LoadMap(int index, int group)
        {
            if (mapLoader == null)
                return;
            if (index == -1)
                index = firstMap;

            lockAreaUpdates = true;
            int season = (group < 4 ? (group & 7) : 0);
            mapLoader.loadMap(index, minimapCreator.getRealMapGroup(group, game), season, true, game);
            lblRealGroup.Text = "Real Map Group: " + minimapCreator.getRealMapGroup(group, game).ToString("X");
            interactionLoader.loadInteractions(index, minimapCreator.getRealMapGroup(group, game));

            Bitmap b;
            if ((game == Program.GameTypes.Ages && cachedAreas[mapLoader.room.area.index] == null) || (game == Program.GameTypes.Seasons && cachedSeasons[mapLoader.room.area.index, season] == null))
            {
                byte[, ,] decompressedTiles = new byte[256, 8, 8];
                decompressor.loadTileset(tilesetBuilder.getHeaderLocation(mapLoader.room.area.vram, game), mapLoader.room.area.unique, mapLoader.room.area.animation, game);
                gb.ReadTiles(16, 16, decompressor.decompressedBuffer, ref decompressedTiles);
                tilesetBuilder.loadTileset(mapLoader.room.area.tileset, game);
                Color[,] palette = paletteLoader.LoadPalette(mapLoader.room.area.palette, game);
                b = tilesetBuilder.drawTileset(palette, decompressedTiles);
                season = (cboArea.SelectedIndex < 4 ? (cboArea.SelectedIndex & 7) : 0);
                if (game == Program.GameTypes.Seasons)
                    cachedSeasons[mapLoader.room.area.index, season] = b;
                else
                    cachedAreas[mapLoader.room.area.index] = b;
            }
            else
            {
                if (game == Program.GameTypes.Seasons)
                    b = cachedSeasons[mapLoader.room.area.index, season];
                else
                    b = cachedAreas[mapLoader.room.area.index];
            }

            nArea.Value = mapLoader.room.area.index;
            nVRAM.Value = mapLoader.room.area.vram;
            nTileset.Value = mapLoader.room.area.tileset;
            nUnique.Value = mapLoader.room.area.unique;
            nPalette.Value = mapLoader.room.area.palette;
            nAnimation.Value = mapLoader.room.area.animation;

            pTileset.Image = b;
            updateMap(); //pMap.Image = mapLoader.DrawMap(b, new bool[] { true, true, true });
            if ((game == Program.GameTypes.Ages && group < 4) || (game == Program.GameTypes.Seasons && group < 5))
            {
                pMinimap.SelectedIndex = index;
                nMap.Value = index;
            }
            else
            {
                nMap.Value = index;
                Point p = minimapCreator.getMapIndexPoint(index, group);
                if (minimapCreator.formationIndexes[p.X + p.Y * 8] != index || index == firstMap)
                    pMinimap.SelectedIndex = p.X + p.Y * 8;
            }

            nInteraction.Maximum = interactionLoader.interactions.Count - 1;
            selectInteraction(-1);
            selectTile(selectedTile);

            if (minimapCreator.dungeon(cboArea.SelectedIndex, game))
                pMap.CanvasSize = new Size(240, 176);
            else
                pMap.CanvasSize = new Size(160, 128);

            if (game == Program.GameTypes.Ages)
            {
                if (cboArea.SelectedIndex < 30 && cboArea.SelectedIndex > 3)
                {
                    dungeonRoomEditorToolStripMenuItem.Enabled = btnArrange.Enabled = true;
                    toolStripMenuItem38.Enabled = toolStripButton31.Enabled = true;
                }
                else
                {
                    dungeonRoomEditorToolStripMenuItem.Enabled = btnArrange.Enabled = false;
                    toolStripMenuItem38.Enabled = toolStripButton31.Enabled = false;
                }
            }
            else
            {
                if (cboArea.SelectedIndex < 27 && cboArea.SelectedIndex > 4)
                    toolStripMenuItem26.Enabled = btnArrangeSeasons.Enabled = true;
                else
                    toolStripMenuItem26.Enabled = btnArrangeSeasons.Enabled = false;
            }

            gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x1095C : 0x1083C) + minimapCreator.getRealMapGroup(group, game) * 2;
            gb.BufferLocation = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            gb.BufferLocation += index;
            nMusic.Value = gb.ReadByte();

            staticObjectLoader.staticObjects = new List<StaticObjectLoader.StaticObject>();
            staticObjectLoader.loadStaticObjects(MinimapCreator.GetDungeon(group, mapLoader.room.area.flags1, game), index);
            nStaticIndex.Maximum = staticObjectLoader.staticObjects.Count - 1;
            /*nStaticID.Enabled = staticObjectLoader.staticObjects.Count != 0;
            nStaticUnknown.Enabled = staticObjectLoader.staticObjects.Count != 0;
            nStaticFactor.Enabled = staticObjectLoader.staticObjects.Count != 0;
            nStaticX.Enabled = staticObjectLoader.staticObjects.Count != 0;
            nStaticY.Enabled = staticObjectLoader.staticObjects.Count != 0;*/
            nStaticIndex.Value = -1;
            nStaticIndex_ValueChanged(null, null);

            transitionLoader.LoadTransitions(minimapCreator.getRealMapGroup(group, game));

            lblMap.Text = "Map: 0x" + mapLoader.room.dataLocation.ToString("X") + " (" + (mapLoader.room.compressionType == 2 ? "16" : mapLoader.room.compressionType == 1 ? "8" : "0") + ")";
            lblInteraction.Text = "Objects: 0x" + interactionLoader.getInteractionLocation().ToString("X");
            lockAreaUpdates = false;
        }

        public void decompressTiles()
        {
            int indexBase = 0x10000 + (game == Program.GameTypes.Ages ? 0x0F9C : 0x0C84);
            for (int i = 0; i < 103; i++)
            {
                byte[, ,] tiles = new byte[256, 8, 8];
                AreaLoader.Area a = mapLoader.areaLoader.loadArea(indexBase + i * 8, i, game);
                decompressor.loadTileset(tilesetBuilder.getHeaderLocation(a.vram, game), a.unique, a.animation, game);
                int space = 0x181000 + i * 0x1000;
                gb.WriteBytes(space, decompressor.decompressedBuffer);
                gb.WriteByte(0x180000 + i * 3, (byte)(space / 0x4000));
                gb.WriteByte((byte)space);
                gb.WriteByte((byte)(((space % 0x4000) >> 8) + 0x40));

                a.tileset &= 0x7F;
                tilesetBuilder.loadTileset(a.tileset, game);
                space = 0x201000 + a.tileset * 2048;
                gb.BufferLocation = 0x200000 + a.tileset * 3;
                gb.WriteByte((byte)(space / 0x4000));
                gb.WriteByte((byte)space);
                gb.WriteByte((byte)(((space % 0x4000) >> 8) + 0x40));
                gb.BufferLocation = 0x201000 + a.tileset * 2048;
                for (int k = 0; k < 2048; k++)
                    gb.WriteByte(tilesetBuilder.assemblyData[0xD000 + k]);
            }
        }

        private void nMap_ValueChanged(object sender, EventArgs e)
        {
            if (mapLoader == null || (pMinimap.SelectedIndex == nMap.Value && !minimapCreator.dungeon(cboArea.SelectedIndex, game)))
                return;
            LoadMap((int)nMap.Value, cboArea.SelectedIndex);
        }

        private void exportEntireMapGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.Title = "Export Map Group";
            s.Filter = "PNG Files (*.png)|*.png";
            if (s.ShowDialog() != DialogResult.OK)
                return;
            exportToFilename = s.FileName;
            frmExport = new frmExportMaps();
            frmExport.pBar.Maximum = 256;
            exportGroupIndex = cboArea.SelectedIndex;

            bigMaps[exportGroupIndex] = null;
            cboArea_SelectedIndexChanged(null, null);
            Bitmap b = bigMaps[exportGroupIndex];

            if (exportToFilename != "")
                b.Save(exportToFilename);
        }

        private void cboArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            if (bigMaps[cboArea.SelectedIndex] == null)
            {
                frmExport = new frmExportMaps();
                if (game == Program.GameTypes.Seasons)
                    frmExport.pBar.Maximum = (cboArea.SelectedIndex < 5 ? 512 : 64);
                else
                    frmExport.pBar.Maximum = (cboArea.SelectedIndex < 4 ? 512 : 64);
                frmExport.Text = "Generating Map";
                //exportToFilename = "";
                exportGroupIndex = cboArea.SelectedIndex;
                new Thread(getMinimap).Start();
                frmExport.ShowDialog();
            }
            else
            {
                exportGroupIndex = cboArea.SelectedIndex;
                getMinimap();
            }

            Bitmap b;
            while (bigMaps[cboArea.SelectedIndex] == null) ;
            Bitmap src = bigMaps[cboArea.SelectedIndex];

            int mapWidth, mapHeight;

            if (mapZoom)
            {
                if (isDungeonLoaded())
                {
                    mapWidth = 720;
                    mapHeight = 528;
                }
                else
                {
                    mapWidth = 1280;
                    mapHeight = 1024;
                }
            }
            else
            {
                if (isDungeonLoaded())
                {
                    mapWidth = 240;
                    mapHeight = 176;
                }
                else {
                    mapWidth = 320;
                    mapHeight = 256;
                }
            }

            b = new Bitmap(mapWidth, mapHeight);

            Graphics g = Graphics.FromImage(b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.DrawImage(bigMaps[cboArea.SelectedIndex], 0, 0, mapWidth, mapHeight);

            pMinimap.Image = b;
            pMinimap.CanvasSize = new Size(mapWidth, mapHeight);

            if (isDungeonLoaded())
            {
                if (mapZoom)
                    pMinimap.BoxSize = new Size(90, 66);
                else
                    pMinimap.BoxSize = new Size(30, 22);
            }
            else
            {
                if (mapZoom)
                    pMinimap.BoxSize = new Size(80, 64);
                else
                    pMinimap.BoxSize = new Size(20, 16);
            }

            if (!isDungeonLoaded())
            {
                LoadMap(pMinimap.SelectedIndex, cboArea.SelectedIndex);
                groupBox4.Visible = false;
            }
            else
            {
                if (pMinimap.SelectedIndex > minimapCreator.formationIndexes.Length)
                    pMinimap.SelectedIndex = 0;
                LoadMap(minimapCreator.formationIndexes[pMinimap.SelectedIndex], cboArea.SelectedIndex);

                // Static objects box
                if (game != Program.GameTypes.Seasons)
                    groupBox4.Visible = true;
            }
        }

        private void getMinimap()
        {
            firstMap = minimapCreator.drawMinimap(exportGroupIndex, false, this, game);
        }

        public Bitmap loadTileset(int index, int groupIndex, int season)
        {
            Bitmap tset;
            int group = groupIndex;
            if (groupIndex > 0)
                season = 0;
            mapLoader.loadMap(index, group, season, (gb.Buffer.Length > 0x100000), game);
            if ((game == Program.GameTypes.Ages && cachedAreas[mapLoader.room.area.index] == null) || (game == Program.GameTypes.Seasons && cachedSeasons[mapLoader.room.area.index, season] == null))
            {
                if (game == Program.GameTypes.Ages && mapLoader.room.area.index >= 103)
                {
                    MessageBox.Show("Warning: Tileset " + mapLoader.room.area.index + " is above 102. It will not be loaded.", "Warning");
                    cachedAreas[mapLoader.room.area.index] = new Bitmap(256, 256);
                    return cachedAreas[mapLoader.room.area.index];
                }
                try
                {
                    byte[, ,] decompressedTiles;
                    Color[,] palette;
                    if (game == Program.GameTypes.Seasons)
                    {
                        decompressedTiles = new byte[256, 8, 8];
                        decompressor.loadTileset(tilesetBuilder.getHeaderLocation(mapLoader.room.area.vram, game), mapLoader.room.area.unique, mapLoader.room.area.animation, game);
                        gb.ReadTiles(16, 16, decompressor.decompressedBuffer, ref decompressedTiles);
                        tilesetBuilder.loadTileset(mapLoader.room.area.tileset, game);
                        palette = paletteLoader.LoadPalette(mapLoader.room.area.palette, game);
                        Bitmap b = tilesetBuilder.drawTileset(palette, decompressedTiles);
                        cachedSeasons[mapLoader.room.area.index, season] = b;
                        return b;
                    }
                    bool past = mapLoader.room.area.tileset > 0xFF;
                    mapLoader.room.area.tileset &= 0x7F;
                    decompressedTiles = new byte[256, 8, 8];
                    byte bank = gb.ReadByte(0x180000 + mapLoader.room.area.index * 3);
                    byte[] buffer = gb.ReadBytes(bank * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100), 0x1000);
                    gb.ReadTiles(16, 16, buffer, ref decompressedTiles);
                    palette = paletteLoader.LoadPalette(mapLoader.room.area.palette, game);
                    bank = gb.ReadByte(0x200000 + (mapLoader.room.area.tileset & 0x7F) * 3);
                    buffer = gb.ReadBytes(bank * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100), 0x8000);
                    if (tilesetBuilder.assemblyData == null)
                        tilesetBuilder.assemblyData = new byte[0x10000];
                    for (int i = 0; i < 0x800; i++)
                        tilesetBuilder.assemblyData[0xD000 + i] = buffer[i];
                    if (past)
                        tilesetBuilder.replacePast();
                    tset = tilesetBuilder.drawTileset(palette, decompressedTiles);
                    cachedAreas[mapLoader.room.area.index] = tset;
                }
                catch (Exception)
                {
                    MessageBox.Show("Bad tileset.");
                    return new Bitmap(256, 256);
                }
            }
            else
            {
                if (game == Program.GameTypes.Seasons)
                    tset = cachedSeasons[mapLoader.room.area.index, season];
                else
                    tset = cachedAreas[mapLoader.room.area.index];
            }

            return tset;
        }

        public Bitmap loadTileset(int area)
        {
            Bitmap tset;
            int indexBase = 0x10000 + (game == Program.GameTypes.Ages ? 0x0F9C : 0x0C84);
            /*if (gb.ReadByte(indexBase) == 0xFF)
            {
                indexBase = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            }*/
            AreaLoader.Area a = mapLoader.areaLoader.loadArea(indexBase + area * 8, area, game);
            if (game == Program.GameTypes.Seasons)
            {
                gb.BufferLocation = indexBase + area * 8;
                if (gb.ReadByte() == 0xFF)
                {
                    gb.BufferLocation = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
                    if (cboArea.SelectedIndex < 4)
                        gb.BufferLocation += cboArea.SelectedIndex * 8;
                }
                else
                    gb.BufferLocation--;
                a = mapLoader.areaLoader.loadArea(gb.BufferLocation, area, game);
            }
            if (cachedAreas[area] == null)
            {
                if (area >= 103 && game == Program.GameTypes.Ages)
                {
                    MessageBox.Show("Warning: Tileset " + area + " is above 102. It will not be loaded.", "Warning");
                    cachedAreas[area] = new Bitmap(256, 256);
                    return cachedAreas[area];
                }
                try
                {
                    byte[, ,] decompressedTiles;
                    Color[,] palette;
                    if (game == Program.GameTypes.Seasons)
                    {
                        decompressedTiles = new byte[256, 8, 8];
                        decompressor.loadTileset(tilesetBuilder.getHeaderLocation(a.vram, game), a.unique, a.animation, game);
                        gb.ReadTiles(16, 16, decompressor.decompressedBuffer, ref decompressedTiles);
                        tilesetBuilder.loadTileset(a.tileset, game);
                        palette = paletteLoader.LoadPalette(a.palette, game);
                        Bitmap b = tilesetBuilder.drawTileset(palette, decompressedTiles);
                        int season = (cboArea.SelectedIndex < 4 ? (cboArea.SelectedIndex & 7) : 0);
                        cachedSeasons[area, season] = b;
                        tset = b;
                        goto UpdateValues;
                    }
                    bool past = a.tileset > 0xFF;
                    a.tileset &= 0x7F;
                    decompressedTiles = new byte[256, 8, 8];
                    byte bank = gb.ReadByte(0x180000 + area * 3);
                    byte[] buffer = gb.ReadBytes(bank * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100), 0x1000);
                    gb.ReadTiles(16, 16, buffer, ref decompressedTiles);
                    palette = paletteLoader.LoadPalette(a.palette, game);
                    bank = gb.ReadByte(0x200000 + (a.tileset & 0x7F) * 3);
                    buffer = gb.ReadBytes(bank * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100), 0x8000);
                    for (int i = 0; i < 0x800; i++)
                        tilesetBuilder.assemblyData[0xD000 + i] = buffer[i];
                    tset = tilesetBuilder.drawTileset(palette, decompressedTiles);
                    cachedAreas[area] = tset;
                }
                catch (Exception)
                {
                    cachedAreas[area] = null;
                    MessageBox.Show("Bad tileset.");
                    return new Bitmap(256, 256);
                }
            }
            else
                tset = cachedAreas[area];

        UpdateValues:
            nVRAM.Value = a.vram;
            nTileset.Value = a.tileset;
            nPalette.Value = a.palette;
            nAnimation.Value = a.animation;
            nUnique.Value = a.unique;

            return tset;
        }

        public Bitmap loadTileset(int area, Color[,] palette)
        {
            Bitmap tset = new Bitmap(256, 256);
            try
            {
                int indexBase = 0x10000 + (game == Program.GameTypes.Ages ? 0x0F9C : 0x0C84);
                AreaLoader.Area a = mapLoader.areaLoader.loadArea(indexBase + area * 8, area, game);
                int season = (cboArea.SelectedIndex < 4 ? (cboArea.SelectedIndex & 7) : 0);
                if (game == Program.GameTypes.Seasons)
                {
                    gb.BufferLocation = indexBase + area * 8;
                    if (gb.ReadByte() == 0xFF)
                    {
                        gb.BufferLocation = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
                        gb.BufferLocation += season * 8;
                    }
                    else
                        gb.BufferLocation--;
                    a = mapLoader.areaLoader.loadArea(gb.BufferLocation, area, game);
                }
                byte[, ,] decompressedTiles = new byte[256, 8, 8];
                decompressor.loadTileset(tilesetBuilder.getHeaderLocation(a.vram, game), a.unique, a.animation, game);
                gb.ReadTiles(16, 16, decompressor.decompressedBuffer, ref decompressedTiles);
                tilesetBuilder.loadTileset(a.tileset, game);
                tset = tilesetBuilder.drawTileset(palette, decompressedTiles);
                /*if (game == Program.GameTypes.Seasons)
                    cachedSeasons[area, season] = tset;
                else
                    cachedAreas[area] = tset;*/
            }
            catch (Exception) { MessageBox.Show("Bad tileset", "Error"); }
            return tset;
        }

        public Bitmap loadTileset(int area, ref int palIndex)
        {
            Bitmap tset = new Bitmap(256, 256);
            try
            {
                int indexBase = 0x10000 + (game == Program.GameTypes.Ages ? 0x0F9C : 0x0C84);
                AreaLoader.Area a = mapLoader.areaLoader.loadArea(indexBase + area * 8, area, game);
                int season = (cboArea.SelectedIndex < 4 ? (cboArea.SelectedIndex & 7) : 0);
                if (game == Program.GameTypes.Seasons)
                {
                    gb.BufferLocation = indexBase + area * 8;
                    if (gb.ReadByte() == 0xFF)
                    {
                        gb.BufferLocation = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
                        gb.BufferLocation += season * 8;
                    }
                    else
                        gb.BufferLocation--;
                    a = mapLoader.areaLoader.loadArea(gb.BufferLocation, area, game);
                }
                byte[, ,] decompressedTiles = new byte[256, 8, 8];
                if (game == Program.GameTypes.Ages)
                {
                    decompressor.loadTileset(tilesetBuilder.getHeaderLocation(a.vram, game), a.unique, a.animation, game);
                    gb.ReadTiles(16, 16, decompressor.decompressedBuffer, ref decompressedTiles);
                    tilesetBuilder.loadTileset(a.tileset, game);
                    Color[,] palette = paletteLoader.LoadPalette(a.palette, game);
                    palIndex = a.palette;
                    tset = tilesetBuilder.drawTileset(palette, decompressedTiles);
                }
                else
                {
                    decompressedTiles = new byte[256, 8, 8];
                    decompressor.loadTileset(tilesetBuilder.getHeaderLocation(a.vram, game), a.unique, a.animation, game);
                    gb.ReadTiles(16, 16, decompressor.decompressedBuffer, ref decompressedTiles);
                    tilesetBuilder.loadTileset(a.tileset, game);
                    Color[,] palette = paletteLoader.LoadPalette(a.palette, game);
                    Bitmap b = tilesetBuilder.drawTileset(palette, decompressedTiles);
                    palIndex = a.palette;
                    tset = b;
                }
                if (game == Program.GameTypes.Seasons)
                    cachedSeasons[area, season] = tset;
                else
                    cachedAreas[area] = tset;
            }
            catch (Exception) { MessageBox.Show("Bad tileset", "Error"); }
            return tset;
        }

        public Bitmap loadTileset(int vram, int tileset, int unique, int animation, int paletteIndex)
        {
            Bitmap tset = new Bitmap(256, 256);
            try
            {
                byte[, ,] decompressedTiles = new byte[256, 8, 8];
                decompressor.loadTileset(tilesetBuilder.getHeaderLocation(vram, game), unique, animation, game);
                gb.ReadTiles(16, 16, decompressor.decompressedBuffer, ref decompressedTiles);
                tilesetBuilder.loadTileset(tileset, game);
                Color[,] palette = paletteLoader.LoadPalette(paletteIndex, game);
                tset = tilesetBuilder.drawTileset(palette, decompressedTiles);
            }
            catch (Exception) { MessageBox.Show("Bad tileset", "Error"); }
            return tset;
        }

        private bool isDungeonLoaded()
        {
            return minimapCreator.dungeon(cboArea.SelectedIndex, game);
        }

        private void pMinimap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!isDungeonLoaded())
                    LoadMap(pMinimap.SelectedIndex, cboArea.SelectedIndex);
                else
                    LoadMap(minimapCreator.formationIndexes[pMinimap.SelectedIndex], cboArea.SelectedIndex);
            }
        }
        private void pMinimap_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void copyMap()
        {
            if (mapLoader == null)
                return;
            int i = (mapLoader.room.type == MapLoader.RoomTypes.Small ? 80 : 0xB0);
            byte[] b = new byte[i + 1];
            b[0] = (byte)mapLoader.room.type;
            Array.Copy(mapLoader.room.decompressed, 0, b, 1, b.Length - 1);
            Clipboard.SetData("mapdata", b);
        }

        private void pasteMap()
        {
            if (mapLoader == null)
                return;
            byte[] b = (byte[])Clipboard.GetData("mapdata");
            if (b == null)
            {
                MessageBox.Show("No data exists.", "Error Pasting");
                return;
            }
            byte type = b[0];
            if ((mapLoader.room.type == MapLoader.RoomTypes.Small && type != 0) || (mapLoader.room.type == MapLoader.RoomTypes.Dungeon && type != 1))
            {
                MessageBox.Show("Incompatible map sizes.", "Error Pasting");
                return;
            }
            Array.Copy(b, 1, mapLoader.room.decompressed, 0, b.Length - 1);
            updateMap();
        }

        private void pMap_Paint(object sender, PaintEventArgs e)
        {
            lastMapHoverPoint = new Point(-1, -1);
            if (interactionLoader != null && chkInteractions.Checked)
            {
                int ind = 0;
                int bi = 0;
                foreach (InteractionLoader.Interaction i in interactionLoader.interactions)
                {
                    if (i.x != -1 && i.y != -1 && i.opcode != 9)
                        e.Graphics.FillRectangle(interactionLoader.getOpcodeColor(i.opcode), i.x - ((i.x & 8) != 0 ? 8 : 0), i.y - ((i.y & 8) != 0 ? 8 : 0), 16, 16);
                    else if (i.opcode == 9)
                        e.Graphics.FillRectangle(interactionLoader.getOpcodeColor(i.opcode), i.x - 8, i.y - 8, 16, 16);
                    else
                    {
                        e.Graphics.FillRectangle(interactionLoader.getOpcodeColor(i.opcode), (ind % 16) * 16 + 4, (ind / 16) * 16 + 4, 8, 8);
                        ind++;
                    }
                    if (bi == (int)nInteraction.Value)
                        if (i.x != -1 && i.y != -1)
                            e.Graphics.DrawRectangle(new Pen(Color.Red, 2), i.x - ((i.x & 8) != 0 ? 8 : 0), i.y - ((i.y & 8) != 0 ? 8 : 0), 16, 16);
                        else
                            e.Graphics.DrawRectangle(new Pen(Color.Red, 2), ((ind - 1) % 16) * 16, ((ind - 1) / 16) * 16, 16, 16);
                    bi++;
                }
            }
            else if (staticObjectLoader != null && staticObjectLoader.staticObjects != null && chkStaticObjects.Checked)
            {
                int i = 0;
                foreach (StaticObjectLoader.StaticObject s in staticObjectLoader.staticObjects)
                {
                    if (s.map == (int)nMap.Value)
                        e.Graphics.FillRectangle(Brushes.Green, new Rectangle(s.x - 8, s.y - 8, 16, 16));
                    else
                        e.Graphics.FillRectangle(Brushes.Red, new Rectangle(s.x - 8, s.y - 8, 16, 16));
                    e.Graphics.DrawRectangle(new Pen((i == nStaticIndex.Value ? Color.Blue : Color.White), 2), new Rectangle(s.x - 8, s.y - 8, 16, 16));
                    i++;
                }
            }
        }

        private void chkInteractions_CheckedChanged(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            if (chkInteractions.Checked)
                chkStaticObjects.Checked = false;
            pMap.SelectedIndex = (int)nInteraction.Value;
            pMap.Invalidate();
        }

        private void selectInteraction(int index)
        {
            if (mapLoader == null)
                return;
            if (nInteraction.Value != index)
                nInteraction.Value = index;
            if (pMap.SelectedIndex != nInteraction.Value)
                pMap.SelectedIndex = (int)nInteraction.Value;
            if (index >= interactionLoader.interactions.Count || index == -1)
            {
                interactionBox.Visible = false;
                lblInteractionType.Visible = false;
                pInteractionColor.Visible = false;
                return;
            }
            lblInteractionType.Visible = true;
            pInteractionColor.Visible = true;
            interactionBox.Visible = true;
            lblInteractionType.Text = interactionLoader.getOpcodeDefinition(interactionLoader.interactions[index].opcode);
            SolidBrush b = (SolidBrush)interactionLoader.getOpcodeColor(interactionLoader.interactions[index].opcode);
            pInteractionColor.BackColor = b.Color;
            interactionLoader.setInteractionDef(interactionLoader.interactions[index], ref interactionBox);
        }

        private void nInteraction_ValueChanged(object sender, EventArgs e)
        {
            selectInteraction((int)nInteraction.Value);
        }

        private void selectTile(int tile)
        {
            selectedTile = (byte)tile;
            if (pTileset.SelectedIndex != tile || (pTileset.SelectionRectangle.Width != 1 || pTileset.SelectionRectangle.Height != 1))
                pTileset.SelectedIndex = selectedTile;
            lblSelectedTile.Text = tile.ToString("X");
            pTile.Invalidate();
        }

        private void pTileset_MouseDown(object sender, MouseEventArgs e)
        {
            selectTile(pTileset.SelectedIndex);
        }

        private void pTile_Paint(object sender, PaintEventArgs e)
        {
            if (pTileset.Image != null)
            {
                e.Graphics.DrawImage(pTileset.Image, new Rectangle(0, 0, 16, 16), (selectedTile % 16) * 16, (selectedTile / 16) * 16, 16, 16, GraphicsUnit.Pixel);
            }
        }

        public void save()
        {
            if (filename == "")
                return;
            mapSaver.gb = gb;

            if (mapLoader.room.type == MapLoader.RoomTypes.Small)
            {
                gb.BufferLocation = mapLoader.room.dataLocation;
                gb.WriteBytes(mapLoader.room.decompressed);
            }
            else
            {
                gb.BufferLocation = mapLoader.room.dataLocation;
                gb.WriteBytes(mapLoader.room.decompressed);
            }

            mapSaver.saveMusic((int)nMap.Value, minimapCreator.getRealMapGroup(cboArea.SelectedIndex, game), (byte)nMusic.Value, game);
            mapSaver.saveAreaIndex((int)nMap.Value, minimapCreator.getRealMapGroup(cboArea.SelectedIndex, game), (byte)nArea.Value, game);
            int season = (cboArea.SelectedIndex < 4 ? (cboArea.SelectedIndex & 7) : 0);
            mapSaver.saveAreaData((int)nArea.Value, (int)nVRAM.Value, (int)nTileset.Value, (int)nUnique.Value, (int)nAnimation.Value, (int)nPalette.Value, season, game);
            interactionLoader.gb = gb;
            interactionLoader.saveInteractions();

            if (game == Program.GameTypes.Ages)
            {
                int dungeon = MinimapCreator.GetDungeon(cboArea.SelectedIndex, mapLoader.room.area.flags1, game);
                if (dungeon != 0xFF)
                    staticObjectLoader.saveObjects(dungeon);
                if (mapLoader.room.area.index != (int)nArea.Value || mapLoader.room.area.palette != (int)nPalette.Value || mapLoader.room.area.animation != (int)nAnimation.Value || mapLoader.room.area.tileset != (int)nTileset.Value || mapLoader.room.area.vram != (int)nVRAM.Value || mapLoader.room.area.unique != (int)nUnique.Value)
                    cachedAreas[(int)nArea.Value] = null;
            }
            else
                if (mapLoader.room.area.index != (int)nArea.Value || mapLoader.room.area.palette != (int)nPalette.Value || mapLoader.room.area.animation != (int)nAnimation.Value || mapLoader.room.area.tileset != (int)nTileset.Value || mapLoader.room.area.vram != (int)nVRAM.Value || mapLoader.room.area.unique != (int)nUnique.Value)
                    cachedSeasons[(int)nArea.Value, season] = null;
            writeFile();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save();
        }

        private void writeFile()
        {
            BinaryWriter bw = new BinaryWriter(File.Open(filename, FileMode.Open));
            bw.Write(gb.Buffer);
            bw.Close();
        }

        private void pMap_MouseDown(object sender, MouseEventArgs e)
        {
                lblHoverPos.Text = "X: " + (e.X / 16).ToString("X") + " Y: " + (e.Y / 16).ToString("X");
                /*if (chkInteractions.Checked)
                {
                    int ind = 0;
                    int bi = 0;
                    for(bi = interactionLoader.interactions.Count - 1; bi > -1; bi--)
                    {
                        InteractionLoader.Interaction i = interactionLoader.interactions[bi];
                        if ((i.x + ((i.x & 8) != 0 ? 8 : 0)) / 16 == e.X / 16 && (i.y + ((i.y & 8) != 0 ? 8 : 0)) / 16 == e.Y / 16)
                        {
                            selectInteraction(bi);
                            return;
                        }
                        else if(i.x == -1 && i.y == -1)
                        {
                            if (e.X / 16 == (ind % 16) && e.Y / 16 == (ind / 16))
                            {
                                selectInteraction(bi);
                                return;
                            }
                            ind++;
                        }
                    }
                    selectInteraction(-1);
                }*/
                if (pMap.Image == null || e.X < 0 || e.Y < 0 || e.X >= pMap.CanvasSize.Width || e.Y >= pMap.CanvasSize.Height || chkInteractions.Checked || chkStaticObjects.Checked)
                    return;
                if (e.Button == MouseButtons.Right)
                {
                    if (pMap.Image.Width == 160)
                    {
                        selectTile(mapLoader.room.decompressed[e.X / 16 + (e.Y / 16) * 10]);
                    }
                    else
                    {
                        selectTile(mapLoader.room.decompressed[((e.X / 16) % 15) + ((e.Y / 16) * 16)]);
                    }
                    lastMapHoverPoint = new Point(-1, -1);
                    return;
                }
                else if (e.Button == MouseButtons.Left)
                {
                    if (lastMapHoverPoint.X == e.X / 16 && lastMapHoverPoint.Y == e.Y / 16)
                        return;
                    Graphics g = Graphics.FromImage(pMap.Image);
                    if (pTileset.SelectionRectangle.Width == 1 && pTileset.SelectionRectangle.Height == 1)
                    {
                        g.DrawImage(pTileset.Image, new Rectangle(e.X / 16 * 16, e.Y / 16 * 16, 16, 16), (selectedTile % 16) * 16, (selectedTile / 16) * 16, 16, 16, GraphicsUnit.Pixel);
                        if (pMap.Image.Width == 160)
                        {
                            int i = ((e.X / 16) % 10) + ((e.Y / 16) * 10);
                            mapLoader.room.overworld[i].id = selectedTile;
                            mapLoader.room.decompressed[i] = selectedTile;
                        }
                        else
                        {
                            int i = ((e.X / 16) % 15) + ((e.Y / 16) * 16);
                            mapLoader.room.dungeon[i].id = selectedTile;
                            mapLoader.room.decompressed[i] = selectedTile;
                        }
                    }
                    else
                    {
                        int mwidth = (pMap.Image.Width == 160 ? 10 : 15);
                        int mheight = (pMap.Image.Width == 160 ? 8 : 11);
                        int xx = e.X / 16;
                        int yy = e.Y / 16;
                        for (int y = 0; y < pTileset.SelectionRectangle.Height; y++)
                        {
                            for (int x = 0; x < pTileset.SelectionRectangle.Width; x++)
                            {
                                if (x + xx >= mwidth)
                                    break;
                                if (y + yy >= mheight)
                                    break;
                                g.DrawImage(pTileset.Image, new Rectangle(e.X / 16 * 16 + x * 16, e.Y / 16 * 16 + y * 16, 16, 16), ((selectedTile % 16) + x) * 16, ((selectedTile / 16) + y) * 16, 16, 16, GraphicsUnit.Pixel);
                                if (pMap.Image.Width == 160)
                                {
                                    int i = (((e.X / 16) % 10) + x) + (((e.Y / 16 + y) * 10));
                                    mapLoader.room.overworld[i].id = (byte)(selectedTile + x + (y * 16));
                                    mapLoader.room.decompressed[i] = (byte)(selectedTile + x + (y * 16));
                                }
                                else
                                {
                                    int i = (((e.X / 16) % 15) + x) + ((e.Y / 16 + y) * 16);
                                    mapLoader.room.dungeon[i].id = (byte)(selectedTile + x + (y * 16));
                                    mapLoader.room.decompressed[i] = (byte)(selectedTile + x + (y * 16));
                                }
                            }
                        }
                    }
                    lastMapHoverPoint = new Point(e.X / 16, e.Y / 16);
                    pMap.Invalidate();
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    byte search;
                    Rectangle p = pTileset.SelectionRectangle;
                    p.Width = p.Height = 1;
                    pTileset.SelectionRectangle = p;
                    if (pMap.Image.Width == 160)
                        search = mapLoader.room.decompressed[e.X / 16 + (e.Y / 16) * 10];
                    else
                        search = mapLoader.room.decompressed[((e.X / 16) % 15) + ((e.Y / 16) * 16)];
                    fillTile(((e.X / 16) % 15), ((e.Y / 16)), search, (pMap.Image.Width != 160));
                    updateMap();
                }
        }

        private bool checkBlock(int x, int y, byte search)
        {
            if (pMap.Image.Width == 160)
            {
                if (x >= 10 || y >= 8)
                    return false;
                return (mapLoader.room.decompressed[x + y * 10] == search && mapLoader.room.decompressed[x + y * 10] != selectedTile);
            }
            else
            {
                if (x >= 16 || y >= 11)
                    return false;
                return (mapLoader.room.decompressed[x + y * 16] == search && mapLoader.room.decompressed[x + y * 16] != selectedTile);
            }
        }

        private void fillTile(int x, int y, byte search, bool dungeon)
        {
            int boundX = (dungeon ? 16 : 11);
            int boundY = (dungeon ? 12 : 9);

            if (x >= boundX - 1 || x < 0 || y >= boundY - 1 || y < 0)
                return;

            if (mapLoader.room.decompressed[x + y * (boundX - (boundX == 11 ? 1 : 0))] == selectedTile)
                return;
            mapLoader.room.decompressed[x + y * (boundX - (boundX == 11 ? 1 : 0))] = selectedTile;
            //Top left
            /*if (x > 0 && y > 0)
            {
                if (checkBlock(x - 1, y - 1, search))
                    fillTile(x - 1, y - 1, search, dungeon);
            }*/
            if (y > 0)
            {
                if (checkBlock(x, y - 1, search))
                    fillTile(x, y - 1, search, dungeon);
            }
            /*if (x < boundX - 1 && y > 0)
            {
                if (checkBlock(x + 1, y - 1, search))
                    fillTile(x + 1, y - 1, search, dungeon);
            }*/
            if (x > 0)
            {
                if (checkBlock(x - 1, y, search))
                    fillTile(x - 1, y, search, dungeon);
            }
            if (x < boundX - 1)
            {
                if (checkBlock(x + 1, y, search))
                    fillTile(x + 1, y, search, dungeon);
            }
            /*
            if (x > 0 && y < boundY - 1)
            {
                if (checkBlock(x - 1, y + 1, search))
                    fillTile(x - 1, y + 1, search, dungeon);
            }*/
            if (y < boundY - 1)
            {
                if (checkBlock(x, y + 1, search))
                    fillTile(x, y + 1, search, dungeon);
            }
            /*
            if (x < boundX - 1 && y < boundY - 1)
            {
                if (checkBlock(x + 1, y + 1, search))
                    fillTile(x + 1, y + 1, search, dungeon);
            }*/
        }

        private void pMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (pMap.Image == null)
                    return;
                if (pMap.Image.Width != 160 && (lastMapHoverPoint.X != e.X / 16 || lastMapHoverPoint.Y != e.Y / 16))
                {
                    pMap.Refresh();
                    Application.DoEvents(); //Needed so the control doesn't repaint right away
                    Graphics g = pMap.CreateGraphics();
                    int index = (e.X / 16) + ((e.Y / 16) * 16);
                    g.FillRectangle((mapLoader.room.dungeon[index].type == 0 ? specialBrushes[1] : specialBrushes[0]), mapLoader.room.dungeon[index].x * 16, mapLoader.room.dungeon[index].y * 16, 16, 16);
                    lastMapHoverPoint = new Point(e.X / 16, e.Y / 16);
                }
            }
        }

        private void interactionBox_ValueChanged(object sender, EventArgs e)
        {
            if (nInteraction.Value != -1)
                interactionLoader.writeInteractions(interactionBox, (int)nInteraction.Value);
        }

        private void warpEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            //if ((mapLoader.room.area.flags2 & 4) != 0)
            //	MessageBox.Show("Note: The warp editor does not detect exiting bottom warps for maps such as these.\nTo access them, until sometime in the future, you will have to use a hex editor.\n\nTo see how they work, click \"Indoor Warps\" from the Help menu.", "Notice");
            byte[] buffer = new byte[(game == Program.GameTypes.Ages ? 0x400000 : 0x200000)];
            Array.Copy(gb.Buffer, buffer, gb.Buffer.Length);
            frmWarp f = new frmWarp(game, gb, (byte)mapLoader.room.area.flags2);
            f.nMap.Value = nMap.Value;
            f.cboArea.SelectedIndex = cboArea.SelectedIndex;
            if (f.ShowDialog() != DialogResult.OK)
            {
                gb.Buffer = buffer;
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (pMap.Image != null)
                Clipboard.SetImage(pMap.Image);
        }

        private void nArea_ValueChanged(object sender, EventArgs e)
        {
            if (mapLoader != null)
            {
                lockAreaUpdates = true;
                int season = (cboArea.SelectedIndex < 4 ? (cboArea.SelectedIndex & 7) : 0);
                if (loadTileset((int)nArea.Value) != null)
                {
                    if (game == Program.GameTypes.Seasons)
                    {
                        pTileset.Image = cachedSeasons[(int)nArea.Value, season];
                    }
                    else
                        pTileset.Image = cachedAreas[(int)nArea.Value];
                }
                if (game == Program.GameTypes.Seasons)
                    pMap.Image = mapLoader.DrawMap(cachedSeasons[(int)nArea.Value, season], new bool[] { false });
                else
                    pMap.Image = mapLoader.DrawMap(cachedAreas[(int)nArea.Value], new bool[] { false });
                lockAreaUpdates = false;
            }
        }

        private void nTileset_ValueChanged(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            if (!lockAreaUpdates)
            {
                Bitmap tset = loadTileset((int)nVRAM.Value, (int)nTileset.Value, (int)nUnique.Value, (int)nAnimation.Value, (int)nPalette.Value);
                pTileset.Image = tset;
                updateMap();
            }
        }

        private void viewRawTilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateMap();
        }

        private void updateMap()
        {
            bool[] flags = new bool[] { true, true, true };
            pMap.Image = mapLoader.DrawMap((Bitmap)pTileset.Image, flags);
        }

        private void viewFillerTilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateMap();
        }

        private void pMap_MouseLeave(object sender, EventArgs e)
        {
            lastMapHoverPoint = new Point(-1, -1);
        }

        private void addRemoveInteractionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;

            frmInteractions f = new frmInteractions(interactionLoader, minimapCreator.getRealMapGroup(cboArea.SelectedIndex, game), (int)nMap.Value, game, gb);
            f.ShowDialog();
            nInteraction.Maximum = interactionLoader.interactions.Count - 1;
            if (chkInteractions.Checked)
            {
                selectInteraction(-1);
                updateMap();
            }
            lblInteraction.Text = "Object: 0x" + interactionLoader.getInteractionLocation().ToString("X");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (interactionLoader == null || nInteraction.Value == -1)
                return;
            int i = (int)nInteraction.Value;
            interactionLoader.deleteInteraction((int)nInteraction.Value);
            nInteraction.Maximum = interactionLoader.interactions.Count - 1;
            selectInteraction(i - 1);
            pMap.Invalidate();
        }

        private void removeForestRandomizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            Patches.removeForestRand(gb);
        }

        private void chestEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;

            byte[] buffer = new byte[(game == Program.GameTypes.Ages ? 0x400000 : 0x200000)];
            Array.Copy(gb.Buffer, buffer, gb.Buffer.Length);
            frmChest f = new frmChest(gb, minimapCreator.getRealMapGroup(cboArea.SelectedIndex, game), (int)nMap.Value, game);
            if (f.ShowDialog() != DialogResult.OK)
            {
                gb.Buffer = buffer;
                return;
            }
        }

        private void rawMapDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            OpenFileDialog s = new OpenFileDialog();
            s.Title = "Import Map Data";
            s.Filter = "Map Data File (*.MDF)|*.MDF";
        Start:
            if (s.ShowDialog() != DialogResult.OK)
                return;
            try
            {
                BinaryReader br = new BinaryReader(File.OpenRead(s.FileName));
                byte type = br.ReadByte();
                if ((type == 0 && mapLoader.room.type != MapLoader.RoomTypes.Small) || (type == 1 && mapLoader.room.type != MapLoader.RoomTypes.Dungeon))
                {
                    MessageBox.Show("Room size differs than current map's.", "Size Mismatch");
                    goto Start;
                }

                if (type == 0)
                    mapLoader.room.decompressed = br.ReadBytes(0x80);
                else
                    mapLoader.room.decompressed = br.ReadBytes(0xB0);
                pMap.Image = mapLoader.DrawMap(cachedAreas[(int)nArea.Value], new bool[] { false });

                br.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("IO Error.\n\n" + ex.Message, "Error");
                goto Start;
            }
        }

        private void mapDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            SaveFileDialog s = new SaveFileDialog();
            s.Title = "Export Map Data";
            s.Filter = "Map Data File (*.MDF)|*.MDF";
            if (s.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                BinaryWriter bw = new BinaryWriter(File.Open(s.FileName, FileMode.OpenOrCreate));
                bw.Write((byte)(mapLoader.room.type == MapLoader.RoomTypes.Small ? 0 : 1));
                bw.Write(mapLoader.room.decompressed);
                bw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("IO Error.\n\n" + ex.Message, "Error");
            }
        }

        private void paletteEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null) return;
            frmPalette f = new frmPalette(paletteLoader, this, (int)nArea.Value, (int)nPalette.Value, game);
            f.AutoScaleMode = AutoScaleMode.None;
            if (f.ShowDialog() != DialogResult.OK)
                return;

            Color[,] pal = f.palette;

            gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x5C8E0 : 0x58830);
            for (int i = 0; i < 4; i++)
            {
                int value = (ushort)PaletteLoader.getHexValue((int)(pal[0, i].R / 8), (int)(pal[0, i].G / 8), (int)(pal[0, i].B / 8));
                gb.WriteBytes(new byte[] { (byte)(value & 0xFF), (byte)(value >> 8) });
            }

            int baseColorAddress = (game == Program.GameTypes.Ages ? (0x17 * 0x4000) : (0x16 * 0x4000));
            gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x632C : 0x6290);
            gb.BufferLocation += (int)f.nPalette.Value * 2;
            gb.BufferLocation = gb.ReadByte() + gb.ReadByte() * 0x100;
            byte a = gb.ReadByte();
            if ((a & 7) + 1 == 6)
            {
                gb.BufferLocation = baseColorAddress + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
                for (int k = 0; k < 6; k++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        int value = (ushort)PaletteLoader.getHexValue((int)(pal[k + 2, i].R / 8), (int)(pal[k + 2, i].G / 8), (int)(pal[k + 2, i].B / 8));
                        gb.WriteBytes(new byte[] { (byte)(value & 0xFF), (byte)(value >> 8) });
                    }
                }
            }

            int season = (cboArea.SelectedIndex < 4 ? (cboArea.SelectedIndex & 7) : 0);
            if (MessageBox.Show("Clear cached images (Tilesets and minimaps. This will cause them to need to be reloaded)?\nIf you choose No, your changes will not show up instantly or for everything.", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                cachedAreas = new Bitmap[256];
                bigMaps = new Bitmap[agesGroupNames.Length];
                cboArea_SelectedIndexChanged(null, null);
            }
            else
            {
                if (game == Program.GameTypes.Ages)
                {
                    cachedAreas[(int)nArea.Value] = null;
                    cachedAreas[(int)nArea.Value] = loadTileset((int)nArea.Value);
                }
                else
                {
                    cachedSeasons[(int)nArea.Value, season] = null;
                    cachedSeasons[(int)nArea.Value, season] = loadTileset((int)nArea.Value);
                }
            }
            LoadMap((int)nMap.Value, cboArea.SelectedIndex);
        }

        private void startEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            frmStart f = new frmStart(gb, game);
            f.ShowDialog();
        }

        public int findFreeSpace(int amount)
        {
            int found = 0;
            gb.BufferLocation = 0x8000;
            while (found < amount)
            {
                if (gb.BufferLocation == gb.Buffer.Length - 1)
                {
                    return -1;
                }
                if (gb.ReadByte() != 0)
                {
                    found = 0;
                    continue;
                }
                if (found == 0)
                {
                    if ((gb.BufferLocation & 1) == 0 && (gb.BufferLocation & 2) == 0 && (gb.BufferLocation & 4) == 0 && (gb.BufferLocation & 8) == 0)
                        found++;
                }
                else
                    found++;
            }

            return gb.BufferLocation - found + 1;
        }

        public int findFreeSpace(int amount, int start)
        {
            int found = 0;
            gb.BufferLocation = start;
            while (found < amount)
            {
                if (gb.BufferLocation == gb.Buffer.Length - 1)
                {
                    return -1;
                }
                if (gb.ReadByte() != 0)
                {
                    found = 0;
                    continue;
                }
                if (found == 0)
                {
                    if ((gb.BufferLocation & 1) == 0 && (gb.BufferLocation & 2) == 0 && (gb.BufferLocation & 4) == 0 && (gb.BufferLocation & 8) == 0)
                        found++;
                }
                else
                    found++;
            }

            return gb.BufferLocation - found + 1;
        }

        private void saveDecompressedTiles(int vram)
        {
            if (decompressor == null || mapLoader == null)
                return;
            int space = findFreeSpace(16384);
            if (space == -1)
            {
                MessageBox.Show("Not enough space found. Needs 4KB (0x1000) with LSN unset.", "Not Enough Space");
                return;
            }
            int lTemp = decompressor.location;
            byte[] dTemp = decompressor.decompressedBuffer;
            byte[] vTemp = decompressor.vramBuffer;

            decompressor.decompressedBuffer = new byte[16384];
            byte a = 0;
            decompressor.loadTilesetHeader(tilesetBuilder.getHeaderLocation(vram, game), 1, true, 0, 0, ref a);
            gb.BufferLocation = space;
            gb.WriteBytes(decompressor.decompressedBuffer);
            gb.BufferLocation = decompressor.location;
            gb.WriteByte(0xFF);
            gb.WriteByte((byte)nVRAM.Value);
            gb.BufferLocation = 0x101000 + (int)nVRAM.Value * 3;
            gb.WriteByte((byte)(space / 0x4000));
            gb.WriteBytes(new byte[] { (byte)space, (byte)((space >> 8) + 0x40) });
            MessageBox.Show("Decompressed to 0x" + space.ToString("X") + ".", "Success");

            decompressor.location = lTemp;
            decompressor.decompressedBuffer = dTemp;
            decompressor.vramBuffer = vTemp;
        }

        private void vRAMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decompressor == null || mapLoader == null)
                return;
            int space = findFreeSpace(4096);
            if (space == -1)
            {
                MessageBox.Show("Not enough space found. Needs 4KB (0x1000) with LSN unset.", "Not Enough Space");
                return;
            }
            gb.BufferLocation = space;
            gb.WriteBytes(decompressor.vramBuffer);
            gb.BufferLocation = decompressor.location;
            gb.WriteByte(0xFF);
            gb.WriteByte((byte)nVRAM.Value);
            gb.BufferLocation = 0x101000 + (int)nVRAM.Value * 3;
            gb.WriteByte((byte)(space / 0x4000));
            gb.WriteBytes(new byte[] { (byte)space, (byte)((space >> 8) + 0x40) });
            MessageBox.Show("Decompressed to 0x" + space.ToString("X") + ".", "Success");
        }

        private void baseTilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null || decompressor == null)
                return;
            SaveFileDialog s = new SaveFileDialog();
            s.Title = "Export Base Tiles (VRAM)";
            s.Filter = "RBT Files (*.RBT)|*.rbt";
            if (s.ShowDialog() != DialogResult.OK)
                return;
            try
            {
                BinaryWriter bw = new BinaryWriter(File.Open(s.FileName, FileMode.OpenOrCreate));
                cachedAreas[(int)nArea.Value] = null;
                cachedAreas[(int)nArea.Value] = loadTileset((int)nArea.Value);
                bw.Write(decompressor.vramBuffer);
                bw.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Bad file.", "Error");
            }
        }

        private void rawBaseTilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null || decompressor == null)
                return;
            OpenFileDialog o = new OpenFileDialog();
            o.Title = "Import Base Tiles (VRAM)";
            o.Filter = "RBT Files (*.RBT)|*.rbt";
            if (o.ShowDialog() != DialogResult.OK)
                return;

            byte[] buffer;
            try
            {
                BinaryReader br = new BinaryReader(File.OpenRead(o.FileName));
                buffer = br.ReadBytes(4096);
                br.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Bad file.", "Error");
                return;
            }

            if (MessageBox.Show("The new data must be added into the ROM. Is this okay?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                int space = findFreeSpace(4096);
                if (space == -1)
                {
                    MessageBox.Show("Not enough space found. Needs 4KB (0x1000) with LSN unset.", "Not Enough Space");
                    return;
                }

                gb.BufferLocation = space;
                decompressor.vramBuffer = buffer;
                gb.WriteBytes(decompressor.vramBuffer);
                gb.BufferLocation = tilesetBuilder.getHeaderLocation((int)nVRAM.Value, game);
                gb.WriteByte(0xFF);
                gb.WriteByte((byte)nVRAM.Value);
                gb.BufferLocation = 0x101000 + (int)nVRAM.Value * 3;
                gb.WriteByte((byte)(space / 0x4000));
                gb.WriteBytes(new byte[] { (byte)space, (byte)((byte)(space >> 8) + 0x40) });
                MessageBox.Show("Wrote to 0x" + space.ToString("X") + ".", "Success");
            }
            catch (Exception)
            {
                MessageBox.Show("Error converting file.", "Error");
            }

            if (MessageBox.Show("Clear cached images (Tilesets and minimaps. This will cause them to need to be reloaded)?\nIf you choose No, your changes will not show up instantly or for everything.", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                cachedAreas = new Bitmap[256];
                bigMaps = new Bitmap[agesGroupNames.Length];
                cboArea_SelectedIndexChanged(null, null);
            }
            else
            {
                cachedAreas[(int)nArea.Value] = null;
                cachedAreas[(int)nArea.Value] = loadTileset((int)nArea.Value);
            }
        }

        private void dungeonRoomEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null || minimapCreator == null)
                return;

            int mapWidth, mapHeight;
            if (isDungeonLoaded())
            {
                mapWidth = 240;
                mapHeight = 176;
            }
            else {
                mapWidth = 320;
                mapHeight = 256;
            }

            Bitmap b = new Bitmap(mapWidth, mapHeight);
            Graphics g = Graphics.FromImage(b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.DrawImage(bigMaps[cboArea.SelectedIndex], 0, 0, mapWidth, mapHeight);

            frmDungeonRooms f = new frmDungeonRooms(gb, minimapCreator.formationIndexes, b);
            if (f.ShowDialog() != DialogResult.OK)
                return;
            int fl = minimapCreator.getFloor(cboArea.SelectedIndex, game);
            int loc = (game == Program.GameTypes.Ages ? 0x4FCE : 0x4F41) + (fl * 0x40);
            gb.WriteBytes(loc, f.mapOrder);
            if (MessageBox.Show("Re-create minimap image?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bigMaps[cboArea.SelectedIndex] = null;
                cboArea_SelectedIndexChanged(null, null);
            }
        }

        private void weaponVRAMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;

            saveDecompressedTiles(0xA);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;

            if (nMap.Value == 0)
            {
                MessageBox.Show("Map scripts can't be added to map 0.", "Error");
                return;
            }
            byte[] buffer = new byte[0x400000];
            Array.Copy(gb.Buffer, buffer, gb.Buffer.Length);
            frmMapScript f = new frmMapScript(gb, (int)nMap.Value, minimapCreator.getRealMapGroup(cboArea.SelectedIndex, game));
            if (f.ShowDialog() != DialogResult.OK)
            {
                gb.Buffer = buffer;
            }
        }

        private void clearSpawnGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;

            InputBox f = new InputBox("Spawn Group ID:", "Clear Spawn Group", true);
            if (f.ShowDialog() != DialogResult.OK)
                return;
            ushort value = (ushort)f.Value;
            if (value < 0x4000)
            {
                if (MessageBox.Show("Warning: This would not be modifying ROM data outside of bank 0. Continue?", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }
            if (value > 0x7FFF)
            {
                MessageBox.Show("Bad group ID. Unacceptable memory pointer.", "Error");
                return;
            }

            if (value < 0x4000)
                gb.BufferLocation = value;
            else
                gb.BufferLocation = 0x48000 + (byte)value + ((value >> 8) - 0x40) * 0x100;
            byte b;
            while ((b = gb.ReadByte()) != 0xFF && b != 0xFE)
            {
                gb.BufferLocation--;
                gb.WriteByte(0);
            }
            gb.BufferLocation--;
            gb.WriteByte(0);

            MessageBox.Show("Group " + value.ToString("X") + " cleared. Be sure to delete the previous object.\nThe game will crash if you don't.", "Success");
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {

        }

        private void x16OverworldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            Patches.sixteenOverworld(gb);
        }

        private void clearMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;

            int type = (int)mapLoader.room.type;
            if (type == 0)
                mapLoader.room.decompressed = new byte[80];
            else
                mapLoader.room.decompressed = new byte[0xB0];
            //if (game == Program.GameTypes.Ages)
            //	pMap.Image = mapLoader.DrawMap(cachedAreas[(int)nArea.Value], new bool[] { false });
            //else
            updateMap();
        }

        private void toolStripMenuItem7_Click_1(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;

            frmDungeonPortal f = new frmDungeonPortal(gb, game);
            if (f.ShowDialog() != DialogResult.OK)
                return;
            gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x28507 : 0x2491C) + (byte)f.nDungeon.Value * 2;
            gb.WriteByte((byte)f.nMap1.Value);
            gb.WriteByte((byte)f.nMap2.Value);
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            frmGaleSeed f = new frmGaleSeed(gb, bigMaps[0], bigMaps[1], game);
            f.ShowDialog();
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCredits f = new frmCredits();
            f.ShowDialog();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            if (mapLoader == null || pTileset.Image == null)
                return;
            SaveFileDialog s = new SaveFileDialog();
            s.Title = "Export Tileset";
            s.Filter = "PNG Files (*.PNG)|*.png";
            if (s.ShowDialog() != DialogResult.OK)
                return;
            pTileset.Image.Save(s.FileName);
        }

        private void aSMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            Patches.removeStartLock(gb, game);
        }

        private void copyMapDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            int i = (mapLoader.room.type == MapLoader.RoomTypes.Small ? 80 : 0xB0);
            byte[] b = new byte[i + 1];
            b[0] = (byte)mapLoader.room.type;
            Array.Copy(mapLoader.room.decompressed, 0, b, 1, b.Length - 1);
            Clipboard.SetData("mapdata", b);
        }

        private void pasteMapDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            byte[] b = (byte[])Clipboard.GetData("mapdata");
            if (b == null)
            {
                MessageBox.Show("No data exists.", "Error Pasting");
                return;
            }
            byte type = b[0];
            if ((mapLoader.room.type == MapLoader.RoomTypes.Small && type != 0) || (mapLoader.room.type == MapLoader.RoomTypes.Dungeon && type != 1))
            {
                MessageBox.Show("Incompatible map sizes.", "Error Pasting");
                return;
            }
            Array.Copy(b, 1, mapLoader.room.decompressed, 0, b.Length - 1);
            updateMap();
        }

        private void updateMinimapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            bigMaps[cboArea.SelectedIndex] = null;
            cboArea_SelectedIndexChanged(null, null);
        }

        private void fromINIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "ZPS File (*.ZPS)|*.zps";
            o.Title = "Open Patch File";
            if (o.ShowDialog() != DialogResult.OK)
                return;

        }

        private void crystalSwitchesInAllDungeonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            Patches.crystalSwitches(gb);
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;

            if (nMap.Value == 0)
            {
                MessageBox.Show("Map scripts can't be added to map 0.", "Error");
                return;
            }
            byte[] buffer = new byte[0x400000];
            Array.Copy(gb.Buffer, buffer, gb.Buffer.Length);

            frmSecondaryScript f = new frmSecondaryScript(gb, new MapScripts(gb), minimapCreator.getRealMapGroup(cboArea.SelectedIndex, game), (int)nMap.Value);
            if (f.ShowDialog() != DialogResult.OK)
            {
                gb.Buffer = buffer;
            }
        }

        private void reApplyASMPatchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            if (game == Program.GameTypes.Ages)
            {
                mapSaver.writeASM();
            }
            else
            {
                mapSaver.WriteSeasonsASM();
            }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;

            if (nMap.Value == 0)
            {
                MessageBox.Show("Tree tops can't be added to map 0.", "Error");
                return;
            }
            byte[] buffer = new byte[0x400000];
            Array.Copy(gb.Buffer, buffer, gb.Buffer.Length);
            frmTreeTop f = new frmTreeTop(gb, minimapCreator.getRealMapGroup(cboArea.SelectedIndex, game), (int)nMap.Value, new TreeTopLoader(gb));
            if (f.ShowDialog() != DialogResult.OK)
            {
                gb.Buffer = buffer;
            }
        }

        private void chkStaticObjects_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStaticObjects.Checked)
                chkInteractions.Checked = false;
            pMap.SelectedIndex = (int)nInteraction.Value;
            pMap.Invalidate();
        }

        private void nStaticIndex_ValueChanged(object sender, EventArgs e)
        {
            pMap.Invalidate();
            if (nStaticIndex.Value == -1)
            {
                nStaticMap.Enabled = false;
                nStaticUnknown.Enabled = false;
                nStaticFactor.Enabled = false;
                nStaticID.Enabled = false;
                nStaticX.Enabled = false;
                nStaticY.Enabled = false;
                button3.Enabled = false;
                return;
            }
            else
            {
                nStaticMap.Enabled = true;
                nStaticUnknown.Enabled = true;
                nStaticFactor.Enabled = true;
                nStaticID.Enabled = true;
                nStaticX.Enabled = true;
                nStaticY.Enabled = true;
                button3.Enabled = true;
            }
            StaticObjectLoader.StaticObject s = staticObjectLoader.staticObjects[(int)nStaticIndex.Value];
            nStaticMap.Value = s.map;
            nStaticUnknown.Value = s.unknown;
            nStaticFactor.Value = s.unknown2;
            nStaticID.Value = s.id;
            nStaticX.Value = s.x;
            nStaticY.Value = s.y;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!staticObjectLoader.addStaticObject(MinimapCreator.GetDungeon(cboArea.SelectedIndex, mapLoader.room.area.flags1, game), (int)nMap.Value))
            {
                MessageBox.Show("Not enough space.", "Error");
                return;
            }
            nStaticIndex.Maximum++;
            nStaticIndex.Value = nStaticIndex.Maximum;
            pMap.Invalidate();
        }

        private void nStaticMap_ValueChanged(object sender, EventArgs e)
        {
            if (nStaticIndex.Value == -1)
                return;
            StaticObjectLoader.StaticObject s = staticObjectLoader.staticObjects[(int)nStaticIndex.Value];
            s.map = (byte)nStaticMap.Value;
            staticObjectLoader.staticObjects[(int)nStaticIndex.Value] = s;
        }

        private void nStaticUnknown_ValueChanged(object sender, EventArgs e)
        {
            if (nStaticIndex.Value == -1)
                return;
            StaticObjectLoader.StaticObject s = staticObjectLoader.staticObjects[(int)nStaticIndex.Value];
            s.unknown = (byte)nStaticUnknown.Value;
            staticObjectLoader.staticObjects[(int)nStaticIndex.Value] = s;
        }

        private void nStaticID_ValueChanged(object sender, EventArgs e)
        {
            if (nStaticIndex.Value == -1)
                return;
            StaticObjectLoader.StaticObject s = staticObjectLoader.staticObjects[(int)nStaticIndex.Value];
            s.id = (byte)nStaticID.Value;
            staticObjectLoader.staticObjects[(int)nStaticIndex.Value] = s;
        }

        private void nStaticFactor_ValueChanged(object sender, EventArgs e)
        {
            if (nStaticIndex.Value == -1)
                return;
            StaticObjectLoader.StaticObject s = staticObjectLoader.staticObjects[(int)nStaticIndex.Value];
            s.unknown2 = (byte)nStaticFactor.Value;
            staticObjectLoader.staticObjects[(int)nStaticIndex.Value] = s;
        }

        private void nStaticX_ValueChanged(object sender, EventArgs e)
        {
            if (nStaticIndex.Value == -1)
                return;
            StaticObjectLoader.StaticObject s = staticObjectLoader.staticObjects[(int)nStaticIndex.Value];
            s.x = (byte)nStaticX.Value;
            staticObjectLoader.staticObjects[(int)nStaticIndex.Value] = s;
        }

        private void nStaticY_ValueChanged(object sender, EventArgs e)
        {
            if (nStaticIndex.Value == -1)
                return;
            StaticObjectLoader.StaticObject s = staticObjectLoader.staticObjects[(int)nStaticIndex.Value];
            s.y = (byte)nStaticY.Value;
            staticObjectLoader.staticObjects[(int)nStaticIndex.Value] = s;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (nStaticIndex.Value == -1)
                return;
            staticObjectLoader.deleteStaticObject(MinimapCreator.GetDungeon(cboArea.SelectedIndex, mapLoader.room.area.flags1, game), (int)nStaticIndex.Value);
            if (nStaticIndex.Value == nStaticIndex.Maximum)
                nStaticIndex.Value--;
            nStaticIndex.Maximum--;
            pMap.Invalidate();
        }

        private void chestsPerOverworldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            ChestLoader cl = new ChestLoader(gb, game);
            cl.repointChests();
        }

        private void currentPaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null || paletteLoader == null)
                return;
            SaveFileDialog s = new SaveFileDialog();
            s.Title = "Export Current Palette";
            s.Filter = "Zelda Palette Files (*.zpf)|*.zpf";
            if (s.ShowDialog() != DialogResult.OK)
                return;
            frmExportPalette f = new frmExportPalette();
            if (f.ShowDialog() != DialogResult.OK)
                return;
            int option = (f.rbRows.Checked ? 1 : 0);
            int row1 = (int)f.nRow1.Value;
            int row2 = (int)f.nRow2.Value;
            Color[,] palette = paletteLoader.LoadPalette((int)nPalette.Value, game);
            try
            {
                BinaryWriter bw = new BinaryWriter(File.Open(s.FileName, FileMode.OpenOrCreate));
                GBHL.GBFile output = new GBHL.GBFile(new byte[100]);
                output.WriteByte((byte)option);
                if (option == 1)
                {
                    output.WriteByte((byte)(row2 + (row1 << 4)));
                }
                for (int i = row1; i < row2 + 1; i++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        int value = (ushort)PaletteLoader.getHexValue((int)(palette[i, k].R / 8), (int)(palette[i, k].G / 8), (int)(palette[i, k].B / 8));
                        output.WriteBytes(new byte[] { (byte)(value & 0xFF), (byte)(value >> 8) });
                    }
                }
                bw.Write(output.Buffer, 0, output.BufferLocation);
                bw.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error saving file.\n\n" + ex.Message, "Error");
            }
        }

        private void paletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null || paletteLoader == null)
                return;
            OpenFileDialog o = new OpenFileDialog();
            o.Title = "Import Palette";
            o.Filter = "Zelda Palette Files (*.zpf)|*.zpf";
            if (o.ShowDialog() != DialogResult.OK)
                return;

            GBHL.GBFile input;
            Color[,] pal = new Color[8, 4];
            int row1 = 2;
            int row2 = 7;
            try
            {
                BinaryReader br = new BinaryReader(File.OpenRead(o.FileName));
                byte[] buffer = br.ReadBytes((int)br.BaseStream.Length);
                br.Close();
                input = new GBHL.GBFile(buffer);
                byte option = input.ReadByte();
                if (option == 1)
                {
                    byte b = input.ReadByte();
                    row1 = b >> 4;
                    row2 = b & 0xF;
                }
                for (int i = row1; i < row2 + 1; i++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        pal[i, k] = input.GetRealColor(input.BufferLocation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error importing palette.\n\n" + ex.Message, "Error");
                return;
            }

            gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x5C8E0 : 0x58830);

            int baseColorAddress = (game == Program.GameTypes.Ages ? (0x17 * 0x4000) : (0x16 * 0x4000));
            gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x632C : 0x6290);
            gb.BufferLocation += (int)nPalette.Value * 2;
            gb.BufferLocation = gb.ReadByte() + gb.ReadByte() * 0x100;
            byte a = gb.ReadByte();
            if ((a & 7) + 1 == 6)
            {
                gb.BufferLocation = baseColorAddress + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
                gb.BufferLocation += (row1 - 2) * 8;
                for (int k = row1 - 2; k < row2 - 1; k++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        int value = (ushort)PaletteLoader.getHexValue((int)(pal[k + 2, i].R / 8), (int)(pal[k + 2, i].G / 8), (int)(pal[k + 2, i].B / 8));
                        gb.WriteBytes(new byte[] { (byte)(value & 0xFF), (byte)(value >> 8) });
                    }
                }
            }

            if (MessageBox.Show("Clear cached images (Tilesets and minimaps. This will cause them to need to be reloaded)?\nIf you choose No, your changes will not show up instantly or for everything.", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                cachedAreas = new Bitmap[256];
                bigMaps = new Bitmap[agesGroupNames.Length];
                cboArea_SelectedIndexChanged(null, null);
            }
            else
            {
                cachedAreas[(int)nArea.Value] = null;
                cachedAreas[(int)nArea.Value] = loadTileset((int)nArea.Value);
            }
            LoadMap((int)nMap.Value, cboArea.SelectedIndex);
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            if (mapLoader == null || transitionLoader == null)
                return;
            TransitionLoader tl = transitionLoader;
            frmTransitions f = new frmTransitions(transitionLoader, gb, game);
            for (int i = 0; i < transitionLoader.transitions.Count; i++)
            {
                if (transitionLoader.transitions[i].map == (int)nMap.Value)
                {
                    f.nIndex.Value = i;
                    break;
                }
            }
            if (f.ShowDialog() != DialogResult.OK)
            {
                transitionLoader = tl;
                return;
            }
            transitionLoader = f.transitionLoader;
            transitionLoader.SaveTransitions(cboArea.SelectedIndex);
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            frmAreaFlags f = new frmAreaFlags(gb, game, mapLoader.areaLoader, (int)nArea.Value);
            if (f.ShowDialog() != DialogResult.OK)
                return;
            int flag = 0;
            if (f.checkBox1.Checked)
                flag |= 1;
            if (f.checkBox2.Checked)
                flag |= 2;
            if (f.checkBox3.Checked)
                flag |= 4;
            if (f.checkBox4.Checked)
                flag |= 8;
            if (f.checkBox5.Checked)
                flag |= 16;
            if (f.checkBox7.Checked)
                flag |= 32;
            if (f.checkBox8.Checked)
                flag |= 64;
            if (f.checkBox9.Checked)
                flag |= 128;
            int indexBase = 0x10000 + (game == Program.GameTypes.Ages ? 0x0F9C : 0x0C84);
            if (gb.ReadByte(indexBase) == 0xFF)
            {
                indexBase = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            }
            gb.BufferLocation = indexBase + (int)f.nArea.Value * 8;
            byte b = gb.ReadByte();
            gb.BufferLocation--;
            gb.WriteByte((byte)(((b >> 4) << 4) + (byte)f.nDungeon.Value));
            gb.WriteByte((byte)flag);
        }

        private void toolStripButton15_Click(object sender, EventArgs args)
        {
            if (mapLoader == null)
                return;
            EssenceTeleportLoader e = new EssenceTeleportLoader();
            e.LoadTeleports(gb);
            frmEssenceTeleport f = new frmEssenceTeleport(e.teleports);
            if (f.ShowDialog() != DialogResult.OK)
                return;
            e.SaveTeleports(gb, f.Teleports);
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            frmEnemyEditor f = new frmEnemyEditor(enemyLoader);
            f.ShowDialog();
        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {
            toolStripButton17_Click(null, null);
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            frmTilesetEditor f = new frmTilesetEditor(gb, this);
            f.AutoScaleMode = AutoScaleMode.None;
            if (f.ShowDialog() != DialogResult.OK)
                return;
            if (MessageBox.Show("Clear cached images?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                cachedAreas = new Bitmap[256];
                bigMaps[cboArea.SelectedIndex] = null;
                cboArea_SelectedIndexChanged(null, null);
                return;
            }
            cachedAreas[(int)nArea.Value] = null;
            pTileset.Image = loadTileset((int)nArea.Value);
            updateMap();
        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {
            int indexBase = 0x10000 + (game == Program.GameTypes.Ages ? 0x0F9C : 0x0C84);
            int i = (int)nArea.Value;
            byte[, ,] tiles = new byte[256, 8, 8];
            AreaLoader.Area a = mapLoader.areaLoader.loadArea(indexBase + i * 8, i, game);
            decompressor.loadTileset(tilesetBuilder.getHeaderLocation((int)nUnique.Value, game), (int)nUnique.Value, a.animation, game);
            int space = 0x181000 + i * 0x1000;
            gb.WriteBytes(space, decompressor.decompressedBuffer);
            gb.WriteByte(0x180000 + i * 3, (byte)(space / 0x4000));
            gb.WriteByte((byte)space);
            gb.WriteByte((byte)(((space % 0x4000) >> 8) + 0x40));

            a.tileset &= 0x7F;
            tilesetBuilder.loadTileset(a.tileset, game);
            space = 0x201000 + a.tileset * 2048;
            gb.BufferLocation = 0x200000 + a.tileset * 3;
            gb.WriteByte((byte)(space / 0x4000));
            gb.WriteByte((byte)space);
            gb.WriteByte((byte)(((space % 0x4000) >> 8) + 0x40));
            gb.BufferLocation = 0x201000 + a.tileset * 2048;
            for (int k = 0; k < 2048; k++)
                gb.WriteByte(tilesetBuilder.assemblyData[0xD000 + k]);

            updateMap();
        }

        private void zOSEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Launch("ZOSE");
        }

        private void hEXEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchHex();
        }

        private void zOTEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Due to ZOTE requiring an older version of GHBL.dll, ZOTE cannot be launched from ZOLE 4.5.  Please save your ROM and close ZOLE 4.5, then launch ZOTE from its separate folder and open your ROM.", "ZOLE 4.5", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Launch(string name)
        {
            if (filename == "" || mapLoader == null)
                return;
            if (!File.Exists(Application.StartupPath + "/" + name + ".exe"))
            {
                MessageBox.Show(name + ".exe not found.");
                return;
            }
            System.Diagnostics.ProcessStartInfo s = new System.Diagnostics.ProcessStartInfo(Application.StartupPath + "/" + name + ".exe", filename);
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo = s;
            p.Start();
            frmLaunch f = new frmLaunch(name);
            f.ShowDialog();
            LoadROM(filename);
        }

        private void LaunchHex()
        {
            if (filename == "" || mapLoader == null)
                return;
            if (!File.Exists(hexEditor))
            {
                MessageBox.Show(hexEditor + "Your HEX Editor wasn't found, link it to ZOLE with the Settings tab.");
                return;
            }
            System.Diagnostics.ProcessStartInfo s = new System.Diagnostics.ProcessStartInfo(hexEditor, filename);
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo = s;
            p.Start();
            frmLaunch f = new frmLaunch("your HEX Editor");
            f.ShowDialog();
            LoadROM(filename);
        }

        private void zOCFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Launch("ZOCF");
        }

        private void removeBeginningLocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null || filename == "")
                return;
            Patches.removeStartLock(gb, game);
            if (game == Program.GameTypes.Seasons)
                MessageBox.Show("Note: You must remove the type 1 object in map 97 for this patch to work.", "Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripMenuItem32_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            gb.WriteByte(0x126F8, 0xC9);
        }

        private void instantlyAwakeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            gb.WriteByte(0x7DD9, 0);
        }

        private void toolStripMenuItem33_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            frmSign f = new frmSign(gb, pMap.Image, minimapCreator.getRealMapGroup(cboArea.SelectedIndex, game), (int)nMap.Value, new Bitmap(toolStripMenuItem33.Image));
            if (f.ShowDialog() != DialogResult.OK)
                return;
            f.signLoader.SaveSigns(minimapCreator.getRealMapGroup(cboArea.SelectedIndex, game));
        }

        private void createBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            string[] parts = filename.Split('\\');
            string directory = "";
            for (int i = 0; i < parts.Length - 1; i++)
                directory += parts[i] + "\\";
            string Filename = parts[parts.Length - 1].Split('.')[0];
            string path = directory + Filename + " Backup " + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".gbc";
            BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate));
            bw.Write(gb.Buffer);
            bw.Close();
        }

        private void toolStripMenuItem37_Click(object sender, EventArgs e)
        {
            if (mapLoader == null || game != Program.GameTypes.Ages)
                return;
            frmDecompressTileset f = new frmDecompressTileset(this, (int)nArea.Value);
            if (f.ShowDialog() != DialogResult.OK)
                return;

            int i = (int)nArea.Value;
            byte[, ,] tiles = new byte[256, 8, 8];
            decompressor.loadTileset(tilesetBuilder.getHeaderLocation((int)f.nVRAM.Value, game), (int)f.nUnique.Value, (int)f.nAnimation.Value, game);
            int space = 0x181000 + i * 0x1000;
            gb.WriteBytes(space, decompressor.decompressedBuffer);
            gb.WriteByte(0x180000 + i * 3, (byte)(space / 0x4000));
            gb.WriteByte((byte)space);
            gb.WriteByte((byte)(((space % 0x4000) >> 8) + 0x40));

            f.nTileset.Value = (int)f.nTileset.Value & 0x7F;
            tilesetBuilder.loadTileset((int)f.nTileset.Value, game);
            space = 0x201000 + (int)f.nTileset.Value * 2048;
            gb.BufferLocation = 0x200000 + (int)f.nTileset.Value * 3;
            gb.WriteByte((byte)(space / 0x4000));
            gb.WriteByte((byte)space);
            gb.WriteByte((byte)(((space % 0x4000) >> 8) + 0x40));
            gb.BufferLocation = 0x201000 + (int)f.nTileset.Value * 2048;
            for (int k = 0; k < 2048; k++)
                gb.WriteByte(tilesetBuilder.assemblyData[0xD000 + k]);

            mapSaver.saveAreaData(i, (int)f.nVRAM.Value, (int)f.nTileset.Value, (int)f.nUnique.Value, (int)f.nAnimation.Value, (int)f.nPalette.Value, -1, game);

            if (MessageBox.Show("Clear cached images?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                cachedAreas = new Bitmap[256];
                bigMaps[cboArea.SelectedIndex] = null;
                cboArea_SelectedIndexChanged(null, null);
                return;
            }
            cachedAreas[(int)nArea.Value] = null;
            pTileset.Image = loadTileset((int)nArea.Value);
            updateMap();
        }

        private void toolStripMenuItem38_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;

            int mapWidth, mapHeight;
            if (isDungeonLoaded())
            {
                mapWidth = 240;
                mapHeight = 176;
            }
            else {
                mapWidth = 320;
                mapHeight = 256;
            }

            Bitmap b = new Bitmap(mapWidth, mapHeight);
            Graphics g = Graphics.FromImage(b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.DrawImage(bigMaps[cboArea.SelectedIndex], 0, 0, mapWidth, mapHeight);

            frmDungeonMinimapEditor f = new frmDungeonMinimapEditor(gb, b, minimapCreator.formationIndexes, minimapCreator.getRealMapGroup(cboArea.SelectedIndex, game));
            if (f.ShowDialog() != DialogResult.OK)
                return;
        }

        private void toolStripMenuItem39_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            frmRepointInteractions f = new frmRepointInteractions(gb, interactionLoader, interactionLoader.loadedMap, interactionLoader.loadedGroup, game);
            if (f.ShowDialog() != DialogResult.OK)
                return;

            if ((int)f.nAddress.Value == interactionLoader.getInteractionLocation())
                return;

            interactionLoader.repointInteractions((int)f.nAddress.Value);
            if (f.chkCopy.Checked)
            {
                interactionLoader.saveInteractions();
            }

            interactionLoader.loadInteractions(interactionLoader.loadedMap, interactionLoader.loadedGroup);
            lblInteraction.Text = "Objects: 0x" + interactionLoader.getInteractionLocation().ToString("X");
            nInteraction.Maximum = interactionLoader.interactions.Count - 1;
            selectInteraction(-1);
            updateMap();
        }

        private void goToZeldaHackingNETToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://wiki.zeldahacking.net/");
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "The web browser failed to open.\n\nTry typing \"wiki.zeldahacking.net\" into the address bar.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void toolStripButton32_Click(object sender, EventArgs e)
        {
            frmZeldaHacking ZH = new frmZeldaHacking();
            ZH.Show();
        }

        private void helpToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            frmHelp Help = new frmHelp();
            Help.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout About = new frmAbout();
            About.Show();
        }

        private void enemyIDsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            femEnemyIDs Enemies = new femEnemyIDs();
            Enemies.Show();
        }

        private void chestIDsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frnChestIDs Chests = new frnChestIDs();
            Chests.Show();
        }

        private void openSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog oh = new OpenFileDialog();
            oh.Title = "Select HEX Editor";
            oh.Filter = "All Supported Types|*.exe";
            if (oh.ShowDialog() != DialogResult.OK)
                return;
            hexEditor = oh.FileName;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove("HexEditor");
            config.AppSettings.Settings.Add("HexEditor", hexEditor);
            config.Save();

            //ConfigurationManager.AppSettings["HexEditor"] = hexEditor;
            //ConfigurationManager.AppSettings.Set("HexEditor", oh.FileName);
            //hexEditor = ConfigurationManager.AppSettings.Get("HexEditor");
        }

        private void memoryAddressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMemoryAddresses MA = new frmMemoryAddresses();
            MA.Show();
        }

        private void multipleObjectsInOneTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMultiObjects Multi = new frmMultiObjects();
            Multi.Show();
        }

        private void musicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMusicList Music = new frmMusicList();
            Music.Show();
        }

        private void ringsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRingList Rings = new frmRingList();
            Rings.Show();
        }

        private void soundEffectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSoundsList Sounds = new frmSoundsList();
            Sounds.Show();
        }

        private void zOSECommandsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmZOSE SE = new frmZOSE();
            SE.Show();
        }

        private void zOTEValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmZOTE TE = new frmZOTE();
            TE.Show();
        }

        private void memoryAddressesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmMemoryAddresses MA = new frmMemoryAddresses();
            MA.Show();
        }

        private void editingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mapGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMapGroups MG = new frmMapGroups();
            MG.Show();
        }

        private void mapZoom_Click(object sender, EventArgs e)
        {
            mapZoom = !mapZoom;
            if (mapLoader == null)
                return;
            bigMaps[cboArea.SelectedIndex] = null;
            cboArea_SelectedIndexChanged(null, null);
        }

        // Ages patch
        private void extraBankForInteractionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            Patches.ExtraInteractionBank(gb, game);
            interactionLoader.enableExtraInteractionBank();
        }

        // Seasons patch
        private void extraInteractionBankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLoader == null)
                return;
            Patches.ExtraInteractionBank(gb, game);
            interactionLoader.enableExtraInteractionBank();
        }

        private void pMinimap_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                panel1.AutoScrollPosition = new Point(
                    -panel1.AutoScrollPosition.X + (mouseX - Cursor.Position.X),
                    -panel1.AutoScrollPosition.Y + (mouseY - Cursor.Position.Y)
                );
            }

            mouseX = Cursor.Position.X;
            mouseY = Cursor.Position.Y;
        }

        private void tabsSecondary_Selected(object sender, TabControlEventArgs e)
        {
            if (tabsSecondary.SelectedIndex == 0)
                chkInteractions.Checked = false;
            else
                chkInteractions.Checked = true;
        }
    }
}
