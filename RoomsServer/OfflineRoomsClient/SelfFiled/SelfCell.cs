using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OfflineRoomClient
{
	public class SelfCell : UserControl, ICell<char>
	{
		public SelfCell()
		{
			DoubleBuffered = true;
			BackColor = Color.Transparent;
			Symbol = ' ';
			IsWall = false;
			Right = false;
			Down = false;
		}

		void label_MouseClick(object sender, MouseEventArgs e)
		{
			OnMouseClick(e);
		}

		void label_MouseDown(object sender, MouseEventArgs e)
		{
			OnMouseDown(e);
		}

		void label_MouseMove(object sender, MouseEventArgs e)
		{
			OnMouseMove(e);
		}

		public new bool Right { get; set; }
		public bool Down { get; set; }
		public bool IsWall { get; set; }


		public char Symbol { get; set; }

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			var symbolSize = e.Graphics.MeasureString(Symbol.ToString(), Font);
			PointF symbolPos = new PointF(Width / 2 - symbolSize.Width / 2, Height / 2 - symbolSize.Height / 2);
			e.Graphics.DrawString(Symbol.ToString(), Font, new SolidBrush(Color.Black), symbolPos);
			if (IsWall)
				e.Graphics.FillRectangle(new SolidBrush(Color.DimGray), new Rectangle(new Point(), this.Size));
			if (Right)
				e.Graphics.DrawLine(new Pen(Color.Black), Width - 1, 0, Width - 1, Height - 1);
			if (Down)
				e.Graphics.DrawLine(new Pen(Color.Black), 0, Height - 1, Width - 1, Height - 1);
		}
	}
}
