using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using GBHL;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmSign : Form
    {
        GBFile gb;
        int group;
        Bitmap signImage;
        public SignLoader signLoader;

        public frmSign(GBFile g, Image img, int group, int map, Bitmap signImage)
        {
            InitializeComponent();
            gb = g;
            lblGroup.Text = "Map Group: " + group;
            pMap.BackgroundImage = img;
            this.group = group;
            signLoader = new SignLoader(g);
            signLoader.LoadSigns(group);
            nSign.Maximum = signLoader.signs.Count - 1;
            if (signLoader.signs.Count == 0)
            {
                MessageBox.Show("There are no signs to edit in this map group.", "Notice");
                this.Close();
            }
            for (int i = 0; i < signLoader.signs.Count; i++)
            {
                if (signLoader.signs[i].map == map)
                {
                    nSign.Value = i;
                    break;
                }
            }
            signImage.MakeTransparent(Color.Magenta);
            for (int x = 0; x < 16; x++)
            {
                signImage.SetPixel(x, 0, Color.Red);
                signImage.SetPixel(x, 15, Color.Red);
                signImage.SetPixel(0, x, Color.Red);
                signImage.SetPixel(15, x, Color.Red);
            }

            this.signImage = signImage;
            if(img.Width > 160)
                pMap.CanvasSize = new Size(240, 176);
            nSign_ValueChanged(null, null);
        }

        private void frmSign_Load(object sender, EventArgs e)
        {

        }

        private void nSign_ValueChanged(object sender, EventArgs e)
        {
            nMap.Value = signLoader.signs[(int)nSign.Value].map;
            nText.Value = signLoader.signs[(int)nSign.Value].text;
            pMap.Invalidate();
        }

        private void UpdateSignPos(int x, int y)
        {
            SignLoader.Sign s = signLoader.signs[(int)nSign.Value];
            s.yx = (byte)(x + (y << 4));
            signLoader.signs[(int)nSign.Value] = s;
            pMap.Invalidate();
        }

        private void pMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (pMap.HoverIndex == -1)
                return;
            if (e.X / 16 == 0 && e.Y / 16 == 0)
            {
                MessageBox.Show("Signs cannot be placed at 0,0.", "Error");
                return;
            }
            UpdateSignPos(e.X / 16, e.Y / 16);
        }

        private void pMap_Paint(object sender, PaintEventArgs e)
        {
            int x = signLoader.signs[(int)nSign.Value].yx & 0xF;
            int y = signLoader.signs[(int)nSign.Value].yx >> 4;
            e.Graphics.DrawImage(signImage, x * 16, y * 16);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void nMap_ValueChanged(object sender, EventArgs e)
        {
            SignLoader.Sign s = signLoader.signs[(int)nSign.Value];
            s.map = (byte)nMap.Value;
            signLoader.signs[(int)nSign.Value] = s;
        }

        private void nText_ValueChanged(object sender, EventArgs e)
        {
            SignLoader.Sign s = signLoader.signs[(int)nSign.Value];
            s.text = (byte)nText.Value;
            signLoader.signs[(int)nSign.Value] = s;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is just a way to determine what sign you're editing.", "Sign Index Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the map that you're viewing.", "Map Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a double-digit text ID.  It can range from 00-FF.  The value determines a certain string of text that the sign shows.  The only way to edit the text is to run the game to see what text it shows, then edit it using ZOTE.",
                "Text ID Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
