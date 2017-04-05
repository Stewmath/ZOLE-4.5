using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;

namespace ZOLE_4
{
    //This class was a mostly direct translate from Z80 Assembly to C#.
    //No optimizing or neat coding intended.
    public class TilesetBuilder
    {
        public GBHL.GBFile gb;
        private byte[] SmallTiles;
        public byte[] assemblyData;
        byte FF8FValue;
        int FF90Value;
        byte FF8EValue; //bank
        byte FF8DValue;
        byte FF8CValue;
        byte FF8BValue;
        byte FF8AValue;
        byte FF97Value;
        int FF92Value;

        public TilesetBuilder(GBHL.GBFile g)
        {
            gb = g;
        }

        public int getHeaderLocation(int tileset, Program.GameTypes game) //VRAM Header
        {
            if (game == Program.GameTypes.Ages)
            {
                return 0x69DA + (tileset * 2);
            }
            else
            {
                return 0x6926 + (tileset * 2);
            }
        }

        public void loadTileset(int tileset, Program.GameTypes game)
        {
            bool flag = tileset > 0xFF;
            tileset &= 0xFF;

            loadSmallTiles(tileset, game);
            decompressSmallTiles(game);

            if (flag && game == Program.GameTypes.Ages)
                replacePast();
        }

        public void loadSmallTiles(int tileset, Program.GameTypes game)
        {
            gb.BufferLocation = game == Program.GameTypes.Ages ? 0x787E : 0x7964;
            gb.BufferLocation += tileset * 2;
            gb.BufferLocation = gb.ReadByte() + gb.ReadByte() * 0x100;
            byte[] ram = new byte[0x10000];

            byte a = gb.ReadByte();
            int headerLocation = gb.BufferLocation;
            gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x7870 : 0x794E) + (a * 2);
            gb.BufferLocation = gb.ReadByte() + gb.ReadByte() * 0x100; //Uses bank 1
            a = gb.ReadByte();
            FF8FValue = a;
            FF90Value = gb.ReadByte() + gb.ReadByte() * 0x100;
            gb.BufferLocation = headerLocation;

            byte bank = gb.ReadByte();
            FF8EValue = bank;
            ushort de = (ushort)(gb.ReadByte() * 0x100 + gb.ReadByte());
            ushort bc;
            int pushedDE = de;
            de = (ushort)(gb.ReadByte() * 0x100 + gb.ReadByte());
            a = (byte)(gb.ReadByte() & 0x7F);
            FF8DValue = a;
            a = gb.ReadByte();
            gb.BufferLocation -= 2;
            FF8CValue = a;
            FF92Value = (gb.BufferLocation >> 8) + ((gb.BufferLocation & 0xFF) * 0x100);
            gb.BufferLocation = pushedDE;

            //Some procedure at 07FE
            a = (byte)((de & 0xFF) & 0xF);
            a ^= (byte)(de & 0xFF);
            de = (ushort)(((de >> 8) * 0x100) + a);
            gb.BufferLocation -= 0x4000;
        Add807: //Not exactly 807...
            if (gb.BufferLocation < FF8EValue * 0x4000)
                gb.BufferLocation += FF8EValue * 0x4000;
            a = gb.ReadByte();
            FF8BValue = a;
            bc = 0x800;
        Add813:
            //bool carry = ((a & 1) == 1);//((bc >> 8) + 1) > 255;
            a = FF8EValue;
            FF97Value = a;
            a = FF8BValue;
            a = Decompressor.RotateRight(a);
            FF8BValue = a;
            bool carry = ((a & 1) == 1);
            if (carry)
                goto Add82D;
            a = gb.ReadByte();
            ram[de] = a;
            de++;
            Procedure878(ref a);
            if (a == 0)
                goto End;
            bc -= 0x100;
            if (bc >> 8 != 0)
                goto Add813;
            goto Add807;
        Add82D:
            ushort pushedBC = bc;
            a = FF8FValue;
            if (a >> 7 == 1)
                goto Add848;
            bc = (ushort)((bc >> 8) * 0x100 + gb.ReadByte());
            a = gb.ReadByte();
            FF8AValue = a;
            a &= 0xF;
            bc = (ushort)(a * 0x100 + (bc & 0xFF));
            a = FF8AValue;
            a >>= 4;
            a += 3;
            FF8AValue = a;
            goto Add84F;
        Add848:
            a = gb.ReadByte();
            FF8AValue = a;
            bc = (ushort)(gb.ReadByte() + gb.ReadByte() * 0x100);
        Add84F:
            int temp = gb.BufferLocation;
            gb.BufferLocation = FF8EValue * 0x4000 + ((FF90Value & 0xFF) * 0x100 + (FF90Value >> 8)) - 0x4000;
            gb.BufferLocation += bc;
            a = FF8AValue;
            bc = (ushort)(a * 0x100 + (bc & 0xFF));
            a = (byte)(FF8FValue & 0x3F);
            FF97Value = a;
            gb.BufferLocation = gb.BufferLocation % 0x4000;
            gb.BufferLocation += a * 0x4000;
        Add863:
            a = gb.ReadByte();
            ram[de] = a;
            de++;
            Procedure878(ref a);
            if (a == 0)
                goto Add875;
            bc -= 0x100;
            if (bc >> 8 > 0)
                goto Add863;
            gb.BufferLocation = temp;
            bc = pushedBC;
            bc -= 0x100;
            if (bc >> 8 > 0)
                goto Add813;
            goto Add807;
        Add875:
            gb.BufferLocation = temp;
            bc = pushedBC;
        //Above are assumptions...

        End:
            SmallTiles = ram;
        }

        private void Procedure878(ref byte a)
        {
            a = FF8DValue;
            a |= FF8CValue;
            if (a == 0)
                return;
            a = FF8CValue;
            bool carry = false;
            if (a - 1 < 0)
                carry = true;
            a--;
            FF8CValue = a;
            a = FF8DValue;
            a -= (byte)(carry ? 1 : 0);
            FF8DValue = a;
            a |= FF8CValue;
            return;
        }

        public void decompressSmallTiles(Program.GameTypes game)
        {
            byte bank = (byte)(game == Program.GameTypes.Ages ? 0x18 : 0x17);
            //373b - start of procedure
            assemblyData = new byte[0x10000];
            ushort de = 0xD000; //destination
            ushort hl = 0xDC00; //copy location
            ushort bc;
            for (int i = 0; i < 256; i++)
            {
                byte a = SmallTiles[hl++];
                bc = (ushort)(a + (SmallTiles[hl++] * 0x100));
                gb.BufferLocation = bank * 0x4000 + 4 + bc * 3;
                
                a = gb.ReadByte();
                bc = (ushort)a;
                a = gb.ReadByte();

                bc = (ushort)(bc + ((a >> 4) * 0x100));
                int temp = gb.BufferLocation;
                gb.BufferLocation = bank * 0x4000;
                gb.BufferLocation += gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
                gb.BufferLocation += bc * 4;
                bc = (ushort)(0x400 + (bc & 0xFF));
                Procedure486(ref a, ref de, ref bc);
                gb.BufferLocation = temp - 1;

                a = (byte)(gb.ReadByte() & 0xF);
                bc = (ushort)(a * 0x100 + gb.ReadByte());
                gb.BufferLocation = bank * 0x4000 + 2;
                gb.BufferLocation = bank * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
                gb.BufferLocation += bc * 4;
                bc = (ushort)(0x400);
                Procedure486(ref a, ref de, ref bc);
            }
        }

        private void Procedure486(ref byte a, ref ushort de, ref ushort bc)
        {
        Start:
            a = gb.ReadByte();
            assemblyData[de++] = a;
            bc -= 0x100;
            if (bc >> 8 > 0)
                goto Start;
        }

        public void replacePast()
        {
            for (int i = 64; i < 128; i++)
            {
                for (int y = 0; y < 2; y++)
                {
                    for (int x = 0; x < 2; x++)
                    {
                        byte b = assemblyData[i * 8 + 4 + x + (y * 2) + 0xD000];
                        if ((b & 7) == 6)
                        {
                            assemblyData[i * 8 + 4 + x + (y * 2) + 0xD000] = (byte)((b & 0x78));
                        }
                    }
                }
            }
        }

        public Bitmap drawTileset(Color[] palette, byte[, ,] gfxData)
        {
            Bitmap b = new Bitmap(256, 256);
            FastPixel fp = new FastPixel(b);
            fp.rgbValues = new byte[256 * 256 * 3];
            fp.Lock();
            for (int i = 0; i < 256; i++)
            {
                for (int y = 0; y < 2; y++)
                {
                    for (int x = 0; x < 2; x++)
                    {
                        byte by = assemblyData[i * 8 + x + (y * 2) + 0xD000];
                        byte props = assemblyData[i * 8 + 4 + x + (y * 2) + 0xD000];
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
                                fp.SetPixel(((i % 16) * 16) + x * 8 + xx, ((i / 16) * 16) + y * 8 + yy, palette[gfxData[by, hflip ? 7 - xx : xx, vflip ? 7 - yy : yy]]);
                            }
                        }
                    }
                }
            }
            fp.Unlock(true);

            return b;
        }

        public Bitmap drawTileset(Color[,] palette, byte[, ,] gfxData)
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
                        byte by = assemblyData[i * 8 + x + (y * 2) + 0xD000];
                        byte props = assemblyData[i * 8 + 4 + x + (y * 2) + 0xD000];
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
                                fp.SetPixel(((i % 16) * 16) + x * 8 + xx, ((i / 16) * 16) + y * 8 + yy, palette[pal, gfxData[by, hflip ? 7 - xx : xx, vflip ? 7 - yy : yy]]);
                            }
                        }
                    }
                }
            }
            fp.Unlock(true);

            return b;
        }
    }
}