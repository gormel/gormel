using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.CSharp;

namespace SomeEasyTests
{
	static class StringUtils
	{
		static Random rand = new Random();
		public static char GetRandomChar()
		{
			char c = ' ';
			do
			{
				c = (char)rand.Next(255);
			} while (char.IsControl(c));
			return c;
		}

		public static string GetRandomString(int length)
		{
			string s = "";
			for (int i = 0; i < length; i++)
			{
				s += GetRandomChar();
			}
			return s;
		}
	}

	class CodePart
	{
		private Random rand = new Random();
		public CodePart(int size)
		{
			for (int i = 0; i < size; i++)
			{
				Genes.Add(StringUtils.GetRandomString(rand.Next(50)));
			}
		}
		public List<string> Genes = new List<string>();

		public IEnumerable<Diagnostic> Errors
		{
			get
			{
				try
				{
					var tree = SyntaxTree.ParseText(Genes.Aggregate("", (s1, s2) => s1 + Environment.NewLine + s2));
					var comp = Compilation.Create("Program.cs").AddSyntaxTrees(tree).GetSemanticModel(tree);
					return tree.GetRoot().GetDiagnostics().Concat(comp.GetDiagnostics());
				}
				catch (Exception ex)
				{
					return null;
				}
			}
		}

		public override string ToString()
		{
			var loc = Errors.FirstOrDefault().Location.GetLineSpan(false);
			return "[" + loc.StartLinePosition.ToString() + "]";
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
		public CodePart CreateNew(CodePart a, CodePart b)
		{
			CodePart newItem = new CodePart(0);
			for (int i = 0; i < a.Genes.Count; i++)
			{
				string s = rand.NextDouble() < 0.5 ? a.Genes[i] : b.Genes[i];
				StringBuilder sb = new StringBuilder(s);
				for (int j = 0; j < s.Length * MutationRate; j++)
				{
					sb[rand.Next(s.Length)] = StringUtils.GetRandomChar();
				}
				s = sb.ToString();
				newItem.Genes.Add(s);
			}
			return newItem;
		}
	}

	class Program
	{
		private static Random rand = new Random();
		private static MutationFabric fabric = new MutationFabric();
		static void Main(string[] args)
		{
			int rectCount = 16;
			fabric.MutationRate = 0.5;
			List<CodePart> parts = new List<CodePart>();
			for (int i = 0; i < rectCount; i++)
			{
				parts.Add(new CodePart(12));
			}

			Console.WriteLine("\t:[0]");
			foreach (var part in parts)
			{
				Console.WriteLine(part.ToString());
			}
			int iteration = 1;
			while (true)
			{
				parts = Process(parts);
				if (iteration++ % 10 == 0)
				{
					Console.Clear();
					Console.WriteLine(string.Format("\t:[{0}]", iteration));
					foreach (var rect in parts)
					{
						Console.WriteLine(rect.ToString());
					}
				}
			}
			Console.ReadKey(true);
		}

		private static List<CodePart> Process(List<CodePart> input)
		{
			input.Sort((r1, r2) => new ErrorListComparer().Compare(r2.Errors, r1.Errors));
			var c = Math.Sqrt(input.Count);
			for (int i = 0; i < c; i++)
			{
				for (int j = 0; j < c; j++)
				{
					var newCode = fabric.CreateNew(input[i], input[j]);
					input.Add(newCode);
				}
			}

			return input.Take(input.Count / 2).ToList();
		}
	}

	class ErrorListComparer : Comparer<IEnumerable<Diagnostic>>
	{
		public override int Compare(IEnumerable<Diagnostic> x, IEnumerable<Diagnostic> y)
		{
			var xFirst = x.FirstOrDefault();
			var yFirst = y.FirstOrDefault();
			if (xFirst == null && yFirst == null)
				return 0;
			else if (xFirst == null)
				return 1;
			else if (yFirst == null)
				return -1;
			else
			{
				var xSpanStart = xFirst.Location.GetLineSpan(false).StartLinePosition;
				var ySpanStart = yFirst.Location.GetLineSpan(false).StartLinePosition;
				int result = xSpanStart.Line.CompareTo(ySpanStart.Line);
				if (result != 0)
					return result;
				else
					return xSpanStart.Character.CompareTo(ySpanStart.Character);
			}
		}
	}
}

