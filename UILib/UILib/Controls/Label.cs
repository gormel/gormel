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
	public enum HorisontalTextAlligment
	{
		Left = -1,
		Center = 0,
		Right = 1
	}

	public enum VerticalTextAlligment
	{
		Top = -1,
		Center = 0,
		Down = 1
	}

	public class Label : Panel
	{
		public SpriteFont TextFont { get; set; }
		public string Text { get; set; }
		public Color TextColor { get; set; }
		public HorisontalTextAlligment HorisontalTextAlligment { get; set; }
		public VerticalTextAlligment VerticalTextAlligment { get; set; }

		protected int Offset { get; set; }

		private float textIndent = 5;
		private string visibleText;

		public Label(UIControl baseConrol, GraphicsDevice device)
			: base(baseConrol, device)
		{
			Text = "";
			TextColor = Color.LightGreen;
			HorisontalTextAlligment = HorisontalTextAlligment.Left;
			VerticalTextAlligment = VerticalTextAlligment.Top;
			Offset = 0;
		}

		public override void Draw(GameTime time)
		{
			base.Draw(time);

			if (Text.Length == 0)
				return;

			var textBounds = TextFont.MeasureString(visibleText);
			var textPos = new Vector2(X, Y);
			textPos.X += (Width - textBounds.X) * ((int)HorisontalTextAlligment + 1) / 2 - textIndent * (int)HorisontalTextAlligment;
			textPos.Y += (Height - textBounds.Y) * ((int)VerticalTextAlligment + 1) / 2 - textIndent * (int)VerticalTextAlligment;

			SpriteBatch.Begin();
			SpriteBatch.DrawString(TextFont, visibleText, textPos, TextColor);
			SpriteBatch.End();
		}

		public override void Update(GameTime time)
		{
			if (Text.Length > 0)
			{
				visibleText = Text.Substring(Offset);
				for (int i = 0; i < visibleText.Length; i++)
				{
					var textBounds = TextFont.MeasureString(visibleText.Substring(0, i));
					if (textBounds.X >= Width - textIndent * 2)
					{
						visibleText = visibleText.Insert(--i, Environment.NewLine);
					}
				}

				int lastIndex = 0;
				while (TextFont.MeasureString(visibleText.Substring(0, lastIndex + Environment.NewLine.Length)).Y < Height - textIndent * 2 && lastIndex >= 0)
				{
					lastIndex = visibleText.IndexOf(Environment.NewLine, lastIndex + Environment.NewLine.Length);
				}
				if (lastIndex > -1)
					visibleText = visibleText.Substring(0, lastIndex);
			}
			base.Update(time);
		}
	}
}
