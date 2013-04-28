using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Packages;

namespace RoomsClient
{
	public class LoginState : State
	{
		LoginPanel control;

		public LoginState()
		{
			control = new LoginPanel(this);
		}

		public override State HandlePackage(Package pack)
		{
			switch (pack.ID)
			{
				case PackageType.LoginSuccess:
					control.LoginResult(true);
					LobbyClient me = new LobbyClient();
					me.Name = control.MyName;
					LobbyState lobby = new LobbyState(me, this);
					return lobby;
				case PackageType.LoginFailed:
					control.LoginResult(false);
					break;
				case PackageType.RegisterResult:
					RegisterResultPackage rrPack = (RegisterResultPackage)pack;
					control.RegistrationResult(rrPack.Result);
					break;
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
