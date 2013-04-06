using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsServer
{
	public class QueueInfo
	{
		public int TeamSize { get; private set; }
		public int TeamCount { get; private set; }
		public string Name { get; private set; }

		public QueueInfo(int teamSize, int teamCount, string queueName) 
		{
			TeamSize = teamSize;
			TeamCount = teamCount;
			Name = queueName;
		}
	}
}
