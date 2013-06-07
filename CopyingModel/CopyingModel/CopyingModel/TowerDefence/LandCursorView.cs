using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CopyingModel
{
	public class LandCursorView : DrawObject<LandCursor, LandCursor.State>
	{
		private Animation cursorAnimation;
		public LandCursorView(SpriteBatch spriteBatch, LandCursor cursor)
			: base(cursor, spriteBatch)
		{
			DrawSettings settings = new DrawSettings();
			settings.Scale =
				new Vector2(cursor.CellWidth / ContentManager.CursorAnimation.First().Width,
					cursor.CellHeight / ContentManager.CursorAnimation.First().Height);

			cursorAnimation = new SpriteAnimation(spriteBatch, settings,
				ContentManager.CursorAnimation, TimeSpan.FromMilliseconds(1000));
			cursorAnimation.Start();
			cursorAnimation.AnimationEnd += (s, e) => ((Animation)s).Start();
		}

		public override void Update(GameTime time)
		{
			var pos = new Vector2(DrawigObject.Position.X, DrawigObject.Position.Y);
			pos.X *= DrawigObject.CellWidth;
			pos.Y *= DrawigObject.CellHeight;
			cursorAnimation.DrawSettings.Position = pos;
			cursorAnimation.Update(time);
		}

		public override void Draw(GameTime time)
		{
			cursorAnimation.Draw(time);
		}
	}
}
