using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CopyingModel
{
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Land land;
		LandView lView;

		//SpriteAnimation anim;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			base.Initialize();
			ContentManager.Initialize(Content);

			land = new Land(20, 20);
			lView = new LandView(land, spriteBatch, land.Width * 30, land.Height * 30);

			graphics.PreferredBackBufferWidth = (int)lView.Width;
			graphics.PreferredBackBufferHeight = (int)lView.Height;
			graphics.ApplyChanges();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			land.Update(gameTime);
			lView.Update(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			lView.Draw(gameTime);

			base.Draw(gameTime);
		}
	}
}
