using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packages
{
	[Package(PackageType.PlayerInfo)]
	public class PlayerInfoPackage : Package
	{
		[Data(0)]
		public string Name { get; set; }

		[Data(1)]
		public int Rating { get; set; }
	}
}
