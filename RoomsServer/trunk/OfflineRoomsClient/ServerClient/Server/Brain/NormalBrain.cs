using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OfflineRoomClient
{
	public class NormalBrain : Brain
	{
		public NormalBrain(IFiled filed)
			: base(filed)
		{}

		public override Cursor Step(Cursor opponentSetep)
		{
			Cursor cursor = new Cursor();
			var freeCells = from x in Enumerable.Range(0, filed.Width)
							from y in Enumerable.Range(0, filed.Height)
							where !filed[x, y].IsWall
							select new Point(x, y);

			foreach (var p in freeCells)
			{
				Cursor c = OneLeft(p);
				if (c != null)
					return c;
			}

			int willTake = freeCells.Count();
			Cursor temp = cursor;
			var cursors = from c in
							  from p in freeCells
							  from d in Enumerable.Range(0, 4)
							  orderby p.Distance(opponentSetep.Position)
							  select new Cursor() { X = p.X, Y = p.Y, Direction = (Direction)d }
						  where Free(c)
						  orderby HowManyTake(c)
						  select c;
			return cursors.FirstOrDefault();
		}

		private Cursor OneLeft(Point position)
		{
			Cursor rv = null;

			var sides = Sides(position);
			if (sides.Count(s => s) == 1)
				rv = new Cursor() { X = position.X, Y = position.Y, Direction = (Direction)Array.IndexOf(sides, true) };
			return rv;
		}

		private int HowManyTake(Cursor cursor)
		{
			Cursor c = (Cursor)cursor.Clone();
			Cursor c1 = c.Mirror;

			Point start = new[] { c.Position, c1.Position }.FirstOrDefault(p => NearCanGo(p).Count() < 3);
			HashSet<Point> visited = new HashSet<Point>();
			if (start != Point.Empty)
				visited.Add(start);

			foreach (var p in NearCanGo(start))
			{
				CalculateTake(visited, p);
			}
			return visited.Count;
		}

		private void CalculateTake(HashSet<Point> visited, Point watching)
		{
			if (visited.Contains(watching))
				return;
			var t = from n in NearCanGo(watching)
					where !visited.Contains(n)
					select n;
			if (t.Count() > 1)
				return;

			visited.Add(watching);
			foreach (var p in t)
				CalculateTake(visited, p);
		}

		private bool[] Sides(Point position)
		{
			bool left = filed[position.X - 1, position.Y].Right;
			bool right = filed[position].Right;
			bool top = filed[position.X, position.Y - 1].Down;
			bool down = filed[position].Down;
			return new[] { !top, !left, !down, !right };
		}

		private IEnumerable<Point> NearCanGo(Point pos)
		{
			if (pos == Point.Empty)
				yield break;

			var sides = Sides(pos);
			for (int i = 0; i < sides.Length; i++)
				if (sides[i])
					yield return new Point(pos.X + (i - 2) % 2, pos.Y + (i - 1) % 2);
		}

		private bool Free(Cursor cursor)
		{
			switch (cursor.Direction)
			{
				case Direction.Up:
					return !filed[cursor.X, cursor.Y - 1].Down;
				case Direction.Left:
					return !filed[cursor.X - 1, cursor.Y].Right;
				case Direction.Down:
					return !filed[cursor.Position].Down;
				case Direction.Right:
					return !filed[cursor.Position].Right;
				default:
					return false;
			}
		}
	}
}
