using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UILib.Utils
{
	internal class StringStringParser : StringParser<string>
	{
		public override object ParseString(string value)
		{
			return value;
		}
	}
}
