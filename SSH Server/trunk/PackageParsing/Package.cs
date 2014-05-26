using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageParsing
{
    public class Package
    {
        [Data(-1)]
        public PackageId Id 
        { 
            get
            {
                return ((PackageIdAttribute)GetType().GetCustomAttributes(typeof(PackageIdAttribute), true).First()).Id;
            }
        }

        private IEnumerable<byte[]> RawData
        {
            get
            {
                var markedFileds = from f in GetType().GetFields()
                                   where f.IsDefined(typeof(DataAttribute), false)
                                   orderby ((DataAttribute)f.GetCustomAttributes(typeof(DataAttribute), false).First()).Position
                                   select f;
                foreach (var f in markedFileds)
                {
                    var value = f.GetValue(this);
                    yield return DataConverter.Convert(value);
                }
            }
        }

        public byte[] Data
        {
            get
            {
                return RawData.SelectMany(a => a).ToArray();
            }
        }
    }
}
