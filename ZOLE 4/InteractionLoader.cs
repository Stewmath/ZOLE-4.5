using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace ZOLE_4
{
    public class InteractionLoader
    {
        public GBHL.GBFile gb;
        public List<Interaction> interactions = new List<Interaction>();
        public int loadedMap;
        public int loadedGroup;
        Program.GameTypes loadedGame;

        // List of which op's can be "stacked" without adding another Fx byte
        private bool[] stackableOpcode = {
        true,true,true,false,
        false,false,false,true,
        true,true,true,true,
        true,true,true,true};

        // Bank which can be used for extra storage space for interaction data
        // Requires the corresponding ASM patch
        private byte extraInteractionBank = 0;


        public struct Interaction
        {
            public ushort id;
            public int x;
            public int y;
            public int opcode;
            public byte value8;
            public byte position;
            public byte type;
            public bool first;
        }

        public InteractionLoader(GBHL.GBFile g, Program.GameTypes game)
        {
            gb = g;
            loadedGame = game;

            if (game == Program.GameTypes.Ages)
            {
                // Check if patch has been applied
                if (gb.ReadByte(0x54328) == 0xc3)
                    enableExtraInteractionBank();

                // Many maps point here when they have no data.
                // But this isn't treated as a special case anymore.
                //noLocation = 0x49977;
            }
            else
            {
                if (gb.ReadByte(0x458dc) == 0xcd)
                    enableExtraInteractionBank();

                //noLocation = 0x4634b;
            }

            loadInteractions(0, 0);
        }

        public void enableExtraInteractionBank()
        {
            if (loadedGame == Program.GameTypes.Ages)
                extraInteractionBank = 0xfa;
            else // Seasons
                extraInteractionBank = 0x7f;
        }

        public void loadInteractions(int mapIndex, int mapGroup)
        {
            loadedGroup = mapGroup;
            loadedMap = mapIndex;

            interactions = readInteractionList(mapIndex, mapGroup);

            if (getDataSize(interactions) != (gb.BufferLocation - getInteractionLocation()))
                Debug.WriteLine("WARNING: calculated interaction size doesn't match actual size");
        }

        public int getTypeSpace(int type, bool first)
        {
            int len = 0;
            if (first || !stackableOpcode[type])
                len++;
            switch (type)
            {
                case 1:
                    return len+2;
                case 2:
                    return len+4;
                case 3:
                    return len+2;
                case 4:
                    return len+2;
                case 5:
                    return len+2;
                case 6:
                    return len+3;
                case 7:
                    if (first)
                        return len + 5;
                    else
                        return len + 4;
                case 8:
                    return len+3;
                case 9:
                    return len+6;
                case 0xA:
                    if (first)
                        return len+3;
                    else
                        return len+2;
            }
            return len;
        }

        public Brush getOpcodeColor(int opcode)
        {
            switch (opcode)
            {
                case 0: return Brushes.Black;
                case 1: return Brushes.Red;
                case 2: return Brushes.DarkOrange;
                case 3: return Brushes.Yellow;
                case 4: return Brushes.Green;
                case 5: return Brushes.Blue;
                case 6: return Brushes.Purple;
                case 7: return new SolidBrush(Color.FromArgb(128, 64, 0));
                case 8: return Brushes.Gray;
                case 9: return Brushes.White;
                case 0xA: return Brushes.Lime;
            }
            return Brushes.Magenta;
        }

        public string getOpcodeDefinition(int opcode)
        {
            switch (opcode)
            {
                case 0: return "Condition";
                case 1: return "No-Value Interaction";
                case 2: return "Double-Value Interaction";
                case 3: return "Object Pointer";
                case 4: return "Boss Object Pointer";
                case 5: return "Anti-Boss Object Pointer";
                case 6: return "Random Position Enemy";
                case 7: return "Specific Position Enemy";
                case 8: return "Owl Statue/Trigger/Switch (\"Part\")";
                case 9: return "Quadruple-Value Object";
                case 0xA: return "Item Drop";
            }
            return "Unknown Type " + (0xF0 + opcode).ToString("X");
        }

        public void setInteractionDef(Interaction i, ref SpriteDefinitionBox s)
        {
            switch (i.opcode)
            {
                case 1:
                    s.SetVisibleBoxes(true, false, false, false, false);
                    s.SetBoxValues(0, "ID:", i.id, 65535);
                    break;
                case 2:
                    s.SetVisibleBoxes(true, true, true, false, false);
                    s.SetBoxValues(0, "ID:", i.id, 65535);
                    s.SetBoxValues(1, "X:", (byte)i.x, 255);
                    s.SetBoxValues(2, "Y:", (byte)i.y, 255);
                    break;
                case 3:
                    s.SetVisibleBoxes(true, false, false, false, false);
                    s.SetBoxValues(0, "ID:", i.id, 65535);
                    break;
                case 4:
                    s.SetVisibleBoxes(true, false, false, false, false);
                    s.SetBoxValues(0, "ID:", i.id, 65535);
                    break;
                case 5:
                    s.SetVisibleBoxes(true, false, false, false, false);
                    s.SetBoxValues(0, "ID:", i.id, 65535);
                    break;
                case 6:
                    s.SetVisibleBoxes(true, true, false, false, false);
                    s.SetBoxValues(0, "ID:", i.id, 65535);
                    s.SetBoxValues(1, "Quantity:", i.value8, 255);
                    break;
                case 7:
                    s.SetVisibleBoxes(true, true, true, i.first, false);
                    s.SetBoxValues(0, "ID:", i.id, 65535);
                    s.SetBoxValues(1, "X:", (byte)i.x, 255);
                    s.SetBoxValues(2, "Y:", (byte)i.y, 255);
                    s.SetBoxValues(3, "Quantity:", i.value8, 255);
                    break;
                case 8:
                    s.SetVisibleBoxes(true, true, false, false, false);
                    s.SetBoxValues(0, "ID:", i.id, 65535);
                    s.SetBoxValues(1, "Position:", i.value8, 255);
                    break;
                case 9:
                    /*s.SetVisibleBoxes(true, true, true, false, false);
                    s.SetBoxValues(0, "ID:", i.id, 65535);
                    s.SetBoxValues(1, "Unknown:", i.value8, 255);
                    s.SetBoxValues(2, "Text Set:", i.value8s, 255);*/
                    s.SetVisibleBoxes(true, true, true, true, true);
                    s.SetBoxValues(0, "Type:", i.type, 255);
                    s.SetBoxValues(1, "ID:", i.id, 65535);
                    s.SetBoxValues(2, "Unknown:", i.value8, 255);
                    s.SetBoxValues(3, "X:", (ushort)i.x, 255);
                    s.SetBoxValues(4, "Y:", (ushort)i.y, 255);
                    break;
                case 0xA:
                    s.SetVisibleBoxes(i.first, true, true, false, false);
                    s.SetBoxValues(0, "Flags:", i.value8, 255);
                    s.SetBoxValues(1, "Item:", i.id, 255);
                    s.SetBoxValues(2, "Position:", i.position, 255);
                    break;
            }
        }

        // Doesn't do any checking to see whether there's enough space; that's dealt with in
        // "addInteraction".
        public void saveInteractions()
        {
            gb.BufferLocation = getInteractionLocation();
            int lastOpcode = -1;
            bool b = false;

            foreach (Interaction i in interactions)
            {
                if (i.opcode != lastOpcode || !stackableOpcode[i.opcode])
                {
                    gb.WriteByte((byte)(0xF0 + i.opcode));
                    lastOpcode = (byte)i.opcode;
                    b = true;
                }

                switch (i.opcode)
                {
                    case 1: //No-value
                        gb.WriteByte((byte)(i.id >> 8));
                        gb.WriteByte((byte)(i.id & 0xFF));
                        break;
                    case 2: //Double-value
                        gb.WriteByte((byte)(i.id >> 8));
                        gb.WriteByte((byte)(i.id & 0xFF));
                        gb.WriteByte((byte)(i.y));
                        gb.WriteByte((byte)(i.x));
                        break;
                    case 3: //Interaction pointer
                        gb.WriteByte((byte)(i.id & 0xFF));
                        gb.WriteByte((byte)(i.id >> 8));
                        break;
                    case 4: //Boss interaction pointer
                        gb.WriteByte((byte)(i.id & 0xFF));
                        gb.WriteByte((byte)(i.id >> 8));
                        break;
                    case 5: //Common for interaction spawns
                        gb.WriteByte((byte)(i.id & 0xFF));
                        gb.WriteByte((byte)(i.id >> 8));
                        break;
                    case 6: //Random-position enemy
                        gb.WriteByte(i.value8);
                        gb.WriteByte((byte)(i.id >> 8));
                        gb.WriteByte((byte)(i.id & 0xFF));
                        break;
                    case 7: //Specific-position enemy
                        if (b)
                            gb.WriteByte(i.value8);
                        gb.WriteByte((byte)(i.id >> 8));
                        gb.WriteByte((byte)(i.id & 0xFF));
                        gb.WriteByte((byte)i.y);
                        gb.WriteByte((byte)i.x);
                        break;
                    case 8: //Used for wise owl statues triggers, and switches
                        gb.WriteByte((byte)(i.id >> 8));
                        gb.WriteByte((byte)(i.id & 0xFF));
                        gb.WriteByte(i.value8);
                        break;
                    case 9: //Used for texts after dungeons
                        gb.WriteByte(i.type);
                        gb.WriteWord(i.id);
                        gb.WriteByte(i.value8);
                        gb.WriteByte((byte)i.y);
                        gb.WriteByte((byte)i.x);
                        break;
                    case 0xA: //Item drop
                        if (b)
                            gb.WriteByte((byte)i.value8);
                        gb.WriteByte((byte)i.id);
                        gb.WriteByte((byte)i.position);
                        break;
                }
                b = false;
            }
            gb.WriteByte(0xFF);

            Debug.Print("Interaction real size: " + (gb.BufferLocation - getInteractionLocation()));
        }

        public void writeInteractions(SpriteDefinitionBox s, int index)
        {
            Interaction i = interactions[index];
            switch (i.opcode)
            {
                case 1:
                    i.id = s.GetBoxValue(0);
                    break;
                case 2:
                    i.id = s.GetBoxValue(0);
                    i.x = s.GetBoxValue(1);
                    i.y = s.GetBoxValue(2);
                    break;
                case 3:
                    i.id = s.GetBoxValue(0);
                    break;
                case 4:
                    i.id = s.GetBoxValue(0);
                    break;
                case 5:
                    i.id = s.GetBoxValue(0);
                    break;
                case 6:
                    i.id = s.GetBoxValue(0);
                    i.value8 = (byte)s.GetBoxValue(1);
                    break;
                case 7:
                    i.id = s.GetBoxValue(0);
                    i.x = (byte)s.GetBoxValue(1);
                    i.y = (byte)s.GetBoxValue(2);
                    if (i.first)
                        i.value8 = (byte)s.GetBoxValue(3);
                    break;
                case 8:
                    i.id = s.GetBoxValue(0);
                    i.value8 = (byte)s.GetBoxValue(1);
                    i.x = (i.value8 & 0xF) * 16;
                    i.y = (i.value8 >> 4) * 16;
                    break;
                case 9:
                    i.type = (byte)s.GetBoxValue(0);
                    i.id = s.GetBoxValue(1);
                    i.value8 = (byte)s.GetBoxValue(2);
                    i.x = (byte)s.GetBoxValue(3);
                    i.y = (byte)s.GetBoxValue(4);
                    break;
                case 0xA:
                    i.value8 = (byte)s.GetBoxValue(0);
                    i.id = (byte)s.GetBoxValue(1);
                    i.position = (byte)s.GetBoxValue(2);
                    i.x = (i.position & 0xF) * 16;
                    i.y = (i.position >> 4) * 16;
                    break;
            }
            interactions[index] = i;
        }

        public void repointInteractions(int location)
        {
            gb.BufferLocation = (loadedGame == Program.GameTypes.Ages ? 0x15 : 0x11) * 0x4000 + loadedGroup * 2 + (loadedGame == Program.GameTypes.Ages ? 0x32B : 0x1B3B);
            gb.BufferLocation = (loadedGame == Program.GameTypes.Ages ? 0x15 : 0x11) * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            gb.BufferLocation += loadedMap * 2;
            if (location / 0x4000 == extraInteractionBank)
            {
                gb.WriteByte((byte)(location&0xff));
                gb.WriteByte((byte)(((location >> 8) & 0x3f) + 0xc0));
            }
            else
                gb.WriteBytes(gb.Get2BytePointer(location));
        }

        public bool addInteraction(int type)
        {
            if (type > 0xA || type < 1)
                return false;
            // Put new interaction into the list
            Interaction it = new Interaction();
            it.opcode = type;
            it.x = it.y = 0;
            interactions.Add(it);

            // Try to reallocate the interaction
            if (!reallocateInteractions())
            {
                // No suitable location found
                interactions.RemoveAt(interactions.Count - 1);
                return false;
            }

            loadInteractions(loadedMap, loadedGroup);
            return true;
        }

        public void deleteInteraction(int index)
        {
            // Reallocate if another map's interaction data overlaps with this one
            if (checkInteractionOverlap()) {
                Debug.WriteLine("Reallocating");
                // TODO: check for failure here...?
                reallocateInteractions();
            }

            clearInteractionSpace();

            interactions.RemoveAt(index);
            saveInteractions();
        }

        public int getInteractionLocation()
        {
            return getInteractionLocation(loadedMap, loadedGroup);
        }

        public int getInteractionLocation(int mapIndex, int mapGroup)
        {
            // TODO: verify that the extra interaction bank works, and Seasons works
            gb.BufferLocation = (loadedGame == Program.GameTypes.Ages ? 0x15 : 0x11) * 0x4000 + mapGroup * 2 + (loadedGame == Program.GameTypes.Ages ? 0x32B : 0x1B3B);
            gb.BufferLocation = (loadedGame == Program.GameTypes.Ages ? 0x15 : 0x11) * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);

            gb.BufferLocation += mapIndex*2;

            int pointer = gb.ReadByte() | (gb.ReadByte()<<8);
            if ((pointer&0x8000) == 0)
                return ((loadedGame == Program.GameTypes.Ages ? 0x12 : 0x11)*0x4000) + (pointer&0x3fff);
            else
                return (extraInteractionBank*0x4000) + (pointer&0x3fff);
        }

        // Private functions

        private bool reallocateInteractions() {
            // Backup state of ROM in case of failure
            byte[] prev = new byte[gb.Buffer.Length];
            Array.Copy(gb.Buffer, prev, gb.Buffer.Length);

            // Delete old interaction data
            clearInteractionSpace();

            int dataSize = getDataSize(interactions);
            int location = -1;
            if (loadedGame == Program.GameTypes.Ages)
                location = findFreeSpace(dataSize, 0x12);
            else
                location = findFreeSpace(dataSize, 0x11);
            if (location == -1 && extraInteractionBank > 0)
                location = findFreeSpace(dataSize, extraInteractionBank);

            if (location == -1) {
                // Revert the state of the ROM
                gb.Buffer = prev;
                return false;
            }

            repointInteractions(location);
            saveInteractions();

            return true;
        }

        // Uses loadedMap and loadedGroup to ignore space used by the current interaction
        private int findFreeSpace(int amount, byte bank)
        {
            int blankByte = 0;
            if (loadedGame == Program.GameTypes.Seasons && bank < 0x40)
                blankByte = bank;

            bool[] usedBytes = getUsedInteractionByteMap(loadedMap, loadedGroup);

            int found = 0;
            gb.BufferLocation = bank * 0x4000;

        FindSpace:
            while (found < amount)
            {
                if (gb.BufferLocation == gb.Buffer.Length - 1 || gb.BufferLocation == bank * 0x4000 + 0x3FFF)
                {
                    return -1;
                }
                if (usedBytes[gb.BufferLocation])
                {
                    gb.ReadByte();
                    found = 0;
                    continue;
                }
                if (gb.ReadByte() != blankByte)
                {
                    found = 0;
                    continue;
                }
                found++;
            }

            int bl = gb.BufferLocation - found;

            // For seasons only, try to make sure it'll be put right next to other interaction data.
            // Ages has a better solution: the "getUsedInteractionByteMap" function blacklists all
            // memory not related to interactions.
            if (loadedGame == Program.GameTypes.Seasons) {
                gb.BufferLocation = bl - 1;
                if (gb.ReadByte() < 0xFE)
                {
                    if (bank == extraInteractionBank && (bl & 0x3fff) == 0)
                        return bl;
                    else
                    {
                        gb.BufferLocation = bl + found;
                        found = 0;
                        goto FindSpace;
                    }
                }
            }
            return bl;
        }

        // This gets the data size of the interactionList passed.
        // The actual data size as the game stores it may vary, see "getPhysicalDataSize".
        private int getDataSize(List<Interaction> interactionList)
        {
            int space = 0;
            int lastOpcode = -1;
            foreach (Interaction i in interactionList)
            {
                space += getTypeSpace(i.opcode, i.opcode != lastOpcode);
                lastOpcode = i.opcode;
            }

            return space+1;
        }

        // This gets the actual size of the data at the interaction's location. The difference from
        // "getDataSize" is that this doesn't care about the current status of the "interactions"
        // list.
        private int getPhysicalDataSize()
        {
            int start = getInteractionLocation();
            List<Interaction> interacList = readInteractionList(loadedMap, loadedGroup);
            int end = gb.BufferLocation;

            return end-start;
        }

        private Interaction readInteraction(int opcode, bool sOneTime)
        {
            Interaction i = new Interaction();
            i.first = sOneTime;
            i.opcode = opcode;
            switch (opcode)
            {
                case 1: //Seems to have values for the last battles and Zelda's sprite in the coffin
                    i.id = (ushort)((gb.ReadByte() << 8) + gb.ReadByte());
                    i.x = i.y = -1;
                    break;
                case 2: //Most common
                    i.id = (ushort)((gb.ReadByte() << 8) + gb.ReadByte());
                    i.y = gb.ReadByte();
                    i.x = gb.ReadByte();
                    break;
                case 3: //Common for enemy spawns
                    i.id = (ushort)(gb.ReadByte() + (gb.ReadByte() << 8));
                    i.x = i.y = -1;
                    break;
                case 4: //Boss
                    i.id = (ushort)(gb.ReadByte() + (gb.ReadByte() << 8));
                    i.x = i.y = -1;
                    break;
                case 5: //Common for interaction spawns
                    i.id = (ushort)(gb.ReadByte() + (gb.ReadByte() << 8));
                    i.x = i.y = -1;
                    break;
                case 6: //Random spawn
                    i.value8 = gb.ReadByte();
                    i.id = (ushort)((gb.ReadByte() << 8) + gb.ReadByte());
                    i.x = i.y = -1;
                    break;
                case 7: //Specific position enemy
                    if (sOneTime)
                        i.value8 = gb.ReadByte();
                    i.id = (ushort)((gb.ReadByte() << 8) + gb.ReadByte());
                    i.y = gb.ReadByte();
                    i.x = gb.ReadByte();
                    break;
                case 8: //Used for wise owl statues triggers, and switches
                    i.id = (ushort)((gb.ReadByte() << 8) + gb.ReadByte());
                    byte locByte = gb.ReadByte();
                    i.value8 = locByte;
                    i.x = (locByte & 0xF) * 16 + 8;
                    i.y = (locByte >> 4) * 16 + 8;
                    break;
                case 9: //Used for texts after dungeons
                    i.type = gb.ReadByte();
                    i.id = (ushort)((gb.ReadByte() << 8) + gb.ReadByte());
                    i.value8 = gb.ReadByte();
                    i.y = gb.ReadByte();
                    i.x = gb.ReadByte();
                    break;
                case 0xA: // Item drops
                    if (sOneTime)
                        i.value8 = gb.ReadByte();
                    i.id = gb.ReadByte();
                    i.position = gb.ReadByte();

                    i.y = (i.position & 0xf0) + 8;
                    i.x = ((i.position << 4) & 0xf0) + 8;
                    break;
                default:
                    gb.ReadByte(); //To make it advance
                    i.x = i.y = -1;
                    break;
            }
            return i;
        }

        private List<Interaction> readInteractionList(int mapIndex, int mapGroup)
        {
            gb.BufferLocation = getInteractionLocation(mapIndex, mapGroup);

            byte b;
            List<Interaction> localList = new List<Interaction>();
            int opcode = -1;
            bool bb = false;
            while ((b = gb.ReadByte()) < 0xFE)
            {
                if (b >= 0xF0)
                {
                    opcode = b & 0xF;
                    bb = true;
                }
                else
                    gb.BufferLocation--;

                if (opcode == -1) {
                    Debug.WriteLine("Malformatted interactions");
                    break;
                }

                localList.Add(readInteraction(opcode, bb));
                bb = false;
            }

            return localList;
        }

        // Clears out the data at the interaction's location.
        // It doesn't clear data that is being used by other rooms (in case of overlapping
        // pointers).
        private void clearInteractionSpace()
        {
            int startLocation = getInteractionLocation();
            int bank = startLocation / 0x4000;

            byte freespaceByte = 0;
            if (loadedGame == Program.GameTypes.Seasons && bank < 0x40)
                    freespaceByte = (byte)(bank);

            bool[] usedBytes = getUsedInteractionByteMap(loadedMap, loadedGroup);

            int oldDataSize = getPhysicalDataSize();
            gb.BufferLocation = startLocation;
            for (int i = 0; i < oldDataSize; i++)
            {
                if (usedBytes[gb.BufferLocation]) {
                    gb.BufferLocation++;
                    continue;
                }
                gb.WriteByte(freespaceByte);
            }
        }

        // Returns a bool array of addresses used by interactions.
        private bool[] getUsedInteractionByteMap(int ignoreMap, int ignoreGroup) {
            bool[] used = new bool[gb.Buffer.Length];

            Action<int,int> fill = (start, end) => {
                for (int i=start; i<end; i++)
                    used[i] = true;
            };

            // Ages: mark hardcoded offsets where it's not ok to place data.
            // (TODO: do this for seasons)
            if (loadedGame == Program.GameTypes.Ages) {
                fill(0x48000, 0x49977);
                fill(0x4b6dd, 0x4be8f);
            }

            for (int g=0; g<6; g++) {
                for (int m=0; m<256; m++) {
                    if (m == ignoreMap && g == ignoreGroup)
                        continue;

                    int start = getInteractionLocation(m, g);
                    List<Interaction> interacList = readInteractionList(m, g);
                    int end = gb.BufferLocation;

                    for (int i=start;i<end;i++)
                        used[i] = true;
                }
            }

            return used;
        }

        // Checks whether the current map's interaction data overlaps with another.
        private bool checkInteractionOverlap() {
            bool[] usedBytes = getUsedInteractionByteMap(loadedMap, loadedGroup);
            int physicalSize = getPhysicalDataSize();
            int start = getInteractionLocation();

            for (int i=0; i<physicalSize; i++) {
                if (usedBytes[start+i])
                    return true;
            }

            return false;
        }
    }
}
