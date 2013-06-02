using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CopyingModel
{
	public class MovingAnimation : Animation
	{
		public Vector2 From { get; private set; }
		public Vector2 To      { get; private set; }
		public Vector2 Pos     { get; private set; }
		public Vector2 Velocity { get; private set; }

		private SpriteBatch spriteBatch;
		private DrawSettings settings;

		TimeSpan processedTime;
		TimeSpan animationTime;

		public MovingAnimation(SpriteBatch spriteBatch, DrawSettings settings, Vector2 from, Vector2 to, TimeSpan time)
		{
			Pos = from;
			this.To = to;
			this.From = from;
			this.spriteBatch = spriteBatch;
			this.settings = settings;
			this.animationTime = time;
			processedTime = TimeSpan.FromMilliseconds(0);

			Velocity = to - from;
			if (time.TotalMilliseconds > 0)
				Velocity /= (float)time.TotalMilliseconds;
		}

		public override void Start()
		{
			Pos = From;
			processedTime = TimeSpan.FromMilliseconds(0);
			InProcess = true;
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
			if (InProcess)
			{
				processedTime += time.ElapsedGameTime;
				Pos += Velocity * (float)time.ElapsedGameTime.TotalMilliseconds;
				if (processedTime >= animationTime)
					Stop();
			}
		}

		public override void Draw(GameTime time)
		{
			spriteBatch.Begin();

			settings.Position = Pos;
			spriteBatch.Draw(settings);

			spriteBatch.End();
		}
	}
}
