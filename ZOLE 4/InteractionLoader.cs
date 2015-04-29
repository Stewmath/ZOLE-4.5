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
		public int interactionLocation;
		public int loadedMap;
		public int loadedGroup;

		// Common address used for maps with no interaction data
		private int noLocation;

		// List of which op's can be "stacked" without adding another Fx byte
		private bool[] stackableOpcode = {
		true,true,true,false,
		false,true,false,true,
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
			public ushort value16;
			public byte value8;
			public byte value8s;
			public bool first;
		}

		public InteractionLoader(GBHL.GBFile g, Program.GameTypes game)
		{
			gb = g;
			if (game == Program.GameTypes.Ages)
			{
				// Check if patch has been applied
				if (gb.ReadByte(0x54328) == 0xc3)
					enableExtraInteractionBank(game);

				noLocation = 0x49977;
			}
			else
			{
				if (gb.ReadByte(0x458dc) == 0xcd)
					enableExtraInteractionBank(game);

				noLocation = 0x4634b;
			}
		}

		public void enableExtraInteractionBank(Program.GameTypes game)
		{
			if (game == Program.GameTypes.Ages)
				extraInteractionBank = 0xfa;
			else // Seasons
				extraInteractionBank = 0x7f;
		}

		public int getInteractionAddress(int mapIndex, int mapGroup, Program.GameTypes game)
		{
			//TODO: Seasons
			loadedMap = mapIndex;
			loadedGroup = mapGroup;
			gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x15 : 0x11) * 0x4000 + mapGroup * 2 + (game == Program.GameTypes.Ages ? 0x32B : 0x1B3B);
			gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x15 : 0x11) * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
			gb.BufferLocation += mapIndex * 2;

			int pointer = gb.ReadByte() + (gb.ReadByte() << 8);

			if (extraInteractionBank != 0 && (pointer & 0x8000) != 0)
				// If using the extra interaction bank, read from that bank
				return extraInteractionBank * 0x4000 + (pointer & 0x3fff);

			if (game == Program.GameTypes.Ages)
				return 0x12 * 0x4000 + (pointer & 0x3fff);
			else // Seasons
				return 0x11 * 0x4000 + (pointer & 0x3fff);
		}

		public void loadInteractions(int mapIndex, int mapGroup, Program.GameTypes game)
		{
			gb.BufferLocation = interactionLocation = getInteractionAddress(mapIndex, mapGroup, game);
			byte b;
			interactions = new List<Interaction>();
			int opcode = 2; //Most common, with basically raw values
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
				loadInteraction(opcode, bb);
				bb = false;
			}

			if (getDataSize() != (gb.BufferLocation - interactionLocation))
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
					return len+1;
			}
			return len;
		}

		public int getDataSize()
		{
			int space = 0;
			int lastOpcode = -1;
			foreach (Interaction i in interactions)
			{
				space += getTypeSpace(i.opcode, i.opcode != lastOpcode);
				lastOpcode = i.opcode;
			}

			return space+1;
		}

		public void loadInteraction(int opcode, bool sOneTime)
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
					if (sOneTime)
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
					/*i.value8 = gb.ReadByte();
					byte temp = gb.ReadByte();
					i.value8s = gb.ReadByte();
					
					i.id = (ushort)(temp + (gb.ReadByte() << 8));
					i.x = i.y = -1;*/
					i.id = (ushort)((gb.ReadByte() << 8) + gb.ReadByte());
					i.value8 = gb.ReadByte();
					i.value8s = gb.ReadByte();
					i.y = gb.ReadByte();
					i.x = gb.ReadByte();
					break;
				case 0xA: //???
					i.id = gb.ReadByte();
					i.x = i.y = -1;
					break;
				default:
					gb.ReadByte(); //To make it advance
					i.x = i.y = -1;
					break;
			}
			interactions.Add(i);
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
				case 1: return "No-Value Interaction";
				case 2: return "Double-Value Interaction";
				case 3: return "Interaction Pointer";
				case 4: return "Boss Interaction Pointer";
				case 5: return "Conditional Interaction Pointer";
				case 6: return "Random Position Enemy";
				case 7: return "Specific Position Enemy";
				case 8: return "Owl Statue/Trigger/Switch";
				case 9: return "Quadrouple-Value Interaction";
				case 0xA: return "Unknown";
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
					s.SetBoxValues(0, "ID:", i.id, 65535);
					s.SetBoxValues(1, "Effect:", i.value8, 255);
					s.SetBoxValues(2, "Unknown:", i.value8s, 255);
					s.SetBoxValues(3, "X:", (ushort)i.x, 255);
					s.SetBoxValues(4, "Y:", (ushort)i.y, 255);
					break;
				case 0xA:
					s.SetVisibleBoxes(true, false, false, false, false);
					s.SetBoxValues(0, "ID:", i.id, 255);
					break;
			}
		}

		public void saveInteractions(ref GBHL.GBFile g, Program.GameTypes game)
		{
			g.BufferLocation = interactionLocation;
			int lastOpcode = -1;
			bool b = false;
			foreach (Interaction i in interactions)
			{
				if (i.opcode != lastOpcode || !stackableOpcode[i.opcode])
				{
					g.WriteByte((byte)(0xF0 + i.opcode));
					lastOpcode = (byte)i.opcode;
					b = true;
				}
				switch (i.opcode)
				{
					case 1: //No-value
						g.WriteByte((byte)(i.id >> 8));
						g.WriteByte((byte)(i.id & 0xFF));
						break;
					case 2: //Double-value
						g.WriteByte((byte)(i.id >> 8));
						g.WriteByte((byte)(i.id & 0xFF));
						g.WriteByte((byte)(i.y));
						g.WriteByte((byte)(i.x));
						break;
					case 3: //Interaction pointer
						g.WriteByte((byte)(i.id & 0xFF));
						g.WriteByte((byte)(i.id >> 8));
						break;
					case 4: //Boss interaction pointer
						g.WriteByte((byte)(i.id & 0xFF));
						g.WriteByte((byte)(i.id >> 8));
						break;
					case 5: //Common for interaction spawns
						g.WriteByte((byte)(i.id & 0xFF));
						g.WriteByte((byte)(i.id >> 8));
						break;
					case 6: //Random-position enemy
						g.WriteByte(i.value8);
						g.WriteByte((byte)(i.id >> 8));
						g.WriteByte((byte)(i.id & 0xFF));
						break;
					case 7: //Specific-position enemy
						if (b)
							g.WriteByte(i.value8);
						g.WriteByte((byte)(i.id >> 8));
						g.WriteByte((byte)(i.id & 0xFF));
						g.WriteByte((byte)i.y);
						g.WriteByte((byte)i.x);
						break;
					case 8: //Used for wise owl statues triggers, and switches
						g.WriteByte((byte)(i.id >> 8));
						g.WriteByte((byte)(i.id & 0xFF));
						g.WriteByte(i.value8);
						break;
					case 9: //Used for texts after dungeons
						/*g.WriteByte(i.value8);
						g.WriteByte((byte)(i.id & 0xFF));
						g.WriteByte(i.value8s);
						g.WriteByte((byte)(i.id >> 8));*/
						g.WriteWord(i.id);
						g.WriteByte(i.value8);
						g.WriteByte(i.value8s);
						g.WriteByte((byte)i.y);
						g.WriteByte((byte)i.x);
						break;
					case 0xA: //???
						g.WriteByte((byte)i.id);
						break;
				}
				b = false;
			}
			g.WriteByte(0xFF);

			Debug.Print("Interaction real size: " + (gb.BufferLocation - interactionLocation));
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
					i.id = s.GetBoxValue(0);
					i.value8 = (byte)s.GetBoxValue(1);
					i.value8s = (byte)s.GetBoxValue(2);
					i.x = (byte)s.GetBoxValue(3);
					i.y = (byte)s.GetBoxValue(4);
					break;
				case 0xA:
					i.id = (byte)s.GetBoxValue(0);
					break;
			}
			interactions[index] = i;
		}

		public int findFreeSpace(int amount, byte bank, Program.GameTypes game)
		{
			int blankByte = 0;
			if (game == Program.GameTypes.Seasons && bank < 0x40)
				blankByte = bank;

			int found = 0;
			gb.BufferLocation = bank * 0x4000;
		FindSpace:
			while (found < amount)
			{
				if (gb.BufferLocation == gb.Buffer.Length - 1 || gb.BufferLocation == bank * 0x4000 + 0x3FFF)
				{
					return -1;
				}
				if (gb.ReadByte() != blankByte)
				{
					found = 0;
					continue;
				}
				if (gb.BufferLocation == noLocation)
				{
					found = 0;
					continue;
				}
				found++;
			}

			int bl = gb.BufferLocation - found;
			gb.BufferLocation = bl;
			if (gb.ReadByte() != blankByte)
			{
				goto FindSpace;
			}
			gb.BufferLocation = bl - 1;

			if (gb.ReadByte() < 0xFE)
			{
				found = 0;
				gb.BufferLocation++;
				if (bank == extraInteractionBank && (bl & 0x3fff) == 0)
				{
					return bl;
				}
				else
				{
					gb.BufferLocation++;
					goto FindSpace;
				}
			}
			return bl;
		}

		public void repointInteractions(int location, Program.GameTypes game)
		{
			gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x15 : 0x11) * 0x4000 + loadedGroup * 2 + (game == Program.GameTypes.Ages ? 0x32B : 0x1B3B);
			gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x15 : 0x11) * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
			gb.BufferLocation += loadedMap * 2;
			if (location / 0x4000 == extraInteractionBank)
			{
				gb.WriteByte((byte)(location&0xff));
				gb.WriteByte((byte)(((location >> 8) & 0x3f) + 0xc0));
			}
			else
				gb.WriteBytes(gb.Get2BytePointer(location));

			interactionLocation = location;
		}

		public bool addInteraction(int type, Program.GameTypes game)
		{
			// Backup state of ROM in case of failure
			byte[] prev = new byte[gb.Buffer.Length];
			Array.Copy(gb.Buffer, prev, gb.Buffer.Length);

			// Delete old interaction data
			clearInteractionSpace(game);
			
			// Put new interaction into the list
			Interaction it = new Interaction();
			it.opcode = type;
			it.x = it.y = 0;
			interactions.Add(it);

			// Find free space
			int dataSize = getDataSize();
			int location = -1;
			if (game == Program.GameTypes.Ages)
				location = findFreeSpace(dataSize, 0x12, game);
			else
				location = findFreeSpace(dataSize, 0x11, game);
			if (location == -1 && extraInteractionBank > 0)
				location = findFreeSpace(dataSize, extraInteractionBank, game);

			if (location == -1)
			{
				// No suitable location found
				interactions.RemoveAt(interactions.Count - 1);
				// Revert the state of the ROM
				gb.Buffer = prev;
				return false;
			}

			interactionLocation = location;
			repointInteractions(interactionLocation, game);

			saveInteractions(ref gb, game);
			loadInteractions(loadedMap, loadedGroup, game);
			return true;
		}

		public void deleteInteraction(int index, Program.GameTypes game)
		{
			clearInteractionSpace(game);
			interactions.RemoveAt(index);
			saveInteractions(ref gb, game);
		}

		// Clears out the data at interactionLocation
		// The amount it clears out is based on getDataSize() so make sure the interaction list is accurate before calling
		private void clearInteractionSpace(Program.GameTypes game)
		{
			if (interactionLocation == noLocation)
				return;
			gb.BufferLocation = interactionLocation;
			int oldDataSize = getDataSize();
			for (int i = 0; i < oldDataSize; i++)
			{
				byte bank = (byte)(interactionLocation / 0x4000);
				if (game == Program.GameTypes.Seasons && bank < 0x40)
					gb.WriteByte(bank);
				else
					gb.WriteByte(0);
			}
		}
	}
}
