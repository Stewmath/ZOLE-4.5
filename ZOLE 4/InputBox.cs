using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace ZOLE_4
{
    public partial class InputBox : Form
    {
        private bool sh = false;
        public InputBox(string text, string caption)
        {
            InitializeComponent();
            lblText.Text = text;
            this.Text = caption;
        }

        public InputBox(string text, string caption, bool useShort)
        {
            InitializeComponent();
            lblText.Text = text;
            this.Text = caption;
            sh = useShort;
            if (useShort)
            {
                nValue.Maximum = 0xFFFF;
            }
        }

        public object Value
        {
            get
            {
                if (!sh)
                    return (byte)nValue.Value;
                else
                    return (ushort)nValue.Value;
            }
            set
            {
                if (!sh)
                    nValue.Value = (byte)value;
                else
                    nValue.Value = (ushort)value;
            }
        }

        private void InputBox_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
