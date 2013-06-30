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
		public event EventHandler Pressed;
		public event EventHandler Released;
		private MouseState lastMouseState;
		private bool mouseIsOn;
		private bool mouseIsDown;
		private bool enterIsDown;

		public Button(UIControl baseControl, GraphicsDevice device)
			: base(baseControl, device)
		{
			HorisontalTextAlligment = HorisontalAlligment.Center;
			VerticalTextAlligment = VerticalAlligment.Center;
			Activable = true;
			Pressed += Button_Pressed;
		}

		void Button_Pressed(object sender, EventArgs e)
		{
			Activate();
		}

		protected override void OnKeyDown(Keys key)
		{
			base.OnKeyDown(key);

			if (key == Keys.Enter)
			{
				enterIsDown = true;
				if (Pressed != null)
					Pressed(this, null);

			}
		}

		protected override void OnKeyUp(Keys key)
		{
			base.OnKeyUp(key);

			if (key == Keys.Enter)
			{
				enterIsDown = false;
				if (Released != null)
					Released(this, null);
			}
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
					if (Pressed != null)
						Pressed(this, null);

				if (lastMouseState.LeftButton == ButtonState.Pressed &&
					mouseState.LeftButton == ButtonState.Released)
					if (Released != null)
						Released(this, null);
			}

			lastMouseState = mouseState;

			base.Update(time);
		}

		protected override void DrawBody(GameTime time)
		{
			base.DrawBody(time);
			Color fill = Color.Transparent;
			if (mouseIsDown || enterIsDown)
			{
				fill = new Color(0, 0, 0, 150);
			}
			else if (mouseIsOn)
			{
				fill = new Color(255, 255, 255, 50);
			}
			PrimetiveDarwHelper.DrawBox(new Vector2(X - BorderWidth / 2, Y - BorderWidth / 2),
				new Vector2(Width + BorderWidth, Height + BorderWidth), fill, fill, 0);
			if (Active)
				PrimetiveDarwHelper.DrawBox(new Vector2(X, Y), new Vector2(Width, Height), Color.Transparent,
					Color.Black, 1);
		}

		private bool Inside(float x, float y)
		{
			return x > X && x < X + Width && y > Y && y < Y + Height;
		}
	}
}
