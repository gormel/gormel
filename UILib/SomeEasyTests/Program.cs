using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeEasyTests
{
	class Program
	{
		private static Random rand = new Random();
		static void Main(string[] args)
		{
			var a = "".Split(new[] { Environment.NewLine }, StringSplitOptions.None);
			Console.ReadKey(true);
		}
	}
}

