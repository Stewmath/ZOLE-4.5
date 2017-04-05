using System;
using System.Collections.Generic;

using System.Text;
using GBHL;
using System.Drawing;

namespace ZOLE_4
{
    //This class was a mostly direct translate from Z80 Assembly to C#.
    //No optimizing or neat coding intended.
    public class Decompressor
    {
        GBFile gb;
        //Color[] bwPalette = new Color[] { Color.White, Color.LightGray, Color.DarkGray, Color.Black };
        public Color[] bwPalette = new Color[] { Color.FromArgb(26 * 8, 23 * 8, 20 * 8), Color.FromArgb(15 * 8, 18 * 8, 20 * 8), Color.FromArgb(8 * 8, 9 * 8, 12 * 8), Color.FromArgb(4 * 8, 2 * 8, 0 * 8) };
        //seemingly unknown values:
        int FF90Value;
        byte FF97Value; //current bank. useless
        byte gfxBank;
        byte FF93Value;
        byte FF8EValue;
        byte FF8BValue;
        byte FF8AValue;
        byte FF92Value;
        byte FF8FValue;
        byte FF8CValue;
        byte FF8DValue;
        byte FF55Value;
        ushort FF51Value;
        ushort FF53Value;
        public byte[] decompressedBuffer;
        public byte[] vramBuffer;
        public int location;

        public Decompressor(GBFile g)
        {
            gb = g;
        }

        //Common 8-bit bitmap?
        public void decompress1(int address, ref byte[] buffer, ref int writeLocation, int count)
        {
            int startWrite = writeLocation;
            gb.BufferLocation = address;
            byte a = 0;
            byte b = 0;
            byte c = 0;
            byte opcode;
            byte insertValue;
            while (true)
            {
            First:
                if (writeLocation - startWrite >= count)
                    break;
                a = gb.ReadByte();
                c = a;
                a = gb.ReadByte();
                opcode = a;
                if ((a | c) != 0)
                    goto EBitLoad1;
                b = 0x10;
            Raw:
                a = gb.ReadByte();
                buffer[writeLocation] = a;
                writeLocation++;
                b--;
                if (b > 0)
                    goto Raw;
                goto First;

            EBitLoad1:
                a = gb.ReadByte();
                insertValue = a;
                b = 8;
            EBitLoad1R:
                c = RotateLeft(c);
                if ((c & 1) == 1)
                    goto EBitLoad1I;
                a = gb.ReadByte();
                goto EBitLoad1VI;
            EBitLoad1I:
                a = insertValue;
            EBitLoad1VI:
                buffer[writeLocation] = a;
                writeLocation++;
                b--;
                if (b > 0)
                    goto EBitLoad1R;

                a = opcode;
                c = a;
                b = 8;
            EBitLoad2R:
                c = RotateLeft(c);
                if ((c & 1) == 1)
                    goto EBitLoad2I;
                a = gb.ReadByte();
                goto EBitLoad2VI;
            EBitLoad2I:
                a = insertValue;
            EBitLoad2VI:
                buffer[writeLocation] = a;
                writeLocation++;
                b--;
                if (b > 0)
                    goto EBitLoad2R;
                goto First;
            }
        }

        public static byte RotateLeft(byte b)
        {
            //if (b == 0xFF) //Yea, it's sadly needed
            //	return 0xFE;
            byte carry = (byte)(b >> 7);
            return (byte)((b << 1) | carry);
        }

        public static byte RotateRight(byte b)
        {
            return (byte)((b >> 1) | (b << 7));
        }

        public byte[, ,] draw(byte[] decompressed, ref Bitmap bmp)
        {
            byte[, ,] graphicsData = new byte[256, 8, 8];
            gb.ReadTiles(16, 16, decompressed, ref graphicsData);
            for (int i = 0; i < 256; i++)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        bmp.SetPixel(x + ((i % 16) * 8), y + ((i / 16) * 8), bwPalette[graphicsData[i, x, y]]);
                    }
                }
            }
            return graphicsData;
        }

        public void loadTileset(int headerAddress, int midTiles, int animation, Program.GameTypes game)
        {
            //3828 - start of procedure
            decompressedBuffer = vramBuffer = new byte[256 * 16];
            byte a = 0;
            loadTilesetHeader(headerAddress, 1, true, 0, 0, ref a);
            if (vramBuffer == null)
            {
                vramBuffer = new byte[4096];
                Array.Copy(decompressedBuffer, vramBuffer, decompressedBuffer.Length);
            }

            //Load mid-tile stuff
            loadTilesetMidTiles(midTiles, game);

            //Load animated tiles
            loadAnimatedTiles(animation, game);
        }

        public void loadTilesetMidTiles(int midTiles, Program.GameTypes game)
        {
            gb.BufferLocation = 0x10000;
            FF97Value = 4;
            byte a = (byte)midTiles;
            a &= 0x7F;
            if (a == 0)
                return;
            gb.BufferLocation = 0x10000 + (game == Program.GameTypes.Ages ? 0x1B28 : 0x195E);
            gb.BufferLocation += a * 2;
            gb.BufferLocation = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
        Loop:
            a = gb.ReadByte();
            a |= a;
            //jr z 487f
            ushort bc = a;
            FF8CValue = (byte)bc;
            bc = (ushort)((gb.ReadByte() * 0x100) + gb.ReadByte());
            ushort de = (ushort)((gb.ReadByte() * 0x100) + gb.ReadByte());
            a = gb.ReadByte();
            gb.BufferLocation--;
            a &= 0x7F;
            FF8DValue = a;
            int pushedHL = gb.BufferLocation;
            ushort pushedDE = de;
            gb.BufferLocation = 0x10000 + bc;
            bc = (ushort)(a * 0x100 + FF8CValue);
            a = (byte)(bc & 0xFF);
            de = 0xD807;
            loadTilesetHeader(0, 0, false, pushedDE, bc, ref a);

            de = pushedDE;
            gb.BufferLocation = pushedHL;
            a = gb.ReadByte();

            bool carry = (a + a > 255);
            a += a;
            if (carry)
                goto Loop;
        }

        public void loadAnimatedTiles(int animation, Program.GameTypes game)
        {
            byte a;
            try
            {
                a = (byte)animation;
                if (a == 0xFF)
                    return;
                byte bank = 4;
                int addr1 = (game == Program.GameTypes.Ages) ? 0x1B52 : 0x19B0;
                int addr2 = (game == Program.GameTypes.Ages) ? 0x1BE9 : 0x1A48;

                gb.BufferLocation = (bank * 0x4000) + addr1 + (a * 2);
                int data1 = bank * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
                gb.BufferLocation = data1;

                int[] anim_speed = new int[4];
                int[] anim_addr = new int[4];
                int[] anim_number = new int[4];

                int tile_animation_flags = gb.ReadByte();
                for (int i = 0; i < 4; i++)
                {
                    int a2_addr = bank * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
                    int temp = gb.BufferLocation;
                    gb.BufferLocation = a2_addr;
                    if (a2_addr >= bank * 0x4000 && a2_addr < 0x4000 + bank * 0x4000)
                    {
                        anim_speed[i] = gb.ReadByte();
                        anim_addr[i] = a2_addr + 1;
                    }
                    else
                    {
                        if ((tile_animation_flags & (1 << i)) != 0)
                        {
                            anim_addr[i] = -1;
                            anim_speed[i] = -1;
                            tile_animation_flags &= ~(1 << i);
                        }
                    }
                    gb.BufferLocation = temp;
                }

                for (int i = 0; i < 4; i++)
                {
                    if ((tile_animation_flags & (1 << i)) != 0)
                    {
                        int addr = anim_addr[i];
                        if (addr >= bank * 0x4000 && addr < 0x4000 + bank * 0x4000)
                        {
                            gb.BufferLocation = addr;
                            int nextspeed = 0;
                            int n;
                            int frame_count = 0;

                            while (nextspeed != 255)
                            {
                                n = gb.ReadByte();
                                nextspeed = gb.ReadByte();
                                frame_count++;
                            }
                            gb.BufferLocation -= 2;
                            for (int frame = frame_count - 1; frame >= 0; frame--)
                            {
                                n = gb.ReadByte();
                                nextspeed = gb.ReadByte();
                                gb.BufferLocation -= 4;
                                anim_number[i] = n;
                                anim_speed[i] = nextspeed;
                                int temp = gb.BufferLocation;

                                gb.BufferLocation = bank * 0x4000 + addr2 + n * 6;
                                byte src_bank = gb.ReadByte();
                                int src_addr = src_bank * 0x4000 + ((gb.ReadByte() - 0x40) * 0x100) + gb.ReadByte();
                                int dest_addr = gb.ReadByte() * 0x100 + gb.ReadByte();
                                dest_addr &= ~0xF;
                                int length = gb.ReadByte();
                                gb.BufferLocation = src_addr;
                                if (dest_addr >= 0x8000 && dest_addr < 0xA000)
                                {
                                    int length16 = (length + 1) * 16;
                                    while (length16 > 0)
                                    {
                                        if (dest_addr == 0x28C0)
                                        {
                                            i = 0;
                                        }
                                        decompressedBuffer[dest_addr - 0x8800] = gb.ReadByte();
                                        dest_addr++;
                                        length16--;
                                    }
                                }
                                gb.BufferLocation = temp;
                            }
                        }
                        else
                        {
                            i = 0;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void procedure58FD(ref byte a, ref ushort bc, ref ushort de, ref byte cd30)
        {
        Add5900:
            a = 1; //Frame index???
            a &= 0x1F;
            ushort hl = 0xDB90;
            bool carry = (a + 0x90) > 255;
            a += 0x90;
            hl = (ushort)(0xDB00 + a);
            if (carry)
                goto Out;
            hl += 0x100;
        Out:
            bc = (ushort)(bc & 0xFF); //The value at *hl seems to always be 00 and ineffective otherwise
            a = 0;
            Procedure59B7(ref a, ref bc, ref de, ref hl);

            //539b
            hl = 0xCB30;
            if ((cd30 & 0x40) == 0)
                cd30 += 40;
            //deals with unknown values at cd30. skipping for now
            if ((cd30 | (hl >> 8)) != 0)
                goto Add5900;
        }

        private void Procedure59B7(ref byte a, ref ushort bc, ref ushort de, ref ushort hl)
        {
            bc = 6;
            //b = 0
            //19d
            de = (ushort)(((de >> 8) * 0x100) + 8);
            int temp = gb.BufferLocation;
            gb.BufferLocation = 0;
        Add1A3:
            gb.BufferLocation += gb.BufferLocation;
            bool carry = (a + a) > 255;
            a += a;
            if (!carry)
                goto Add1A8;
            gb.BufferLocation += bc;
        Add1A8:
            de--;
            if ((de & 0xFF) > 0)
                goto Add1A3;
            //59bc
            gb.BufferLocation += 0x10000;
            gb.BufferLocation += 0x1BE9;
            bc = (ushort)(((bc >> 8) * 0x100) + gb.ReadByte());
            de = (ushort)((gb.ReadByte() * 0x100) + gb.ReadByte());
            ushort pushedDE = de; //gets popped to hl
            de = (ushort)((gb.ReadByte() * 0x100) + gb.ReadByte());
            bc = (ushort)((gb.ReadByte() * 0x100) + (bc & 0xFF));
            hl = pushedDE;

            //58a
            //some lcd stuff i have no control over. hardcoded no carry for now, so skip straight to 5af
            //5af
            a = FF97Value;
            byte pushedA = a;
            pushedDE = de; //at this point, de is the place to write the animated tile to
            a = (byte)(bc & 0xFF); //graphics bank
            de = hl;
            FF51Value = de;
            de = pushedDE;
            FF53Value = de;
            FF55Value = (byte)(bc >> 8);
            a = pushedA;
            gb.BufferLocation = a * 0x4000;
            a = 0;
        }

        private void Procedure3698(ref byte a, ref ushort de, ref byte[] ram)
        {
            int pushedHL = gb.BufferLocation;
            gb.BufferLocation = 0x10000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
            ram[de++] = gb.ReadByte();
            ram[de++] = (byte)(gb.BufferLocation & 0xFF);
            ram[de++] = (byte)(((gb.BufferLocation >> 8) & 0xFF) + 0x40);
            gb.BufferLocation = pushedHL + 2;
        }

        public void loadTilesetHeader(int src, byte bank, bool flag, int address2, int address3, ref byte a)
        {
            try
            {
                int writeLocation = 0;
                byte b;
                byte c;
                if (flag)
                {
                    location = src;
                    gb.BufferLocation = src;
                    if (gb.ReadByte() == 0xFF) //Uncompressed hack
                    {
                        gb.BufferLocation = 0x101000 + gb.ReadByte() * 3;
                        byte bb = gb.ReadByte();
                        gb.BufferLocation = bb * 0x4000 + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
                        decompressedBuffer = vramBuffer = gb.ReadBytes(decompressedBuffer.Length);
                        return;
                    }
                    gb.BufferLocation--;
                    gb.BufferLocation = (bank * 0x4000) + gb.ReadByte() + ((gb.ReadByte() - 0x40) * 0x100);
                }
                else
                {
                    b = (byte)(address3 >> 8);
                    c = (byte)(address3 & 0xFF);
                    //writeLocation = ((address2 >> 8) * 0x100) - 0x8800;
                    writeLocation = ((address2 >> 8) * 0x100) - 0x8800;
                    goto Main;
                }
            First:
                c = gb.ReadByte();
                int address = gb.ReadByte() * 0x100 + gb.ReadByte() - 0x4000 + (bank * 0x4000); //Graphics data. pushed de
                address2 = (gb.ReadByte() * 0x100) + gb.ReadByte(); //de
                writeLocation = ((address2 >> 8) * 0x100) - 0x8800;
                b = (byte)(gb.ReadByte() & 0x7F);
                address3 = c + (b * 0x100); //bc
                gb.BufferLocation--;
                FF90Value = gb.BufferLocation;
                gb.BufferLocation = address - 0x4000;
                goto Main;
            //End of header. Begin loading data

        Secondary:
                a = 1;
                FF97Value = a;
                a = gb.ReadByte(FF90Value);
                bool carry1 = (a + a > 255);
                a = (byte)(a + a);
                if (carry1)
                {
                    goto First;
                }
                return;

            Main:
                a = (byte)(address2 & 0xFF);
                a &= 0xF;
                a ^= (byte)(address2 & 0xFF);
                address2 = (ushort)(((address2 >> 8) * 0x100) + a);
                if (a != 0)
                    writeLocation += a;
                //writeLocation = address2 - 0x8800;
                a = (byte)((address3 & 0xFF) & 0x3F);
                gfxBank = a;
                if (gb.BufferLocation / 0x4000 != gfxBank)
                    gb.BufferLocation = (gb.BufferLocation % 0x4000) + gfxBank * 0x4000;
                address3 += 0x100;
                b++;
                a = (byte)((address3 & 0xFF) & 0xC0);
                if (a == 0)
                {
                    goto Add6E0;
                }
                if (a == 0xC0)
                {
                    goto Add6EE;
                }
                if (a == 0x40)
                {
                    goto Add6F2;
                }
                a = (byte)(address3 >> 8); //amount of bytes to decompress
                decompress1(gb.BufferLocation, ref decompressedBuffer, ref writeLocation, a * 16);
                goto Secondary;

            Add6E0:
                c = 0x10;
            Add6E2:
                a = gb.ReadByte();
                gfxBank = (byte)(gb.BufferLocation / 0x4000);
                decompressedBuffer[writeLocation] = a;
                writeLocation++;
                c--;
                if (c > 0)
                    goto Add6E2;
                b--;
                if (b > 0)
                    goto Add6E0;
                goto Secondary;
            Add6EE:
                a = 0xFF;
                goto Add6F5;
            Add6F2:
                a = 0;
                FF93Value = a;
            Add6F5:
                FF8EValue = a;
                b = (byte)(address3 >> 8);
                b = (byte)(((b & 0xF) << 4) + (b >> 4));
                a = b;
                a &= 0xF0;
                c = a;
                a ^= b;
                b = a;
                a = 1;
                FF8BValue = a;
            Add703:
                a = FF8BValue;
                a--;
                FF8BValue = a;
                if (a != 0)
                    goto Add714;
                a = 8;
                FF8BValue = a;
                a = gb.ReadByte();
                FF8AValue = a;
                procedure776(ref a, ref b, ref c);
            Add714:
                a = FF8AValue;
                bool carry = (a + a > 255);
                a = (byte)(a + a);
                FF8AValue = a;
                if (carry)
                    goto Add721;
                procedure772(ref a, ref b, ref c, ref writeLocation);
                if (a != 0)
                    goto Add703;
                goto Secondary;
            Add721:
                a = FF8EValue;
                a |= a;
                if (a != 0)
                {
                    goto Add734;
                }
                a = gb.ReadByte();
                gb.BufferLocation--;
                a &= 0x1F;
                FF92Value = a;
                a ^= gb.ReadByte();
                gb.BufferLocation--;
                if (a == 0)
                {
                    goto Add749;
                }
                a = (byte)(((a & 0xF) >> 4) + (a >> 4));
                a >>= 1;
                a++;
                goto Add74E;
            Add734:
                a = gb.ReadByte();
                FF92Value = a;
                procedure776(ref a, ref b, ref c);
                a = gb.ReadByte();
                gb.BufferLocation--;
                a &= 7;
                FF93Value = a;
                a ^= gb.ReadByte();
                gb.BufferLocation--;
                if (a == 0)
                    goto Add749;
                a >>= 3;
                a += 2;
                goto Add74E;
            Add749:
                gb.BufferLocation++;
                procedure776(ref a, ref b, ref c);
                a = gb.ReadByte();
                gb.BufferLocation--;
            Add74E: //Copy
                FF8FValue = a;
                gb.BufferLocation++;
                procedure776(ref a, ref b, ref c);
                int temp = gb.BufferLocation;
                a = (byte)(0xFF - FF92Value);
                byte l = a;
                a = (byte)(0xFF - FF93Value);
                byte h = a;
                ushort copyAddress = (ushort)(writeLocation - (0xFFFF - (h * 0x100 + l)) - 1);//h * 0x100 + l - 0x8800;
            StartCopy:
                a = decompressedBuffer[copyAddress];
                copyAddress++;
                decompressedBuffer[writeLocation] = a;
                writeLocation++;
                int i = b * 0x100 + c;
                i--;
                c = (byte)(i & 0xFF);
                b = (byte)(i >> 8);
                a = b;
                a |= c;
                if (a == 0)
                {
                    //gb.BufferLocation = temp;
                    goto Secondary;
                }
                a = FF8FValue;
                a--;
                FF8FValue = a;
                if (a != 0)
                    goto StartCopy;
                gb.BufferLocation = temp;
                goto Add703;
            }
            catch (Exception)
            {
                //used to catch bad tileset loads since the header is a user-inputted value
            }
        }

        void procedure776(ref byte a, ref byte b, ref byte c)
        {
            a = (byte)(((gb.BufferLocation % 0x4000) >> 8) + 0x40);
            if (a != 0x80)
                goto Add785;
            a = gfxBank;
            a++;
            gfxBank = a;
            gb.BufferLocation = gfxBank * 0x4000 + (gb.BufferLocation & 0xFF);
        Add785:
            a = b;
            a |= c;
            return;
        }

        void procedure772(ref byte a, ref byte b, ref byte c, ref int writeLocation)
        {
            try
            {
                int i = b * 0x100 + c;
                a = gb.ReadByte();
                decompressedBuffer[writeLocation] = a;
                writeLocation++;
                i--;
                c = (byte)(i & 0xFF);
                b = (byte)(i >> 8);
                procedure776(ref a, ref b, ref c);
            }
            catch (Exception)
            {
            }
        }
    }
}