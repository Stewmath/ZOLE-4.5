using System;
using System.Collections.Generic;

using System.Text;

namespace ZOLE_4
{
    public class MapScripts
    {
        GBHL.GBFile gb;
        public int scriptLocation;

        public MapScripts(GBHL.GBFile g)
        {
            gb = g;
        }

        public int loadScript(int map, int group)
        {
            //This one checks the the previous map you're coming from, so we won't use it
            /*gb.BufferLocation = 0x11F5F + group * 2;
            gb.BufferLocation = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);

            while (true)
            {
                byte check = gb.ReadByte();
                if (check == 0)
                {
                    break;
                }
                if (check == map)
                {
                    byte r = gb.ReadByte();
                    return r;
                }
            }*/

            gb.BufferLocation = 0x124A7 + group * 2;
            gb.BufferLocation = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            while (true)
            {
                byte check = gb.ReadByte();
                if (check == 0)
                {
                    return -1;
                }
                if (check == map)
                {
                    scriptLocation = gb.BufferLocation - 1;
                    byte r = gb.ReadByte();
                    return r;
                }
                else
                    gb.ReadByte();
            }

            /*gb.BufferLocation = 0xBAAA + group * 2;
            gb.BufferLocation = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            while (true)
            {
                byte check = gb.ReadByte();
                if (check == 0)
                {
                    break;
                }
                if (check == map)
                {
                    scriptLocation = gb.BufferLocation - 1;
                    byte r = gb.ReadByte();
                    return r;
                }
            }

            gb.BufferLocation = 0x49898 + group * 2;
            gb.BufferLocation = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            while (true)
            {
                byte check = gb.ReadByte();
                if (check == 0)
                {
                    return -1;
                }
                if (check == map)
                {
                    scriptLocation = gb.BufferLocation - 1;
                    byte r = gb.ReadByte();
                    return r;
                }
            }*/
        }

        public int loadSecondaryScriptRef(int group, int map)
        {
            gb.BufferLocation = 0xBAAA + group * 2;
            gb.BufferLocation = 0x8000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            while (true)
            {
                byte b = gb.ReadByte();
                if (b == 0)
                    return -1;
                if (b == map)
                {
                    return gb.BufferLocation - 1;
                }
                gb.ReadByte();
            }
        }
    }
}
