using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CopyingModel
{
	public class MonsterFactory
	{
		public int MonsterLife { get; set; }
		public int Radius { get; set; }

		public MonsterFactory()
		{
			MonsterLife = 3;
			Radius = 4;
		}

		public Monster CreateMonster(Point pos)
		{
			Monster monster = new Monster(pos);
			monster.Life = MonsterLife;
			monster.Radius = Radius;

			return monster;
		}
	}
}
