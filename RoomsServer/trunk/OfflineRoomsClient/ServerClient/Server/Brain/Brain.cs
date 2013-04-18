using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineRoomClient
{
	public abstract class Brain
	{
		protected IFiled filed;
		public Brain(IFiled filed)
		{
			this.filed = filed;
		}
		public abstract Cursor Step(Cursor opponentStep);
	}
}
