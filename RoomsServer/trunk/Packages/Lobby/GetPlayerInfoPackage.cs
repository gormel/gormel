using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packages
{
	[Package(PackageType.GetPlayerInfo)]
	public class GetPlayerInfoPackage : Package
	{
		[Data(0)]
		public string Name { get; set; }
	}
}
