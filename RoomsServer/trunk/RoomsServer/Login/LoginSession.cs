using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Gameplay;
using Packages;

namespace RoomsServer
{
	public class LoginSession : Session<LoginClientInfo>
	{
		private bool ValidateLogin(LoginPackage pack)
		{
			ClientRecord record = Server.Instance.ClientStorage[pack.Name];
			if (record == null)
				return false;
			if (record.PasswordMD5 != Encrypter.GetMD5(Server.Instance.Salt + pack.PasswordMD5))
				return false;
			if (Server.Instance.LobbySession.Clients.Any(c => c.Name == pack.Name))
				return false;
			return true;
		}

		protected override void OnPackageRecive(LoginClientInfo info, Package pack)
		{
			switch (pack.ID)
			{
				case PackageType.Login:
					LoginPackage package = (LoginPackage)pack;
					string name = package.Name;
					if (!ValidateLogin(package))
					{
						info.Client.Send(new LoginFailedPackage());
					}
					else
					{
						info.Client.Send(new LoginSuccessPackage());
						LobbyClientInfo newInfo = new LobbyClientInfo(info.Client, name);
						Server.Instance.LobbySession.Clients.Add(newInfo);
					}
					break;
				case PackageType.Register:
					RegisterPackage rPack = (RegisterPackage)pack;
					var creationResult = Server.Instance.ClientStorage.DAO.CreateClientRecord(rPack.Name, 
						Encrypter.GetMD5(Server.Instance.Salt + rPack.PasswordMD5));
					RegisterResultPackage result = new RegisterResultPackage();
					result.Result = creationResult != null;
					info.Client.Send(result);
					break;
				default:
					break;
			}
		}
	}
}
