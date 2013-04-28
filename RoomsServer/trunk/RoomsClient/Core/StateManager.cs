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
		private List<State> states = new List<State>();

		public StateManager()
		{
			ServerComunicator.Instance.PackageRecive += Instance_PackageRecive;
			CurrentState = new LoginState();
			CurrentState.StateChanged += CurrentState_StateChanged;
			states.Add(CurrentState);
		}

		void CurrentState_StateChanged(object sender, State e)
		{
			UpdateState(e);
		}

		void Instance_PackageRecive(object sender, PackageReciveEventArgs e)
		{
			foreach (var state in states)
			{
				if (state != CurrentState)
					state.HandlePackage(e.Data);
			}
			var newState = CurrentState.HandlePackage(e.Data);
			UpdateState(newState);
		}

		private void UpdateState(State candidate)
		{
			if (candidate == CurrentState)
				return;

			CurrentState = candidate;
			CurrentState.StateChanged += CurrentState_StateChanged;
			if (!states.Contains(CurrentState))
				states.Add(CurrentState);

			if (StateChaged != null)
				StateChaged(this, CurrentState);
		}
	}
}
