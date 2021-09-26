using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MematorSQL.Tests
{
	public static class DbPerformanceTests
	{
		public static void StartTests(AppContext db)
		{
			TestGetFirstEntry(db);
			TestSearchingByName(db);
			TestSearchingByAlias(db);
		}

		private static void TestGetFirstEntry(AppContext db)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			Logger.Log("Попытка прочитать первый элемент БД...");
			var meme = db.Memes.First();
			Logger.Log($"Имя: {meme.Name}");
			Logger.Log($"Id: {meme.Id}");
			Logger.Log($"Картинок: {meme.Images.Count}");
			Logger.Log($"Имя файла первой картинки: {meme.Images[0].FileName}");
			Logger.Log($"Base64 первой картинки (50 символов): {meme.Images[0].Base64.Substring(0, 49)}...");
			sw.Stop();
			Logger.Success("Время затраченное на чтение первого элемента: " + ((int)sw.Elapsed.TotalMilliseconds) + " мс");
		}

		private static void TestSearchingByName(AppContext db)
		{
			throw new NotImplementedException();
		}

		private static void TestSearchingByAlias(AppContext db)
		{
			throw new NotImplementedException();
		}
	}
}
