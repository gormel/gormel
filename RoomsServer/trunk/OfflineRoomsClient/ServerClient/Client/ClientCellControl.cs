using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OfflineRoomClient
{
	public class ClientCellControl : UserControl
	{
		public ClientCell Cell { get; set; }
		public ClientCellControl(ClientCell cell)
		{
			Cell = cell;
			DoubleBuffered = true;
			BackColor = Color.Transparent;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (Cell.Symbol != null)
				e.Graphics.DrawImage(Cell.Symbol.Image, new Rectangle(1, 1, Width - 2, Height - 2));
			if (Cell.IsWall)
				e.Graphics.FillRectangle(new SolidBrush(Color.DimGray), new Rectangle(new Point(), this.Size));
			if (Cell.Right)
				e.Graphics.DrawLine(new Pen(Color.Black), Width - 1, 0, Width - 1, Height - 1);
			if (Cell.Down)
				e.Graphics.DrawLine(new Pen(Color.Black), 0, Height - 1, Width - 1, Height - 1);
		}
	}
}
