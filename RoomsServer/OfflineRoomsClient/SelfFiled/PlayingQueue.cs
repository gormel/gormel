using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineRoomClient
{
	public class PlayingQueue
	{
		private Queue<Player> players;
		public PlayingQueue(IEnumerable<Player> players)
		{
			this.players = new Queue<Player>(players);
		}

		public Player PlayingPlayer
		{
			get { return players.Peek(); }
		}

		public void Step()
		{
			players.Enqueue(players.Dequeue());
		}
	}
}
