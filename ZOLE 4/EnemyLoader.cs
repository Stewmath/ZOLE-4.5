using System;
using GBHL;

namespace ZOLE_4
{
    public class EnemyLoader
    {
        public struct Enemy
        {
            public int baseLocation;
            public byte id;
            public byte subid;
            public byte sprite;
            public byte vulnerable;
            public int subPointer;
            public byte paletteFace;
            public byte collisionHeight;
            public byte collisionWidth;
            public byte damageDealt;
            public byte health;

            public byte aiBank;
            public int aiPointer;
            public int aiFinal;
        }

        private GBFile gb;
        public EnemyLoader(GBFile g)
        {
            gb = g;
        }

        public Enemy LoadEnemy(byte id, byte supid)
        {
            Enemy e = new Enemy();
            e.subPointer = -1;

            gb.BufferLocation = 0xFDD4B + (id * 2) * 2;
            e.baseLocation = gb.BufferLocation;
            byte b;
            e.sprite = gb.ReadByte();
            e.vulnerable = gb.ReadByte();
            b = gb.ReadByte();
            if ((b & 0x80) != 0)
            {
                b &= 0x7F;
                e.subPointer = ((b - 0x40) << 8) + gb.ReadByte();
                e.subPointer += 0xFC000;
                gb.BufferLocation = e.subPointer;
                for (int i = 0; i < supid; i++)
                {
                    b = gb.ReadByte();
                    gb.BufferLocation--;
                    if ((b & 0x80) == 0)
                        goto LoadRealData;
                    gb.BufferLocation += 2;
                }
            }
            else
                gb.BufferLocation--;
        LoadRealData:
            e.subPointer = gb.BufferLocation;
            b = gb.ReadByte();
            b = (byte)(b * 2);
            gb.BufferLocation = 0xFDFB9 + b * 2;
            e.collisionHeight = gb.ReadByte();
            e.collisionWidth = gb.ReadByte();
            e.damageDealt = gb.ReadByte();
            e.health = gb.ReadByte();
            gb.BufferLocation = e.subPointer + 1;
            e.paletteFace = gb.ReadByte();
            GetAIAddress(id, out e.aiPointer, out e.aiFinal, out e.aiBank);
            return e;
        }

        public void GetAIAddress(byte id, out int pointerAddress, out int address, out byte bank)
        {
            bank = 0xF;
            if (id <= 0x70)
                bank--;
            if (id <= 0x30)
                bank--;
            if (id <= 8)
                bank = 0x10;
            id += id;
            bool carry = (int)(id + 0x34) > (byte)(id + 0x34);
            id += 0x34;
            address = id;
            address += (0x2F + (carry ? 1 : 0)) * 0x100;
            int temp = gb.BufferLocation;
            gb.BufferLocation = address;
            pointerAddress = address;
            address = bank * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            gb.BufferLocation = temp;
        }

        public void SaveEnemy(Enemy e)
        {
            gb.BufferLocation = e.baseLocation;
            gb.WriteByte(e.sprite);
            gb.WriteByte(e.vulnerable);
            gb.BufferLocation = e.subPointer + 1;
            gb.WriteByte(e.paletteFace);
            gb.BufferLocation -= 2;
            byte b = gb.ReadByte();
            b = (byte)(b * 2);
            gb.BufferLocation = 0xFDFB9 + b * 2;
            gb.WriteByte(e.collisionHeight);
            gb.WriteByte(e.collisionWidth);
            gb.WriteByte(e.damageDealt);
            gb.WriteByte(e.health);
            gb.BufferLocation = e.aiPointer;
            gb.WriteBytes(gb.Get2BytePointer(e.aiFinal));
        }
    }
}
