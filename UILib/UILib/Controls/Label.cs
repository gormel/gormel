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
	public enum HorisontalAlligment
	{
		Left = 0,
		Center = 1,
		Right = 2
	}

	public enum VerticalAlligment
	{
		Top = 0,
		Center = 1,
		Bottom = 2
	}

	public class Label : Panel
	{
		public SpriteFont TextFont { get; set; }
		public string Text { get; set; }
		public string[] Lines
		{
			get { return Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None); }
		}
		public Color TextColor { get; set; }
		public HorisontalAlligment HorisontalTextAlligment { get; set; }
		public VerticalAlligment VerticalTextAlligment { get; set; }
		public bool AutoTranslit { get; set; }

		protected int TopOffset { get; set; }
		protected int LeftOffset { get; set; }
		protected IEnumerable<string> CuttedLines
		{
			get
			{
				int index = 0;

				foreach (var line in Lines)
				{
					int lineOffset = LeftOffset;
					do
					{
						var l = line.Substring(lineOffset).Aggregate("", (s, c) =>
							s + ((TextFont.MeasureString(s + c).X < Width - textIndent * 2) ? c.ToString() : ""));
						lineOffset += Math.Max(l.Length, 1) + (!AutoTranslit ? line.Length : 0);

						if (index++ < TopOffset)
							continue;

						yield return l;
					} while (lineOffset < line.Length);
				}
			}
		}
		
		private float textIndent = 5;

		public Label(UIControl baseConrol, GraphicsDevice device)
			: base(baseConrol, device)
		{
			Text = "";
			TextColor = Color.LightGreen;
			HorisontalTextAlligment = HorisontalAlligment.Left;
			VerticalTextAlligment = VerticalAlligment.Top;
			AutoTranslit = true;
		}
		
		protected override void DrawBody(GameTime time)
		{
			base.DrawBody(time);
			if (Text.Length == 0)
				return;

			SpriteBatch.Begin();

			float offsetY = Math.Max((Height - 
				CuttedLines.Count() * TextFont.LineSpacing) / 2 * 
							(int)VerticalTextAlligment, 0);

			foreach (var line in CuttedLines)
			{
				if (offsetY + TextFont.MeasureString(line).Y >= Height - textIndent * 2)
					break;

				var offsetX = (Width - textIndent * 2 - TextFont.MeasureString(line).X) / 2 * 
								(int)HorisontalTextAlligment;
				SpriteBatch.DrawString(TextFont, line,
					new Vector2(X + textIndent + offsetX, Y + textIndent + offsetY), TextColor);
				offsetY += Math.Max(TextFont.MeasureString(line).Y ,TextFont.LineSpacing);
			}

			SpriteBatch.End();
		}

		public override void Update(GameTime time)
		{
			base.Update(time);
		}
	}
}
