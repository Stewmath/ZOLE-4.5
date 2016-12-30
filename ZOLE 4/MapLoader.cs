using System;
using System.Collections.Generic;

using System.Text;
using GBHL;
using System.Drawing;

namespace ZOLE_4
{
    public class MapLoader
    {
        private GBFile gb;
        public AreaLoader areaLoader;

        public struct OverworldBlock
        {
            public byte id;
            public byte x;
            public byte y;
            public byte set;
            public byte type;
        }
        public struct DungeonBlock
        {
            public byte id;
            public byte x;
            public byte y;
            public byte size;
            public byte set;
            public byte type;
        }

        public MapLoader(GBFile g)
        {
            gb = g;
            areaLoader = new AreaLoader(g);
        }

        public enum RoomTypes
        {
            Small=0,
            Dungeon=1
        }

        public struct Room
        {
            public byte[] decompressed;
            public OverworldBlock[] overworld;
            public DungeonBlock[] dungeon;
            public byte[] common;
            public bool[] bitsas1;
            public AreaLoader.Area area;
            public RoomTypes type;
            public byte compressionType;
            public byte minorBank;
            public byte bank;
            public int relativeDataPointerM;
            public int relativeDataPointer;
            public int dataLocation;
            public int dictionaryLocation;
        }

        public Room room;

        public void LoadSmallMap()
        {
            int ind = 0;
            room.decompressed = new byte[80];
            room.overworld = new OverworldBlock[80];
            int compression = room.compressionType;

            gb.BufferLocation = room.dataLocation;
            //Compression types
            //2 - 16-bit
            //1 - 8-bit
            //0 - uncompressed

            if (compression != 0)
            {
                byte filler = 0;
                while (ind < 80)
                {
                    room.overworld[ind] = new OverworldBlock();
                    int bitmap = gb.ReadByte() + (compression == 2 ? (gb.ReadByte() << 8) : 0);
                    if (bitmap != 0)
                        filler = gb.ReadByte();
                    room.overworld[ind].x = (byte)(ind % 10);
                    room.overworld[ind].y = (byte)(ind / 10);
                    room.overworld[ind].set = (byte)((compression == 2 ? ind / 16 : ind / 8));
                    for (int i = 0; i < (compression == 2 ? 16 : 8); i++)
                    {
                        if ((bitmap & 1) == 1)
                        {
                            room.decompressed[ind] = filler;
                            room.overworld[ind].id = filler;
                            room.overworld[ind].type = 1;
                        }
                        else
                        {
                            room.decompressed[ind] = gb.ReadByte();
                            room.overworld[ind].id = room.decompressed[ind];
                        }
                        bitmap >>= 1;
                        ind++;
                    }
                }
            }
            else
            {
                room.decompressed = gb.ReadBytes(80);
                for (int i = 0; i < 80; i++)
                {
                    room.overworld[i] = new OverworldBlock();
                    room.overworld[i].id = room.decompressed[i];
                    room.overworld[i].x = (byte)(i % 10);
                    room.overworld[i].y = (byte)(i / 10);
                }
            }
        }

        public void LoadDungeonMap()
        {
            room.decompressed = new byte[176]; //the appearance is actually 15x11, but the game uses 0xb0 (16x11) for some reason.
            //3a27
            gb.BufferLocation = room.dataLocation; //(room.dataLocation % 0x4000) + room.bank * 0x4000;
            byte[] ce = new byte[0xB0]; //compressed data...?
            for (int i = 0; i < 0xB0; i++)
            {
                ce[i] = gb.ReadByte();
            }
            int srcIndex = 0;
            int destIndex = 0;
            byte left = 1;
            byte b = 0;
            room.dungeon = new DungeonBlock[176];
            while (destIndex < 176)
            {
                //ld b,8
                left--;
                b >>= 1;
                room.dungeon[destIndex] = new DungeonBlock();
                room.dungeon[destIndex].set = (byte)((destIndex - 1) / 8);
                if (left == 0)
                {
                    left = 8;
                    b = ce[srcIndex++];
                }
                if ((b & 1) == 0)
                {
                    room.dungeon[destIndex].x = (byte)(destIndex % 16);
                    room.dungeon[destIndex].y = (byte)(destIndex / 16);
                    room.decompressed[destIndex++] = ce[srcIndex++];
                    room.dungeon[destIndex - 1].id = room.decompressed[destIndex - 1];
                    room.dungeon[destIndex - 1].size = 1;
                    room.dungeon[destIndex - 1].type = 0;
                    if (destIndex == 0xB0)
                        break;
                }
                else
                {
                    int srcD = ce[srcIndex++] + (ce[srcIndex++] * 0x100); //gb.BufferLocation = room.dictionaryLocation + ce[srcIndex++] + ((ce[srcIndex++] - 0x40) * 0x100);
                    int size = (srcD >> 12) + 3; //ld a,d  swap a  and a,f. equal to >> 8, >> 4
                    srcD &= 0xFFF;
                    gb.BufferLocation = room.dictionaryLocation + srcD;
                    for (int i = 0; i < size; i++)
                    {
                        room.dungeon[destIndex].x = (byte)(destIndex % 16);
                        room.dungeon[destIndex].y = (byte)(destIndex / 16);
                        room.decompressed[destIndex++] = gb.ReadByte();
                        room.dungeon[destIndex - 1].id = room.decompressed[destIndex - 1];
                        room.dungeon[destIndex - 1].size = (byte)size;
                        room.dungeon[destIndex - 1].type = 1;
                        if (destIndex == 0xB0)
                            return;
                    }
                }
            }
        }

        public void getMapArea(int index, int group, int season, Program.GameTypes game)
        {
            //CC2D - map group
            //CC30 - map index
            //CD24 - map area
            //FF8D - area | 0x80, and later that & 0x7f - compression type?
            //FF8B - area

            int indexBase = 0x10000 + (game == Program.GameTypes.Ages ? 0x0F9C : 0x0C84);
            //4:6d7a - start of procedure
            gb.BufferLocation = 0x10000 + (game == Program.GameTypes.Ages ? 0x12D4 : 0x133C) + (group * 2);
            //6da1
            gb.BufferLocation = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100) + index;
            byte a = gb.ReadByte();
            byte area = (byte)(a & 0x7F);
            gb.BufferLocation = indexBase + area * 8;
            if (game == Program.GameTypes.Seasons)
            {
                if (gb.ReadByte() == 0xFF)
                {
                    gb.BufferLocation = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
                    gb.BufferLocation += season * 8;
                }
                else
                    gb.BufferLocation--;
            }
            AreaLoader.Area ar = new AreaLoader.Area();
            ar = areaLoader.loadArea(gb.BufferLocation, area, game);
            room.area = ar;
        }

        public void loadMapHeader(int index, int group, int season, bool flag, Program.GameTypes game)
        {
            //3986
            int indexBase = 0x10000 + (game == Program.GameTypes.Ages ? 0x0F6C : 0x0C4C);
            byte a = (byte)(room.area.unknown * 8);
            if (game == Program.GameTypes.Ages || group > 0 || !flag)
                gb.BufferLocation = indexBase + a;
            else
                gb.BufferLocation = indexBase + season * 8;

            if (game == Program.GameTypes.Seasons && flag)
            {
                if (group == 0)
                    a = (byte)season;
                else
                    a = (byte)room.area.unknown;
                gb.BufferLocation = 0x42 * 0x4000 + a * 0x400 + index * 4;
                room.type = (gb.ReadByte() == 0 ? RoomTypes.Dungeon : RoomTypes.Small);
                gb.BufferLocation = gb.ReadByte() * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
                room.dataLocation = gb.BufferLocation;
                return;
            }

            byte temp = gb.ReadByte();
            room.minorBank = gb.ReadByte();
            room.relativeDataPointerM = gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            room.bank = gb.ReadByte();
            room.relativeDataPointer = gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);

            if (temp != 0) //Small map
            {
                room.type = RoomTypes.Small;
                gb.BufferLocation = room.minorBank * 0x4000 + room.relativeDataPointerM + (index * 2);
                if (flag)
                {
                    int prev = gb.BufferLocation;
                    if (game == Program.GameTypes.Ages)
                    {
                        gb.BufferLocation = 0x54 * 0x4000 + group * 2;
                        gb.BufferLocation = 0x54 * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100) + index;
                    }
                    else
                    {
                        int groupValue = room.area.unknown;
                        if(group == 0)
                            groupValue = season;
                        gb.BufferLocation = 0x5C * 0x4000 + groupValue * 2;
                        gb.BufferLocation = 0x5C * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100) + index;
                    }
                    room.bank = gb.ReadByte();//(byte)(Math.Abs(gb.ReadByte() - room.bank));
                    gb.BufferLocation = prev;
                }
                int pointer = gb.ReadByte() + gb.ReadByte() * 0x100;
                room.compressionType = (byte)(pointer >> 14);
                gb.BufferLocation = room.bank * 0x4000 + (pointer & 0x3FFF);
                gb.BufferLocation += room.relativeDataPointer;
                room.dataLocation = gb.BufferLocation;
            }
            else
            {
                room.type = RoomTypes.Dungeon;
                //3928 - dictionary loading
                gb.BufferLocation = room.minorBank * 0x4000 + room.relativeDataPointerM + 0x1000 + (index * 2);
                int dictionaryLocation = gb.BufferLocation - 0x1000 - (index * 2);
                int pointer = gb.ReadByte() + gb.ReadByte() * 0x100; //Found to be below 4000
                if (pointer > 0x4000)
                {
                    pointer -= 0x4000;
                    room.bank++;
                }
                if (flag)
                {
                    int prev = gb.BufferLocation;
                    if (game == Program.GameTypes.Ages)
                    {
                        gb.BufferLocation = 0x54 * 0x4000 + group * 2;
                        gb.BufferLocation = 0x54 * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100) + index;
                    }
                    else
                    {
                        int groupValue = room.area.unknown;
                        if (group == 0)
                            groupValue = season;
                        gb.BufferLocation = 0x5C * 0x4000 + groupValue * 2;
                        gb.BufferLocation = 0x5C * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100) + index;
                    }
                    room.bank = gb.ReadByte();//(byte)(Math.Abs(gb.ReadByte() - room.bank));
                    gb.BufferLocation = prev;
                }
                gb.BufferLocation = room.bank * 0x4000 + (ushort)((pointer + (room.relativeDataPointer + 0x4000)) + 0xFE00) - 0x4000;
                room.dataLocation = gb.BufferLocation;
                room.dictionaryLocation = room.minorBank * 0x4000 + (dictionaryLocation % 0x4000);
                //gb.BufferLocation = room.minorBank * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            }
        }

        public void loadMap(int index, int group, int season, bool flag, Program.GameTypes game)
        {
            room = new Room();
            getMapArea(index, group, season, game);
            loadMapHeader(index, group, season, flag, game);
            if (room.type == RoomTypes.Small)
                LoadSmallMap();
            else
            {
                if (!flag)
                    LoadDungeonMap();
                else
                {
                    room.dungeon = new DungeonBlock[176];
                    room.decompressed = gb.ReadBytes(room.dataLocation, 176);
                    for (int i = 0; i < 176; i++)
                    {
                        room.dungeon[i].id = room.decompressed[i];
                        room.dungeon[i].x = (byte)(i % 16);
                        room.dungeon[i].y = (byte)(i / 16);
                    }
                }
            }
        }

        public Bitmap DrawMap(Bitmap srcTileset, bool[] flags)
        {
            Bitmap b;
            if (room.type == RoomTypes.Small)
                b = new Bitmap(160, 128);
            else
                b = new Bitmap(240, 176);
            FastPixel fp = new FastPixel(b);
            FastPixel src = new FastPixel(srcTileset);
            if (room.type == RoomTypes.Small)
                fp.rgbValues = new byte[160 * 128 * 4];
            else
                fp.rgbValues = new byte[240 * 176 * 4];
            src.rgbValues = new byte[256 * 256 * 4];
            fp.Lock();
            src.Lock();
            //Graphics g = Graphics.FromImage(b);

            if (room.type == RoomTypes.Small)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        byte v = room.decompressed[x + y * 10];
                        for (int yy = 0; yy < 16; yy++)
                        {
                            for (int xx = 0; xx < 16; xx++)
                            {
                                fp.SetPixel(x * 16 + xx, y * 16 + yy, src.GetPixel((v % 16) * 16 + xx, (v / 16) * 16 + yy));
                            }
                        }
                        //g.DrawImage(srcTileset, new Rectangle(x * 16, y * 16, 16, 16), (room.decompressed[x + (y * 10)] % 16) * 16, (room.decompressed[x + (y * 10)] / 16) * 16, 16, 16, GraphicsUnit.Pixel);
                    }
                }
            }
            else
            {
                for (int y = 0; y < 11; y++)
                {
                    for (int x = 0; x < 15; x++)
                    {
                        byte v = room.decompressed[x + y * 16];
                        for (int yy = 0; yy < 16; yy++)
                        {
                            for (int xx = 0; xx < 16; xx++)
                            {
                                fp.SetPixel(x * 16 + xx, y * 16 + yy, src.GetPixel((v % 16) * 16 + xx, (v / 16) * 16 + yy));
                            }
                        }
                    }
                }
            }
            src.Unlock(false);
            fp.Unlock(true);

            return b;
        }
    }
}
