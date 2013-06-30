using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UILib.Utils
{
	internal class BoolStringParser : StringParser<bool>
	{

		public override object ParseString(string value)
		{
			return bool.Parse(value);
		}
	}
}
