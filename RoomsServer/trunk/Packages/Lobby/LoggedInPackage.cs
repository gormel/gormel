using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packages
{
	[Package(PackageType.LoggedIn)]
	public class LoggedInPackage : Package
	{
		[Data(0, true, true)]
		public string Name { get; set; }
	}
}
