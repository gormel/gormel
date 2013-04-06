using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OfflineRoomClient
{
	public class SelfFiled : Filed<SelfCell, char>
	{
		private PlayingQueue queue;

		public Cursor Cursor { get; private set; }
		public SelfFiled(int width, int height)
			: base(width, height)
		{
			queue = new PlayingQueue(new[] { new Player('X', "Player 1"), new Player('O', "Payer 2") });
			Cursor = new Cursor();
			Generate();
		}

		private void Generate()
		{
			Random r = new Random();
			//все - стенка
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					this[x, y] = new SelfCell();
					this[x, y].MouseMove += SelfFiled_MouseMove;
					this[x, y].MouseClick += SelfFiled_MouseClick;
					this[x, y].IsWall = true;
				}
			}
			//ставим "ключевые" точки
			List<Point> points = new List<Point>();
			for (int i = 0; i < Width * Height / 10; i++)
			{
				int x = r.Next(1, Width - 1);
				int y = r.Next(1, Height - 1);
				points.Add(new Point(x, y));
				this[x, y].IsWall = false;
			}

			//прорезаем горизонтальные линии
			foreach (var point in points.GroupBy(p => p.Y)
				.Select(g =>new Point(g.Max(p => p.X), g.Key)))
			{
				bool fillSate = false;
				for (int x = 0; x < point.X; x++)
				{
					fillSate = fillSate ? true : !this[x, point.Y].IsWall;
					this[x, point.Y].IsWall = !fillSate;
				}
			}

			//прорезаем вертикальные линии
			foreach (var point in points.GroupBy(p => p.X)
				.Select(g => new Point(g.Key, g.Max(p => p.Y))))
			{
				bool fillSate = false;
				for (int y = 0; y < point.Y; y++)
				{
					fillSate = fillSate ? true : !this[point.X, y].IsWall;
					this[point.X, y].IsWall = !fillSate;
				}
			}

			//убираем внутрениие обособленные области
			for (int y = 0; y < Height; y++)
			{
				int xStart = Enumerable.Range(0, Width).FirstOrDefault(x => !this[x, y].IsWall);
				int xEnd = Enumerable.Range(0, Width).LastOrDefault(x => !this[x, y].IsWall);
				if (xStart != xEnd)
					for (int x = xStart + 1; x < xEnd; x++)
						this[x, y].IsWall = false;
			}

			//убираем читерские клетки сверху и снизу
			for (int y = 0; y < Height; y++)
			{
				foreach (var x in Enumerable.Range(1, Width - 1)
					.Where(x => !this[x, y].IsWall && this[x - 1, y].IsWall && this[x + 1, y].IsWall))
					this[x, y].IsWall = true;
			}

			//убираем читерские клетки по бокам
			for (int x = 0; x < Width; x++)
			{
				foreach (var y in Enumerable.Range(1, Height - 1)
					.Where(y => !this[x, y].IsWall && this[x, y - 1].IsWall && this[x, y + 1].IsWall))
					this[x, y].IsWall = true;
			}

			//ставим стенки по краям (наркоманияяя!!!!!)
			var xy = Enumerable.Range(0, Width - 1)
				.Select(x => 
					Enumerable.Range(0, Height - 1)
					.Select(y => Tuple.Create(x, y)))
				.SelectMany(c => c);
			var right = xy.Where(t => this[t.Item1, t.Item2].IsWall ^ this[t.Item1 + 1, t.Item2].IsWall);
			var down = xy.Where(t => this[t.Item1, t.Item2].IsWall ^ this[t.Item1, t.Item2 + 1].IsWall);
			foreach (var t in right)
				this[t.Item1, t.Item2].Right = true;
			foreach (var t in down)
				this[t.Item1, t.Item2].Down = true;
		}

		bool Check(int x, int y)
		{
			SelfCell lefter = this[x - 1, y];
			SelfCell upper = this[x, y - 1];
			return this[x, y].Right && this[x, y].Down && lefter.Right && upper.Down && this[x, y].Symbol == ' ';
		}

		void SelfFiled_MouseClick(object sender, MouseEventArgs e)
		{
			var clicked = this[Cursor.X, Cursor.Y];
			var upper   = this[Cursor.X, Cursor.Y - 1];
			var downer  = this[Cursor.X, Cursor.Y + 1];
			var lefter  = this[Cursor.X - 1, Cursor.Y];
			var rihter  = this[Cursor.X + 1, Cursor.Y];

			int convertedX = Cursor.X;
			int convertedY = Cursor.Y;
			bool isRight = true;

			switch (Cursor.Direction)
			{
				case Direction.Up:
					convertedY--;
					isRight = false;
					break;
				case Direction.Left:
					convertedX--;
					break;
				case Direction.Down:
					isRight = false;
					break;
			}

			if (isRight ? !this[convertedX, convertedY].Right : !this[convertedX, convertedY].Down)
			{
				int checkX = isRight ? convertedX + 1 : convertedX;
				int checkY = isRight ? convertedY : convertedY + 1;

				if (this[checkX, checkY].IsWall || this[convertedX, convertedY].IsWall)
					return;

				this[convertedX, convertedY].Right = isRight ? true : this[convertedX, convertedY].Right;
				this[convertedX, convertedY].Down = isRight ? this[convertedX, convertedY].Down : true;

				bool changed = false;
				if (Check(checkX, checkY))
				{
					this[checkX, checkY].Symbol = queue.PlayingPlayer.Symbol;
					queue.PlayingPlayer.Score++;
					changed = true;
				}

				if (Check(convertedX, convertedY))
				{
					this[convertedX, convertedY].Symbol = queue.PlayingPlayer.Symbol;
					queue.PlayingPlayer.Score++;
					changed = true;
				}

				if (!changed)
				{
					queue.Step();
				}
			}
		}

		void SelfFiled_MouseMove(object sender, MouseEventArgs e)
		{
			SelfCell cell = (SelfCell)sender;
			int index = Array.IndexOf(data, cell);
			int cellX = index % Width;
			int cellY = index / Width;
			Cursor.X = cellX;
			Cursor.Y = cellY;

			Point center = new Point(cell.Width / 2, cell.Height / 2);
			Point luCorner = new Point(0, 0);
			Point ruCorner = new Point(cell.Width, 0);
			Point ldCorner = new Point(0, cell.Height);
			Point rdCorner = new Point(cell.Width, cell.Height);
			Point cursor = e.Location;

			if (Test(luCorner, center, ldCorner, cursor))
			{
				Cursor.Direction = Direction.Left;
				return;
			}

			if (Test(luCorner, ruCorner, center, cursor))
			{
				Cursor.Direction = Direction.Up;
				return;
			}

			if (Test(ruCorner, rdCorner, center, cursor))
			{
				Cursor.Direction = Direction.Right;
				return;
			}

			if (Test(rdCorner, ldCorner, center, cursor))
			{
				Cursor.Direction = Direction.Down;
				return;
			}
		}

		public bool Test(Point a, Point b, Point c, Point pos)
		{
			Point s1 = b.Subtract(a);
			Point s2 = c.Subtract(a);
			Point s3 = c.Subtract(b);
			Point p1 = pos.Subtract(a);
			Point p2 = pos.Subtract(b);

			return  s2.CrossZValue(p1) * s2.CrossZValue(s1) > 0 && 
					s1.CrossZValue(p1) * s1.CrossZValue(s2) > 0 &&
					s3.CrossZValue(p2) * s3.CrossZValue(s1.Negate()) > 0;
		}
	}
}
