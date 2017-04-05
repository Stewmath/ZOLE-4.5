using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    [DefaultEvent("ValueChanged")]
    public partial class SpriteDefinitionBox : UserControl
    {
        public SpriteDefinitionBox()
        {
            InitializeComponent();
        }

        public event valueChanged ValueChanged;

        public void SetVisibleBoxes(bool id, bool box1, bool box2, bool box3, bool box4)
        {
            label1.Visible = nID.Visible = id;
            label2.Visible = nValue1.Visible = box1;
            label3.Visible = nValue2.Visible = box2;
            label4.Visible = nValue3.Visible = box3;
            label5.Visible = nValue4.Visible = box4;
        }

        public void SetBoxValues(int box, string text, ushort value, ushort maximum)
        {
            if (box == 0) //ID
            {
                label1.Text = text;
                nID.Maximum = maximum;
                nID.Value = value;
            }
            else if (box == 1) //Value1
            {
                label2.Text = text;
                nValue1.Maximum = maximum;
                nValue1.Value = value;
            }
            else if (box == 2) //Value2
            {
                label3.Text = text;
                nValue2.Maximum = maximum;
                nValue2.Value = value;
            }
            else if (box == 3) //Value3
            {
                label4.Text = text;
                nValue3.Maximum = maximum;
                nValue3.Value = value;
            }
            else if (box == 4) //Value4
            {
                label5.Text = text;
                nValue4.Maximum = maximum;
                nValue4.Value = value;
            }
        }

        public ushort GetBoxValue(int box)
        {
            if (box == 0)
                return (ushort)nID.Value;
            if (box == 1)
                return (ushort)nValue1.Value;
            if (box == 2)
                return (ushort)nValue2.Value;
            if (box == 3)
                return (ushort)nValue3.Value;
            if (box == 4)
                return (ushort)nValue4.Value;
            return 0;
        }

        public delegate void valueChanged(object sender, EventArgs e);

        private void SpriteDefinitionBox_Load(object sender, EventArgs e)
        {

        }

        private void nID_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(sender, new EventArgs());
        }
    }
}
