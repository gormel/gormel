using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CopyingModel
{
	public enum CursorStates
	{

	}
	public class Cursor : GameObject<CursorStates>
	{
		public Vector2 Position { get; private set; }
		public Cursor()
		{

		}

		public override void Update(GameTime time)
		{
			MouseState mouseState = Mouse.GetState();
			Position = new Vector2(mouseState.X, mouseState.Y);
		}
	}
}
