using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CopyingModel
{
	public class Tower : GameObject<Tower.Status>
	{
		public enum Status { }
		public int Radius { get; private set; }
		public int Energy { get; set; }

		public override void Update(GameTime time)
		{
		}
	}
}
