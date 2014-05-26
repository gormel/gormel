using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Data = System.Tuple<System.Net.Sockets.Socket, byte[]>;

namespace SSH_Server
{
    class Server
    {
        private static Server inst = null;
        private Socket listener;

        static void Main(string[] args)
        {
            Server s = Instance;
        }

        private Server()
        {
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(new IPEndPoint(IPAddress.Any, 5568));
            listener.Listen(100);
            listener.BeginAccept(AcceptCallback, listener);
        }

        private void AcceptCallback(IAsyncResult result)
        {
            Socket listener = (Socket)result.AsyncState;
            Socket clientSocket = listener.EndAccept(result);

            Data willReceved = Tuple.Create(clientSocket, new byte[4]);
            clientSocket.BeginReceive(willReceved.Item2, 0, 4, SocketFlags.None, ReadSizeCallback, willReceved);

            listener.BeginAccept(AcceptCallback, listener);
        }

        private void ReadSizeCallback(IAsyncResult result)
        {
            Data receved = (Data)result.AsyncState;
            Socket clientSock = receved.Item1;
            byte[] sizeData = receved.Item2;

            clientSock.EndReceive(result);

            int size = BitConverter.ToInt32(sizeData, 0);

            Data willReceved = Tuple.Create(clientSock, new byte[size]);
            clientSock.BeginReceive(willReceved.Item2, 0, size, SocketFlags.None, ReadDataCallback, willReceved);
        }

        private void ReadDataCallback(IAsyncResult result)
        {
            Data receved = (Data)result.AsyncState;
            Socket clientSock = receved.Item1;
            byte[] data = receved.Item2;

            clientSock.EndReceive(result);

            //TODO: Обработка данных

            Data willReceved = Tuple.Create(clientSock, new byte[4]);
            clientSock.BeginReceive(willReceved.Item2, 0, 4, SocketFlags.None, ReadSizeCallback, willReceved);
        }

        public static Server Instance
        {
            get
            {
                if (inst == null)
                    inst = new Server();
                return inst;
            }
        }
    }
}
