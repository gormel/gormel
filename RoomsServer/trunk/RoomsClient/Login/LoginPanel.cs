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
			package.Name = textBox1.Text;
			package.PasswordMD5 = "md5";
			ServerComunicator.Instance.Send(package);
			MyName = textBox1.Text;
		}
	}
}
