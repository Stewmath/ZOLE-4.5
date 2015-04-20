using System;
using System.Collections.Generic;

using System.Text;
using GBHL;

namespace ZOLE_4
{
	public class Patches
	{
		public static void removeForestRand(GBFile gb)
		{
			gb.WriteByte(0x5F54, 0x18);
		}

		public static void setMakuGate(GBFile gb, byte b)
		{

		}

		public static void sixteenOverworld(GBFile gb)
		{
			gb.WriteByte(0x41A5, 0xF);
			gb.WriteByte(0x41AE, 0xF0);
		}

		public static void removeStartLock(GBFile gb, Program.GameTypes game)
		{
			//ASM script
			if(game == Program.GameTypes.Ages)
				gb.WriteBytes(0x1FF00, new byte[] { 0xEA, 0x11, 0x11, 0x3E, 0xDD, 0xEA, 0xD1, 0xC6, 0xC9 });
			else
				gb.WriteBytes(0x1FF00, new byte[] { 0xEA, 0x11, 0x11, 0x3E, 0xDD, 0xEA, 0xCB, 0xC6, 0xC9 });
			//Calling of ASM script
			if(game == Program.GameTypes.Ages)
				gb.WriteBytes(0x1C10B, new byte[] { 0xCD, 0x00, 0x7F });
			else
				gb.WriteBytes(0x1C103, new byte[] { 0xCD, 0x00, 0x7F });
		}

		public static void InstantAwake(GBFile gb)
		{

		}

		public static void crystalSwitches(GBFile gb)
		{
			gb.WriteByte(0x3657, 0xFF);
		}
	}
}
