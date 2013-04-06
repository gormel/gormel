using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsServer
{
	public class ClientsUnion
	{
		public int Elo
		{
			get { return Premades.Sum(i => i.Elo) / Premades.Count; }
		}
		public List<LobbyClientInfo> Premades { get; private set; }
		public ClientsUnion(IEnumerable<LobbyClientInfo> clients)
		{
			Premades = new List<LobbyClientInfo>();
			Premades.AddRange(clients);
		}

		public ClientsUnion(params LobbyClientInfo[] clients)
		{
			Premades = new List<LobbyClientInfo>();
			Premades.AddRange(clients);
		}
	}
}
