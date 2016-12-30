using System;
using System.Collections.Generic;

using System.Text;

namespace ZOLE_4
{
    public class ChestLoader
    {
        public byte map;
        public byte yx;
        public ushort id;
        public int chestLocation;
        public bool mapFirst;

        GBHL.GBFile gb;
        Program.GameTypes game;

        public ChestLoader(GBHL.GBFile g, Program.GameTypes gm)
        {
            gb = g;
            game = gm;
        }

        public bool loadChest(int group, int map)
        {
            gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x59108 : 0x54F6C) + group * 2;
            gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x58000 : 0x54000) + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);

            while (true)
            {
                if (gb.BufferLocation >= (game == Program.GameTypes.Ages ? 0x5C000 : 0x58000))
                    return false;
                byte b = gb.ReadByte();
                if (b == 0xFF)
                    return false;

                /*if (b == map)
                {
                    mapFirst = true;
                    chestLocation = gb.BufferLocation - 1;
                    gb.BufferLocation--;
                    yx = gb.ReadByte();
                    gb.BufferLocation++;
                    id = gb.ReadWord();
                    return true;
                }*/
                if (gb.ReadByte() == map)
                {
                    mapFirst = false;
                    chestLocation = gb.BufferLocation - 2;
                    yx = b;
                    id = gb.ReadWord();
                    return true;
                }
                gb.BufferLocation += 2;
            }
        }

        public void repointChests()
        {
            for (int i = 0; i < 4; i++)
            {
                int loc = 0x5BE02 + i * 124;

                gb.BufferLocation = 0x59108 + i * 2;
                gb.BufferLocation = 0x58000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
                byte[] copy = new byte[0x400];
                byte b;
                int k = 0;
                while ((b = gb.ReadByte()) != 0xFF || (k < 24 && i == 3))
                {
                    copy[k] = b;
                    k++;
                }

                gb.BufferLocation = 0x59108 + i * 2;
                gb.WriteByte((byte)loc);
                gb.WriteByte((byte)(((loc - 0x58000) >> 8) + 0x40));

                gb.BufferLocation = loc;
                for (int j = 0; j < k; j++)
                {
                    gb.WriteByte(copy[j]);
                }
                gb.BufferLocation = 0x5BE02 + (i + 1) * 124 - 1;
                gb.WriteByte(0xFF);
            }
        }
    }
}
