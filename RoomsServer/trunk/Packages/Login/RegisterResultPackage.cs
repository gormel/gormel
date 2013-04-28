using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packages
{
	[Package(PackageType.RegisterResult)]
	public class RegisterResultPackage : Package
	{
		[Data(0)]
		public bool Result { get; set; }
	}
}
