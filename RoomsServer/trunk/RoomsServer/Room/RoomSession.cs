using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packages;

namespace RoomsServer
{
	public class RoomSession : Session<RoomClientInfo>
	{
		public RoomSession(IEnumerable<RoomClientInfo> clients)
		{
			foreach (var client in clients)
			{
				YouJoinedRoomPackage yjrPack = new YouJoinedRoomPackage();
				yjrPack.Team = client.Team.Name;
				client.Client.Send(yjrPack);
				foreach (var item in clients)
				{
					JoinedRoomPackage p = new JoinedRoomPackage();
					p.Name = item.Name;
					p.Team = item.Team.Name;
					client.Client.Send(p);
				}
				Clients.Add(client);
			}
		}

		protected override void OnPackageRecive(RoomClientInfo info, Packages.Package pack)
		{
			switch (pack.ID)
			{
				case PackageType.PublicRoomMessage:
					foreach (var client in Clients)
					{
						client.Client.Send(pack);
					}
					break;
				case PackageType.TeamRoomMessage:
					foreach (var teamate in Clients.Where(i => i.Team == info.Team))
					{
						teamate.Client.Send(pack);
					}
					break;
				case PackageType.PrivateRoomMessage:
					PrivateRoomMessagePackage prmPack = (PrivateRoomMessagePackage)pack;
					Clients.First(i => i.Name == prmPack.To).Client.Send(pack);
					break;
				default:
					break;
			}
		}
	}
}
