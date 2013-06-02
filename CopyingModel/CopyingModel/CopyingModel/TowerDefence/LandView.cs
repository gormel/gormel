using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CopyingModel
{
	public class LandView : DrawObject<Land, Land.States>
	{
		public float Width { get; private set; }
		public float Height { get; private set; }

		private float enteryWidth;
		private float enteryHeight;

		public LandView(Land land, SpriteBatch spriteBatch, float width, float height)
			: base(land, spriteBatch)
		{
			Width = width;
			Height = height;

			enteryWidth = Width / land.Width;
			enteryHeight = Height / land.Height;

			land.StateChanged += land_StateChanged;
		}

		void land_StateChanged(object sender, StateChangedEventArgs<Land.States> e)
		{
			switch (e.State)
			{
				case Land.States.MonsterCreated:
					var pos = (Point)e.Args[1];
					var sprites = SpriteAnimation.Cut(ContentManager.MonsterAnimations, 10, 10, 6, 10);

					DrawSettings settings = new DrawSettings();
					settings.Scale = new Vector2(enteryWidth / sprites.First().Width, 
												enteryHeight / sprites.First().Height);
					settings.Origin = settings.Scale / 2;
					settings.Position = new Vector2(pos.X * enteryWidth, pos.Y * enteryHeight);

					SpriteAnimation anim = new SpriteAnimation(spriteBatch, settings, sprites, TimeSpan.FromMilliseconds(250 * 6));
					anim.AnimationEnd += (s, ev) =>
						{
							((Animation)s).Start();
						};
					anim.Start();

					Animations.Add(anim);
					break;
				default:
					break;
			}
		}
	}
}
