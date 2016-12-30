using System;
using System.Collections.Generic;
using System.Text;
using GBHL;

namespace ZOLE_4
{
    public class TreeTopLoader
    {
        GBFile gb;
        public TreeTopLoader(GBFile g)
        {
            gb = g;
        }

        public int getTreeTopLocation(int group, int map)
        {
            gb.BufferLocation = 0xBC72 + group * 2;
            gb.BufferLocation = 0x8000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            while (true)
            {
                byte b = gb.ReadByte();
                if (b == 0)
                    return -1;
                if (b == map)
                    return gb.BufferLocation - 1;
                gb.BufferLocation += 3;
            }
        }
    }
}
