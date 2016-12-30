using System;
using System.Collections.Generic;
using GBHL;

namespace ZOLE_4
{
    public class EssenceTeleportLoader
    {
        public struct EssenceTeleport
        {
            public byte mapgroup;
            public byte map;
            public byte yx;
            public byte unknown;
            public byte realmapgroup;
        }

        public EssenceTeleport[] teleports;
        public void LoadTeleports(GBFile gb)
        {
            teleports = new EssenceTeleport[8];
            gb.BufferLocation = 0x2874F;
            for (int i = 0; i < 8; i++)
            {
                EssenceTeleport t = new EssenceTeleport();
                t.realmapgroup = gb.ReadByte();
                t.mapgroup = (byte)(t.realmapgroup & 0xF);
                t.map = gb.ReadByte();
                t.yx = gb.ReadByte();
                t.unknown = gb.ReadByte();
                teleports[i] = t;
            }
        }

        public void SaveTeleports(GBFile gb, EssenceTeleport[] teleports)
        {
            gb.BufferLocation = 0x2874F;
            for (int i = 0; i < 8; i++)
            {
                if (teleports[i].realmapgroup == 0)
                    teleports[i].realmapgroup = 0x80;
                gb.WriteByte((byte)(((teleports[i].realmapgroup >> 4) << 4) + teleports[i].mapgroup));
                gb.WriteByte(teleports[i].map);
                gb.WriteByte(teleports[i].yx);
                gb.WriteByte(teleports[i].unknown);
            }
        }
    }
}
