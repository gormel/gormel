using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packages
{
	[Package(PackageType.PrivateRoomMessage)]
	public class PrivateRoomMessagePackage : Package
	{
		[Data(0, true, true)]
		public string From { get; set; }

		[Data(1, true, true)]
		public string To { get; set; }

		[Data(2, true, true)]
		public string Text { get; set; }
	}
}
