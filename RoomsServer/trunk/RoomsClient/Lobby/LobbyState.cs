using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Packages;
using RoomsClient.Lobby;
using Resources;

namespace RoomsClient
{
	public class LobbyState : State
	{
		LobbyPanel control;
		public LobbyClient Me { get; private set; }

		public LobbyState(LobbyClient me)
		{
			Me = me;
			control = new LobbyPanel(this);
		}

		public override State HandlePackage(Package pack)
		{
			switch (pack.ID)
			{
				case Packages.PackageType.LoggedIn:
					LoggedInPackage liPackage = (LoggedInPackage)pack;
					LobbyClient connectedClient = new LobbyClient();
					connectedClient.Name = liPackage.Name;
					control.AddClient(connectedClient);
					break;
				case Packages.PackageType.LoggedOut:
					LoggedOutPackage loPackage = (LoggedOutPackage)pack;
					control.RemoveClient(control.Clients.First(c => c.Name == loPackage.Name));
					break;
				case PackageType.YouJoinedRoom:
					YouJoinedRoomPackage yjrPackage = (YouJoinedRoomPackage)pack;
					RoomClient me = new RoomClient();
					me.Name = Me.Name;
					me.Team = yjrPackage.Team;

					ClientPlayer player = new ClientPlayer(me.Name,
						ResourceManager.GetImage(yjrPackage.Image.ToString()));
					me.Player = player;

					ClientFiled filed = new ClientFiled(yjrPackage.Width, yjrPackage.Height);

					RoomState room = new RoomState(this, me, filed);
					control.InQueue = false;
					return room;
				default:
					break;
			}
			return this;
		}

		public override Control View
		{
			get { return control; }
		}
	}
}
