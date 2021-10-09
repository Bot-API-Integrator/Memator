using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MematorSQL.Util
{
	public class Base64File
	{
		public String Name { get; set; }
		public String Base64 { get; set; }



		public Base64File(string path)
		{
			Name = Path.GetFileName(path);
			try
			{
				byte[] AsBytes = File.ReadAllBytes(path);
				String AsBase64String = Convert.ToBase64String(AsBytes);

				Base64 = AsBase64String;
			}
			catch (Exception e)
			{
				Logger.Error(e.Message);
			}
		}

		/// <summary>
		/// Сохраняет файл по указанному пути. Принудительно устанавливает исходное расширение
		/// </summary>
		/// <param name="path">Путь до файла</param>
		public void SaveTo(string path)
		{
			Path.ChangeExtension(path,Path.GetExtension(Name));
			IO.EnsureExists(path);
		}
	}
}
