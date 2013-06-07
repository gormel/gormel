using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CopyingModel
{
	public class Tower : LandEntery
	{
		public int Radius { get; set; }
		public int Energy { get; set; }

		public readonly static TimeSpan shootTimeout = TimeSpan.FromMilliseconds(1000);
		private TimeSpan lastShoot = TimeSpan.FromMilliseconds(0);

		public Tower(Point pos)
			: base(pos)
		{

		}

		public override void Update(GameTime time)
		{
			if ((lastShoot += time.ElapsedGameTime) < shootTimeout)
				return;
			lastShoot = TimeSpan.FromMilliseconds(0);

			FireStateChanged(LandEntery.State.TowerShoot, this, Position);
		}
	}
}
