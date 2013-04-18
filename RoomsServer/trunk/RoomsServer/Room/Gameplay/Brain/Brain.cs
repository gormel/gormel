using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gameplay;

namespace RoomsServer
{
	public abstract class Brain
	{
		protected IFiled filed;
		public Brain(IFiled filed)
		{
			this.filed = filed;
		}
		public abstract FiledCursor Step(FiledCursor opponentStep);
	}
}
