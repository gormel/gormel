using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsServer
{
	public class ClientQueue
	{
		private SortedSet<ClientsUnion> clients = 
			new SortedSet<ClientsUnion>(Comparer<ClientsUnion>.Create((a, b) => a.Elo - b.Elo));

		public QueueInfo Info { get; private set; }
		public ClientQueue(QueueInfo info)
		{
			Info = info;
		}

		public void Add(ClientsUnion union)
		{
			clients.Add(union);
		}

		private List<List<LobbyClientInfo>> GetTeams()
		{
			var copyClients = clients.ToList();
			List<List<LobbyClientInfo>> teams = new List<List<LobbyClientInfo>>();
			for (int i = 0; i < Info.TeamCount; i++)
			{
				List<LobbyClientInfo> team = new List<LobbyClientInfo>();
				while (team.Count < Info.TeamSize)
				{
					var union = copyClients.FirstOrDefault(u => u.Premades.Count <= Info.TeamSize - team.Count);
					if (union == null)
						throw new Exception("Cant select teams!!!");
					team.AddRange(union.Premades);
					copyClients.Remove(union);
				}
				teams.Add(team);
			}
			clients.Clear();
			foreach (var item in copyClients)
				clients.Add(item);

			return teams;
		}

		public bool TryGetTeams(out List<List<LobbyClientInfo>> teams)
		{
			try
			{
				teams = GetTeams();
			}
			catch (Exception ex)
			{
				teams = null;
				return false;
			}
			return true;
		}
	}
}