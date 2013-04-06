using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packages
{
	[Package(PackageType.JoinedRoom)]
	public class JoinedRoomPackage : Package
	{
		[Data(0, true, true)]
		public string Name { get; set; }

		[Data(1, true, true)]
		public string Team { get; set; }
	}
}
