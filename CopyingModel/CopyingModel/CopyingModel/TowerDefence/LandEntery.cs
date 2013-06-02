using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CopyingModel
{
	public abstract class LandEntery : GameObject<LandEntery.State>
	{
		public enum State
		{
			MonsterCreated,
		}

		public Point Position { get; private set; }
		public LandEntery(Point pos)
		{
			Position = pos;
		}
	}
}
