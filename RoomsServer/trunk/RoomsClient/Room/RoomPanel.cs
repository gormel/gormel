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
	public enum MessageType
	{
		Public,
		Private,
		Team,
	}

	public partial class RoomPanel : StatePanel<RoomState>
	{
		public RoomPanel(RoomState state)
			: base(state)
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string rawMessage = textBox1.Text;
			if (rawMessage.StartsWith("/team "))
			{
				TeamRoomMessagePackage pack = new TeamRoomMessagePackage();
				pack.Name = MyState.Me.Name;
				pack.Text = rawMessage.Substring(6);
				ServerComunicator.Instance.Send(pack);
				return;
			}

			PublicRoomMessagePackage pack1 = new PublicRoomMessagePackage();
			pack1.Name = MyState.Me.Name;
			pack1.Text = rawMessage;
			ServerComunicator.Instance.Send(pack1);
		}

		public void WriteMessge(RoomClient from, string text, MessageType messageType)
		{
			if (InvokeRequired)
				Invoke(new Action<RoomClient, string, MessageType>(WriteMessge), from, text, messageType);
			else
			{
				string outString = "";
				switch (messageType)
				{
					case MessageType.Public:
						outString = string.Format("{0}[{3}]: {1}{2}", from.Name, text, Environment.NewLine, from.Team);
						break;
					case MessageType.Private:
						outString = string.Format("{0}[p]: {1}{2}", from.Name, text, Environment.NewLine);
						break;
					case MessageType.Team:
						outString = string.Format("{0}[t]: {1}{2}", from.Name, text, Environment.NewLine);
						break;
					default:
						break;
				}
				textBox2.AppendText(outString);
			}
		}
	}
}
