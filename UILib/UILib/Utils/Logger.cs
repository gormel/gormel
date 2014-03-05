using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UILib.Utils
{
	public class Logger
	{
		private static Logger gebugInstance = null;
		public static Logger Debug
		{
			get
			{
				if (gebugInstance == null)
					gebugInstance = new Logger(new StreamWriter("./debug.log"));
				return gebugInstance;
			}
		}

		StreamWriter writer;
		private Logger(StreamWriter output)
		{
			writer = output;
		}

		public void Write(string value)
		{
			writer.Write(value);
			writer.Flush();
		}

		public void WriteLine(string value)
		{
			writer.WriteLine(value);
			writer.Flush();
		}
	}
}
