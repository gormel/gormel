using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceMeGL
{
	public class StructDataAttribute : Attribute
	{
		public string FileName { get; set; }

		public string Text()
		{
			return File.ReadAllText(FileName);
		}
	}
}
