using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Packages;
using System.Security.Cryptography;
using Gameplay;

namespace RoomsClient
{
	public partial class LoginPanel : StatePanel<LoginState>
	{
		public string MyName { get; private set; }
		
		public LoginPanel(LoginState myState)
			: base(myState)
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			LoginPackage package = new LoginPackage();
			package.Name = nameText.Text;
			package.PasswordMD5 = Encrypter.GetMD5(passText.Text);
			ServerComunicator.Instance.Send(package);
			MyName = nameText.Text;
			nameText.Text = "";
			passText.Text = "";
		}

		private void button2_Click(object sender, EventArgs e)
		{
			RegisterPackage pack = new RegisterPackage();
			pack.Name = nameText.Text;
			pack.PasswordMD5 = Encrypter.GetMD5(passText.Text);
			ServerComunicator.Instance.Send(pack);
			nameText.Text = "";
			passText.Text = "";
		}

		public void RegistrationResult(bool result)
		{
			if (status.InvokeRequired)
			{
				status.Invoke(new Action<bool>(RegistrationResult), result);
			}
			else
			{
				status.Text = "Registration result: " + (result ? "Success!" : "Failed!");
			}
		}

		public void LoginResult(bool result)
		{
			if (status.InvokeRequired)
			{
				status.Invoke(new Action<bool>(LoginResult), result);
			}
			else
			{
				status.Text = "Login result: " + (result ? "Success!" : "Failed!");
			}
		}
	}
}
