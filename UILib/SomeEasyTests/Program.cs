using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeEasyTests
{
	class Program
	{
		static void Main(string[] args)
		{
			for (int j = 0; j < 10; j++ )
				using (new TimeWatcher())
				{
					int k = 0;
					bool b = true;
					//int b = 1;
					for (int i = 0; i < 10000000; i++)
					{
						//int v = k + (b ? 1 : - 1);
						int v = (b ? k + 1 : k - 1);
					}
				}
			Console.ReadKey(true);
		}
	}
}
