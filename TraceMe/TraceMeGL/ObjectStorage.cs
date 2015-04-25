using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceMeGL
{
	public class ObjectStorage<T> where T : struct
	{
		public T Data { get; set; }
	}
}
