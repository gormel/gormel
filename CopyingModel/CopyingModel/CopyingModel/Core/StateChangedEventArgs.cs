using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CopyingModel
{
	public class StateChangedEventArgs<T> : EventArgs
	{
		public T State { get; set; }
		public List<Object> Args { get; private set; }

		public StateChangedEventArgs()
		{
			Args = new List<object>();
		}
	}
}
