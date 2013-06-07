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

		public TowerFactory()
		{
			Radius = 3;
			Energy = 3;
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
