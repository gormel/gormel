﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packages;

namespace RoomsServer
{
	public class LobbySession : Session<LobbyClientInfo>
	{
		public LobbySession()
		{
			Clients.CollectionChanged += Clients_CollectionChanged;
			queues.Add(QueueType.Queue1v1, new ClientQueue(new QueueInfo(1, 2, "1v1")));
			queues.Add(QueueType.Queue2v2, new ClientQueue(new QueueInfo(2, 2, "2v2")));
		}

		void Clients_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems == null)
				return;
			foreach (var i in e.NewItems)
			{
				LobbyClientInfo info = (LobbyClientInfo)i;
				foreach (var inf in Clients)
				{
					if (inf != info)
					{
						LoggedInPackage p = new LoggedInPackage();
						p.Name = info.Name;
						inf.Client.Send(p);
						p.Name = inf.Name;
						info.Client.Send(p);
					}
				}
			}
		}
		protected override void OnPackageRecive(LobbyClientInfo info, Package pack)
		{
			switch (pack.ID)
			{
				case PackageType.Logout:
					Clients.Remove(info);
					foreach (var i in Clients)
					{
						LoggedOutPackage p = new LoggedOutPackage();
						p.Name = info.Name;
						i.Client.Send(p);
					}
					info.Client.Disconnect();
					break;
				case PackageType.JoinQueue:
					if (info.InRoom)
						break;
					var jqPackage = (JoinQueuePackage)pack;
					var teamates = Clients.Where(i => jqPackage.Teammates.Contains(i.Name))
						.Concat( new LobbyClientInfo[] { info } );
					queues[jqPackage.QueueType].Add(new ClientsUnion(teamates));
					UpdateQueue(jqPackage.QueueType);
					break;
				case PackageType.ExitedRoom:
					info.InRoom = false;
					break;
				default:
					break;
			}
		}

		private Dictionary<QueueType, ClientQueue> queues = new Dictionary<QueueType, ClientQueue>();

		private void UpdateQueue(QueueType queueType)
		{
			ClientQueue queue = queues[queueType];
			List<List<LobbyClientInfo>> teams = null;
			if (!queue.TryGetTeams(out teams))
				return;

			List<RoomClientInfo> roomClients = new List<RoomClientInfo>();
			int i = 1;
			foreach (var team in teams)
			{
				Team t = new Team(string.Format("Team {0}", i));
				i++;
				foreach (var client in team)
				{
					RoomClientInfo clientInfo = new RoomClientInfo(client.Client, client.Name, t);
					roomClients.Add(clientInfo);
					client.InRoom = true;
				}
			}
			RoomSession room = new RoomSession(roomClients);
			Server.Instance.Rooms.Add(room);
		}
	}
}