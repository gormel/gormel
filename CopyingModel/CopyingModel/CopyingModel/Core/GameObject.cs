using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CopyingModel
{
	public abstract class GameObject<T>
	{
		public event EventHandler<StateChangedEventArgs<T>> StateChanged;
		public abstract void Update(GameTime time);

		protected void FireStateChanged(StateChangedEventArgs<T> args)
		{
			if (StateChanged != null)
				StateChanged(this, args);
		}

		protected void FireStateChanged(T state, params object[] args)
		{
			var eventArgs = new StateChangedEventArgs<T>();
			eventArgs.State = state;
			eventArgs.Args.AddRange(args);
			FireStateChanged(eventArgs);
		}
	}
}
