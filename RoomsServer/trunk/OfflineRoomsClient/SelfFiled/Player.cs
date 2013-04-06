using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineRoomClient
{
	public class Player
	{
		public Player(char symbol, string name)
		{
			Symbol = symbol;
			Name = name;
		}

		public char Symbol { get; private set; }
		public string Name { get; private set; }
		public int Score { get; set; } 
	}
}
