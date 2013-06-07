using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CopyingModel
{
	public class LandCursor : GameObject<LandCursor.State>
	{
		public enum State 
		{
			CursorDown,
			CursorUp,
		}
		public Point Position { get; private set; }
		public float CellWidth { get; private set; }
		public float CellHeight { get; private set; }

		private MouseState lastMouseState;

		public LandCursor(float cellWidth, float cellHeight)
		{
			this.CellWidth = cellWidth;
			this.CellHeight = cellHeight;
		}

		public override void Update(GameTime time)
		{
			MouseState state = Mouse.GetState();

			int x = (int)(state.X / CellWidth);
			int y = (int)(state.Y / CellHeight);

			Position = new Point(x, y);

			if (state.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
				FireStateChanged(State.CursorDown, this, Position);

			if (state.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed)
				FireStateChanged(State.CursorUp, this, Position);

			lastMouseState = state;
		}
	}
}
