﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UILib.Utils;

namespace UILib.Base
{
	internal enum ActivationDirection
	{
		Left,
		Right,
		Down,
	}
	/// <summary>
	/// Base class for all UI controls.
	/// </summary>
	public abstract class UIControl
	{
		/// <summary>
		/// Gets or sets horizontal drawing offset of control's bounding box in screen coordinates.
		/// </summary>
		public virtual float X
		{
			get { return BaseControl.X + Margin; }
			set { throw new NotSupportedException(); }
		}
		/// <summary>
		/// Gets or sets vertical drawing offset of control's bounding box in screen coordinates.
		/// </summary>
		public virtual float Y
		{
			get { return BaseControl.Y + Margin; }
			set { throw new NotSupportedException(); }
		}
		/// <summary>
		/// Gets or sets width of control's bounding box.
		/// </summary>
		public virtual float Width
		{
			get { return BaseControl.Width - Margin * 2; }
			set { throw new NotSupportedException(); }
		}
		/// <summary>
		/// Gets or sets height of control's bounding box.
		/// </summary>
		public virtual float Height
		{
			get { return BaseControl.Height - Margin * 2; }
			set { throw new NotSupportedException(); }
		}

		private string name = "";

		/// <summary>
		/// Gets or sets a name of this control.
		/// </summary>
		public string Name
		{
			get { return name; }
			set
			{
				if (value == null)
					throw new ArgumentNullException();
				name = value;
			}
		}
		/// <summary>
		/// Specifies control's render box distance from it's bounding box.
		/// </summary>
		public float Margin { get; set; }
		/// <summary>
		/// Child controls of this control.
		/// </summary>
		public List<UIControl> Controls { get; private set; }

		public bool Active { get; private set; }

		public bool Activable { get; set; }
		/// <summary>
		/// Parent control of this control.
		/// </summary>
		protected UIControl BaseControl { get; private set; }
		/// <summary>
		/// Graphics device associated with this control.
		/// </summary>
		protected GraphicsDevice GraphicsDevice { get; private set; }
		/// <summary>
		/// Sprite batch associated with this control.
		/// </summary>
		protected SpriteBatch SpriteBatch { get; private set; }

		protected PrimetiveDrawHelper PrimetiveDarwHelper { get; private set; }
		/// <summary>
		/// Returns a value indicating whether control has almost zero-sized bounding box.
		/// </summary>
		protected bool IsNullSize { get { return Math.Abs(Width) < float.Epsilon || Math.Abs(Height) < float.Epsilon; } }

		private KeyboardState lastKeyboardState;

		/// <summary>
		/// Initializes a new UI control.
		/// </summary>
		/// <param name="baseControl">Parent of this control.</param>
		/// <param name="device">Graphics device associated with this control.</param>
		public UIControl(UIControl baseControl, GraphicsDevice device)
		{
			Margin = 0;
			Controls = new List<UIControl>();
			BaseControl = baseControl;
			GraphicsDevice = device;
			SpriteBatch = new SpriteBatch(GraphicsDevice);
			PrimetiveDarwHelper = new PrimetiveDrawHelper(GraphicsDevice);

			if (BaseControl != null)
				BaseControl.Controls.Add(this);
		}

		private void ActivateNext(ActivationDirection activationDirection, int sender)
		{
			Active = false;
			switch (activationDirection)
			{
				case ActivationDirection.Right:
					if (sender < Controls.Count - 1)
					{
						Controls[sender + 1].Active = true;
						return;
					}
					break;
				case ActivationDirection.Down:
					if (Controls.Count > 0)
					{
						Controls[0].Active = true;
						return;
					}
					break;
				default:
					break;
			}
			if (BaseControl != null)
			{
				BaseControl.ActivateNext(ActivationDirection.Right, BaseControl.Controls.IndexOf(this));
				return;
			}
			Active = true;
		}

		private void Deactivate(bool down)
		{
			if (!down)
			{
				if (BaseControl == null)
				{
					Deactivate(true);
					return;
				}
				BaseControl.Deactivate(false);
				return;
			}
			Active = false;
			foreach (var c in Controls)
			{
				c.Deactivate(true);
			}
		}

		public void Activate()
		{
			Deactivate(false);
			Active = true;
		}

		protected virtual void OnKeyDown(Keys key)
		{
			if (key == Keys.Tab)
			{
				ActivateNext(ActivationDirection.Down, 0);
			}
		}

		protected virtual void OnKeyUp(Keys key)
		{

		}

		/// <summary>
		/// Draws control using associated <see cref="GraphicsDevice"/>.
		/// </summary>
		/// <param name="time">Time elapsed from last draw invocation.</param>
		public virtual void Draw(GameTime time)
		{
			if (IsNullSize)
				return;
			for (int i = 0; i < Controls.Count; i++)
			{
				Controls[i].Draw(time);
			}
		}
		/// <summary>
		/// Updates control state.
		/// </summary>
		/// <param name="time">Time elapsed from last update invocation.</param>
		public virtual void Update(GameTime time)
		{
			for (int i = 0; i < Controls.Count; i++)
			{
				Controls[i].Update(time);
			}

			KeyboardState keyboardState = Keyboard.GetState();
			if (Active)
			{

				var downedKeys = from k in keyboardState.GetPressedKeys()
								 where !lastKeyboardState.GetPressedKeys().Contains(k)
								 select k;
				foreach (var key in downedKeys)
				{
					OnKeyDown(key);
				}

				var uppedKeys = from k in lastKeyboardState.GetPressedKeys()
								where !keyboardState.GetPressedKeys().Contains(k)
								select k;
				foreach (var key in uppedKeys)
				{
					OnKeyUp(key);
				}
			}
			lastKeyboardState = keyboardState;
		}
	}
}
