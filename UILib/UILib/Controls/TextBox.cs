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
	public class TextBox : Label
	{
		public int Cursor { get { return Lines.Take(CursorRow).Sum(s => s.Length + 
			Environment.NewLine.Length) + CursorCol; } }
		private TimeSpan typeCooldown = TimeSpan.FromMilliseconds(200);
		private TimeSpan typeTimeSpend = TimeSpan.Zero;
		private Keys typeingKey = Keys.None;

		private int CursorCol { get; set; }
		private int CursorRow { get; set; }
		private float CursorHeight { get { return TextFont.LineSpacing; } }
		
		public TextBox(UIControl baseControl, GraphicsDevice device)
			: base(baseControl, device)
		{
			Activable = true;
		}

		private void Type(Keys value)
		{
			string add = value.ToString();
			if (value == Keys.Enter)
				add = Environment.NewLine;

			var splitted = add.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
			int cursorDy = splitted.Length - 1;
			int cursorDx = add.Length;
			if (splitted.Length > 1)
				cursorDx = splitted.Last().Length - CursorCol;
			
			Text = Text.Insert(Cursor, add);
			CursorCol += cursorDx;
			CursorRow += cursorDy;
		}

		protected override void OnKeyDown(Keys key)
		{
			//base.OnKeyDown(key);
			typeingKey = key;
			if (Active)
				Type(key);
			typeTimeSpend = TimeSpan.Zero;
		}

		protected override void OnKeyUp(Keys key)
		{
			//base.OnKeyUp(key);
			typeingKey = Keys.None;
		}

		public override void Update(GameTime time)
		{
			base.Update(time);
			if (!Active)
				return;
			if (typeingKey != Keys.None)
			{
				//сильно не ругайте, так прикольно ^^
				if ((typeTimeSpend += time.ElapsedGameTime) < typeCooldown) { }
				else
				{
					typeTimeSpend = TimeSpan.Zero;
					Type(typeingKey);
				}
			}
		}

		protected override void DrawBody(GameTime time)
		{
			base.DrawBody(time);

//			var debugData = string.Format("Col:{0}, Row:{1}, Cur:{2}", CursorCol, CursorRow, Cursor);
//			SpriteBatch.Begin();
//			SpriteBatch.DrawString(TextFont, debugData, Vector2.Zero, Color.White);
//			SpriteBatch.End();
		}
	}
}
