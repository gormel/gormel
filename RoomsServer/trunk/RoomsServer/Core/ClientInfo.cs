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
		public ClientInfo(Client client)
		{
			Client = client;
		}
	}
}
