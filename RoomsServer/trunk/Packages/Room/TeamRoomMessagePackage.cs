using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packages
{
	[Package(PackageType.TeamRoomMessage)]
	public class TeamRoomMessagePackage : Package
	{
		[Data(0, true, true)]
		public string Name { get; set; }
		[Data(1, true, true)]
		public string Text { get; set; }
	}
}
