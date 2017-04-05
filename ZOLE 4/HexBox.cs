using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Zelda_Oracles_Suite
{
	public class HexBox : UserControl
	{
		private int offsetSize { get { return (int)Font.Size * 5 + (int)Font.Size / 3; } }
		private byte[] bytes;
		public byte[] Bytes { get { return bytes; } set { bytes = value; HexBox_Resize(null, null); this.Invalidate(); } }

		private static string[] HexDigits = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };

		public int SelectionIndex { get; set; }

		private bool multiline;
		[Browsable(true), DefaultValue(false)]
		public bool MultiLine { get { return multiline; } set { multiline = value; ScrollBar.Visible = multiline; } }

		private bool showOffsets;
		[Browsable(true), DefaultValue(false)]
		public bool ShowOffsets { get { return showOffsets; } set { showOffsets = value; this.Invalidate(); HexBox_Resize(null, null); } }

		private int statusBarSize { get { return (int)Font.Size + 4; } }
		private bool showStatusBar;
		[Browsable(true), DefaultValue(false)]
		public bool ShowStatusBar { get { return showStatusBar; } set { showStatusBar = value; this.Invalidate(); HexBox_Resize(null, null); } }

		private SolidBrush stripeColor = new SolidBrush(Color.FromArgb(240, 240, 240));
		private bool showStripes = true;
		[Browsable(true), DefaultValue(true)]
		public bool ShowStripes { get { return showStripes; } set { showStripes = value; this.Invalidate(); } }

		private bool grayOnFocusLost;
		[Browsable(true), DefaultValue(true)]
		public bool GrayOnFocusLost { get { return grayOnFocusLost; } set { grayOnFocusLost = value; this.Invalidate(); } }

		private VScrollBar ScrollBar;

		public HexBox()
			: base()
		{
			InitializeComponent();
			ScrollBar.GotFocus += new EventHandler(ScrollBar_GotFocus);
			this.GotFocus += new EventHandler(HexBox_LostFocus);
			this.LostFocus += new EventHandler(HexBox_LostFocus);
			this.MouseWheel += new MouseEventHandler(HexBox_MouseWheel);

			bytes = new byte[18];
			this.Font = new System.Drawing.Font("Courier New", 12);
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			pevent.Graphics.Clear(BackColor);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			int bytesPerLine = (multiline ? (this.Width - (showOffsets ? offsetSize : 0) - ScrollBar.Width - 2) / ((int)(Font.Size - 3) * 3) * 2 : bytes.Length);
			int x = e.X - 6 - (showOffsets ? offsetSize : 0);
			int y = (e.Y - 4) / (int)Font.Size;
			if ((x / (int)(Font.Size - 3)) % 3 == 2)
				x -= 5;
			this.SelectionIndex = Math.Min(Math.Max(0, (x / (int)(Font.Size - 3)) - (x / ((int)(Font.Size - 3) * 3)) + ScrollBar.Value * bytesPerLine + y * bytesPerLine), bytes.Length * 2 - 1);
			if (!this.ScrollToSelection())
				this.Invalidate();
			this.Focus();
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Left && SelectionIndex > 0)
			{
				SelectionIndex--;
				if (!this.ScrollToSelection())
					this.Invalidate();
			}
			else if (keyData == Keys.Right && SelectionIndex < bytes.Length * 2 - 1)
			{
				SelectionIndex++;
				if (!this.ScrollToSelection())
					this.Invalidate();
			}
			if (keyData == Keys.Up && SelectionIndex > 0)
			{
				int bytesPerLine = (multiline ? (this.Width - (showOffsets ? offsetSize : 0) - ScrollBar.Width - 2) / ((int)(Font.Size - 3) * 3) * 2 : bytes.Length);
				SelectionIndex = Math.Max(0, SelectionIndex - bytesPerLine);
				if (!this.ScrollToSelection())
					this.Invalidate();
			}
			else if (keyData == Keys.Down && SelectionIndex < bytes.Length * 2 - 1)
			{
				int bytesPerLine = (multiline ? (this.Width - (showOffsets ? offsetSize : 0) - ScrollBar.Width - 2) / ((int)(Font.Size - 3) * 3) * 2 : bytes.Length);
				SelectionIndex = Math.Min(bytes.Length * 2 - 1, SelectionIndex + bytesPerLine);
				if (!this.ScrollToSelection())
					this.Invalidate();
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			byte c = 0;
			if (e.KeyValue >= (int)Keys.D0 && e.KeyValue <= (int)Keys.D9)
			{
				c = (byte)(e.KeyValue - (int)Keys.D0);
			}
			else if (e.KeyValue >= (int)Keys.NumPad0 && e.KeyValue <= (int)Keys.NumPad9)
			{
				c = (byte)(e.KeyValue - (int)Keys.NumPad0);
			}
			else if (e.KeyValue >= (int)Keys.A && e.KeyValue <= (int)Keys.F)
			{
				c = (byte)(e.KeyValue - (int)Keys.A + 10);
			}
			else
				return;
			if (SelectionIndex % 2 == 0) //left nybble
			{
				bytes[SelectionIndex / 2] = (byte)((c << 4) | (byte)(bytes[SelectionIndex / 2] & 0xF));
			}
			else //right nybble
			{
				bytes[SelectionIndex / 2] = (byte)((c & 0xF) | (byte)(bytes[SelectionIndex / 2] & 0xF0));
			}
			if (SelectionIndex < bytes.Length * 2 - 1)
				SelectionIndex++;

			if (!this.ScrollToSelection())
				this.Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
			e.Graphics.SetClip(new Rectangle(0, 0, this.Width, this.Height - (showStatusBar ? statusBarSize : 0)));
			int bytesPerLine = (multiline ? (this.Width - (showOffsets ? offsetSize : 0) - ScrollBar.Width - 2) / ((int)(Font.Size - 3) * 3) : bytes.Length);
			for (int i = (multiline ? ScrollBar.Value * bytesPerLine : 0); i < bytes.Length; i++)
			{
				int y = (int)Font.Size - 11 + (i - (multiline ? ScrollBar.Value * bytesPerLine : 0)) / bytesPerLine * (int)Font.Size;
				if (y >= this.Height - (showStatusBar ? statusBarSize : 0))
					break;
				if (multiline && showStripes && i % (2 * bytesPerLine) == bytesPerLine)
					e.Graphics.FillRectangle(stripeColor, 0, y + 3, this.Width, (int)Font.Size);
				int x = 1 + (i % bytesPerLine) * 3 * ((int)Font.Size - 3) + (showOffsets ? offsetSize : 0);

				if (SelectionIndex != i * 2 && SelectionIndex != i * 2 + 1)
				{
					e.Graphics.DrawString(HexDigits[(bytes[i] >> 4)] + HexDigits[(bytes[i] & 0xF)], Font, (grayOnFocusLost && !this.Focused ? Brushes.LightGray : Brushes.Black), new PointF(x, y));
				}
				else
				{
					if (SelectionIndex == i * 2 && this.Focused)
						e.Graphics.FillRectangle(Brushes.Black, x + 2, y + 3, (int)Font.Size - 2, (int)Font.Size);
					e.Graphics.DrawString(HexDigits[(bytes[i] >> 4)].ToString(), Font, (SelectionIndex == i * 2 && this.Focused ? Brushes.White : (grayOnFocusLost && !this.Focused ? Brushes.LightGray : Brushes.Black)), new PointF(x, y));
					if (SelectionIndex == i * 2 + 1 && this.Focused)
						e.Graphics.FillRectangle(Brushes.Black, x + 10, y + 3, (int)Font.Size - 2, (int)Font.Size);
					e.Graphics.DrawString(HexDigits[(bytes[i] & 0xF)].ToString(), Font, (SelectionIndex == i * 2 + 1 && this.Focused ? Brushes.White : (grayOnFocusLost && !this.Focused ? Brushes.LightGray : Brushes.Black)), new PointF(x + 8, y));
				}
				if ((y + (int)Font.Size >= this.Height - (showStatusBar ? statusBarSize : 0) || i + bytesPerLine >= bytes.Length) && i - (multiline ? ScrollBar.Value * bytesPerLine : 0) > 0)
					e.Graphics.DrawLine(Pens.Gray, new Point(x, 0), new Point(x, this.Height - (showStatusBar ? statusBarSize : 0) - 1));

				if (showOffsets && i % bytesPerLine == 0)
					e.Graphics.DrawString(ToHex(i), Font, (grayOnFocusLost && !this.Focused ? Brushes.LightGray : Brushes.Black), new PointF(-1, y));
			}

			e.Graphics.ResetClip();
			if (showStatusBar)
			{
				e.Graphics.DrawLine(Pens.Gray, 0, this.Height - statusBarSize, this.Width - 1, this.Height - statusBarSize);
				e.Graphics.DrawString(ToHex(SelectionIndex / 2), this.Font, (grayOnFocusLost && !this.Focused ? Brushes.LightGray : Brushes.Black), new PointF(this.Width - (showOffsets ? offsetSize : 0), this.Height - statusBarSize - 1));

				e.Graphics.DrawString(ToHex(bytes[SelectionIndex / 2], 1) + " = " + ASCIIEncoding.ASCII.GetString(bytes, SelectionIndex / 2, 1), this.Font, (grayOnFocusLost && !this.Focused ? Brushes.LightGray : Brushes.Black), (showOffsets ? offsetSize : 0) + 1, this.Height - statusBarSize - 1);
			}

			e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
		}

		private string ToHex(int i, int digits = 3)
		{
			StringBuilder sb = new StringBuilder();
			if (digits >= 3)
			{
				sb.Append(HexDigits[(i >> 20) & 0xF]);
				sb.Append(HexDigits[(i >> 16) & 0xF]);
			}
			if (digits >= 2)
			{
				sb.Append(HexDigits[(i >> 12) & 0xF]);
				sb.Append(HexDigits[(i >> 8) & 0xF]);
			}
			sb.Append(HexDigits[(i >> 4) & 0xF]);
			sb.Append(HexDigits[(i >> 0) & 0xF]);
			return sb.ToString();
		}

		public void Goto(int address)
		{
			if (address < 0)
				address = 0;
			else if (address >= bytes.Length)
				address = bytes.Length - 1;
			SelectionIndex = address * 2;
			ScrollToSelection();
		}

		public bool ScrollToSelection()
		{
			int bytesPerLine = (multiline ? (this.Width - (showOffsets ? offsetSize : 0) - ScrollBar.Width - 2) / ((int)(Font.Size - 3) * 3) : bytes.Length);
			int lines = (this.Height - (showStatusBar ? statusBarSize : 0) - 2) / bytesPerLine;

			if (SelectionIndex / 2 / bytesPerLine < ScrollBar.Value)
				ScrollBar.Value = Math.Max(0, SelectionIndex / 2 / bytesPerLine);
			else if (SelectionIndex / 2 / bytesPerLine >= ScrollBar.Value + lines - bytesPerLine - 1)
				ScrollBar.Value = Math.Min(ScrollBar.Maximum - ScrollBar.LargeChange + 1, SelectionIndex / 2 / bytesPerLine - lines + bytesPerLine + 1);
			else
				return false;

			this.Invalidate();
			return true;
		}

		private void InitializeComponent()
		{
			this.ScrollBar = new System.Windows.Forms.VScrollBar();
			this.SuspendLayout();
			// 
			// ScrollBar
			// 
			this.ScrollBar.LargeChange = 1;
			this.ScrollBar.Location = new System.Drawing.Point(83, 0);
			this.ScrollBar.Maximum = 0;
			this.ScrollBar.Name = "ScrollBar";
			this.ScrollBar.Size = new System.Drawing.Size(17, 20);
			this.ScrollBar.TabIndex = 0;
			this.ScrollBar.Visible = false;
			this.ScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBar_Scroll);
			// 
			// HexBox
			// 
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this.ScrollBar);
			this.Name = "HexBox";
			this.Size = new System.Drawing.Size(100, 20);
			this.Resize += new System.EventHandler(this.HexBox_Resize);
			this.ResumeLayout(false);

		}

		private void HexBox_Resize(object sender, EventArgs e)
		{
			ScrollBar.Location = new Point(this.Width - ScrollBar.Width - 1, 1);
			ScrollBar.Size = new Size(ScrollBar.Width, this.Height - (showStatusBar ? statusBarSize : 0) - 2);
			int bytesPerLine = Math.Max(1, (multiline ? (this.Width - (showOffsets ? offsetSize : 0) - ScrollBar.Width - 2) / ((int)(Font.Size - 3) * 3) : bytes.Length));
			int largeChange = 8;
			ScrollBar.Maximum = Math.Max(0, (bytes.Length / bytesPerLine) - (this.Height - (showStatusBar ? statusBarSize : 0) - 2) / bytesPerLine + largeChange * 2 - 1);
			ScrollBar.LargeChange = Math.Min(ScrollBar.Maximum, largeChange);
		}

		private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			this.Invalidate();
		}

		private void ScrollBar_GotFocus(object sender, EventArgs e)
		{
			this.Focus();
		}

		private void HexBox_LostFocus(object sender, EventArgs e)
		{
			this.Invalidate();
		}

		private void HexBox_MouseWheel(object sender, MouseEventArgs e)
		{
			if (e.Delta < 0)
				ScrollBar.Value = Math.Min(ScrollBar.Maximum - ScrollBar.LargeChange + 1, ScrollBar.Value + ScrollBar.LargeChange);
			else if (e.Delta > 0)
				ScrollBar.Value = Math.Max(0, ScrollBar.Value - ScrollBar.LargeChange);
			this.Invalidate();
		}
	}
}
