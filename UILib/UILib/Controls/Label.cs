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
		public IEnumerable<string> Lines
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
				var copyText = Text;
				float countedX = 0;
				for (int i = 0; i < copyText.Length; i++)
				{
					if (copyText.IndexOf(Environment.NewLine, i) == i)
					{
						countedX = 0;
						continue;
					}
					var addX = TextFont.MeasureString(copyText[i].ToString()).X; 
					if (countedX + addX > Width - TextIndent * 2)
					{
						copyText = copyText.Insert(i, Environment.NewLine);
						i += Environment.NewLine.Length - 1;
						countedX = 0;
						continue;
					}
					countedX += addX;
				}
				return copyText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
			}
		}
		
		protected float TextIndent = 5;

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
			var cutted = CuttedLines.Skip(TopOffset).Select(s => s.Substring(LeftOffset));

			float offsetY = Math.Max((Height - 
				cutted.Count() * TextFont.LineSpacing - TextIndent * 2) / 2 * 
							(int)VerticalTextAlligment, 0);

			foreach (var line in cutted)
			{
				if (offsetY + TextFont.MeasureString(line).Y >= Height - TextIndent * 2)
					break;

				var offsetX = (Width - TextIndent * 2 - TextFont.MeasureString(line).X) / 2 * 
								(int)HorisontalTextAlligment;
				SpriteBatch.DrawString(TextFont, line,
					new Vector2(X + TextIndent + offsetX, Y + TextIndent + offsetY), TextColor);
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
