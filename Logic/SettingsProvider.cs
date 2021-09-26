using MematorSQL.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MematorSQL.Logic
{
	public static class SettingsProvider
	{
		private static bool loaded = false;
		private const String settingsFileName = "AppSettings.json";
		private static readonly Settings _debug = new Settings("DEBUG")
		{
			DEBUG = true,
			CLEAR_DB_ON_START = true
		};

		private static readonly Settings _default = new Settings()
		{
			DEBUG = false,
			CLEAR_DB_ON_START = false
		};

		private static Settings current;

		public static Settings CURRENT
		{
			get
			{
				if (loaded)
					return current;
				return null;
			}
			private set
			{
				if (loaded)
					current = value;
			}
		}


		/// <summary>
		/// Сериализует текущие настройки в формат JSON
		/// </summary>
		/// <param name="settings"></param>
		public static String SerializeSettingsJson(Settings settings)
		{

			var options = new JsonSerializerOptions
			{
				WriteIndented = true
			};
			var jsonCode = JsonSerializer.Serialize(settings, options);
			return jsonCode;
		}

		/// <summary>
		/// Загружает настройки из файла или применяет стандартные в случае Зггога
		/// </summary>
		public static void Load()
		{
			string jsonCode;
			try
			{
				jsonCode = File.ReadAllText(settingsFileName);
				current = JsonSerializer.Deserialize<Settings>(jsonCode);
			}
			catch (Exception e)
			{
				Logger.Error($"Ошибка загрузки файла настроек: {e.Message}");
				Logger.Error($"Используются настройки по умолчанию");

				current = _debug;

				File.WriteAllText(settingsFileName, SerializeSettingsJson(CURRENT));
			}

			loaded = true;
			if (CURRENT.DEBUG)
			{
				Logger.Debug(SerializeSettingsJson(CURRENT));
			}
		}
	}
}
