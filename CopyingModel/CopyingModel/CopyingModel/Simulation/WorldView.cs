using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CopyingModel
{
	public class WorldView : DrawObject<World, WorldStates>
	{
		public double Width { get; set; }
		public double Height { get; set; }

		private float partWidth;
		private float partHeight;

		private Vector2 partScale;

		public WorldView(World w, double width, double height, SpriteBatch spriteBatch)
			: base(w, spriteBatch)
		{
			Width = width;
			Height = height;
			w.StateChanged += w_StateChanged;

			partWidth = (float)Width / DrawigObject.Width;
			partHeight = (float)Height / DrawigObject.Height;

			partScale = new Vector2(partWidth / ContentManager.PartTexture.Width,
									partHeight / ContentManager.PartTexture.Height);
		}

		void w_StateChanged(object sender, StateChangedEventArgs<WorldStates> e)
		{
			switch (e.State)
			{
				case WorldStates.PartCreated:
					var part = (Part)e.Args[0];
					var pWhere = (Point)e.Args[1];
					var where = new Vector2(pWhere.X * partWidth, pWhere.Y * partHeight);
					AddScaleAnimation(Vector2.Zero, partScale, where, part);
					break;
				case WorldStates.PartMoved:
					var part1 = (Part)e.Args[0];
					var pFrom = (Point)e.Args[1];
					var pTo = (Point)e.Args[2];
					Vector2 from = new Vector2(pFrom.X * partWidth, pFrom.Y * partHeight);
					Vector2 to = new Vector2(pTo.X * partWidth, pTo.Y * partHeight);
					AddMoveAnimation(from, to, part1);
					break;
				case WorldStates.PartDeleted:
					var part2 = (Part)e.Args[0];
					var pWhere1 = (Point)e.Args[1];
					var where1 = new Vector2(pWhere1.X * partWidth, pWhere1.Y * partHeight);
					AddScaleAnimation(partScale, Vector2.Zero, where1, part2);
					Animations.RemoveAll(a => a is MovingAnimation && ((MovingAnimation)a).To == where1);
					break;
				default:
					break;
			}
		}

		public override void Draw(GameTime time)
		{
			base.Draw(time);
		}

		public override void Update(GameTime time)
		{
			base.Update(time);
		}

		private void AddMoveAnimation(Vector2 from, Vector2 to, Part part)
		{
			Animations.RemoveAll(a => a is MovingAnimation && ((MovingAnimation)a).To == from);

			Color color = new Color(part.Red, part.Green, part.Blue);
			DrawSettings settings = new DrawSettings();
			settings.Texture = ContentManager.PartTexture;
			settings.Color = color;
			settings.Scale = partScale;

			MovingAnimation newAnim =
				new MovingAnimation(spriteBatch, settings, from, to,
					TimeSpan.FromMilliseconds(DrawigObject.StepTimeout));

			newAnim.Start();
			Animations.Add(newAnim);
		}

		private void AddScaleAnimation(Vector2 from, Vector2 to, Vector2 where, Part part)
		{
			Color color = new Color(part.Red, part.Green, part.Blue);
			DrawSettings settings = new DrawSettings();
			settings.Texture = ContentManager.PartTexture;
			settings.Color = color;
			settings.Position = where + partScale / 2;
			settings.Origin = partScale / 2;

			Animation newAnim =
				new ScaleAnimation(spriteBatch, settings, from, to,
					TimeSpan.FromMilliseconds(DrawigObject.StepTimeout + 1));

			newAnim.AnimationEnd += (s, e) =>
				{
					Animations.Remove((Animation)s);
				};

			newAnim.Start();

			Animations.Add(newAnim);
		}
	}
}
