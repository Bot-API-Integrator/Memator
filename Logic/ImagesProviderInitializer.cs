using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MematorSQL.Types;
using MematorSQL.Util;

namespace MematorSQL.Logic
{
	public static partial class ImagesProvider
	{
		/// <summary>
		/// Подготавливает поставщик изображений к работе. Остальные публичные методы не будут работать, если поставщик не запущен.
		/// </summary>
		public static void Start()
		{
			Prepare();

			Logger.Log("Запуск поставщика изображений...");
			if (!started)
			{
				Logger.Log("Проверка базы данных...");
				using (AppContext db = new AppContext())
				{
					if (db.Memes.Count() == 0)
					{
						FillMemesDb(db);
					}
					else
					{
						UpdateMemesDb(db);
					}

					if (db.RandomMemes.Count() == 0)
					{
						FillRandomDb(db);
					}
					else
					{
						UpdateRandomDb(db);
					}

					Tests.DbPerformanceTests.StartTests(db);
				}
			}
			else
			{
				Logger.Warning("Что-то попыталось вызвать ImagesProvider.Start() когда поставщик изображений уже был запущен.");
			}

			started = true;
		}

		/// <summary>
		/// Сканирует директорию Assets/RandomMemes и обновляет базу данных случайных картинок
		/// </summary>
		/// <param name="db"></param>
		private static void UpdateRandomDb(AppContext db)
		{
			Logger.Log($"В таблице случайных мемов есть {db.RandomMemes.Count()} записей");
			var files = new List<String>(Directory.GetFiles(randomPath));
			var randomMemes = db.RandomMemes.ToList();


			foreach (var file in files)
			{
				var fileName = Path.GetFileName(file);
				if (!randomMemes.Exists(m => m.Name.ToLower() == fileName.ToLower()))
				{
					db.Add(new RandomMeme(fileName, new RandomImage(file)));
					Logger.Debug($"Найден мем {fileName}");
				}
			}

		}

		/// <summary>
		/// Сканирует директорию Assets/Memes и обновляет базу данных картинок
		/// </summary>
		/// <param name="db"></param>
		private static void UpdateMemesDb(AppContext db)
		{
			Logger.Log($"В таблице мемов есть {db.Memes.Count()} записей");
			// do nothing
		}

		/// <summary>
		/// Сканирует директорию Assets/RandomMemes и обновляет базу данных картинок
		/// </summary>
		/// <param name="db"></param>
		private static void FillRandomDb(AppContext db)
		{
			var files = new List<String>(Directory.GetFiles(randomPath));
			var memes = new List<RandomMeme>();

			var _warnings = 0;
			var _errors = 0;
			var _memesCount = files.Count;

			Stopwatch sw = new Stopwatch();

			Logger.Log("База данных случайных мемов пуста. Сканирование МеМеСоВ");
			sw.Start();

			foreach (var file in files)
			{
				var fileName = Path.GetFileName(file);
				memes.Add(new RandomMeme(fileName, new RandomImage(file)));
				Logger.Debug($"Найден мем {fileName}");
			}

			sw.Stop();
			Logger.Success($"Время затраченное на сканирование случайных мемов: {((int)sw.Elapsed.TotalMilliseconds)} мс");

			sw = new Stopwatch();
			sw.Start();
			db.RandomMemes.AddRange(memes);
			db.SaveChanges();
			sw.Stop();
			Logger.Success($"Время затраченное на запись: {((int)sw.Elapsed.TotalMilliseconds)} мс. " +
				$"Записано {_memesCount} мемов");

			PrintWarnings(_warnings, _errors);
			warnings += _warnings;
			errors += _errors;
		}


		/// <summary>
		/// Сканирует папку Assets/Memes (по умолчанию) на наличие картинок с подходящим именем.
		/// </summary>
		private static void FillMemesDb(AppContext db)
		{
			/// Проверка названий файлов мемов на соответствие шаблону [НазваниеМема][НомерКартинки?].[расширение]
			/// Кроме того, название каждого файла будет разбито на 4 группы по порядку:
			/// 0. Название файла
			/// 1. Название мема
			/// 2. Номер картинки (при наличии, либо пустая строка) (0 не может быть первой цифрой номера (см. предпоследнюю строку в примере))
			/// 3. Расширение (включая точку)
			/// Пример работы выражения: https://regex101.com/r/qzgzBK
			Regex regex = new Regex(@"([^1-9]*)([1-9]\d*)?(\.[a-zA-Z]*)");

			var files = new List<String>(Directory.GetFiles(memesPath));
			var memes = new List<Meme>();

			var _warnings = 0;
			var _errors = 0;
			var _memesCount = 0;
			var _imagesCount = 0;

			Stopwatch sw = new Stopwatch();

			Logger.Log("База данных мемов пуста. Сканирование МеМеСоВ");
			sw.Start();
			foreach (var file in files)
			{
				MatchCollection matches = regex.Matches(Path.GetFileName(file));
				try
				{
					var match = matches.First();
					var groups = match.Groups.Values.ToList();
					string _grp = "";
					foreach (var group in groups)
					{
						_grp += group.Value + (groups.Last() == group ? "" : ", ");
					}
					Logger.Debug($"Совпадение: \"{match.Value}\". Группы: {_grp}");
					var memeName = groups[1].Value;
					var foundMeme = memes.Find(m => m.Name.ToLower() == memeName.ToLower());

					if (foundMeme != null)
					{
						foundMeme.Images.Add(new Image(file));
					}
					else
					{
						memes.Add(new Meme(memeName, new Image(file)));
						_memesCount++;
					}
					_imagesCount++;

				}
				catch (Exception e)
				{
					_errors++;
					Logger.Error(e.Message);
				}
			}
			sw.Stop();
			Logger.Success($"Время затраченное на сканирование: {((int)sw.Elapsed.TotalMilliseconds)} мс");
			Logger.Success("Заполнение завершено!");

			sw = new Stopwatch();
			sw.Start();
			db.Memes.AddRange(memes);
			db.SaveChanges();
			sw.Stop();

			Logger.Success($"Время затраченное на запись: {((int)sw.Elapsed.TotalMilliseconds)} мс. " +
				$"Записано {_memesCount} мемов, содержащих {_imagesCount} картинок");
			PrintWarnings(_warnings, _errors);
			warnings += _warnings;
			errors += _errors;
		}

		/// <summary>
		/// Выводит в консоль сообщения о количестве предупреждений и ошибок при их наличии
		/// </summary>
		/// <param name="warns">Количество предупреждений</param>
		/// <param name="errs">Количество ошибок</param>
		private static void PrintWarnings(int warns, int errs)
		{
			if (warns > 0)
			{
				Logger.Warning("\tПредупреждений: " + warns);
			}
			if (errs > 0)
			{
				Logger.Warning("\tОшибок: " + errs);
			}
		}


		private static void Prepare()
		{
			Logger.Debug("Сборка путей...");
			assetsPath = Path.Combine(currentPath, _assetsFolderName);

			memesPath = Path.Combine(assetsPath, _memesFolderName);
			randomPath = Path.Combine(assetsPath, _randomMemesFolderName);
			savePath = Path.Combine(assetsPath, _savedImagesFolderName);

			Logger.Debug("Проверка директорий...");
			IO.EnsureExists(assetsPath);
			IO.EnsureExists(memesPath);
			IO.EnsureExists(randomPath);
			IO.EnsureExists(savePath);
		}

		
	}
}
