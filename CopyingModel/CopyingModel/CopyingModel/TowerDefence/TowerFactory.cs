using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CopyingModel
{
	public class TowerFactory
	{
		public int Radius { get; set; }
		public int Energy { get; set; }

		public static int EnergyUpgaradeCost { get { return 100; } }
		public static int RadiusUpgardeCost { get { return 400; } }

		public TowerFactory()
		{
			Radius = 1;
			Energy = 10;
		}

		public Tower CreateTower(Point pos)
		{
			Tower tower = new Tower(pos);
			tower.Energy = Energy;
			tower.Radius = Radius;

			return tower;
		}
	}
}
