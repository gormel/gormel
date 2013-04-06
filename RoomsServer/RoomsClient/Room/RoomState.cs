using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packages;

namespace RoomsClient
{
	public class RoomState : State
	{
		private RoomPanel control;
		private List<RoomClient> clients = new List<RoomClient>();
		private LobbyState lobby;
		public RoomClient Me { get; private set; }

		public RoomState(LobbyState lobby, RoomClient me)
		{
			this.lobby = lobby;
			control = new RoomPanel(this);
			Me = me;
		}

		public override State HandlePackage(Package pack)
		{
			switch (pack.ID)
			{
				case PackageType.JoinedRoom:
					JoinedRoomPackage jrPack = (JoinedRoomPackage)pack;
					RoomClient newClient = new RoomClient();
					newClient.Name = jrPack.Name;
					newClient.Team = jrPack.Team;
					clients.Add(newClient);
					control.WriteMessge(newClient, "Joined room!", MessageType.Public);
					break;
				case PackageType.RoomSessionEnd:
					ExitedRoomPackage erPack = new ExitedRoomPackage();
					ServerComunicator.Instance.Send(erPack);
					return lobby;
				case PackageType.PublicRoomMessage:
					PublicRoomMessagePackage prmPack = (PublicRoomMessagePackage)pack;
					control.WriteMessge(clients.First(c => c.Name == prmPack.Name), prmPack.Text, MessageType.Public);
					break;
				case PackageType.TeamRoomMessage:
					TeamRoomMessagePackage trmPack = (TeamRoomMessagePackage)pack;
					control.WriteMessge(clients.First(c => c.Name == trmPack.Name), trmPack.Text, MessageType.Team);
					break;
				default:
					break;
			}
			return this;
		}

		public override System.Windows.Forms.Control View
		{
			get { return control; }
		}
	}
}
