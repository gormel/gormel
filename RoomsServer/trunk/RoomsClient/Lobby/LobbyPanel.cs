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
using System.Collections.ObjectModel;

namespace RoomsClient
{
	public partial class LobbyPanel : StatePanel<LobbyState>
	{
		public LobbyPanel(LobbyState state)
			: base(state)
		{
			InitializeComponent();
			label1.Text = MyState.Me.Name;
			Clients = new ObservableCollection<LobbyClient>();
			Clients.CollectionChanged += Clients_CollectionChanged;
		}

		void Clients_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			listBox1.Items.Clear();
			listBox1.Items.AddRange(Clients.Select(c => c.Name).ToArray());
		}

		public bool InQueue
		{
			set
			{
				Action setAction = delegate
				{
					button1.Enabled = !value;
					button2.Enabled = !value;
					button3.Enabled = !value;
				};

				if (InvokeRequired)
					Invoke(setAction);
				else
					setAction();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			ServerComunicator.Instance.Send(new LogOutPackage());
			//ServerComunicator.Instance.Disconnect();
		}

		public void AddClient(LobbyClient client)
		{
			if (InvokeRequired)
				Invoke(new Action<LobbyClient>(AddClient), client);
			else
			{
				Clients.Add(client);
			}
		}

		public void RemoveClient(LobbyClient client)
		{
			if (InvokeRequired)
				Invoke(new Action<LobbyClient>(RemoveClient), client);
			else
				Clients.Remove(client);
		}

		public ObservableCollection<LobbyClient> Clients { get; private set; }

		private void button2_Click(object sender, EventArgs e)
		{
			InQueue = true;
			JoinQueuePackage jqPack = new JoinQueuePackage();
			jqPack.QueueType = QueueType.Queue1v1;
			ServerComunicator.Instance.Send(jqPack);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			InQueue = true;
			JoinQueuePackage jqPack = new JoinQueuePackage();
			jqPack.QueueType = QueueType.Queue2v2;
			ServerComunicator.Instance.Send(jqPack);
		}
	}
}
