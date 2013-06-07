using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CopyingModel
{
	public class StaticAnimation : Animation
	{
		private SpriteBatch spriteBatch;
		public StaticAnimation(SpriteBatch spriteBatch, DrawSettings drawSettings)
			: base(drawSettings)
		{
			this.spriteBatch = spriteBatch;
		}

		public override void Start()
		{
		}

		public override void Continue()
		{
		}

		public override void Stop()
		{
			FireAnimationEnd();
		}

		public override void Pause()
		{
		}

		public override void Update(GameTime time)
		{
		}

		public override void Draw(GameTime time)
		{
			spriteBatch.Begin();

			spriteBatch.Draw(DrawSettings);

			spriteBatch.End();
		}
	}
}
