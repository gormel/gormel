using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OfflineRoomClient
{
	public partial class SelfFiledControl : UserControl
	{
		public SelfFiled Filed { get; private set; }
		public SelfFiledControl(int width, int height)
		{
			InitializeComponent();

			this.Resize += SelfFiledControl_Resize;

			int cellWidth = Width / width;
			int cellHeight = Height / height;
			Filed = new SelfFiled(width, height);
			lastCursor = new Cursor();
			for (int x = 0; x < Filed.Width; x++)
			{
				for (int y = 0; y < Filed.Height; y++)
				{
					Filed[x, y].SetBounds(x * cellWidth, y * cellHeight, cellWidth, cellHeight);
					Filed[x, y].MouseClick += SelfFiledControl_MouseClick;
					Filed[x, y].MouseMove += SelfFiledControl_MouseMove;
					//Filed[x, y].BackColor = Color.Transparent;
					Controls.Add(Filed[x, y]);
				}
			}
		}

		void SelfFiledControl_Resize(object sender, EventArgs e)
		{
			int cellWidth = Width / Filed.Width;
			int cellHeight = Height / Filed.Height;
			for (int x = 0; x < Filed.Width; x++)
			{
				for (int y = 0; y < Filed.Height; y++)
				{
					Filed[x, y].SetBounds(x * cellWidth, y * cellHeight, cellWidth, cellHeight);
				}
			}
		}

		Cursor lastCursor;
		void SelfFiledControl_MouseMove(object sender, MouseEventArgs e)
		{
			if (lastCursor.X != Filed.Cursor.X || 
				lastCursor.Y != Filed.Cursor.Y || 
				lastCursor.Direction != Filed.Cursor.Direction)
			{
				RefreshNear(Filed.Cursor.Position);
				RefreshNear(lastCursor.Position);
				lastCursor = (Cursor)Filed.Cursor.Clone();
			}
		}

		void SelfFiledControl_MouseClick(object sender, MouseEventArgs e)
		{
			RefreshNear(Filed.Cursor.Position);
		}

		private void RefreshNear(Point position)
		{
			var ctrl = Filed[position.X, position.Y];
			int x = ctrl.Left - ctrl.Width;
			int y = ctrl.Top - ctrl.Height;
			int w = ctrl.Width * 3;
			int h = ctrl.Height * 3;
			Invalidate(new Rectangle(x, y, w, h));
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			int ctrlX = Filed.Cursor.X;
			int ctrlY = Filed.Cursor.Y;

			var control = Filed[ctrlX, ctrlY];

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
	}
}
