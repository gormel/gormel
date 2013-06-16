using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace UILib.Utils
{
	internal class TextureStringParser : ContentStringParser<Texture2D>
	{
		public TextureStringParser(ContentManager content)
			: base(content) { }
	}
}
