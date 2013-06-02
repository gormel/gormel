using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CopyingModel
{
	public enum PartStates
	{

	}
	public class Part : GameObject<PartStates>
	{
		public byte Red { get; set; }
		public byte Green { get; set; }
		public byte Blue { get; set; }
		public int CreationTimeout { get { return 3; } }
		public int Life { get; private set; }
		public bool Movable { get; private set; }

		private int lastCreation = 0;
		private int dLife = 0;
		private int lastUpdate = 0;

		public Part()
		{
			Life = 20;
			Movable = false;
		}

		public Part CreateChildren(Part partner)
		{
			if (lastCreation < CreationTimeout)
				return null;

			lastCreation = 0;
			partner.lastCreation = 0;
			dLife++;
			partner.dLife++;

			Random r = new Random();

			Part p = new Part();
			p.Red = r.Next(2) == 1 ? Red : partner.Red;
			p.Green = r.Next(2) == 1 ? Green : partner.Green;
			p.Blue = r.Next(2) == 1 ? Blue : partner.Blue;

			return p;
		}

		public override void Update(GameTime time)
		{
			if (lastUpdate > 0)
				Movable = true;
			lastCreation = Math.Min(lastCreation + 1, CreationTimeout + 1);
			lastUpdate++;
			Life -= dLife;

		}
	}
}
