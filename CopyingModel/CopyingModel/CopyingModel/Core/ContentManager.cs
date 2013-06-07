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
			DebugFont = content.Load<SpriteFont>("Debug");
			MonsterStandAnimation = SpriteAnimation.Cut(content.Load<Texture2D>("Monster"), 10, 10, 6, 10);
			CursorAnimation = SpriteAnimation.Cut(content.Load<Texture2D>("Cursor"), 10, 10, 2, 0);
			TowerStandAnimation = content.Load<Texture2D>("Tower");
			ShootTexture = content.Load<Texture2D>("Shoot");
		}
		public static Texture2D PartTexture { get; private set; }
		public static SpriteFont DebugFont { get; private set; }
		public static IEnumerable<Texture2D> MonsterStandAnimation { get; private set; }
		public static Texture2D TowerStandAnimation { get; private set; }
		public static IEnumerable<Texture2D> CursorAnimation { get; private set; }
		public static Texture2D ShootTexture { get; private set; }
	}
}
