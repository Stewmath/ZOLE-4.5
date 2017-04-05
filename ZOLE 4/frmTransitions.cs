using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmTransitions : Form
    {
        public TransitionLoader transitionLoader;
        GBHL.GBFile gb;
        Program.GameTypes game;

        public frmTransitions(TransitionLoader tl, GBHL.GBFile g, Program.GameTypes ga)
        {
            InitializeComponent();
            gb = g;
            game = ga;
            transitionLoader = tl;
            nIndex.Maximum = tl.transitions.Count - 1;
            if (tl.transitions.Count == 0)
                groupBox1.Enabled = false;
            else
            {
                nMap.Value = transitionLoader.transitions[(int)nIndex.Value].map;
                cboDirection.SelectedIndex = transitionLoader.transitions[(int)nIndex.Value].direction;
                nFrom.Value = PaletteLoader.GetPaletteIndex(gb, game, transitionLoader.transitions[(int)nIndex.Value].from);
                nTo.Value = PaletteLoader.GetPaletteIndex(gb, game, transitionLoader.transitions[(int)nIndex.Value].to);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void nIndex_ValueChanged(object sender, EventArgs e)
        {
            if (nIndex.Value == -1)
                return;
            nMap.Value = transitionLoader.transitions[(int)nIndex.Value].map;
            cboDirection.SelectedIndex = transitionLoader.transitions[(int)nIndex.Value].direction;
            nFrom.Minimum = nTo.Minimum = -1;
            nFrom.Value = PaletteLoader.GetPaletteIndex(gb, game, transitionLoader.transitions[(int)nIndex.Value].from);
            nTo.Value = PaletteLoader.GetPaletteIndex(gb, game, transitionLoader.transitions[(int)nIndex.Value].to);
            nFrom.Minimum = nTo.Minimum = 0;
        }

        private void nMap_ValueChanged(object sender, EventArgs e)
        {
            TransitionLoader.Transition t = transitionLoader.transitions[(int)nIndex.Value];
            t.map = (byte)nMap.Value;
            transitionLoader.transitions[(int)nIndex.Value] = t;
        }

        private void cboDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            TransitionLoader.Transition t = transitionLoader.transitions[(int)nIndex.Value];
            t.direction = (byte)cboDirection.SelectedIndex;
            transitionLoader.transitions[(int)nIndex.Value] = t;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void nUnknown1_ValueChanged(object sender, EventArgs e)
        {
            TransitionLoader.Transition t = transitionLoader.transitions[(int)nIndex.Value];
            t.from = PaletteLoader.GetPaletteAddress(gb, game, (int)nFrom.Value);
            transitionLoader.transitions[(int)nIndex.Value] = t;
        }

        private void nUknown2_ValueChanged(object sender, EventArgs e)
        {
            TransitionLoader.Transition t = transitionLoader.transitions[(int)nIndex.Value];
            t.to = PaletteLoader.GetPaletteAddress(gb, game, (int)nTo.Value);
            transitionLoader.transitions[(int)nIndex.Value] = t;
        }
    }
}
