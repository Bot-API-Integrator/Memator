using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MematorSQL.Util
{
	public static class Extensions
	{
		public static byte[] FromBase64(this String base64)
		{
			return Convert.FromBase64String(base64);
		}

		public static Stream ToStream(this Byte[] bytes)
		{
			var stream = new MemoryStream(bytes);
			stream.Position = 0;
			return stream;
		}

		public static String ToLine(this List<Object> list)
		{
			String respond = "";
			foreach (String word in list)
				respond += word.ToString() + (word == list[list.Count - 1] ? "" : ", ");

			return respond;
		}

		public static T GetRandom<T>(this List<T> list)
		{
			return list[new Random().Next(0, list.Count)];
		}

		public static byte[] ToArray(this Bitmap bitmap)
		{
			MemoryStream memoryStream = new MemoryStream();
			bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
			return memoryStream.ToArray();
		}

		public static Stream ToStream(this Bitmap bitmap)
		{
			MemoryStream memoryStream = new MemoryStream();
			bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
			memoryStream.Position = 0;
			return memoryStream;
		}

		public static Stream ToStream(this string s)
		{
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}
	}
}
