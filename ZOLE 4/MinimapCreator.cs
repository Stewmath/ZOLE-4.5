using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;

namespace ZOLE_4
{
    public class MinimapCreator
    {
        public GBHL.GBFile gb;
        public byte[] formationIndexes;
        public byte[] formation26Ages;
        public byte[] formation27Ages;
        public byte[] formation27Seasons;
        public byte[] formation28Seasons;
        bool[] flags = new bool[] { false };

        public MinimapCreator(GBHL.GBFile g)
        {
            gb = g;
        }

        public int getRealMapGroup(int groupIndex, Program.GameTypes game)
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

        public int getFloor(int groupIndex, Program.GameTypes game)
        {
            if (groupIndex < (game == Program.GameTypes.Ages ? 4 : 5))
                return -1;
            groupIndex -= (game == Program.GameTypes.Ages ? 4 : 5);
            return groupIndex;
        }

        public int getGroupSize(int groupIndex, Program.GameTypes game)
        {
            if (getFloor(groupIndex, game) == -1)
                return 16;
            return 8;
        }

        public bool dungeon(int groupIndex, Program.GameTypes game)
        {
            if (game == Program.GameTypes.Seasons)
            {
                if (groupIndex >= 5)
                    return true;
                return false;
            }
            if (groupIndex >= 4)
                return true;
            return false;
        }

        public byte[] getMinimapIndexes(int groupIndex, Program.GameTypes game)
        {
            if (!dungeon(groupIndex, game))
                return null;
            int f = getFloor(groupIndex, game);
            int loc = (game == Program.GameTypes.Ages ? 0x4FCE : 0x4F41) + (f * 0x40);
            gb.BufferLocation = loc;
            return gb.ReadBytes(0x40);
        }

        public int drawMinimap(int groupIndex, bool realMinimap, Form1 form, Program.GameTypes game)
        {
            formationIndexes = getMinimapIndexes(groupIndex, game);
            int realGroup = getRealMapGroup(groupIndex, game);
            Bitmap drawnBitmap = null;
            if (form.bigMaps[groupIndex] != null)
            {
                if (game == Program.GameTypes.Ages)
                {
                    if (groupIndex == 0x1E && formation26Ages != null)
                        formationIndexes = formation26Ages;
                    else if (groupIndex == 0x1F && formation27Ages != null)
                        formationIndexes = formation27Ages;
                    if (formationIndexes != null)
                    {
                        for (int i = 0; i < 64; i++)
                        {
                            if (formationIndexes[i] != 0)
                                return formationIndexes[i];
                        }
                    }
                }
                else
                {
                    if (groupIndex == 0x1B && formation27Seasons != null)
                        formationIndexes = formation27Seasons;
                    else if (groupIndex == 0x1C && formation28Seasons != null)
                        formationIndexes = formation28Seasons;
                    if (formationIndexes != null)
                    {
                        for (int i = 0; i < 64; i++)
                        {
                            if (formationIndexes[i] != 0)
                                return formationIndexes[i];
                        }
                    }
                }
                if ((game == Program.GameTypes.Ages && groupIndex < 26) || (game == Program.GameTypes.Seasons && groupIndex < 27))
                {
                    if (formationIndexes == null) return 0;
                    for (int i = 0; i < 64; i++)
                    {
                        if (formationIndexes[i] != 0)
                            return formationIndexes[i];
                    }
                }
                return 0;
            }
            if (formationIndexes == null) //Small maps
            {
                drawnBitmap = new Bitmap(16 * 160, 16 * 128);
                Graphics g = Graphics.FromImage(drawnBitmap);
                if (!realMinimap)
                {
                    for (int y = 0; y < 16; y++)
                    {
                        for (int x = 0; x < 16; x++)
                        {
                            Bitmap tileset = form.loadTileset(x + y * 16, realGroup, (groupIndex & 7));
                            g.DrawImageUnscaled(form.mapLoader.DrawMap(tileset, new bool[] {false}), x * 160, y * 128);
                            form.frmExport.setValue(x + y * 16 + 1, 256);
                        }
                    }
                }
            }
            else
            {
                drawnBitmap = new Bitmap(8 * 240, 8 * 176);
                Graphics g = Graphics.FromImage(drawnBitmap);
                if (!realMinimap)
                {
                    if (getFloor(groupIndex, game) < (game == Program.GameTypes.Ages ? 26 : 22))
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                Bitmap tileset = form.loadTileset(formationIndexes[x + y * 8], realGroup, (groupIndex & 7));
                                g.DrawImageUnscaled(form.mapLoader.DrawMap(tileset, flags), x * 240, y * 176);
                                form.frmExport.setValue(x + y * 8 + 1, 64);
                            }
                        }
                    }
                    else if (game == Program.GameTypes.Ages)
                    {
                        if (getFloor(groupIndex, game) == 26)
                        {
                            formationIndexes = new byte[64];
                            for (int y = 0; y < 8; y++)
                            {
                                for (int x = 0; x < 8; x++)
                                {
                                    if (x < 5 && y == 0)
                                    {
                                        formationIndexes[x] = (byte)(x + 0xD0);
                                        Bitmap tileset = form.loadTileset(0xD0 + x, realGroup, (groupIndex & 7));
                                        g.DrawImageUnscaled(form.mapLoader.DrawMap(tileset, flags), x * 240, y * 176);
                                    }
                                    else
                                    {
                                        Bitmap tileset = form.loadTileset(0, realGroup, (groupIndex & 7));
                                        g.DrawImageUnscaled(form.mapLoader.DrawMap(tileset, flags), x * 240, y * 176);
                                    }
                                    form.frmExport.setValue(x + y * 8 + 1, 64);
                                }
                            }
                            formation26Ages = formationIndexes;
                        }
                        else
                        {
                            formationIndexes = new byte[64];
                            for (int y = 0; y < 8; y++)
                            {
                                for (int x = 0; x < 8; x++)
                                {
                                    if (y == 0)
                                    {
                                        formationIndexes[x] = (byte)(x);
                                        Bitmap tileset = form.loadTileset(formationIndexes[x + y * 8], realGroup, (groupIndex & 7));
                                        g.DrawImageUnscaled(form.mapLoader.DrawMap(tileset, flags), x * 240, y * 176);
                                    }
                                    else if (y == 1 && x < 3)
                                    {
                                        formationIndexes[x + y * 8] = (byte)(x + y * 8);
                                        Bitmap tileset = form.loadTileset(formationIndexes[x + y * 8], realGroup, (groupIndex & 7));
                                        g.DrawImageUnscaled(form.mapLoader.DrawMap(tileset, flags), x * 240, y * 176);
                                    }
                                    else if (y == 2 && x < 4)
                                    {
                                        formationIndexes[x + y * 8] = (byte)(0xAB + x);
                                        Bitmap tileset = form.loadTileset(formationIndexes[x + y * 8], realGroup, (groupIndex & 7));
                                        g.DrawImageUnscaled(form.mapLoader.DrawMap(tileset, flags), x * 240, y * 176);
                                    }
                                    else if (y == 3 && x < 6)
                                    {
                                        formationIndexes[x + y * 8] = (byte)(0xF6 + x);
                                        Bitmap tileset = form.loadTileset(formationIndexes[x + y * 8], realGroup, (groupIndex & 7));
                                        g.DrawImageUnscaled(form.mapLoader.DrawMap(tileset, flags), x * 240, y * 176);
                                    }
                                    else
                                    {
                                        Bitmap tileset = form.loadTileset(0, realGroup, (groupIndex & 7));
                                        g.DrawImageUnscaled(form.mapLoader.DrawMap(tileset, flags), x * 240, y * 176);
                                    }
                                    form.frmExport.setValue(x + y * 8 + 1, 64);
                                }
                            }
                            formation27Ages = formationIndexes;
                        }
                    }

                    else

                    {
                        if (getFloor(groupIndex, game) == 22)
                        {
                            formationIndexes = new byte[64];
                            for (int y = 0; y < 8; y++)
                            {
                                for (int x = 0; x < 8; x++)
                                {
                                    if (y < 2)
                                    {
                                        formationIndexes[x + y * 8] = (byte)(x + y * 8 + 0xE0);
                                        Bitmap tileset = form.loadTileset(x + y * 8 + 0xE0, realGroup, (groupIndex & 7));
                                        g.DrawImageUnscaled(form.mapLoader.DrawMap(tileset, flags), x * 240, y * 176);
                                    }
                                    else
                                    {
                                        Bitmap tileset = form.loadTileset(0, realGroup, (groupIndex & 7));
                                        g.DrawImageUnscaled(form.mapLoader.DrawMap(tileset, flags), x * 240, y * 176);
                                    }
                                    form.frmExport.setValue(x + y * 8 + 1, 64);
                                }
                            }
                            formation27Seasons = formationIndexes;
                        }
                        else
                        {
                            formationIndexes = new byte[64];
                            for (int y = 0; y < 8; y++)
                            {
                                for (int x = 0; x < 8; x++)
                                {
                                    if (y < 2)
                                    {
                                        formationIndexes[x + y * 8] = (byte)(x + y * 8);
                                        Bitmap tileset = form.loadTileset(x + y * 8, realGroup, (groupIndex & 7));
                                        g.DrawImageUnscaled(form.mapLoader.DrawMap(tileset, flags), x * 240, y * 176);
                                    }
                                    else if (y == 2 && x < 3)
                                    {
                                        formationIndexes[x + y * 8] = (byte)(x + 0x10);
                                        Bitmap tileset = form.loadTileset(x + y * 8, realGroup, (groupIndex & 7));
                                        g.DrawImageUnscaled(form.mapLoader.DrawMap(tileset, flags), x * 240, y * 176);
                                    }
                                    else if (y > 2)
                                    {
                                        formationIndexes[x + y * 8] = (byte)(x + y * 8 + 0x98);
                                        Bitmap tileset = form.loadTileset(x + y * 8 + 0x98, realGroup, (groupIndex & 7));
                                        g.DrawImageUnscaled(form.mapLoader.DrawMap(tileset, flags), x * 240, y * 176);
                                    }
                                    else
                                    {
                                        Bitmap tileset = form.loadTileset(0, realGroup, (groupIndex & 7));
                                        g.DrawImageUnscaled(form.mapLoader.DrawMap(tileset, flags), x * 240, y * 176);
                                    }
                                    form.frmExport.setValue(x + y * 8 + 1, 64);
                                }
                            }
                            formation28Seasons = formationIndexes;
                        }
                    }
                }
            }

            form.bigMaps[groupIndex] = drawnBitmap;
            if (formationIndexes == null) return 0;
            for (int i = 0; i < 64; i++)
            {
                if (formationIndexes[i] != 0)
                    return formationIndexes[i];
            }
            return 0;
        }

        public Point getMapIndexPoint(int index, int groupIndex)
        {
            if (formationIndexes == null)
                return new Point(index % 16, index * 16);
            for (int i = 0; i < 64; i++)
            {
                if (formationIndexes[i] == index)
                    return new Point(i % 8, i / 8);
            }
            return new Point(0, 0);
        }

        public static int GetDungeon(int group, int areaFlag, Program.GameTypes game)
        {
            if (game == Program.GameTypes.Seasons)
            {
                if (group < 5)
                    return 0xFF;
                return areaFlag & 0xF;
            }
            if (group < 4)
                return 0xFF;
            return areaFlag & 0xF;
            /*if (game == Program.GameTypes.Seasons)
            {
                return 0;
            }
            if (index < 4)
                return 0xFF; //The game has it as FF if in no dungeon
            index -= 4;
            switch (index)
            {
                case 0: return 0;
                case 1: return 0xD;
                case 2: return 1;
                case 3: return 2;
                case 4: return 2;
                case 5: return 3;
                case 6: return 3;
                case 7: return 4;
                case 8: return 4;
                case 9: return 5;
                case 10: return 5;
                case 11: return 0xB;
                case 12: return 0xB;
                case 13: return 0xF;
                case 14: return 6;
                case 15: return 0xC;
                case 16: return 0xC;
                case 17: return 7;
                case 18: return 7;
                case 19: return 7;
                case 20: return 8;
                case 21: return 8;
                case 22: return 8;
                case 23: return 8;
                case 24: return 0xE;
                case 25: return 0xA;
            }
            return 0xFF;*/
        }
    }
}
