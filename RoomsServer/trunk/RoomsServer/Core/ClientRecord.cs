using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsServer
{
	public class ClientRecord
	{
		public string Name { get; set; }
		public int Rating { get; set; }
		public string PasswordMD5 { get; set; }
	}
}
