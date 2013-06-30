using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeEasyTests
{
	class Rectangle
	{
		public int Width { get; set; }
		public int Height { get; set; }
		public int Square { get { return Width * Height; } }

		public Rectangle(int width, int height)
		{
			Width = width;
			Height = height;
		}


		public override string ToString()
		{
			return string.Format("[{0}, {1}]:{2}", Width, Height, Square);
		}
	}

	class MutationFabric
	{
		public double MutationRate { get; set; }
		public MutationFabric()
		{
			MutationRate = 0;
		}
		private Random rand = new Random();
		public Rectangle CreateNew(Rectangle a, Rectangle b)
		{
			int w = rand.NextDouble() < 0.5 ? a.Width : b.Width;
			int h = rand.NextDouble() < 0.5 ? a.Height : b.Height;
			return new Rectangle((int)(w + w * rand.NextDouble() * MutationRate), 
								 (int)(h + h * rand.NextDouble() * MutationRate));
		}
	}

	class Program
	{
		private static Random rand = new Random();
		private static MutationFabric fabric = new MutationFabric();
		static void Main(string[] args)
		{
			int rectCount = 10;
			int maxWidth = 100;
			int maxHeight = 100;
			List<Rectangle> rects = new List<Rectangle>();
			for (int i = 0; i < rectCount; i++)
			{
				rects.Add(new Rectangle(rand.Next(maxWidth), rand.Next(maxHeight)));
			}

			foreach (var rect in rects)
			{
				Console.WriteLine(rect.ToString());
			}
			Console.WriteLine("----");

			while (true)
			{
				rects = Process(rects);
				foreach (var rect in rects)
				{
					Console.WriteLine(rect.ToString());
				}
				Console.WriteLine("----");
				Console.ReadKey(true);
			}
		}

		private static List<Rectangle> Process(List<Rectangle> input)
		{
			int c = input.Count;
			for (int i = 0; i < c; i++)
			{
				for (int j = 0; j < c; j++)
				{
					if (i != j)
						input.Add(fabric.CreateNew(input[i], input[j]));
				}
			}
			for (int i = 0; i < c; i++)
			{
				input.RemoveAt(0);
			}
			input.Sort((r1, r2) => r2.Square - r1.Square);
			return input.Take(c).ToList();
		}
	}
}
