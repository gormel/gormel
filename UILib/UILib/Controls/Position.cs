using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using UILib.Base;

namespace UILib.Controls
{
	public class Position : UIControl
	{
		private float x;
		public override float X
		{
			get
			{
				float visibleX = x;
				if (BaseControl != null)
					visibleX = Math.Max(x + BaseControl.X, BaseControl.X);
				return visibleX;
			}
			set
			{
				x = value;
			}
		}

		private float y;
		public override float Y
		{
			get
			{
				float visibleY = y;
				if (BaseControl != null)
					visibleY = Math.Max(y + BaseControl.Y, BaseControl.Y);
				return visibleY;
			}
			set
			{
				y = value;
			}
		}

		private float width;
		public override float Width
		{
			get
			{
				float visibleWidth = width;
				if (BaseControl != null)
					visibleWidth = Math.Min(width, BaseControl.X + BaseControl.Width - X);
				return Math.Max(0, visibleWidth);
			}
			set
			{
				width = value;
			}
		}

		private float height;
		public override float Height
		{
			get
			{
				float visibleHeight = height;
				if (BaseControl != null)
					visibleHeight = Math.Min(height, BaseControl.Y + BaseControl.Height - Y);
				return Math.Max(0, visibleHeight);
			}
			set
			{
				height = value;
			}
		}

		public Position(UIControl baseControl, GraphicsDevice device)
			: base(baseControl, device)
		{
			X = 0;
			Y = 0;
			Width = 60;
			Height = 20;
		}
	}
}
