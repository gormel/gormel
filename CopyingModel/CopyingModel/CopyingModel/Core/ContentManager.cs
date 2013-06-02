using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CopyingModel
{
	public static class ContentManager
	{
		public static void Initialize(Microsoft.Xna.Framework.Content.ContentManager content)
		{
			PartTexture = content.Load<Texture2D>("Part");
			CursorTexture = content.Load<Texture2D>("Cursor");
			DebugFont = content.Load<SpriteFont>("Debug");
			MonsterAnimations = content.Load<Texture2D>("Monster");
		}
		public static Texture2D PartTexture { get; private set; }
		public static Texture2D CursorTexture { get; private set; }
		public static SpriteFont DebugFont { get; private set; }
		public static Texture2D MonsterAnimations { get; private set; }
	}
}
