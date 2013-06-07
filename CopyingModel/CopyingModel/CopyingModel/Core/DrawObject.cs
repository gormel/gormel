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

		public DrawObject(T drawingObject, SpriteBatch spriteBatch)
		{
			DrawigObject = drawingObject;
			this.spriteBatch = spriteBatch;
		}

		public abstract void Draw(GameTime time);
		public abstract void Update(GameTime time);
	}
}
