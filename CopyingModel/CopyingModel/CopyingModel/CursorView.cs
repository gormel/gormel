using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CopyingModel
{
	public class CursorView : DrawObject<Cursor, CursorStates>
	{
		private TimeSpan animationTimeout = TimeSpan.FromMilliseconds(50);
		private TimeSpan spendTime = TimeSpan.FromMilliseconds(0);
		public CursorView(Cursor cursor, SpriteBatch spriteBatch)
			: base(cursor, spriteBatch)
		{

		}

		public override void Update(GameTime time)
		{
			base.Update(time);

			if (spendTime < animationTimeout)
			{
				spendTime += time.ElapsedGameTime;
				return;
			}

			spendTime = TimeSpan.FromMilliseconds(0);
			DrawSettings settings = new DrawSettings();
			settings.Position = DrawigObject.Position;
			settings.Texture = ContentManager.CursorTexture;
			settings.Origin = new Vector2(settings.Texture.Width / 2, settings.Texture.Height / 2);

			ScaleAnimation anim = new ScaleAnimation(spriteBatch, settings, new Vector2(1, 1), Vector2.Zero, TimeSpan.FromMilliseconds(animationTimeout.TotalMilliseconds * 10));
			anim.AnimationEnd += (s, e) =>
			{
				Animations.Remove((Animation)s);
			};

			anim.Start();
			Animations.Add(anim);
		}

		public override void Draw(GameTime time)
		{
			base.Draw(time);

			spriteBatch.Begin();

			DrawSettings settings = new DrawSettings();
			settings.Position = DrawigObject.Position;
			settings.Texture = ContentManager.CursorTexture;
			settings.Origin = new Vector2(settings.Texture.Width / 2, settings.Texture.Height / 2);
			spriteBatch.Draw(settings);

			spriteBatch.End();
		}
	}
}
