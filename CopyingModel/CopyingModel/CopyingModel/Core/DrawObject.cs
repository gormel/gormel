using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CopyingModel
{
	public abstract class DrawObject<T, TState> where T : GameObject<TState>
	{
		protected T DrawigObject { get; set; }
		protected SpriteBatch spriteBatch;
		public List<Animation> Animations { get; private set; }

		public DrawObject(T drawingObject, SpriteBatch spriteBatch)
		{
			DrawigObject = drawingObject;
			Animations = new List<Animation>();
			this.spriteBatch = spriteBatch;
		}

		public virtual void Draw(GameTime time)
		{
			for (int i = 0; i < Animations.Count; i++)
			{
				Animations[i].Draw(time);
			}
		}
		public virtual void Update(GameTime time)
		{
			for (int i = 0; i < Animations.Count; i++)
			{
				Animations[i].Update(time);
			}
		}
	}
}
