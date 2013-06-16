using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UILib.Utils
{
	internal class IntStringParser : StringParser<int>
	{
		public override object ParseString(string value)
		{
			return int.Parse(value);
		}
	}
}
