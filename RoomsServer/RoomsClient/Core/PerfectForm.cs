using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomsClient
{
	public partial class PerfectForm : Form
	{
		public PerfectForm()
		{
			InitializeComponent();
			manager.StateChaged += manager_StateChaged;
			Controls.Add(manager.CurrentState.View);
		}

		private delegate void Method(object o, State s);
		void manager_StateChaged(object sender, State s)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new Method(manager_StateChaged)	, sender, s);
			}
			else
			{
				Controls.Clear();
				Controls.Add(s.View);
			}
		}

		StateManager manager = new StateManager();
	}
}
