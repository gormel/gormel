using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RoomsServer
{
	class Server
	{
		public Random Random { get; private set; }
		private static Server inst = null;
		public static Server Instance
		{
			get
			{
				if (inst == null)
					inst = new Server();
				return inst;
			}
		}

		Server()
		{
			LoginSession = new LoginSession();
			LobbySession = new LobbySession();
			Rooms = new List<RoomSession>();
			Random = new Random();
			ClientStorage = new ClientStorage();
		}

		static void Main(string[] args)
		{
			Socket server = new Socket(SocketType.Stream, ProtocolType.IP);
			int port = 4000;
			server.Bind(new IPEndPoint(IPAddress.Any, port));
			server.Listen(1);

			Console.WriteLine("Server started at port {0}.", port);
			server.BeginAccept(new AsyncCallback(AcceptCallback), server);

			while (true) ;
		}

		static void AcceptCallback(IAsyncResult ar)
		{
			Socket server = (Socket)ar.AsyncState;
			Socket client = server.EndAccept(ar);

			Console.WriteLine(string.Format("Connected new client from {0}.", client.LocalEndPoint));
			Instance.LoginSession.Clients.Add(new LoginClientInfo(new Client(client)));

			server.BeginAccept(new AsyncCallback(AcceptCallback), server);
		}

		public LoginSession LoginSession { get; private set; }

		public LobbySession LobbySession { get; private set; }

		public List<RoomSession> Rooms { get; private set; }

		public ClientStorage ClientStorage { get; private set; }
	}
}
