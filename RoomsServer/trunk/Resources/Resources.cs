using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
	public static class ResourceManager
	{
		private static string[] FindFiles(string resourceName)
		{
			return Directory.GetFiles(".\\", string.Format("{0}.*", resourceName), SearchOption.AllDirectories);
		}

		public static byte[] GetResource(string resourceName)
		{
			return File.ReadAllBytes(FindFiles(resourceName).First());
		}

		public static string GetText(string resourceName)
		{
			return File.ReadAllText(FindFiles(resourceName).First());
		}

		public static Image GetImage(string resourceName)
		{
			return Image.FromFile(FindFiles(resourceName).First());
		}
	}
}
