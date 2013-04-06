using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsClient
{
	public class StateManager
	{
		public State CurrentState { get; private set; }
		public event EventHandler<State> StateChaged;

		public StateManager()
		{
			ServerComunicator.Instance.PackageRecive += Instance_PackageRecive;
			CurrentState = new LoginState();
			CurrentState.StateChanged += CurrentState_StateChanged;
		}

		void CurrentState_StateChanged(object sender, State e)
		{
			if (e != CurrentState)
			{
				CurrentState = e;
				if (StateChaged != null)
					StateChaged(this, CurrentState);
			}
		}

		void Instance_PackageRecive(object sender, PackageReciveEventArgs e)
		{
			var newState = CurrentState.HandlePackage(e.Data);
			if (newState != CurrentState)
			{
				CurrentState = newState;
				if (StateChaged != null)
					StateChaged(this, CurrentState);
			}
		}
	}
}
