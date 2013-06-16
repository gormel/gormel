using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UILib.Base;

namespace UILib.Controls
{
	public class Button : Label
	{
		public event EventHandler MouseDown;
		public event EventHandler MouseUp;
		private MouseState lastMouseState;
		private bool mouseIsOn;
		private bool mouseIsDown;

		public Button(UIControl baseControl, GraphicsDevice device)
			: base(baseControl, device)
		{
			HorisontalTextAlligment = HorisontalTextAlligment.Center;
			VerticalTextAlligment = VerticalTextAlligment.Center;
		}

		public override void Update(GameTime time)
		{
			MouseState mouseState = Mouse.GetState();

			mouseIsOn = false;
			mouseIsDown = false;
			if (Inside(mouseState.X, mouseState.Y))
			{
				mouseIsOn = true;
				if (mouseState.LeftButton == ButtonState.Pressed)
					mouseIsDown = true;

				if (lastMouseState.LeftButton == ButtonState.Released &&
					mouseState.LeftButton == ButtonState.Pressed)
					if (MouseDown != null)
						MouseDown(this, null);

				if (lastMouseState.LeftButton == ButtonState.Pressed &&
					mouseState.LeftButton == ButtonState.Released)
					if (MouseUp != null)
						MouseUp(this, null);
			}

			lastMouseState = mouseState;

			base.Update(time);
		}

		public override void Draw(GameTime time)
		{
			base.Draw(time);
			Color fill = Color.Transparent;
			if (mouseIsDown)
			{
				fill = new Color(0, 0, 0, 150);
			}
			else if (mouseIsOn)
			{
				fill = new Color(255, 255, 255, 50);
			}
			PrimetiveDarwHelper.DrawBox(new Vector2(X - BorderWidth / 2, Y - BorderWidth / 2), 
				new Vector2(Width + BorderWidth, Height + BorderWidth), fill, fill, 0);
		}

		private bool Inside(float x, float y)
		{
			return x > X && x < X + Width && y > Y && y < Y + Height;
		}
	}
}
