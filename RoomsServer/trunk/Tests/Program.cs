using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Packages;
using RoomsServer;

namespace Tests
{
	public class Foo
	{
		public event EventHandler Action;
	}

	class Program
	{
		static void Main(string[] args)
		{
			JoinQueuePackage p = new JoinQueuePackage();
			p.QueueType = QueueType.Queue1v1;
			p.Teammates.Add("1");
			p.Teammates.Add("test");
			var data = ByteConverter.ToBytes(p);

			JoinQueuePackage pack = (JoinQueuePackage)ByteConverter.FromBytes(data);

			FileRecord record = new FileRecord();
			record["aaa"] = "bbb";
			record["hhhh"] = "lop";
			record.Save("record.xml");
			record["aaa"] = "ttt";
			record.Save("record.xml");

			record = new FileRecord("record.xml");

			Directory.CreateDirectory("Base");
			FileClientRecordDAO dao = new FileClientRecordDAO("Base");
			ClientRecord rec = new ClientRecord();
			foreach (var name in new[] { "foo", "bar", "bax", "player", "admin" })
			{
				rec.Name = name;
				rec.PasswordMD5 = "md5";
				rec.Rating = 1200;
				dao.SaveClientRecord(rec);
			}

			
			int a = 7;
		}
	}
}
