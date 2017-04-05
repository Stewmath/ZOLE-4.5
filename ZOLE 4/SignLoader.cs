using System;
using System.Collections.Generic;
using System.Text;
using GBHL;

namespace ZOLE_4
{
    public class SignLoader
    {
        GBFile gb;

        public SignLoader(GBFile g)
        {
            gb = g;
        }

        public struct Sign
        {
            public byte map;
            public byte yx;
            public byte text;
        }

        public List<Sign> signs;
        public void LoadSigns(int mapGroup)
        {
            gb.BufferLocation = 0x1B784 + mapGroup * 2;
            gb.BufferLocation = 0x18000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            signs = new List<Sign>();
            while (true)
            {
                if (gb.ReadByte() == 0)
                    break;
                gb.BufferLocation--;
                Sign s = new Sign();
                s.yx = gb.ReadByte();
                s.map = gb.ReadByte();
                s.text = gb.ReadByte();
                signs.Add(s);
            }
        }

        public void SaveSigns(int mapGroup)
        {
            gb.BufferLocation = 0x1B784 + mapGroup * 2;
            gb.BufferLocation = 0x18000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            foreach (Sign s in signs)
            {
                gb.WriteByte(s.yx);
                gb.WriteByte(s.map);
                gb.WriteByte(s.text);
            }
            gb.WriteByte(0);
        }
    }
}
