using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packages
{
    public abstract class Package
    {
		[Data(-1, false, true)]
		public PackageType ID 
		{ 
			get 
			{
				PackageAttribute attr = (PackageAttribute)GetType().
					GetCustomAttributes(typeof(PackageAttribute), false)[0];
				return attr.Type;
			} 
		}
    }
}
