using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;

namespace ZOLE_4
{
    public class PaletteLoader
    {
        GBHL.GBFile gb;
        public PaletteLoader(GBHL.GBFile g)
        {
            gb = g;
        }

        public static int getHexValue(int red, int green, int blue)
        {
            int r = red;
            int g = green;
            int b = blue;

            g *= 2;
            b *= 4;

            b *= 256;
            g *= 16;

            int i = r + g + b;
            return i;
        }

        public Color[,] LoadPalette(int index, Program.GameTypes game)
        {
            Color[,] final = new Color[8, 4];
            Color[,] basicFour = gb.GetPalette((game == Program.GameTypes.Ages ? 0x5C8E0 : 0x58830)); //Always used
            byte[] ram = new byte[0x10000];
            for (int k = 0; k < 4; k++)
                final[0, k] = basicFour[0, k];

            int baseColorAddress = (game == Program.GameTypes.Ages ? (0x17 * 0x4000) : (0x16 * 0x4000));
            gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x632C : 0x6290);
            gb.BufferLocation += index * 2;
            gb.BufferLocation = gb.ReadByte() + gb.ReadByte() * 0x100;
            byte a;
            bool b = true;
            if (b) { }
            //while (b)
            //{
            a = gb.ReadByte();
            if ((a & 0x80) == 0)
                b = false;

            int count = (a & 7) + 1;
            int d = 0xDE80 + (a & 0x78);
            int baseDest = d;
            gb.BufferLocation = baseColorAddress + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);

            for (int k = 0; k < count * 8; k++)
            {
                ram[d++] = gb.ReadByte();
            }

            for (int i = 8 - count; i < count + (8 - count); i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    ushort value = (ushort)(ram[0xDE80 + i * 8 + k * 2] + (ram[0xDE80 + i * 8 + k * 2 + 1] << 8));
                    final[i, k] = gb.GetRealColor(value);
                }
            }
            //}
            return final;

            /*gb.BufferLocation--;
            a &= 7;
            a++;
            ushort bc = (ushort)(a * 0x100);
            a = gb.ReadByte();
            gb.BufferLocation--;
            a = Decompressor.RotateLeft(a);
            a = (byte)((a >> 4) + ((a & 0xF) << 4));
            a &= 7;
            a++;
            int temp = gb.BufferLocation;
            a = gb.ReadByte(a);
            gb.BufferLocation = temp;
            bc = (ushort)((bc >> 8) * 0x100 + a);
            a = 0;
        Add541:
            a |= (byte)(bc & 0xFF);
            bc -= 0x100;
            if ((bc >> 8) == 0)
                goto Add548;
            a = Decompressor.RotateLeft(a);
            goto Add541;

        Add548:
            bc = (ushort)(a * 0x100 + 0xA6);
            if ((bc & 0x40) == 0)
                goto Add551;
            bc = (ushort)((bc >> 8) * 0x100 + 0xA7);
        Add551:
            a = gb.ReadByte();
            gb.BufferLocation--;
            a &= 0x78;
            a += 0x80;
            ushort de = (ushort)(0xDE * 0x100 + a);
            a = gb.ReadByte();
            gb.BufferLocation--;
            a &= 7;
            a++;
            bc = (ushort)(a * 0x100);
            a = gb.ReadByte();
            a = Decompressor.RotateLeft(a);
            a = gb.ReadByte();
            bc = (ushort)((bc >> 8) * 0x100 + a);
            a = gb.ReadByte();
            int pushedHL = gb.BufferLocation;
            gb.BufferLocation = (0x17 * 0x4000) + ((a - 0x40) * 0x100) + (bc & 0xFF);
            byte b = (byte)(bc >> 8);
            byte c = (byte)(bc & 0xFF);
        Add570:
            c = 8;
        Add572:
            a = gb.ReadByte();
            //Calculate the proper position.
            int i = (de - 0xDE80);
            int bigIndex = (i / 16);
            int smallIndex = (i / 2) % 4;
            ram[de] = a;
            c--;
            de += 1;
            if (c > 0)
                goto Add572;
            b--;
            if (b > 0)
                goto Add570;

            int add = 0xDE90;
            for (int big = 2; big < 8; big++)
            {
                for (int small = 0; small < 4; small++)
                {
                    ushort value = (ushort)(ram[add++] + (ram[add++] * 0x100));
                    final[big, small] = gb.GetRealColor(value);
                }
            }

            return final;*/
        }

        public static int GetPaletteIndex(GBHL.GBFile gb, Program.GameTypes game, int dest)
        {
            int baseColorAddress = (game == Program.GameTypes.Ages ? (0x17 * 0x4000) : (0x16 * 0x4000));
            for (int i = 0; i < 255; i++)
            {
                gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x632C : 0x6290);
                gb.BufferLocation += i * 2;
                gb.BufferLocation = gb.ReadByte() + gb.ReadByte() * 0x100;

                byte a = gb.ReadByte();

                gb.BufferLocation = baseColorAddress + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
                if ((gb.BufferLocation % 0x4000) + 0x4000 == dest)
                    return i;
            }
            return -1;
        }

        public static int GetPaletteAddress(GBHL.GBFile gb, Program.GameTypes game, int index)
        {
            int baseColorAddress = (game == Program.GameTypes.Ages ? (0x17 * 0x4000) : (0x16 * 0x4000));
            gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x632C : 0x6290);
            gb.BufferLocation += index * 2;
            gb.BufferLocation = gb.ReadByte() + gb.ReadByte() * 0x100;

            byte a = gb.ReadByte();
            gb.BufferLocation = baseColorAddress + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);

            return (gb.BufferLocation % 0x4000) + 0x4000;
        }
    }
}