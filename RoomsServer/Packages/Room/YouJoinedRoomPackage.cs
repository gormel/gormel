using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packages
{
	[Package(PackageType.YouJoinedRoom)]
	public class YouJoinedRoomPackage : Package
	{
		[Data(0, true, true)]
		public string Team { get; set; }
	}
}
