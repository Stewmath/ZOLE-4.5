using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class frmEnemyEditor : Form
    {
        EnemyLoader enemyLoader;
        EnemyLoader.Enemy enemy;
        public frmEnemyEditor(EnemyLoader el)
        {
            InitializeComponent();
            enemyLoader = el;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            enemy = enemyLoader.LoadEnemy((byte)nMainID.Value, (byte)nSubID.Value);
            nSprite.Value = enemy.sprite;
            nVulnerability.Value = enemy.vulnerable;
            nFace.Value = (enemy.paletteFace & 0xF);
            nPalette.Value = (enemy.paletteFace >> 4);
            nCollisionWidth.Value = enemy.collisionWidth;
            nCollisionHeight.Value = enemy.collisionHeight;
            nDamage.Value = enemy.damageDealt;
            nHealth.Value = enemy.health;
            lblAIBank.Text = "AI Memory Address (Bank " + enemy.aiBank.ToString("X") + "):";
            int addr = enemy.aiFinal;
            if (addr < 0x4000)
                nAIAddress.Value = addr;
            else
                nAIAddress.Value = (addr % 0x4000) + 0x4000;
            pnlItems.Visible = true;
        }

        private void nAIAddress_ValueChanged(object sender, EventArgs e)
        {
            int final = (int)nAIAddress.Value;
            if (final >= 0x4000)
            {
                final -= 0x4000;
                final += enemy.aiBank * 0x4000;
            }
            enemy.aiFinal = final;
            lblAIAddress.Text = "0x" + final.ToString("X");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            enemy.sprite = (byte)nSprite.Value;
            enemy.vulnerable = (byte)nVulnerability.Value;
            enemy.paletteFace = (byte)(nFace.Value + ((byte)nPalette.Value << 4));
            enemy.collisionWidth = (byte)nCollisionWidth.Value;
            enemy.collisionHeight = (byte)nCollisionHeight.Value;
            enemy.damageDealt = (byte)nDamage.Value;
            enemy.health = (byte)nHealth.Value;
            enemyLoader.SaveEnemy(enemy);
            button1_Click(null, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The Main ID is the first two digits in the Enemy ID.  The Sub ID is the second two.  Enemy IDs can be found in the Resources menu.  Please use this editor with caution and don't change what you aren't exactly sure about as this could alter your enemy in a way you don't want.",
                "Enemy Editor Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
