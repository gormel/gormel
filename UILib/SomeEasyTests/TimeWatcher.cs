using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeEasyTests
{
	public class TimeWatcher : IDisposable
	{
		DateTime startTime;
		public TimeWatcher()
		{
			startTime = DateTime.Now;
		}

		#region Члены IDisposable

		public void Dispose()
		{
			TimeSpan result = DateTime.Now - startTime;
			Console.WriteLine(result);
		}

		#endregion
	}
}
