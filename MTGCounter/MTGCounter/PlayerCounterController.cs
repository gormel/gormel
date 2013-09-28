using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGCounter
{
	public class PlayerCounterController : BaseController
	{
		int life;
		public int Life
		{
			get { return life; }
			set
			{
				life = value;
				FirePropertyChange("Life");
			}
		}

		private double width;
		public double Width
		{
			get { return width; }
			set
			{
				width = value;
				FirePropertyChange("Width");
			}
		}

		private double height;
		public double Height
		{
			get { return height; }
			set
			{
				height = value;
				FirePropertyChange("Height");
			}
		}
		public PlayerCounterController()
		{
			Life = 20;
			Width = 100;
			Height = 100;
		}
	}
}
