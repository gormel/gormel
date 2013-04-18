using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packages
{
	[Package(PackageType.FiledDataUpdated)]
	public class FiledDataUpdatedPackage : Package
	{
		[Data(0)]
		public List<int> X { get; private set; }

		[Data(1)]
		public List<int> Y { get; private set; }

		[Data(2)]
		public List<string> Values { get; private set; }

		[Data(3)]
		public List<bool> Right { get; private set; }

		[Data(4)]
		public List<bool> Down { get; private set; }

		[Data(5)]
		public List<bool> IsWall { get; private set; }

		public FiledDataUpdatedPackage()
		{
			X = new List<int>();
			Y = new List<int>();
			Values = new List<string>();
			Right = new List<bool>();
			Down = new List<bool>();
			IsWall = new List<bool>();
		}
	}
}
