using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CopyingModel
{
	public enum WorldStates
	{
		PartCreated,
		PartMoved,
		PartDeleted,
	}

	public class World : GameObject<WorldStates>
	{
		public int Width { get; set; }
		public int Height { get; set; }

		public readonly int StepTimeout = 700;
		private int lastStep = 0;
		Random r = new Random();

		public IEnumerable<Part> Parts
		{
			get
			{
				return from p in parts
					   where p != null
					   select p;
			}
		}

		Part[] parts;
		Part[] buffer;

		public World(int w, int h)
		{
			Width = w;
			Height = h;
			Initialize();
		}

		public override void Update(GameTime time)
		{
			if (lastStep < StepTimeout)
			{
				lastStep += time.ElapsedGameTime.Milliseconds;
				return;
			}
			lastStep = 0;

			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					if (this[x, y] == null)
						continue;

					Part child = null;
					for (int dx = -1; dx < 2; dx++)
					{
						for (int dy = -1; dy < 2; dy++)
						{
							if (x + dx < 0 || x + dx >= Width)
								continue;
							if (y + dy < 0 || y + dy >= Height)
								continue;
							if (this[x + dx, y + dy] == null)
								continue;
							if (dx == 0 && dy == 0)
								continue;
							child = this[x, y].CreateChildren(this[x + dx, y + dy]);
							break;
						}
					}

					if (child == null)
						continue;

					var xy = from dx in Enumerable.Range(-1, 3)
							 from dy in Enumerable.Range(-1, 3)
							 where x + dx >= 0 && x + dx < Width
							 where y + dy >= 0 && y + dy < Height
							 where this[x + dx, y + dy] == null
							 select new Point(x + dx, y + dy);
					Point p = xy.ElementAt(r.Next(xy.Count()));
					this[p] = child;

					FireStateChanged(WorldStates.PartCreated, child, p);
				}
			}

			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					if (this[x, y] == null)
						continue;
					if (this[x, y].Life <= 0)
					{
						FireStateChanged(WorldStates.PartDeleted, this[x, y], new Point(x, y));
						this[x, y] = null;
						continue;
					}

					this[x, y].Update(time);
					Move(x, y);
				}
			}

			SwapBuffers();
		}

		private void Initialize()
		{
			parts = new Part[Width * Height];
			buffer = new Part[Width * Height];

			for (int i = 0; i < 20; i++)
			{
				int x;
				int y;
				do
				{
					x = r.Next(0, Width);
					y = r.Next(0, Height);

				}
				while (this[x, y] != null);

				this[x, y] = new Part();
				var colors = new[] { Color.Black, Color.White };
				var color = colors[r.Next(colors.Length)];

				this[x, y].Red = color.R;
				this[x, y].Green = color.G;
				this[x, y].Blue = color.B;
			}
		}

		private void Move(int x, int y)
		{
			if (!this[x, y].Movable)
			{
				buffer[x + y * Width] = this[x, y];
				return;
			}
			int dx = 0;
			int dy = 0;
			int c = 8;
			do
			{
				dx = r.Next(-1, 2);
				dy = r.Next(-1, 2);
				c--;
				if (c < 0)
				{
					buffer[x + y * Width] = this[x, y];
					return;
				}
			}
			while(x + dx < 0 || x + dx >= Width ||
				y + dy < 0 || y + dy >= Height ||
					this[x + dx, y + dy] != null ||
				buffer[x + dx + (y + dy) * Width] != null);
			
			SetBuffer(x + dx, y + dy, this[x, y]);
			FireStateChanged(WorldStates.PartMoved, this[x, y], new Point(x, y), new Point(x + dx, y + dy));
		}

		public Part this[int x, int y]
		{
			get { return parts[x + y * Width]; }
			set { parts[x + y * Width] = value; }
		}

		public Part this[Point p]
		{
			get { return this[p.X, p.Y]; }
			set { this[p.X, p.Y] = value; }
		}

		private void SetBuffer(int x, int y, Part p)
		{
			buffer[x + y * Width] = p;
		}

		private void SwapBuffers()
		{
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					this[x, y] = buffer[x + y * Width];
					buffer[x + y * Width] = null;
				}
			}
		}
	}
}
