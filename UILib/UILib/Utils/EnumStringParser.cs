using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UILib.Utils
{
	internal class EnumStringParser<T> : StringParser<T>
	{
		static EnumStringParser()
		{
			if (!typeof(T).IsEnum)
				throw new Exception("T must be enum.");
		}

		public override object ParseString(string value)
		{
			return Enum.Parse(typeof(T), value);
		}
	}
}
