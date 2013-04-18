using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameplay;

namespace Packages
{
	[Package(PackageType.Step)]
	public class StepPackage : Package
	{
		[Data(0, true, true)]
		public int X { get; set; }

		[Data(1, true, true)]
		public int Y { get; set; }

		[Data(2, true, true)]
		public Direction Direction { get; set; }
	}
}
