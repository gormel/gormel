using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsServer
{
	public class FileClientRecordDAO : ClientRecordDAO
	{
		public string Path { get; private set; }
		public FileClientRecordDAO(string path)
		{
			Path = path;
		}

		private static object FromString(string value)
		{
			byte byteObj;
			if (byte.TryParse(value, out byteObj))
				return byteObj;

			short shortObj;
			if (short.TryParse(value, out shortObj))
				return shortObj;

			int intObj;
			if (int.TryParse(value, out intObj))
				return intObj;

			long longObj;
			if (long.TryParse(value, out longObj))
				return longObj;

			float floatObj;
			if (float.TryParse(value, out floatObj))
				return floatObj;

			double doubleObj;
			if (double.TryParse(value, out doubleObj))
				return doubleObj;

			return value;
		}

		public override ClientRecord GetClientRecord(string name)
		{
			string path = string.Format("{0}{1}{2}.xml",
							Path, System.IO.Path.DirectorySeparatorChar, name);
			if (!File.Exists(path))
				return null;
			FileRecord file = new FileRecord(path);
			ClientRecord record = new ClientRecord();
			foreach (var prop in record.GetType().GetProperties())
			{
				prop.SetValue(record, FromString(file[prop.Name]));
			}
			return record;
		}

		public override bool SaveClientRecord(ClientRecord record)
		{
			FileRecord file = new FileRecord();
			foreach (var prop in record.GetType().GetProperties())
			{
				file[prop.Name] = prop.GetValue(record).ToString();
			}
			file.Save(string.Format("{0}{1}{2}.xml",
					Path, System.IO.Path.DirectorySeparatorChar, record.Name));
			return true;
		}
	}
}
