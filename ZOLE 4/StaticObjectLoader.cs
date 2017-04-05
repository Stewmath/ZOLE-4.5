using System;
using System.Collections.Generic;
using System.Text;
using GBHL;

namespace ZOLE_4
{
    public class StaticObjectLoader
    {
        GBFile gb;
        public StaticObjectLoader(GBFile g)
        {
            gb = g;
        }

        public struct StaticObject
        {
            public byte unknown;
            public byte map;
            public byte id;
            public byte unknown2;
            public byte y;
            public byte x;
        }

        public List<StaticObject> staticObjects;
        public void loadStaticObjects(int dungeon, int map)
        {
            if (dungeon == 0xFF)
                return;
            gb.BufferLocation = 0x590A7 + dungeon * 2;
            gb.BufferLocation = 0x58000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            List<StaticObject> objects = new List<StaticObject>();
            while (true)
            {
                byte b = gb.ReadByte();
                if (b == 0xFF)
                    break;
                StaticObject s = new StaticObject();
                s.unknown = b;
                s.map = gb.ReadByte();
                //bool bmap = s.map == map;
                s.id = gb.ReadByte();
                s.unknown2 = gb.ReadByte();
                s.y = gb.ReadByte();
                s.x = gb.ReadByte();
                objects.Add(s);
            }
            staticObjects = objects;
        }

        public bool addStaticObject(int dungeon, int map)
        {
            int count = staticObjects.Count * 6 + 7; //+1 for FF and +6 for the new object
            int address = findFreeSpace(count, 0x16);
            if (address == -1)
                return false;
            //Erase the old stuff
            gb.BufferLocation = 0x590A7 + dungeon * 2;
            gb.BufferLocation = 0x58000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            if (gb.BufferLocation != 0x59107)
            {
                foreach (StaticObject o in staticObjects)
                {
                    for (int i = 0; i < 6; i++)
                        gb.WriteByte(0);
                }
                gb.WriteByte(0); //Clear the FF
            }
            gb.BufferLocation = 0x590A7 + dungeon * 2;
            gb.WriteByte((byte)address);
            gb.WriteByte((byte)(((address - 0x58000) >> 8) + 0x40));
            StaticObject obj = new StaticObject();
            obj.map = (byte)map;
            obj.x = obj.y = 8;
            staticObjects.Add(obj);
            saveObjects(dungeon);
            return true;
        }

        public void deleteStaticObject(int dungeon, int index)
        {
            //Erase the old stuff
            gb.BufferLocation = 0x590A7 + dungeon * 2;
            gb.BufferLocation = 0x58000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            foreach (StaticObject o in staticObjects)
            {
                for (int i = 0; i < 6; i++)
                    gb.WriteByte(0);
            }
            gb.WriteByte(0); //Clear the FF
            staticObjects.RemoveAt(index);
            saveObjects(dungeon);
        }

        public int findFreeSpace(int amount, byte bank)
        {
            int found = 0;
            gb.BufferLocation = bank * 0x4000;
            while (found < amount)
            {
                if (gb.BufferLocation == gb.Buffer.Length - 1 || gb.BufferLocation == bank * 0x4000 + 0x3FFF)
                {
                    return -1;
                }
                if (gb.ReadByte() != 0)
                {
                    found = 0;
                    continue;
                }
                found++;
            }

            return gb.BufferLocation - found;
        }

        public void saveObjects(int dungeon)
        {
            gb.BufferLocation = 0x590A7 + dungeon * 2;
            gb.BufferLocation = 0x58000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            foreach (StaticObject o in staticObjects)
            {
                gb.WriteByte(o.unknown);
                gb.WriteByte(o.map);
                gb.WriteByte(o.id);
                gb.WriteByte(o.unknown2);
                gb.WriteByte(o.y);
                gb.WriteByte(o.x);
            }
            gb.WriteByte(0xFF);
        }
    }
}
