using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MematorSQL.Types
{
    public class Meme
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public List<string> Aliases { get; set; } = new List<string>();
        public virtual List<Image> Images { get; set; } = new List<Image>();


		public Meme() { }
		public Meme(String name, Image image) {
			Name = name;
			Images.Add(image);
		}


		public Meme AddAlias(string alias)
		{
			Aliases.Add(alias);
			return this;
		}


		/// <summary>
		/// Добавляет альтернативное название команды
		/// </summary>
		/// <param name="alias">Альтернативное название</param>
		/// <returns>Текущая команда</returns>
		public Meme AddAliases(params string[] aliases)
		{
			foreach (var alias in aliases)
			{
				Aliases.Add(alias);
			}

			return this;
		}

		/// <summary>
		/// Проверяет, соответствует ли команда искомой строке (проверка по названию и псевдонимам).
		/// </summary>
		/// <param name="text">Искомая команда</param>
		/// <returns></returns>
		public bool CheckMeme(string text)
		{
			string command = text.Replace("!", "").Split(' ')[0];

			if (command.ToLower() == Name.ToLower())
			{
				return true;
			}
			else
			{
				foreach (string alias in Aliases)
				{
					if (command.ToLower() == alias.ToLower())
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
