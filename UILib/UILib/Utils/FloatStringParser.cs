﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace UILib.Utils
{
	internal class FloatStringParser : StringParser<float>
	{
		public override object ParseString(string value)
		{
			return float.Parse(value, CultureInfo.InvariantCulture);
		}
	}
}
