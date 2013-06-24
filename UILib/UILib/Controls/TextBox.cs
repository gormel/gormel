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
		public int Cursor { get; set; }
		private TimeSpan typeCooldown = TimeSpan.FromMilliseconds(100);
		private TimeSpan typeTimeSpend = TimeSpan.Zero;
		private Keys typeingKey = Keys.None;


		public TextBox(UIControl baseControl, GraphicsDevice device)
			: base(baseControl, device)
		{
			
		}

		protected override void OnKeyDown(Keys key)
		{
			base.OnKeyDown(key);
			typeingKey = key;
		}

		protected override void OnKeyUp(Keys key)
		{
			base.OnKeyUp(key);
			typeingKey = Keys.None;
		}

		public override void Update(GameTime time)
		{//сильно не ругайте, так прикольно ^^
			if ((typeTimeSpend += time.ElapsedGameTime) < typeCooldown) ;
			else
			{
				typeTimeSpend = TimeSpan.Zero;
				if (typeingKey != Keys.None)
				{
					string add = typeingKey.ToString();
					Text = Text.Insert(Cursor, add);
					Cursor += add.Length;
				}
			}
			base.Update(time);
		}
	}
}
