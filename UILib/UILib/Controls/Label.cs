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
		public IEnumerable<string> Lines
		{
			get
			{
				if (Text.Length == 0)
					yield break;

				int t = 0;
				for (int i = 0; i < Text.Length; i++)
				{
					var substr = Text.Substring(t, i - t);
					if (substr.EndsWith(Environment.NewLine) || 
						TextFont.MeasureString(substr).X > Width - textIndent * 2)
					{
						yield return substr.Substring(0, substr.Length - 1);
						t = --i;
						if (!AutoTranslit)
						{
							var enter = Text.IndexOf(Environment.NewLine, i);
							i = t = enter < 0 ? Text.Length : enter + Environment.NewLine.Length;
						}
					}
				}
				yield return Text.Substring(t);
			}
		}
		public Color TextColor { get; set; }
		public HorisontalTextAlligment HorisontalTextAlligment { get; set; }
		public VerticalTextAlligment VerticalTextAlligment { get; set; }
		public bool AutoTranslit { get; set; }

		protected int TopOffset { get; set; }
		protected int LeftOffset { get; set; }
		protected IEnumerable<string> VisibleLines 
		{ 
			get { return Lines.Where((s, i) => Lines.Take(i + 1).Sum(s2 => TextFont.MeasureString(s2).Y) < Height - textIndent * 2); } 
		}

		private float textIndent = 5;

		public Label(UIControl baseConrol, GraphicsDevice device)
			: base(baseConrol, device)
		{
			Text = "";
			TextColor = Color.LightGreen;
			HorisontalTextAlligment = HorisontalTextAlligment.Left;
			VerticalTextAlligment = VerticalTextAlligment.Top;
			AutoTranslit = false;
		}
		
		protected override void DrawBody(GameTime time)
		{
			base.DrawBody(time);
			if (Text.Length == 0)
				return;

			var visibleText = new String(VisibleLines.Skip(TopOffset).SelectMany(s => s.Substring(LeftOffset) + Environment.NewLine).ToArray());
			if (visibleText.Length > 0)
				visibleText = visibleText.Remove(visibleText.Length - Environment.NewLine.Length);
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
			base.Update(time);
		}
	}
}
