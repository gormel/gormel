using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packages
{
	public class PackageAttribute : Attribute
	{
		public PackageType Type { get; private set; }
		public PackageAttribute(PackageType type)
		{
			Type = type;
		}
	}
}
