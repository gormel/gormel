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
			MonsterDied,
			TowerCreated,
			TowerDied,
			TowerShoot,
		}

		private LandEntery[,] data;
		public int Width { get; private set; }
		public int Height { get; private set; }
		public MonsterFactory MonsterFactory { get; private set; }
		public TowerFactory TowerFactory { get; private set; }

		private Random rand = new Random();

		public Land(int w, int h)
		{
			Width = w;
			Height = h;
			data = new LandEntery[w, h];
			MonsterFactory = new MonsterFactory();
			TowerFactory = new TowerFactory();

			Initialize();
		}

		private void Initialize()
		{
			int x = rand.Next(Width);
			int y = rand.Next(Height);
			var mon = MonsterFactory.CreateMonster(new Point(x, y));
			mon.StateChanged += mon_StateChanged;
			data[x, y] = mon;
			FireStateChanged(States.MonsterCreated, data[x, y], new Point(x, y));
		}

		public void PlaceTower(Point pos)
		{
			if (pos.X < 0 || pos.Y < 0 ||
				pos.X >= Width || pos.Y >= Height)
				return;
			if (data[pos.X, pos.Y] != null)
				return;

			Tower tow = TowerFactory.CreateTower(pos);
			tow.StateChanged += mon_StateChanged;
			data[pos.X, pos.Y] = tow;
			FireStateChanged(States.TowerCreated, tow, pos);
		}

		private void CreateMonster(Point around)
		{
			Monster parent = (Monster)data[around.X, around.Y];
			int x = -1;
			int y = -1;
			int c = parent.Radius * parent.Radius;
			do
			{
				x = rand.Next(-parent.Radius, parent.Radius + 1) + around.X;
				y = rand.Next(-parent.Radius, parent.Radius + 1) + around.Y;

				if (c-- <= 0)
					return;
			} while (x < 0 || x >= Width ||
					y < 0 || y >= Height ||
					data[x, y] != null);

			data[x, y] = MonsterFactory.CreateMonster(new Point(x, y));
			data[x, y].StateChanged += mon_StateChanged;
			FireStateChanged(States.MonsterCreated, data[x, y], new Point(x, y));
		}

		private void mon_StateChanged(object sender, StateChangedEventArgs<LandEntery.State> e)
		{
			switch (e.State)
			{
				case LandEntery.State.MonsterCreated:
					var mon = (Monster)e.Args[0];
					var where = (Point)e.Args[1];
					CreateMonster(where);
					CreateMonster(where);
					break;
				case LandEntery.State.TowerShoot:
					var tow = (Tower)e.Args[0];
					var tWhere = (Point)e.Args[1];
					//TODO: логика выстрела башни

					int xStart = Math.Max(tWhere.X - tow.Radius, 0);
					int yStart = Math.Max(tWhere.Y - tow.Radius, 0);
					int xEnd = Math.Min(tWhere.X + tow.Radius, Width - 1);
					int yEnd = Math.Min(tWhere.Y + tow.Radius, Height - 1);

					for (int x = xStart; x <= xEnd; x++)
					{
						for (int y = yStart; y <= yEnd; y++)
						{
							if (data[x, y] == null)
								continue;
							var monster = data[x, y] as Monster;
							if (monster == null)
								continue;

							if (--monster.Life <= 0)
							{
								FireStateChanged(States.MonsterDied, data[x, y], new Point(x, y));
								data[x, y] = null;
							}
						}
					}

					if (--tow.Energy <= 0)
					{
						FireStateChanged(States.TowerDied, tow, tWhere);
						data[tWhere.X, tWhere.Y] = null;
					}

					FireStateChanged(States.TowerShoot, tow, tWhere);
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
