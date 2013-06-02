using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CopyingModel
{
	public class ScaleAnimation : Animation
	{
		private TimeSpan animationTime;
		private TimeSpan spendTime;
		private DrawSettings drawSettings;
		private Vector2 scaleFrom;
		private Vector2 scaleTo;
		private Vector2 currentScale;
		private Vector2 velocity;
		private SpriteBatch spriteBatch;

		public ScaleAnimation(SpriteBatch spriteBatch, DrawSettings settings, Vector2 from, Vector2 to, TimeSpan time)
		{
			animationTime = time;
			scaleFrom = from;
			scaleTo = to;
			drawSettings = settings;
			velocity = (to - from) / (float)time.TotalMilliseconds;
			this.spriteBatch = spriteBatch;
		}

		public override void Start()
		{
			InProcess = true;
			currentScale = scaleFrom;
			spendTime = TimeSpan.FromMilliseconds(0);
		}

		public override void Continue()
		{
			InProcess = true;
		}

		public override void Stop()
		{
			InProcess = false;
			FireAnimationEnd();
		}

		public override void Pause()
		{
			InProcess = false;
		}

		public override void Update(GameTime time)
		{
			if (!InProcess)
				return;
			spendTime += time.ElapsedGameTime;
			currentScale += velocity * (float)time.ElapsedGameTime.TotalMilliseconds;

			if (spendTime >= animationTime)
				Stop();
		}

		public override void Draw(GameTime time)
		{
			spriteBatch.Begin();

			drawSettings.Scale = currentScale;
			spriteBatch.Draw(drawSettings);

			spriteBatch.End();
		}
	}
}
