using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameplay;

namespace Packages
{
	[Package(PackageType.YouJoinedRoom)]
	public class YouJoinedRoomPackage : Package
	{
		[Data(0, true, true)]
		public string Team { get; set; }

		[Data(1, true, true)]
		public Images Image { get; set; }

		[Data(2)]
		public int Width { get; set; }

		[Data(3)]
		public int Height { get; set; }
	}
}
