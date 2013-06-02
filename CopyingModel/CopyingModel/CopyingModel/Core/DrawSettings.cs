using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CopyingModel
{
	public class DrawSettings
	{
		public Texture2D Texture { get; set; }
		public Vector2 Position { get; set; }
		public Rectangle? SourceRectangle { get; set; }
		public Color Color { get; set; }
		public float Rotation { get; set; }
		public Vector2 Origin { get; set; }
		public Vector2 Scale { get; set; }
		public SpriteEffects Effects { get; set; }
		public float LayerDepth { get; set; }
		public DrawSettings()
		{
			Texture = null;
			Position = new Vector2();
			SourceRectangle = null;
			Color = Color.White;
			Rotation = 0;
			Origin = new Vector2();
			Scale = new Vector2(1, 1);
			Effects = SpriteEffects.None;
			LayerDepth = 0;
		}
	}
}
