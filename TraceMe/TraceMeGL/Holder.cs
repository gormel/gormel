using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceMeGL
{
	public struct Holder<T> where T : struct
	{
		T[] data;
		int index;

		public Holder(T[] data, int index)
		{
			this.data = data;
			this.index = index;
		}

		//public void Delete()
		//{
		//	Array.Copy(data, index + 1, data, index, data.Length - index - 1);
		//}

		public void Modify(Func<T, T> func)
		{
			data[index] = func(data[index]);
		}
	}
}
