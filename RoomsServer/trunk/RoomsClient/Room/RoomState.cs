using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packages;
using Gameplay;
using System.Drawing;
using Resources;

namespace RoomsClient
{
	public class RoomState : State
	{
		private RoomPanel control;
		private List<RoomClient> clients = new List<RoomClient>();
		private LobbyState lobby;
		public ClientFiled Filed { get; private set; }
		public RoomClient Me { get; private set; }

		public RoomState(LobbyState lobby, RoomClient me, ClientFiled filed)
		{
			this.lobby = lobby;
			this.Filed = filed;
			Me = me;
			control = new RoomPanel(this);
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
					newClient.Player = new ClientPlayer(newClient.Name,
						ResourceManager.GetImage(jrPack.Image.ToString()));
					clients.Add(newClient);
					control.WriteMessge(newClient, "Joined room!", MessageType.Public);
					break;
				case PackageType.RoomSessionEnd:
					RoomSessionEndPackage rsePack = (RoomSessionEndPackage)pack;
					ExitedRoomPackage erPack = new ExitedRoomPackage();
					ServerComunicator.Instance.Send(erPack);
					StatsState stats = new StatsState(lobby, from c in clients select c.Name, rsePack.EloAdded);
					return stats;
				case PackageType.PublicRoomMessage:
					PublicRoomMessagePackage prmPack = (PublicRoomMessagePackage)pack;
					control.WriteMessge(clients.First(c => c.Name == prmPack.Name), prmPack.Text, MessageType.Public);
					break;
				case PackageType.TeamRoomMessage:
					TeamRoomMessagePackage trmPack = (TeamRoomMessagePackage)pack;
					control.WriteMessge(clients.First(c => c.Name == trmPack.Name), trmPack.Text, MessageType.Team);
					break;
				case PackageType.FiledDataUpdated:
					FiledDataUpdatedPackage fduPack = (FiledDataUpdatedPackage)pack;
					FiledData<ClientPlayer> data = new FiledData<ClientPlayer>();
					
					for (int i = 0; i < fduPack.X.Count; i++)
					{
						Point p = new Point(fduPack.X[i], fduPack.Y[i]);
						ClientCell cell = new ClientCell();
						cell.Down = fduPack.Down[i];
						cell.IsWall = fduPack.IsWall[i];
						cell.Right = fduPack.Right[i];
						cell.Symbol = null;
						if (fduPack.Values[i] != "")
							cell.Symbol = clients.FirstOrDefault(c => c.Name == fduPack.Values[i]).Player;
						data.Add(p, cell);
					}
					Filed.Update(data);
					control.UpdateFiled(fduPack.X.Zip(fduPack.Y, (x, y) => new Point(x, y)));
					break;
				case PackageType.PlayerStep:
					PlayerStepPackage psPack = (PlayerStepPackage)pack;
					control.PlayigPlayerName = psPack.Name;
					if (psPack.Name == Me.Name)
					{
						Filed.Unlock();
					}
					else
					{
						Filed.Lock();
					}
					break;
				default:
					break;
			}//32748067
			return this;
		}

		public override System.Windows.Forms.Control View
		{
			get { return control; }
		}
	}
}
