using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Xna.Framework;

namespace UILib.Utils
{
	internal class ColorStringParser : StringParser<Color>
	{
		public override object ParseString(string value)
		{
			var member = typeof(Color).GetMember(value).FirstOrDefault();
			
			var field = typeof(Color).GetField(value, BindingFlags.Public | BindingFlags.Static);
			if (member != null)
				if (member.MemberType == MemberTypes.Property)
					return ((PropertyInfo)member).GetValue(null, null);
				else if (member.MemberType == MemberTypes.Field)
					return ((FieldInfo)member).GetValue(null);
			if (value[0] == '#')
				return new Color(int.Parse(value.Substring(1, 2), NumberStyles.HexNumber), int.Parse(value.Substring(3, 2), NumberStyles.HexNumber),
					int.Parse(value.Substring(5, 2), NumberStyles.HexNumber), int.Parse(value.Substring(7, 2), NumberStyles.HexNumber));
			throw new Exception("Wrong color format.");
		}
	}
}
