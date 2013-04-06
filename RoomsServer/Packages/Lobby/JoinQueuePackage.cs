using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packages
{
	[Package(PackageType.JoinQueue)]
	public class JoinQueuePackage : Package
	{
		[Data(0, true, true)]
		public QueueType QueueType { get; set; }

		[Data(1, true, true)]
		public List<string> Teammates { get; private set; }

		public JoinQueuePackage()
		{
			Teammates = new List<string>();
		}
	}
}
