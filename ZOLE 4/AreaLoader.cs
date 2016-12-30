using System;
using System.Collections.Generic;

using System.Text;

namespace ZOLE_4
{
    public class AreaLoader
    {
        GBHL.GBFile gb;
        public AreaLoader(GBHL.GBFile g)
        {
            gb = g;
        }

        public struct Area
        {
            public int palette;
            public int tileset;
            public int unique;
            public int vram;
            public int animation;
            public int flags1;
            public int flags2;
            public int unknown;
            public int index;
        }

        public Area loadArea(int location, int index, Program.GameTypes game)
        {
            Area a = new Area();
            a.index = index;
            gb.BufferLocation = location;
            a.flags1 = gb.ReadByte();
            a.flags2 = gb.ReadByte();
            a.unique = gb.ReadByte();
            a.vram = gb.ReadByte();
            a.palette = gb.ReadByte();
            a.tileset = gb.ReadByte();
            if(game == Program.GameTypes.Ages)
                if ((a.flags1 & 0x70) == 0 && (a.flags2 & 0x80) != 0 && (a.flags2 & 2) == 0)
                    a.tileset += 0x100;
            a.unknown = gb.ReadByte();
            a.animation = gb.ReadByte();
            return a;
        }
    }
}