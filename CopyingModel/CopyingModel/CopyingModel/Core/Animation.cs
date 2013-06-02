using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CopyingModel
{
	public abstract class Animation
	{
		public bool InProcess { get; protected set; }
		public event EventHandler AnimationEnd;
		protected void FireAnimationEnd()
		{
			if (AnimationEnd != null)
				AnimationEnd(this, null);
		}

		public abstract void Start();
		public abstract void Continue();
		public abstract void Stop();
		public abstract void Pause();

		public abstract void Update(GameTime time);
		public abstract void Draw(GameTime time);
	}
}
