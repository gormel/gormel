using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UILib.Base;

namespace UILib.Controls
{
	public class Panel : UIControl
	{
		public Color Background { get; set; }
		public Color Border { get; set; }
		public float BorderWidth { get; set; }
		public Texture2D BackgroundImage { get; set; }
		public Panel(UIControl baseControl, GraphicsDevice device)
			: base(baseControl, device)
		{
			Background = new Color(0, 100, 0, 128);
			Border = Color.Green;
			BorderWidth = 2;
			BackgroundImage = null;
		}

		protected override void DrawBody(GameTime time)
		{
			base.DrawBody(time);
			if (BackgroundImage == null)
				PrimetiveDarwHelper.DrawBox(new Vector2(X, Y), new Vector2(Width, Height), Background, Border, BorderWidth);
			else
			{
				SpriteBatch.Begin();
				SpriteBatch.Draw(BackgroundImage, new Rectangle((int)X, (int)Y, (int)Width, (int)Height), 
					new Rectangle(0, 0, BackgroundImage.Width, BackgroundImage.Height), Color.White);
				SpriteBatch.End();
			}
		}
	}
}
