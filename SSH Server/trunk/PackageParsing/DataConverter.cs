using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageParsing
{
    public class DataConverter
    {
        public static byte[] Convert(object obj)
        {
            return null;
        }

        public static object Convert(byte[] data, int offset, out int length, Type type)
        {
            length = 0;
            return null;
        }

        public static Package GetPackage(byte[] data)
        {
            PackageId id = (PackageId)BitConverter.ToInt32(data, 0);
            var packageTypes = from a in AppDomain.CurrentDomain.GetAssemblies()
                               from t in a.GetTypes()
                               where t.IsDefined(typeof(PackageIdAttribute), true)
                               select t;
        }
    }
}
