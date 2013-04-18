using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packages
{
	public static class ByteConverter
	{
		public static byte[] ToBytes(object o)
		{
			if (o.GetType() == typeof(int))
			{
				return BitConverter.GetBytes((int)o);
			}

			if (o.GetType() == typeof(bool))
			{
				return BitConverter.GetBytes((bool)o);
			}

			if (o.GetType() == typeof(long))
			{
				return BitConverter.GetBytes((long)o);
			}

			if (o.GetType() == typeof(string))
			{
				int len = Encoding.UTF8.GetByteCount((string)o);
				List<byte> data = new List<byte>();
				data.AddRange(ByteConverter.ToBytes(len));
				data.AddRange(Encoding.UTF8.GetBytes((string)o));
				return data.ToArray();
			}

			if (o.GetType() == typeof(double))
			{
				return BitConverter.GetBytes((double)o);
			}

			if (o.GetType().IsEnum)
			{
				return BitConverter.GetBytes((int)o);
			}

			if (typeof(System.Collections.IList).IsAssignableFrom(o.GetType()))
			{
				var collection = (System.Collections.ICollection)o;
				List<byte[]> objects = new List<byte[]>();
				foreach (var item in collection)
				{
					objects.Add(ToBytes(item));
				}

				byte[] size = ToBytes(objects.Count);
				byte[] data = objects.SelectMany(b => b).ToArray();
				return size.Concat(data).ToArray();
			}

			throw new ArgumentException("Wrong argument type.");
		}

		public static byte[] ToBytes(Package package)
		{
			List<byte> data = new List<byte>();
			var props = package.GetType().GetProperties();
			var result = props.Where(pi => pi.GetCustomAttributes(typeof(DataAttribute), false).Length > 0)
				.Where(p => ((DataAttribute)p.GetCustomAttributes(typeof(DataAttribute), false).First()).Write)
				.OrderBy(pi => ((DataAttribute)pi.GetCustomAttributes(typeof(DataAttribute), false)[0]).Number);

			foreach (var p in result)
			{
				data.AddRange(ByteConverter.ToBytes(p.GetValue(package, null)));
			}

			return data.ToArray();
		}

		public static object FromBytes(Type type, byte[] data, int startIndex, out int bytesReaded)
		{

			if (type == typeof(int))
			{
				bytesReaded = sizeof(int);
				return BitConverter.ToInt32(data, startIndex);
			}

			if (type == typeof(bool))
			{
				bytesReaded = sizeof(bool);
				return BitConverter.ToBoolean(data, startIndex);
			}

			if (type == typeof(long))
			{
				bytesReaded = sizeof(long);
				return BitConverter.ToInt64(data, startIndex);
			}

			if (type == typeof(string))
			{
				int len = (int)ByteConverter.FromBytes(typeof(int), data, startIndex, out bytesReaded);
				bytesReaded += len;
				return Encoding.UTF8.GetString(data, startIndex + 4, len);
			}

			if (type == typeof(double))
			{
				bytesReaded = sizeof(double);
				return BitConverter.ToDouble(data, startIndex);
			}

			if (type.IsEnum)
			{
				return FromBytes(typeof(int), data, startIndex, out bytesReaded);
			}

			if (typeof(System.Collections.IList).IsAssignableFrom(type))
			{
				var generics = type.GenericTypeArguments;
				if (generics.Length == 1)
				{
					var itemType = generics[0];
					var rv = (System.Collections.IList)Activator.CreateInstance(type);

					int readed = 0;
					int length = (int)FromBytes(typeof(int), data, startIndex, out readed);

					int start = startIndex + readed;
					for (int i = 0; i < length; i++)
					{
						rv.Add(FromBytes(itemType, data, start, out readed));
						start += readed;
					}
					bytesReaded = start - startIndex;
					return rv;
				}
			}

			throw new ArgumentException("Wrong type.");
		}

		public static Package FromBytes(byte[] data)
		{
			int readed = 0;
			PackageType id = (PackageType)FromBytes(typeof(PackageType), data, 0, out readed);
			var type = typeof(Package).Assembly.GetTypes()
				.Where(t => t.GetCustomAttributes(typeof(PackageAttribute), false).Length > 0)
				.Where(t => ((PackageAttribute)t.GetCustomAttributes(typeof(PackageAttribute), false)[0]).Type == id)
				.First();

			Package pack = (Package)Activator.CreateInstance(type);

			var props = type.GetProperties()
				.Where(p => p.GetCustomAttributes(typeof(DataAttribute), false).Length > 0)
				.Where(p => ((DataAttribute)p.GetCustomAttributes(typeof(DataAttribute), false).First()).Read)
				.OrderBy(p => ((DataAttribute)p.GetCustomAttributes(typeof(DataAttribute), false).First()).Number);

			int startIndex = readed;
			foreach (var p in props)
			{
				int add = 0;
				object value = FromBytes(p.PropertyType, data, startIndex, out add);
				startIndex += add;

				p.SetValue(pack, value);
			}

			return pack;
		}
	}
}
