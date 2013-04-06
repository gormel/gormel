using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Packages;

namespace RoomsClient
{
	public class ServerComunicator
	{
		private Socket sock;

		public event EventHandler<PackageReciveEventArgs> PackageRecive;

		private static ServerComunicator inst;
		public static ServerComunicator Instance
		{
			get
			{
				if (inst == null)
					inst = new ServerComunicator();
				return inst;
			}
		}

		private ServerComunicator()
		{
			sock = new Socket(SocketType.Stream, ProtocolType.IP);
			IPEndPoint servAddr = new IPEndPoint((127 << 24) + (0 << 16) + (0 << 8) + 1, 4000);
			sock.Connect("127.0.0.1", 4000);
			
			Tuple<Socket, byte[]> stateObject = new Tuple<Socket, byte[]>(sock, new byte[4]);
			sock.BeginReceive(stateObject.Item2, 0, 4, SocketFlags.None, ReadSizeCallback, stateObject);
		}

		private void ReadSizeCallback(IAsyncResult ar)
		{
			Tuple<Socket, byte[]> stateObject = (Tuple<Socket, byte[]>)ar.AsyncState;
			if (!stateObject.Item1.Connected)
				return;
			int readed = stateObject.Item1.EndReceive(ar);

			int size = BitConverter.ToInt32(stateObject.Item2, 0);

			Tuple<Socket, byte[]> newStateObject = new Tuple<Socket, byte[]>(stateObject.Item1, new byte[size]);
			newStateObject.Item1.BeginReceive(newStateObject.Item2, 0, size, SocketFlags.None, ReadDataCallback, newStateObject);
		}

		private void ReadDataCallback(IAsyncResult ar)
		{
			Tuple<Socket, byte[]> stateObject = (Tuple<Socket, byte[]>)ar.AsyncState;
			if (!stateObject.Item1.Connected)
				return;
			int readed = stateObject.Item1.EndReceive(ar);

			Package p = ByteConverter.FromBytes(stateObject.Item2);
			if (PackageRecive != null)
				PackageRecive(this, new PackageReciveEventArgs(p));

			Tuple<Socket, byte[]> newStateObject = new Tuple<Socket, byte[]>(sock, new byte[4]);
			sock.BeginReceive(newStateObject.Item2, 0, 4, SocketFlags.None, ReadSizeCallback, newStateObject);
		}

		private void Send(byte[] data)
		{
			sock.Send(data);
		}

		public void Send(Package p)
		{
			byte[] data = ByteConverter.ToBytes(p);
			Send(BitConverter.GetBytes(data.Length));
			Send(data);
		}

		public void Disconnect()
		{
			sock.Disconnect(false);
		}
	}
}
