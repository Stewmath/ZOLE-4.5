using System;
using System.Collections.Generic;
using System.Text;

namespace ZOLE_4
{
    public class TransitionLoader
    {
        GBHL.GBFile gb;
        public TransitionLoader(GBHL.GBFile g)
        {
            gb = g;
        }

        public struct Transition
        {
            public byte map;
            public byte direction;
            public int from;
            public int to;
        }

        public List<Transition> transitions;
        public void LoadTransitions(int group)
        {
            transitions = new List<Transition>();
            gb.BufferLocation = 0x4886 + group;
            gb.BufferLocation += gb.ReadByte();
            while (true)
            {
                byte b = gb.ReadByte();
                if (b == 0xFF)
                    break;
                Transition t = new Transition();
                t.map = gb.ReadByte();
                t.direction = b;
                t.from = gb.ReadByte() + gb.ReadByte() * 0x100;
                t.to = gb.ReadByte() + gb.ReadByte() * 0x100;
                transitions.Add(t);
            }
        }

        public void SaveTransitions(int group)
        {
            gb.BufferLocation = 0x4886 + group;
            gb.BufferLocation += gb.ReadByte();
            foreach (Transition t in transitions)
            {
                gb.WriteByte(t.direction);
                gb.WriteByte(t.map);
                gb.WriteByte((byte)(t.from & 0xFF));
                gb.WriteByte((byte)(t.from >> 8));
                gb.WriteByte((byte)(t.to & 0xFF));
                gb.WriteByte((byte)(t.to >> 8));
            }
        }
    }
}
