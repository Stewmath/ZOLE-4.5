using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmEssenceTeleport : Form
    {
        EssenceTeleportLoader.EssenceTeleport[] teleports;
        public EssenceTeleportLoader.EssenceTeleport[] Teleports
        {
            get { return teleports; }
            set { teleports = value; }
        }
        public frmEssenceTeleport(EssenceTeleportLoader.EssenceTeleport[] teles)
        {
            teleports = teles;
            InitializeComponent();
            nDungeon_ValueChanged(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void nDungeon_ValueChanged(object sender, EventArgs e)
        {
            EssenceTeleportLoader.EssenceTeleport t = teleports[(int)nDungeon.Value - 1];
            nGroup.Value = t.mapgroup;
            nMap.Value = t.map;
            nXY.Value = t.yx;
            nStyle.Value = t.unknown;
        }

        private void nGroup_ValueChanged(object sender, EventArgs e)
        {
            teleports[(int)nDungeon.Value - 1].mapgroup = (byte)nGroup.Value;
        }

        private void nMap_ValueChanged(object sender, EventArgs e)
        {
            teleports[(int)nDungeon.Value - 1].map = (byte)nMap.Value;
        }

        private void nXY_ValueChanged(object sender, EventArgs e)
        {
            teleports[(int)nDungeon.Value - 1].yx = (byte)nXY.Value;
        }

        private void nStyle_ValueChanged(object sender, EventArgs e)
        {
            teleports[(int)nDungeon.Value - 1].unknown = (byte)nStyle.Value;
        }

        private void frmEssenceTeleport_Load(object sender, EventArgs e)
        {

        }
    }
}
