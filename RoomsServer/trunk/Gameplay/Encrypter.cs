using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
	public static class Encrypter
	{
		public static string GetMD5(string from)
		{
			byte[] hash = MD5.Create().ComputeHash(Encoding.Unicode.GetBytes(from));
			var chars = from b in hash
						select b.ToString("x2");
			return new String(chars.SelectMany(s => s).ToArray());
		}
	}
}
