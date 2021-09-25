using MematorSQL.Logic;
using System;

namespace MematorSQL
{
	class Program
	{
		static void Main(string[] args)
		{
			Logger.DEBUG = true;
			Logger.Log("Запущен Memator v 0.1 pre-apha-in-dev-please-do-not-use-that-shit-god-please-no");
			if (SettingsProvider.CURRENT.CLEAR_DB_ON_START)
			{
				Logger.Log("Очистка базы данных");
				using (AppContext db = new AppContext())
				{
					db.Database.EnsureDeleted();
					db.Database.EnsureCreated();
				}
			}

			ImagesProvider.Start();
		}
	}
}
