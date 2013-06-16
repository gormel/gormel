using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UILib.Utils
{
	public class PrimetiveDrawHelper
	{
		private GraphicsDevice device;
		private BasicEffect effect;
		public PrimetiveDrawHelper(GraphicsDevice graphics)
		{
			device = graphics;
			effect = new BasicEffect(device);
			effect.Projection = Matrix.CreateOrthographicOffCenter(0, device.Viewport.Width, device.Viewport.Height, 0, 0, 100);
			effect.VertexColorEnabled = true;
			
		}

		public void DrawLine(Vector2 start, Vector2 end, Color color, float width = 1)
		{
			var t = end - start;
			t.Normalize();
			t *= width / 2;

			var rotate90 = Matrix.CreateRotationZ((float)(Math.PI / 2));
			var unrotate90 = Matrix.CreateRotationZ(-(float)(Math.PI / 2));

			var corner1 = start - t + Vector2.Transform(t, rotate90);
			var corner2 = start - t + Vector2.Transform(t, unrotate90);
			var corner3 = end + t + Vector2.Transform(t, rotate90);
			var corner4 = end + t + Vector2.Transform(t, unrotate90);

			DrawPoly(color, true, corner1, corner2, corner3, corner4);
		}

		public void DrawBox(Vector2 position, Vector2 bounds, Color fillColor, Color borderColor, float borderWidth)
		{
			var v1 = position;
			var v2 = position;
			v2.X += bounds.X;
			var v3 = v2;
			v3.Y += bounds.Y;
			var v4 = v1;
			v4.Y += bounds.Y;

			DrawPoly(fillColor, true, v1, v2, v4, v3);
			DrawLine(v1, v2, borderColor, borderWidth);
			DrawLine(v2, v3, borderColor, borderWidth);
			DrawLine(v3, v4, borderColor, borderWidth);
			DrawLine(v4, v1, borderColor, borderWidth);
		}

		private void DrawPoly(Color color, bool fill, params Vector2[] vertices)
		{
			VertexPositionColor[] verts = new VertexPositionColor[vertices.Length];
			for (int i = 0; i < verts.Length; i++)
			{
				verts[i].Position = new Vector3(vertices[i], 0);
				verts[i].Color = color;
			}

			RasterizerState rastState = new RasterizerState();
			rastState.FillMode = fill ? FillMode.Solid : FillMode.WireFrame;
			rastState.CullMode = CullMode.None;
			device.RasterizerState = rastState;

			effect.Alpha = (float)color.A / 255;
			effect.GraphicsDevice.BlendState = BlendState.AlphaBlend;
			foreach (var pass in effect.CurrentTechnique.Passes)
			{
				pass.Apply();
				device.DrawUserPrimitives(PrimitiveType.TriangleStrip, verts, 0, verts.Length - 2);
			}
		}
	}
}
