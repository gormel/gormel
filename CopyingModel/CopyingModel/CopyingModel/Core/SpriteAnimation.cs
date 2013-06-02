using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CopyingModel
{
	public class SpriteAnimation : Animation
	{
		private SpriteBatch spriteBatch;
		DrawSettings settings;

		private TimeSpan processeTime;
		private TimeSpan animationTime;

		private List<Texture2D> sprites = new List<Texture2D>();
		private int currentSprite;

		public SpriteAnimation(SpriteBatch spriteBatch, DrawSettings settings, 
			IEnumerable<Texture2D> sprites, TimeSpan animationTime)
		{
			this.sprites.AddRange(sprites);
			this.spriteBatch = spriteBatch;
			this.animationTime = animationTime;
			this.settings = settings;
		}

		public override void Start()
		{
			InProcess = true;
			currentSprite = 0;
			processeTime = TimeSpan.FromMilliseconds(0);
		}

		public override void Continue()
		{
			InProcess = true;
		}

		public override void Stop()
		{
			InProcess = false;
			FireAnimationEnd();
		}

		public override void Pause()
		{
			InProcess = false;
		}

		public override void Update(GameTime time)
		{
			if (processeTime >= animationTime)
				Stop();
			if (!InProcess)
				return;
			processeTime += time.ElapsedGameTime;
			var a = TimeSpan.FromMilliseconds(
				animationTime.TotalMilliseconds * (currentSprite + 1) / sprites.Count);
			if (processeTime >= a)
				currentSprite = Math.Min(sprites.Count - 1, currentSprite + 1);
		}

		public override void Draw(GameTime time)
		{
			settings.Texture = sprites[currentSprite];

			spriteBatch.Begin();

			spriteBatch.Draw(settings);

			spriteBatch.End();
		}

		/// <summary>
		/// режет текстуру на плитки
		/// </summary>
		/// <param name="tex">исходная текстура</param>
		/// <param name="width">количество плиток по горизонтали</param>
		/// <param name="height">количество плиток по вертикали</param>
		/// <param name="count">количество значимых плиток</param>
		/// <returns>плитки</returns>
		public static IEnumerable<Texture2D> Cut(Texture2D tex, int width, int height, int count, int start)
		{
			int i = 0;
			Color[] data = new Color[tex.Width * tex.Height];
			tex.GetData(data);
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					if (i++ < start)
						continue;
					if (i >= start + count + 1)
						yield break;
					int w = tex.Width / width;
					int h = tex.Height / height;
					Texture2D sprite = new Texture2D(tex.GraphicsDevice, w, h);
					Color[] newData = new Color[w * h];
					for (int x_ = 0; x_ < w;  x_++)
					{
						for (int y_ = 0; y_ < h;  y_++)
						{
							newData[x_ * h + y_] = data[(x * tex.Width / width + x_) * tex.Height + 
														(y * tex.Height / height + y_)];
						}
					}
					sprite.SetData(newData);
					yield return sprite;
				}
			}
		}
	}
}
