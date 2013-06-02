using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace CopyingModel
{
	public static class SpriteBatchExt
	{
		public static void Draw(this SpriteBatch sb, DrawSettings settings)
		{
			sb.Draw(settings.Texture, 
				settings.Position, settings.SourceRectangle, settings.Color, 
				settings.Rotation, settings.Origin, settings.Scale, settings.Effects, settings.LayerDepth);
		}
	}
}
