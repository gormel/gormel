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
		static Sphere sphere;
		static Sphere another;
		static Vector3 vel = new Vector3(0, 0, 10); //UpS
		static Vector3 vel1 = new Vector3(10, 0, 0);
		static Vector3 vel2 = new Vector3(0, -10, 0);

		static Triangle trgl;
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
					sphere = new Sphere() { color = new Vector4(1, 0, 0, 1), position = new Vector3(0, 0, 100), radius = 10, reflection = 0.5f };

					another = ObjectManager.Instance.Value.CreateObject<Sphere>();
					another = new Sphere() { color = new Vector4(0, 1, 0, 1), position = new Vector3(5, 0, 100), radius = 10, reflection = 0.5f };

					another = ObjectManager.Instance.Value.CreateObject<Sphere>();
					another = new Sphere() { color = new Vector4(0, 0, 1, 1), position = new Vector3(-5, 0, 100), radius = 10, reflection = 0.5f };

					var chess = ObjectManager.Instance.Value.CreateObject<ChessPlane>();
					chess = new ChessPlane() { color1 = new Vector4(1, 1, 0, 0), color2 = new Vector4(0, 1, 1, 0),
														d = 5, reflection = 0.5f, sqHeight = 2f, sqWidth = 2f };

					trgl = ObjectManager.Instance.Value.CreateObject<Triangle>();
					trgl = new Triangle() { a = new Vector3(-5, 0, 50), b = new Vector3(5, 0, 50), c = new Vector3(0, 10, 50), ca = new Vector4(1, 0, 0, 0),
													  cb = new Vector4(0, 1, 0, 0), cc = new Vector4(0, 0, 1, 0)};

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
						sphere.position -= vel * (float)e.Time;
					}

					if (game.Keyboard[Key.Up])
					{
						sphere.position += vel * (float)e.Time;
					}

					if (game.Keyboard[Key.Left])
					{
						sphere.position -= vel1 * (float)e.Time;
					}

					if (game.Keyboard[Key.Right])
					{
						sphere.position += vel1 * (float)e.Time;
					}

					if (game.Keyboard[Key.R])
					{
						sphere.position += vel2 * (float)e.Time;
					}

					if (game.Keyboard[Key.F])
					{
						sphere.position -= vel2 * (float)e.Time;
					}

					var m = Matrix4.CreateTranslation(new Vector3(0, 0, -50)) * Matrix4.CreateRotationY((float)Math.PI / 2 * (float)e.Time) * Matrix4.CreateTranslation(new Vector3(0, 0, 50));
					trgl.a = Vector3.Transform(trgl.a, m);
					trgl.b = Vector3.Transform(trgl.b, m);
					trgl.c = Vector3.Transform(trgl.c, m);
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
