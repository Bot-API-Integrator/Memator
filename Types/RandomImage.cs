using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MematorSQL.Types
{
	public class RandomImage
	{
		public int Id { get; set; }
		public string Base64 { get; set; }
		public string FileName { get; set; }

		public RandomImage() { }
		public RandomImage(String path)
		{
			FileName = path;
			try
			{
				byte[] AsBytes = File.ReadAllBytes(path);
				String AsBase64String = Convert.ToBase64String(AsBytes);
				FileInfo fileInfo = new FileInfo(path);

				Base64 = AsBase64String;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		/// <summary>
		/// Сохранияет изображение в указанную папку с сохранением оригинального имени файла
		/// </summary>
		/// <param name="path">Путь до папки сохранения</param>
		public void SaveToDirectory(String path)
		{
			try
			{
				byte[] tempBytes = Convert.FromBase64String(Base64);
				File.WriteAllBytes(Path.Combine(path,Path.GetFileName(FileName)), tempBytes);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
