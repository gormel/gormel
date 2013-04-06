using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineRoomClient
{
	public class ServerFiled : Filed<ServerCell, ServerPlayer>
	{
		public ServerFiled(int width, int height)
			: base(width, height)
		{
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
					this[x, y] = new ServerCell();
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
				.Select(g => new Point(g.Max(p => p.X), g.Key)))
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
					.Select(y => new { x, y }))
				.SelectMany(c => c);
			var right = xy.Where(t => this[t.x, t.y].IsWall ^ this[t.x + 1, t.y].IsWall);
			var down = xy.Where(t => this[t.x, t.y].IsWall ^ this[t.x, t.y + 1].IsWall);
			foreach (var t in right)
				this[t.x, t.y].Right = true;
			foreach (var t in down)
				this[t.x, t.y].Down = true;
		}

		public bool Step(ServerPlayer player, Cursor cursor)
		{
			int convertedX = cursor.X;
			int convertedY = cursor.Y;
			bool isRight = true;

			switch (cursor.Direction)
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
					return false;

				this[convertedX, convertedY].Right = isRight ? true : this[convertedX, convertedY].Right;
				this[convertedX, convertedY].Down = isRight ? this[convertedX, convertedY].Down : true;
				bool changed = false;
				if (Check(checkX, checkY))
				{
					this[checkX, checkY].Symbol = player;
					changed = true;
				}

				if (Check(convertedX, convertedY))
				{
					this[convertedX, convertedY].Symbol = player;
					changed = true;
				}

				return !changed;
			}
			return false;
		}

		bool Check(int x, int y)
		{
			ServerCell lefter = this[x - 1, y];
			ServerCell upper = this[x, y - 1];
			return this[x, y].Right && this[x, y].Down && lefter.Right && upper.Down && this[x, y].Symbol == null;
		}

		public FiledData<ServerCell, ServerPlayer> GetData(IEnumerable<Point> where)
		{
			FiledData<ServerCell, ServerPlayer> data = new FiledData<ServerCell, ServerPlayer>();
			foreach (var p in where)
			{
				data.Add(p, this[p]);
			}
			return data;
		}

		public FiledData<ServerCell, ServerPlayer> GetData()
		{
			return GetData(
				from x in Enumerable.Range(0, Width)
				from y in Enumerable.Range(0, Height)
				select new Point(x, y));
		}
	}
}
