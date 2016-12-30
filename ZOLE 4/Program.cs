using System;
using System.Collections.Generic;

using System.Windows.Forms;

namespace ZOLE_4
{
    public static class Program
    {
        public enum GameTypes
        {
            Ages,
            Seasons
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 f = new Form1();
            f.AutoScaleMode = AutoScaleMode.None;
            Application.Run(f);
        }

        public static string GetROMHeader(GBHL.GBFile gb)
        {
            gb.BufferLocation = 0x134;
            byte[] bytes = gb.ReadBytes(10);
            return System.Text.ASCIIEncoding.ASCII.GetString(bytes);
        }
    }
}
