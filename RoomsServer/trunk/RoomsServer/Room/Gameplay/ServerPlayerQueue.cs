using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomsServer
{
	public class ServerPlayerQueue
	{
		public ServerPlayer PlayingPlayer { get { return queue.Peek(); } }
		private Queue<ServerPlayer> queue = new Queue<ServerPlayer>();
		public ServerPlayerQueue(IEnumerable<ServerPlayer> players)
		{
			foreach (var p in players)
			{
				queue.Enqueue(p);
			}
		}

		public void Step()
		{
			queue.Enqueue(queue.Dequeue());
		}
	}
}
