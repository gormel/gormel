using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Packages;

namespace RoomsClient
{
	public abstract class State
	{
		public abstract State HandlePackage(Package pack);
		public abstract Control View { get; }
		public event EventHandler<State> StateChanged;
		protected void ChangeState(State state)
		{
			if (StateChanged != null)
				StateChanged(this, state);
		}
	}
}
