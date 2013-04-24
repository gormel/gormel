using System;
using System.Net.Sockets;
using Packages;

namespace RoomsServer
{
	public class Client
	{
		private struct StateData
		{
			byte[] data;
			public StateData(Socket s, int size)
			{
				data = new byte[size];
				Socket = s;
			}

			public StateData(Socket s)
				: this(s, 4)
			{
			}

			public int Size
			{
				get { return data.Length; }
			}

			public byte[] Buffer
			{
				get { return data; }
			}

			public Socket Socket;
		}

		public event EventHandler<Package> PackageRecive;

		Socket sock;
		public Client(Socket s)
		{
			sock = s;

			StateData data = new StateData(sock);
			sock.BeginReceive(data.Buffer, 0, data.Size, SocketFlags.None, new AsyncCallback(ReadSizeCallback), data);
		}

		private void ReadSizeCallback(IAsyncResult ar)
		{
			StateData state = (StateData)ar.AsyncState;
			if (!state.Socket.Connected)
				return;
			int readed = state.Socket.EndReceive(ar);

			int size = BitConverter.ToInt32(state.Buffer, 0);
			StateData newState = new StateData(state.Socket, size);

			state.Socket.BeginReceive(newState.Buffer, 0, newState.Size, 
				SocketFlags.None, new AsyncCallback(ReadDataCallback), newState);
		}

		private void ReadDataCallback(IAsyncResult ar)
		{
			StateData state = (StateData)ar.AsyncState;
			if (!state.Socket.Connected)
				return;
			int readed = state.Socket.EndReceive(ar);

			Package p = ByteConverter.FromBytes(state.Buffer);

			if (PackageRecive != null)
				PackageRecive(this, p);

			StateData nextSize = new StateData(state.Socket);
			nextSize.Socket.BeginReceive(nextSize.Buffer, 0, nextSize.Size, 
				SocketFlags.None, new AsyncCallback(ReadSizeCallback), nextSize);
		}

		public void Send(Package p)
		{
			byte[] data = ByteConverter.ToBytes(p);
			byte[] size = BitConverter.GetBytes(data.Length);
			sock.Send(size);
			sock.Send(data);
		}

		public void Disconnect()
		{
			sock.Disconnect(false);
		}
	}
}
