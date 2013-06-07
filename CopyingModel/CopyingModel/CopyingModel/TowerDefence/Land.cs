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
			TowerCreated,
			TowerShoot,
		}

		private LandEntery[,] data;
		public int Width { get; private set; }
		public int Height { get; private set; }
		public MonsterFactory MonsterFactory { get; private set; }
		public TowerFactory TowerFactory { get; private set; }

		private TimeSpan stepTimeout;
		private TimeSpan spendTime;
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
