using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageParsing
{
    public class DataAttribute : Attribute
    {
        public DataAttribute(int position)
        {
            Position = position;
        }

        public int Position { get; private set; }
    }
}
