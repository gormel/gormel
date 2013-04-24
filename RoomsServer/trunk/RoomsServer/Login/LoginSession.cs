using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packages;

namespace RoomsServer
{
	public class LoginSession : Session<LoginClientInfo>
	{
		protected override void OnPackageRecive(LoginClientInfo info, Package pack)
		{
			switch (pack.ID)
			{
				case PackageType.Login:
					LoginPackage package = (LoginPackage)pack;
					string name = package.Name;
					if (Server.Instance.LobbySession.Clients.Any(i => i.Name == name))
					{
						info.Client.Send(new LoginFailedPackage());
					}
					else
					{
						info.Client.Send(new LoginSuccessPackage());
						LobbyClientInfo newInfo = new LobbyClientInfo(info.Client, name);
						newInfo.Elo = 1200;//TODO DB
						Server.Instance.LobbySession.Clients.Add(newInfo);
						Clients.Remove(info);
					}
					break;
				default:
					break;
			}
		}
	}
}
