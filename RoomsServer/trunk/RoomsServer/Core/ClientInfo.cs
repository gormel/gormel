using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsServer
{
	public class ClientInfo
	{
		public Client Client { get; private set; }
		public string Name { get; private set; }
		public ClientInfo(string name, Client client)
		{
			Name = name;
			Client = client;
		}
	}
}
