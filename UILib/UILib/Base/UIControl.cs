using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UILib.Utils;

namespace UILib.Base
{
	public class UIControl
	{
		public virtual float X 
		{
			get { return BaseControl.X + Margin; }
			set {  }
		}

		public virtual float Y
		{
			get { return BaseControl.Y + Margin; }
			set {  }
		}

		public virtual float Width
		{
			get { return BaseControl.Width - Margin * 2; }
			set {  }
		}

		public virtual float Height
		{
			get { return BaseControl.Height - Margin * 2; }
			set {  }
		}

		public float Margin { get; set; }
		public List<UIControl> Controls { get; private set; }
		public string Name { get; set; }
		protected UIControl BaseControl { get; private set; }
		protected GraphicsDevice GraphicsDevice { get; private set; }
		protected SpriteBatch SpriteBatch { get; private set; }
		protected PrimetiveDrawHelper PrimetiveDarwHelper { get; private set; }
		protected bool IsNullSize { get { return Math.Abs(Width) < float.Epsilon || Math.Abs(Height) < float.Epsilon; } }

		public UIControl(UIControl baseControl, GraphicsDevice device)
		{
			Name = "";
			Margin = 0;
			Controls = new List<UIControl>();
			BaseControl = baseControl;
			GraphicsDevice = device;
			SpriteBatch = new SpriteBatch(GraphicsDevice);
			PrimetiveDarwHelper = new PrimetiveDrawHelper(GraphicsDevice);

			if (BaseControl != null)
				BaseControl.Controls.Add(this);
		}

		public virtual void Draw(GameTime time)
		{
			if (IsNullSize)
				return;
			for (int i = 0; i < Controls.Count; i++)
			{
				Controls[i].Draw(time);
			}
		}
		public virtual void Update(GameTime time)
		{
			for (int i = 0; i < Controls.Count; i++)
			{
				Controls[i].Update(time);
			}
		}
	}
}
