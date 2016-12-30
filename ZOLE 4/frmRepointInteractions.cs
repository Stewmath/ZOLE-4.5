using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GBHL;

namespace ZOLE_4
{
    public partial class frmRepointInteractions : Form
    {
        InteractionLoader loader;
        GBFile gb;
        int map, group = 0;
        Program.GameTypes game;

        public frmRepointInteractions(GBFile g, InteractionLoader i, int map, int group, Program.GameTypes game)
        {
            InitializeComponent();
            gb = g;
            loader = i;
            this.map=map;
            this.group=group;
            this.game = game;

            i.loadInteractions(map, group);
            nAddress.Value = i.getInteractionLocation();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
