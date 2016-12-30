using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmInteractions : Form
    {
        InteractionLoader interactionLoader;
        GBHL.GBFile gb;
        int group;
        int map;
        Program.GameTypes game;

        public frmInteractions(InteractionLoader i, int grp, int mp, Program.GameTypes game, GBHL.GBFile g)
        {
            InitializeComponent();
            this.game = game;
            interactionLoader = i;
            gb = g;
            group = grp;
            map = mp;
        }

        private void frmInteractions_Load(object sender, EventArgs e)
        {
            nType_ValueChanged(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!interactionLoader.addInteraction((int)nType.Value))
            {
                MessageBox.Show("Not enough space. Try applying the \"Extra Object Bank\" patch if you haven't already.", "Not Enough Space");
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbAdd_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void nType_ValueChanged(object sender, EventArgs e)
        {
            lblInteractionType.Text = interactionLoader.getOpcodeDefinition((int)nType.Value);
            SolidBrush b = (SolidBrush)interactionLoader.getOpcodeColor((int)nType.Value);
            pInteractionColor.BackColor = b.Color;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("FOR BEGINNERS\nBasically, all you need to know to make a good hack is: Objects consist of anything that isn't available in the tileset selector, such as a colored rotating block in a dungeon.  They can also be used for events.  Basically, you can use existing objects by adding them the exact same way with the same object type found in the original game, or you could make custom scripts, which either uses Type 1 or Type 2.",
                "Interactions Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
