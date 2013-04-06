using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Packages;

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

			Foo foo = new Foo();
			foo.Action += foo_Action;

			Type type = typeof(Foo);
			var fields = type.GetFields();
			var methods = type.GetMethods();
			var propertys = type.GetProperties();
			var events = type.GetEvents();
			var members = type.GetMembers();
			var metods = events[0].GetOtherMethods();
			var runtimeEvents = type.GetRuntimeEvents();
			var runtimeFields = type.GetRuntimeFields();
			var actionHandler = runtimeFields.First().GetValue(foo) as EventHandler;
			actionHandler.Invoke(foo, null);
			
			int a = 7;
		}

		static void foo_Action(object sender, EventArgs e)
		{
			int x = 6;
		}
	}
}
