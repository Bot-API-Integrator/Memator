using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MematorSQL.Util
{
	public static class IO
	{
		public static void EnsureExists(String path)
		{
			if (!Directory.Exists(path))
			{
				Logger.Debug($"[{path}] не существует. Создание...");
				Directory.CreateDirectory(path);
				return;
			}
			Logger.Debug($"[{path}] Существует.");
		}
	}
}
