using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UILib.Utils
{
	public abstract class StringParser<T> : BaseStringParser
	{
		public T Parse(string value)
		{
			return (T)ParseString(value);
		}
	}
}
