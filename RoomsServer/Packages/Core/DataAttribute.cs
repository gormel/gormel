using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packages
{
	public class DataAttribute : Attribute
	{
		public int Number { get; private set; }
		public bool Read { get; private set; }
		public bool Write { get; private set; }
		public DataAttribute(int number, bool read, bool write)
		{
			Number = number;
			Read = read;
			Write = write;
		}
	}
}
