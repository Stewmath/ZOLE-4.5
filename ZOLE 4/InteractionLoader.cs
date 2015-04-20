using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;

namespace ZOLE_4
{
	public class InteractionLoader
	{
		public GBHL.GBFile gb;
		public List<Interaction> interactions = new List<Interaction>();
		public int interactionLocation;
		public int loadedMap;
		public int loadedGroup;
		public int noLocation = 0x49977;

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

		public InteractionLoader(GBHL.GBFile g)
		{
			gb = g;
		}

		public int getInteractionAddress(int mapIndex, int mapGroup, Program.GameTypes game)
		{
			//TODO: Seasons
			loadedMap = mapIndex;
			loadedGroup = mapGroup;
			gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x15 : 0x11) * 0x4000 + mapGroup * 2 + (game == Program.GameTypes.Ages ? 0x32B : 0x1B3B);
			gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x15 : 0x11) * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
			gb.BufferLocation += mapIndex * 2;
			return (game == Program.GameTypes.Ages ? 0x12 : 0x11) * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
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
		}

		public int getTypeSpace(int type)
		{
			switch (type)
			{
				case 1:
					return 2;
				case 2:
					return 4;
				case 3:
					return 2;
				case 4:
					return 2;
				case 5:
					return 2;
				case 6:
					return 3;
				case 7:
					return 5;
				case 8:
					return 3;
				case 9:
					return 6;
				case 0xA:
					return 1;
			}
			return 0;
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
					{
						i.first = true;
						i.value8 = gb.ReadByte();
					}
					i.id = (ushort)((gb.ReadByte() << 8) + gb.ReadByte());
					i.x = i.y = -1;
					break;
				case 7: //Non-returning interaction
					if (sOneTime)
					{
						i.first = true;
						i.value8 = gb.ReadByte(); //Some byte that controls 
					}
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
					s.SetVisibleBoxes(true, i.first, false, false, false);
					s.SetBoxValues(0, "ID:", i.id, 65535);
					s.SetBoxValues(1, "Quantity:", i.value8, 255);
					//s.SetVisibleBoxes(true, true, false, false, false);
					//s.SetBoxValues(0, "ID:", i.id, 65535);
					//s.SetBoxValues(1, "Unkown:", i.value8, 255);
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

		public void saveInteractions(ref GBHL.GBFile g)
		{
			g.BufferLocation = interactionLocation;
			byte lastOpcode = 0x0;
			bool b = false;
			foreach (Interaction i in interactions)
			{
				if (i.opcode != lastOpcode || i.opcode == 0x3)
				{
					g.WriteByte((byte)(0xF0 + i.opcode));
					lastOpcode = (byte)i.opcode;
					b = true;
				}
				switch (i.opcode)
				{
					case 1: //Seems to have values for the last battles and Zelda's sprite in the coffin
						g.WriteByte((byte)(i.id >> 8));
						g.WriteByte((byte)(i.id & 0xFF));
						break;
					case 2: //Most common
						g.WriteByte((byte)(i.id >> 8));
						g.WriteByte((byte)(i.id & 0xFF));
						g.WriteByte((byte)(i.y));
						g.WriteByte((byte)(i.x));
						break;
					case 3: //Common for enemy spawns
						g.WriteByte((byte)(i.id & 0xFF));
						g.WriteByte((byte)(i.id >> 8));
						break;
					case 4: //Boss
						g.WriteByte((byte)(i.id & 0xFF));
						g.WriteByte((byte)(i.id >> 8));
						break;
					case 5: //Common for interaction spawns
						g.WriteByte((byte)(i.id & 0xFF));
						g.WriteByte((byte)(i.id >> 8));
						break;
					case 6: //Unknown
						if (b)
							g.WriteByte(i.value8);
						g.WriteByte((byte)(i.id >> 8));
						g.WriteByte((byte)(i.id & 0xFF));
						//g.WriteByte(i.value8);
						break;
					case 7: //Non-returning interaction
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
					//i.value8 = (byte)s.GetBoxValue(1);
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

		public int findFreeSpace(int amount, byte bank)
		{
			int found = 0;
			gb.BufferLocation = bank * 0x4000;
			bool warn = true;
		FindSpace:
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
				if (gb.BufferLocation == noLocation)
				{
					found = 0;
					continue;
				}
				found++;
			}

			int bl = gb.BufferLocation - found;
			gb.BufferLocation = bl;
			if (gb.ReadByte() != 0)
			{
				goto FindSpace;
			}
			gb.BufferLocation = bl - 1;

			if (gb.ReadByte() < 0xFE)
			{
				found = 0;
				gb.BufferLocation++;
				if (!warn)
				{
					gb.BufferLocation += 2;
					goto FindSpace;
				}
				else
				{
					if (System.Windows.Forms.MessageBox.Show("Warning: The byte before the free space is not ended with interaction data ending.\nAdding interactions here may intercept and corrupt other data.\nDo you wish to continue anyway (If you say no, safe space will attempt to be found)?", "Warning", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
					{
						return bl;
					}
					else
					{
						warn = false;
						gb.BufferLocation++;
						goto FindSpace;
					}
				}
			}
			int i = gb.Buffer[bl - 1];
			if (i < 0xFE)
			{
				gb.BufferLocation = bl + 1;
				goto FindSpace;
			}
			return bl;
		}

		public int findFreeSpaceSeasons(int amount, byte bank)
		{
			int found = 0;
			gb.BufferLocation = bank * 0x4000;
			while (found < amount)
			{
				if (gb.BufferLocation == gb.Buffer.Length - 1 || gb.BufferLocation == bank * 0x4000 + 0x3FFF)
				{
					return -1;
				}
				if (gb.ReadByte() != bank)
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

			return gb.BufferLocation - found;
		}

		public void repointInteractions(int location, Program.GameTypes game)
		{
			gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x15 : 0x11) * 0x4000 + loadedGroup * 2 + (game == Program.GameTypes.Ages ? 0x32B : 0x1B3B);
			gb.BufferLocation = (game == Program.GameTypes.Ages ? 0x15 : 0x11) * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
			gb.BufferLocation += loadedMap * 2;
			gb.WriteBytes(gb.Get2BytePointer(location));
		}

		public void deleteInteractions(Program.GameTypes game)
		{
			gb.BufferLocation = interactionLocation;
			if (interactions.Count == 0)
				return;
			byte b;
			while ((b = gb.ReadByte()) != 0xFF && b != 0xFE)
			{
				gb.BufferLocation--;
				gb.WriteByte(0);
			}
			gb.BufferLocation--;
			gb.WriteByte(0);
			repointInteractions(noLocation, game);
		}

		public bool addInteraction(int type, Program.GameTypes game)
		{
			byte[] prev = new byte[(game == Program.GameTypes.Ages ? 0x400000 : 0x200000)];
			Array.Copy(gb.Buffer, prev, gb.Buffer.Length);

			//Clear the old ones
			if (interactionLocation != noLocation && interactions.Count > 0)
			{
				gb.BufferLocation = interactionLocation;
				while (gb.ReadByte() < 0xFE)
				{
					gb.BufferLocation--;
					if (game == Program.GameTypes.Ages)
						gb.WriteByte(0);
					else
						gb.WriteByte((byte)(gb.BufferLocation / 0x4000));
				}
				gb.BufferLocation--;
				if (game == Program.GameTypes.Ages)
					gb.WriteByte(0);
				else
					gb.WriteByte((byte)(gb.BufferLocation / 0x4000));
			}

			int space = getTypeSpace(type);
			int lastOpcode = -1;
			foreach (Interaction i in interactions)
			{
				if (i.opcode != lastOpcode)
				{
					space++;
					lastOpcode = i.opcode;
				}
				space += getTypeSpace(i.opcode);
			}
			int location = -1;
			if (game == Program.GameTypes.Ages)
				location = findFreeSpace(space + 2, 0x12);
			else
				location = findFreeSpaceSeasons(space + 2, 0x11);
			if (location == -1)
			{
				gb.Buffer = prev;
				return false;
			}

			interactionLocation = location;
			repointInteractions(interactionLocation, game);

			//Something to set its position
			Interaction it = new Interaction();
			it.opcode = type;

			int temp = gb.BufferLocation;
			loadInteraction(type, false);
			it.x = interactions[interactions.Count - 1].x;
			it.y = interactions[interactions.Count - 1].y;
			interactions.RemoveAt(interactions.Count - 1);
			if (it.x != -1 || it.y != -1)
				it.x = it.y = 0;
			interactions.Add(it);

			saveInteractions(ref gb);
			loadInteractions(loadedMap, loadedGroup, game);
			return true;
		}

		public void deleteInteraction(int index, Program.GameTypes game)
		{
			gb.BufferLocation = interactionLocation;
			while (gb.ReadByte() < 0xFE)
			{
				gb.BufferLocation--;
				if (game == Program.GameTypes.Ages)
					gb.WriteByte(0);
				else
					gb.WriteByte((byte)(gb.BufferLocation / 0x4000));
			}
			gb.BufferLocation--;

			if (game == Program.GameTypes.Ages)
				gb.WriteByte(0);
			else
				gb.WriteByte((byte)(gb.BufferLocation / 0x4000));
			interactions.RemoveAt(index);
			saveInteractions(ref gb);
		}
	}
}
