using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageParsing
{
    public class PackageIdAttribute : Attribute
    {
        public PackageIdAttribute(PackageId id)
        {
            Id = id;
        }

        public PackageId Id { get; private set; }
    }
}
