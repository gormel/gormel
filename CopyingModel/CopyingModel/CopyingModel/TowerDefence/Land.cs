using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CopyingModel
{
	public class Land : GameObject<Land.States>
	{
		public enum States
		{
			MonsterCreated,
		}

		private LandEntery[,] data;
		public int Width { get; private set; }
		public int Height { get; private set; }

		private TimeSpan stepTimeout;
		private TimeSpan spendTime;
		private Random rand = new Random();

		public Land(int w, int h)
		{
			Width = w;
			Height = h;
			data = new LandEntery[w, h];

			Initialize();
		}

		private void Initialize()
		{
			int x = rand.Next(Width);
			int y = rand.Next(Height);
			var mon = new Monster(new Point(x, y));
			mon.StateChanged += mon_StateChanged;
			data[x, y] = mon;
		}

		private void mon_StateChanged(object sender, StateChangedEventArgs<LandEntery.State> e)
		{
			switch (e.State)
			{
				case LandEntery.State.MonsterCreated:
					var mon = (Monster)e.Args[0];
					var where = (Point)e.Args[1];
					var xy = from x in Enumerable.Range(@where.X - mon.Radius, mon.Radius * 2)
							 from y in Enumerable.Range(@where.Y - mon.Radius, mon.Radius * 2)
							 where x >= 0 && x < Width
							 where y >= 0 && y < Height
							 where data[x, y] == null
							 orderby rand.Next(mon.Radius * mon.Radius * 4)
							 select new Point(x, y);
					if (xy.Count() == 0)
						break;

					var p = xy.First();
					var p1 = xy.Last();

					data[p.X, p.Y] = new Monster(p);
					data[p.X, p.Y].StateChanged += mon_StateChanged;
					
					data[p1.X, p1.Y] = new Monster(p1);
					data[p1.X, p1.Y].StateChanged += mon_StateChanged;

					FireStateChanged(States.MonsterCreated, data[p.X, p.Y], p);
					FireStateChanged(States.MonsterCreated, data[p1.X, p1.Y], p1);
					break;
				default:
					break;
			}
		}
		
		public override void Update(GameTime time)
		{
			foreach (var ent in data)
			{
				if (ent == null)
					continue;
				ent.Update(time);
			}
		}
	}
}
