using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomsClient
{
	public partial class StatePanel<T> : UserControl where T : State
	{
		protected T MyState { get; private set; }
		public StatePanel(T myState)
		{
			InitializeComponent();
			MyState = myState;
			Dock = DockStyle.Fill;
		}
	}
}
