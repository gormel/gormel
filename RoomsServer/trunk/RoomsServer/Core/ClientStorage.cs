using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsServer
{
	public class ClientStorage
	{
		private Dictionary<string, ClientRecord> clients = new Dictionary<string,ClientRecord>();
		public ClientRecordDAO DAO { get; private set; }
		public ClientStorage()
		{
			DAO = new FileClientRecordDAO("Base");
		}

		public void Save()
		{
			foreach (var c in clients.Values)
			{
				DAO.SaveClientRecord(c);
			}
			clients.Clear();
		}

		public void Save(string name)
		{
			DAO.SaveClientRecord(clients[name]);
		}

		public ClientRecord this[string name]
		{
			get
			{
				ClientRecord rv = null;
				if (clients.TryGetValue(name, out rv))
					return rv;
				rv = DAO.GetClientRecord(name);
				if (clients.Count > 1000)
					Save();
				clients.Add(name, rv);
				return rv;
			}
		}
	}
}
