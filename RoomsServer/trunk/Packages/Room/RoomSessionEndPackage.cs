﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packages
{
	[Package(PackageType.RoomSessionEnd)]
	public class RoomSessionEndPackage : Package
	{
		[Data(0)]
		public int EloAdded { get; set; }
	}
}
