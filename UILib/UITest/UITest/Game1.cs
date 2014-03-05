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
using UILib.Base;
using UILib.Controls;
using UILib.Utils;

namespace UITest
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		PrimetiveDrawHelper primetiveDrawHelper;

		private bool dragMode;
		private MouseState lastMouseState;

		UIContainer testConteiner;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			var font = Content.Load<SpriteFont>("DebugFont");
			var x = font.MeasureString("ag");
			var y = font.MeasureString("a");
			var z = font.MeasureString("g");

			// TODO: Add your initialization logic here
			this.IsMouseVisible = true;
			testConteiner = new XmlUIContainer(this, GraphicsDevice, Content, "TestUI.xml");
			//testConteiner = new TestUIContainer(this, GraphicsDevice, Content);
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			primetiveDrawHelper = new PrimetiveDrawHelper(GraphicsDevice);
			// TODO: use this.Content to load your game content here
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		private void button1_MouseDown(object sender, EventArgs e)
		{
			dragMode = true;
		}

		private void button1_MouseUp(object sender, EventArgs e)
		{
			dragMode = false;
		}

		private void button2_MouseUp(object sender, EventArgs e)
		{
			testConteiner.GetControl<Position>("root").Width += 10;
			testConteiner.GetControl<Position>("root").Height += 10;
		}

		private void button3_MouseUp(object sender, EventArgs e)
		{
			testConteiner.GetControl<Position>("root").Width -= 10;
			testConteiner.GetControl<Position>("root").Height -= 10;
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			MouseState mouseState = Mouse.GetState();

			testConteiner.Update(gameTime);

			if (dragMode)
			{
				var dx = lastMouseState.X - mouseState.X;
				var dy = lastMouseState.Y - mouseState.Y;

				testConteiner.GetControl<Position>("root").X -= dx;
				testConteiner.GetControl<Position>("root").Y -= dy;
			}

			lastMouseState = mouseState;
			// TODO: Add your update logic here
			
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			testConteiner.Draw(gameTime);

			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}
	}
}
