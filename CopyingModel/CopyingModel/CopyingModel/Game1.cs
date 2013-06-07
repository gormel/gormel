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

		LandCursor cursor;
		LandCursorView cursorView;

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

			float cellWidth = 50;
			float cellHeight = 50;

			land = new Land(10, 10);
			lView = new LandView(land, spriteBatch, land.Width * cellWidth, land.Height * cellHeight);

			cursor = new LandCursor(cellWidth, cellHeight);
			cursor.StateChanged += cursor_StateChanged;
			cursorView = new LandCursorView(spriteBatch, cursor);

			graphics.PreferredBackBufferWidth = (int)lView.Width;
			graphics.PreferredBackBufferHeight = (int)lView.Height;
			graphics.ApplyChanges();
		}

		void cursor_StateChanged(object sender, StateChangedEventArgs<LandCursor.State> e)
		{
			switch (e.State)
			{
				case LandCursor.State.CursorDown:
					var dPos = (Point)e.Args[1];
					land.PlaceTower(dPos);
					break;
				case LandCursor.State.CursorUp:
					break;
				default:
					break;
			}
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

			cursor.Update(gameTime);
			cursorView.Update(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			lView.Draw(gameTime);

			cursorView.Draw(gameTime);

			base.Draw(gameTime);
		}
	}
}
