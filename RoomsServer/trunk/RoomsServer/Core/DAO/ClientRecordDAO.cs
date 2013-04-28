using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsServer
{
	public abstract class ClientRecordDAO
	{
		public abstract ClientRecord GetClientRecord(string name);

		public abstract bool SaveClientRecord(ClientRecord record);
	}
}
