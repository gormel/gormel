using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Packages;

namespace RoomsClient
{
	public class StatsState : State
	{
		StatsPanel control = null;
		private LobbyState lobby;
		public StatsState(LobbyState lobby, IEnumerable<string> who, int added)
		{
			control = new StatsPanel(this, added);
			this.lobby = lobby;
			foreach (var name in who)
			{
				GetPlayerInfoPackage gpiPack = new GetPlayerInfoPackage();
				gpiPack.Name = name;
				ServerComunicator.Instance.Send(gpiPack);
			}
		}

		public override State HandlePackage(Package pack)
		{
			switch (pack.ID)
			{
				case PackageType.PlayerInfo:
					PlayerInfoPackage piPack = (PlayerInfoPackage)pack;
					control.AddStats(piPack.Name, piPack.Elo);
					break;
				case PackageType.Stats:
					return lobby;
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
