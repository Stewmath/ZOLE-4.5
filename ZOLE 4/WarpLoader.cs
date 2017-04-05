using System;
using System.Collections.Generic;

using System.Text;

namespace ZOLE_4
{
    public class WarpLoader
    {
        GBHL.GBFile gb;

        public WarpLoader(GBHL.GBFile g)
        {
            gb = g;
        }

        public enum WarpTypes
        {
            Spot,
            Whole,
            Direct
        }

        public int getWarpHeaderAddress(int group, int map, bool dungeon, byte areaFlag2, Program.GameTypes game)
        {
            gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x1359E : 0x13457) + (group * 2);
            gb.BufferLocation = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            while (gb.ReadByte() != 0xFF && gb.BufferLocation < 0x14000)
            {
                gb.BufferLocation--;
                byte b = gb.ReadByte();
                if ((b & 0x80) != 0)
                    if ((areaFlag2 & 4) == 0)
                        return gb.BufferLocation - 1;
                    else
                        return -1;
                if ((b & 0x40) != 0)
                    if (gb.ReadByte() == map)
                        return gb.BufferLocation - 2;
                    else
                        gb.BufferLocation--;
                if((areaFlag2 & 4) == 0)
                    if ((b & 0xF) != 0)
                    {
                        if (gb.ReadByte() == map && dungeon)
                            return gb.BufferLocation - 2;
                        else
                            gb.BufferLocation += 2;
                        continue;
                    }
                if (gb.ReadByte() == map)
                    return gb.BufferLocation - 2;
                else
                    gb.BufferLocation += 2;
            }
            return -1;
        }

        public int getSideWarpHeaderAddress(int group, int map, Program.GameTypes game)
        {
            gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x1359E : 0x13457) + (group * 2); //TODO: Seasons
            gb.BufferLocation = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            while (gb.ReadByte() != 0xFF && gb.BufferLocation < 0x14000)
            {
                if (gb.ReadByte() == map)
                {
                    return gb.BufferLocation - 2;
                }
                else
                    gb.BufferLocation += 2;
            }
            return -1;
        }

        public Warp loadWarp(int headerAddress)
        {
            if (headerAddress == -1)
                return null;
            Warp w = new Warp();
            gb.BufferLocation = headerAddress;
            w.opcode = gb.ReadByte();
            w.map = gb.ReadByte();
            if ((w.opcode & 0x40) == 0)
            {
                w.index = gb.ReadByte();
                byte b = gb.ReadByte();
                w.group = (byte)(b >> 4);
                w.entrance = (byte)(b & 0xF);
            }
            else
            {
                byte b = gb.ReadByte();
                byte b2 = gb.ReadByte();
                w.fpointer = b + ((b2 - 0x40) * 0x100);
            }

            return w;
        }

        public Warp loadWarpProps(int refByte, int destGroup, Program.GameTypes game)
        {
            Warp w = new Warp();
            gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x12F5B : 0x12D4E) + (destGroup * 2);
            gb.BufferLocation = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            gb.BufferLocation += refByte * 3;
            w.map = gb.ReadByte();
            w.pos = gb.ReadByte();
            w.unknown = gb.ReadByte();
            return w;
        }

        public Warp loadSideWarp(int headerAddress)
        {
            if (headerAddress == -1)
                return null;
            Warp w = new Warp();
            gb.BufferLocation = headerAddress;
            w.x = gb.ReadByte();
            w.map = gb.ReadByte();
            w.index = gb.ReadByte();
            byte b = gb.ReadByte();
            w.group = (byte)(b >> 4);
            w.entrance = (byte)(b & 0xF);

            return w;
        }
    }

    public class Warp
    {
        public Warp secondaryWarp;
        public byte unknown;
        public byte srcGroup;
        public byte group;
        public byte entrance;
        public byte map;
        public byte x;
        public byte y;
        public byte pos;
        public int fpointer;
        public byte destX;
        public byte destY;
        public byte opcode;
        public byte index;
    }
}
