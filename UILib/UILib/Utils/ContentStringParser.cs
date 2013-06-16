using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace UILib.Utils
{
	internal class ContentStringParser<T> : StringParser<T>
	{
		protected ContentManager ContentManager { get; private set; }
		public ContentStringParser(ContentManager content)
		{
			ContentManager = content;
		}
		public override object ParseString(string value)
		{
			return ContentManager.Load<T>(value);
		}
	}
}
