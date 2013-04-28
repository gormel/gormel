using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RoomsServer
{
	public class FileRecord
	{
		private Dictionary<string, string> records = new Dictionary<string, string>();

		public FileRecord()
		{

		}

		public FileRecord(string fileName)
		{
			foreach (var e in XElement.Load(fileName).Elements("pair"))
			{
				records.Add(e.FirstAttribute.Name.ToString(), e.FirstAttribute.Value);
			}
		}

		public string this[string key]
		{
			get
			{
				return records[key];
			}
			set
			{
				records[key] = value;
			}

		}
		public void Save(string fileName)
		{
			new XElement("pairs",
					from p in records
					select new XElement("pair", new XAttribute(p.Key, p.Value))).Save(fileName);
		}
	}
}
