using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packages;

namespace RoomsClient
{
	public class PackageReciveEventArgs
	{
		public Package Data { get; private set; }
		public PackageReciveEventArgs(Package data)
		{
			Data = data;
		}
	}
}
