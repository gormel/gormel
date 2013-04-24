using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Packages;

namespace RoomsClient
{
	public class StatsPanel : StatePanel<StatsState>
	{
		private ListBox statsList = new ListBox();
		private Button exitButton = new Button();

		public StatsPanel(StatsState state, int added)
			: base(state)
		{
			InitializeComponents();
			statsList.Items.Add(string.Format("changed elo: {1}{0}", added, (added > 0 ? "+" : "")));
		}

		private void InitializeComponents()
		{
			statsList.Dock = DockStyle.Fill;

			exitButton.Text = "Return to lobby";
			exitButton.Click += exitButton_Click;
			exitButton.Dock = DockStyle.Bottom;
			exitButton.Height = 20;

			Controls.Add(exitButton);
			Controls.Add(statsList);
		}

		void exitButton_Click(object sender, EventArgs e)
		{
			ServerComunicator.Instance.Send(new StatsPackage());
		}

		public void AddStats(string name, int elo)
		{
			if (statsList.InvokeRequired)
				statsList.Invoke(new Action<string, int>(AddStats), name, elo);
			else
				statsList.Items.Add(string.Format("{0} - {1}", name, elo));
		}
	}
}
