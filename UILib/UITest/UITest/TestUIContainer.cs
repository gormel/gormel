using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UILib.Base;
using UILib.Controls;

namespace UITest
{
	public class TestUIContainer : UIContainer
	{
		private bool dragMode = false;
		private Position Top;
		private MouseState lastMouseState;

		public TestUIContainer(Object controller, GraphicsDevice device, ContentManager content)
			: base(controller, device)
		{/*
			Top = new Position(null, GraphicsDevice);
			Top.X = 10;
			Top.Y = 10;
			Top.Width = 500;
			Top.Height = 300;

			Panel panel1 = new Panel(Top, GraphicsDevice);

			RowItem row1 = new RowItem(panel1, GraphicsDevice);
			row1.Index = 0;
			row1.ProcentHeight = 0.5f;

			ColItem col1 = new ColItem(row1, GraphicsDevice);
			col1.Index = 0;

			Button button1 = new Button(col1, GraphicsDevice);
			button1.Margin = 3;
			button1.TextFont = content.Load<SpriteFont>("LabelFont");
			button1.BackgroundImage = content.Load<Texture2D>("applelogo");
			button1.Text = "Button 1";
			button1.TextColor = Color.DarkGreen;
			button1.MouseDown += button1_MouseDown;
			button1.MouseUp += button1_MouseUp;

			RowItem row2 = new RowItem(panel1, GraphicsDevice);
			row2.Index = 1;
			//row2.ProcentHeight = 0.2f;

			ColItem col2 = new ColItem(row2, GraphicsDevice);
			col2.Index = 0;

			Button button2 = new Button(col2, GraphicsDevice);
			button2.Margin = 3;
			button2.TextFont = content.Load<SpriteFont>("LabelFont");
			button2.Text = "Button 2";
			button2.MouseUp += button2_MouseDown;

			ColItem col3 = new ColItem(row2, GraphicsDevice);
			col3.Index = 1;

			Button button3 = new Button(col3, GraphicsDevice);
			button3.Margin = 3;
			button3.TextFont = content.Load<SpriteFont>("LabelFont");
			button3.Text = "Button 3";
			button3.MouseUp += button3_MouseDown;

			RowItem row3 = new RowItem(panel1, GraphicsDevice);
			row3.Index = 2;
			row3.ProcentHeight = 0.3f;

			Label label1 = new Label(row3, GraphicsDevice);
			label1.Margin = 3;
			label1.TextFont = content.Load<SpriteFont>("LabelFont");
			label1.Text = "Label 1";

			BaseControl = Top;*/
		}

		void button1_MouseUp(object sender, EventArgs e)
		{
			dragMode = false;
		}

		void button1_MouseDown(object sender, EventArgs e)
		{
			dragMode = true;
		}

		void button3_MouseDown(object sender, EventArgs e)
		{
			Top.Width -= 10;
			Top.Height -= 10;
		}

		void button2_MouseDown(object sender, EventArgs e)
		{
			Top.Width += 10;
			Top.Height += 10;
		}

		public override void Update(GameTime time)
		{
			MouseState mouseState = Mouse.GetState();

			if (dragMode)
			{
				var dx = mouseState.X - lastMouseState.X;
				var dy = mouseState.Y - lastMouseState.Y;
				Top.X += dx;
				Top.Y += dy;
			}

			lastMouseState = mouseState;
			base.Update(time);
		}
	}
}
