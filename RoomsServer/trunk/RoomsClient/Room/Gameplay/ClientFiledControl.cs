using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gameplay;

namespace RoomsClient
{
	public class ClientFiledControl : UserControl
	{
		public ClientFiled Filed { get; private set; }
		public event EventHandler<ClickedEventArgs> Clicked;
		private FiledCursor lastCursor = new FiledCursor();
		private Dictionary<Point, ClientCellControl> cells = new Dictionary<Point, ClientCellControl>();

		public ClientFiledControl(ClientFiled filed)
		{
			Filed = filed;
			Resize += ClientFiledControl_Resize;
			var xy = Enumerable.Range(0, Filed.Width)
							  .Select(x => Enumerable.Range(0, Filed.Height)
										  .Select(y => new Point(x, y)))
							  .SelectMany(p => p);
			foreach (var p in xy)
			{
				ClientCellControl cellControll = new ClientCellControl(Filed[p]);
				cellControll.MouseMove += ClientFiledControl_MouseMove;
				cellControll.MouseClick += ClientFiledControl_MouseClick;
				cellControll.MouseClick += (s, e) => OnMouseClick(e);
				Controls.Add(cellControll);
				cells.Add(p, cellControll);
			}
			SetControls();
		}

		public void UpdateCells(IEnumerable<Point> where)
		{
			foreach (var c in cells.Values)
			{
				if (c.Marked)
				{
					c.Marked = false;
					c.Invalidate();
				}

			}
			foreach (var p in where)
			{
				cells[p].Cell = Filed[p];
				cells[p].Marked = true;
				cells[p].Invalidate();
			}
		}

		void ClientFiledControl_Resize(object sender, EventArgs e)
		{
			SetControls();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			int ctrlX = Filed.Cursor.X;
			int ctrlY = Filed.Cursor.Y;

			var control = cells[new Point(ctrlX, ctrlY)];

			int x1 = control.Location.X;
			int y1 = control.Location.Y;
			int x2 = x1 + control.Width;
			int y2 = y1 + control.Height;

			switch (Filed.Cursor.Direction)
			{
				case Direction.Up:
					y2 -= control.Height;
					break;
				case Direction.Left:
					x2 -= control.Width;
					break;
				case Direction.Down:
					y1 += control.Height;
					break;
				case Direction.Right:
					x1 += control.Width;
					break;
				default:
					break;
			}

			e.Graphics.DrawLine(new Pen(Color.Black, 2), x1, y1, x2, y2);
		}

		void ClientFiledControl_MouseClick(object sender, MouseEventArgs e)
		{
			if (Filed.Locked)
				return;

			if (Clicked != null)
				Clicked(this, new ClickedEventArgs(Filed.Cursor));
			RefreshNear(Filed.Cursor);
		}

		void ClientFiledControl_MouseMove(object sender, MouseEventArgs e)
		{
			ClientCellControl cell = (ClientCellControl)sender;
			var position = cells.Where(p => p.Value == cell).Select(p => p.Key).First();
			int cellX = position.X;
			int cellY = position.Y;
			Filed.Cursor.X = cellX;
			Filed.Cursor.Y = cellY;

			Point center = new Point(cell.Width / 2, cell.Height / 2);
			Point luCorner = new Point(0, 0);
			Point ruCorner = new Point(cell.Width, 0);
			Point ldCorner = new Point(0, cell.Height);
			Point rdCorner = new Point(cell.Width, cell.Height);
			Point cursor = e.Location;

			if (Test(luCorner, center, ldCorner, cursor))
			{
				Filed.Cursor.Direction = Direction.Left;
			}
			else if (Test(luCorner, ruCorner, center, cursor))
			{
				Filed.Cursor.Direction = Direction.Up;
			}
			else if (Test(ruCorner, rdCorner, center, cursor))
			{
				Filed.Cursor.Direction = Direction.Right;
			}
			else if (Test(rdCorner, ldCorner, center, cursor))
			{
				Filed.Cursor.Direction = Direction.Down;
			}
			RefreshNear();
		}

		private bool Test(Point a, Point b, Point c, Point pos)
		{
			Point s1 = b.Subtract(a);
			Point s2 = c.Subtract(a);
			Point s3 = c.Subtract(b);
			Point p1 = pos.Subtract(a);
			Point p2 = pos.Subtract(b);

			return s2.CrossZValue(p1) * s2.CrossZValue(s1) > 0 &&
					s1.CrossZValue(p1) * s1.CrossZValue(s2) > 0 &&
					s3.CrossZValue(p2) * s3.CrossZValue(s1.Negate()) > 0;
		}

		private void SetControls()
		{
			for (int x = 0; x < Filed.Width; x++)
			{
				for (int y = 0; y < Filed.Height; y++)
				{
					var controll = cells[new Point(x, y)];
					controll.Width = Width / Filed.Width;
					controll.Height = Height / Filed.Height;
					controll.Left = x * controll.Width;
					controll.Top = y * controll.Height;
				}
			}
		}

		private void RefreshNear()
		{
			if (Filed.Cursor.Position != lastCursor.Position ||
				Filed.Cursor.Direction != lastCursor.Direction)
			{
				RefreshNear(Filed.Cursor);
				RefreshNear(lastCursor);
				lastCursor = (FiledCursor)Filed.Cursor.Clone();
			}
		}

		private void RefreshNear(FiledCursor cursor)
		{
			var ctrl = cells[cursor.Position];
			int x = ctrl.Left - ctrl.Width;
			int y = ctrl.Top - ctrl.Height;
			int w = ctrl.Width * 3;
			int h = ctrl.Height * 3;
			Invalidate(new Rectangle(x, y, w, h));
		}
	}
}
