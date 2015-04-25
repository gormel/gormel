using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using TraceMeGL.Objects;

namespace TraceMeGL
{
	static class Program
	{
		static Holder<Sphere> sphere;
		static Holder<Sphere> another;
		static Vector3 vel = new Vector3(0, 0, 10); //UpS
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main()
		{
			using (var game = new GameWindow())
			{
				game.Load += (sender, e) =>
				{

					int buf = GL.GenBuffer();
					
					var cc = Color.CornflowerBlue;
					ObjectManager.Instance.Value.SetClearColor(new Vector4(cc.R / 255f, cc.G / 255f, cc.B / 255f, cc.A / 255f));

					sphere = ObjectManager.Instance.Value.CreateObject<Sphere>();
					sphere.Modify(s => new Sphere() { color = new Vector4(1, 0, 0, 1), position = new Vector3(0, 0, 100), radius = 10, reflection = 0.5f });

					another = ObjectManager.Instance.Value.CreateObject<Sphere>();
					another.Modify(s => new Sphere() { color = new Vector4(0, 1, 0, 1), position = new Vector3(5, 0, 100), radius = 10, reflection = 0 });

					another = ObjectManager.Instance.Value.CreateObject<Sphere>();
					another.Modify(s => new Sphere() { color = new Vector4(0, 0, 1, 1), position = new Vector3(-5, 0, 100), radius = 10, reflection = 0 });

					game.VSync = VSyncMode.On;
				};
 
				game.Resize += (sender, e) =>
				{
					GL.Viewport(0, 0, game.Width, game.Height);
					ObjectManager.Instance.Value.CalculateEye(game.Width, game.Height, (float)(Math.PI / 4));
				};
 
				game.UpdateFrame += (sender, e) =>
				{
					// add game logic, input handling
					if (game.Keyboard[Key.Escape])
					{
						game.Exit();
					}

					if (game.Keyboard[Key.Down])
					{
						sphere.Modify(s => new Sphere() { color = s.color, position = s.position - vel * (float)e.Time, radius = s.radius, reflection = s.reflection });
					}

					if (game.Keyboard[Key.Up])
					{
						sphere.Modify(s => new Sphere() { color = s.color, position = s.position + vel * (float)e.Time, radius = s.radius, reflection = s.reflection });
					}
				};
 
				game.RenderFrame += (sender, e) =>
				{
					// render graphics
					GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

					//GL.UseProgram(program.ID);
					GL.UseProgram(ObjectManager.Instance.Value.Program.ID);

					GL.EnableVertexAttribArray(0);
					GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, new float[] { -1, 1, 0, 
																										1, 1, 0,
																										1, -1, 0,
																										1, -1, 0, 
																										-1, -1, 0,
																										-1, 1, 0});

					ObjectManager.Instance.Value.FlushObjects();

					GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

					game.SwapBuffers();
				};
 
				// Run the game at 60 updates per second
				game.Run(60.0);
			}
		}
	}
}
