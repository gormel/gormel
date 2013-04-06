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
					LobbyClient me = new LobbyClient();
					me.Name = control.MyName;
					LobbyState lobby = new LobbyState(me);
					return lobby;
				case PackageType.LoginFailed:
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
