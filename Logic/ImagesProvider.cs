using MematorSQL.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MematorSQL.Util;
using System.Net;

namespace MematorSQL.Logic
{
	public static partial class ImagesProvider
	{
		private const String _assetsFolderName = "Assets";
		private const String _memesFolderName = "Memes";
		private const String _randomMemesFolderName = "RandomMemes";
		private const String _savedImagesFolderName = "Saved";

		private static String currentPath = Directory.GetCurrentDirectory();

		private static String memesPath, randomPath, savePath, assetsPath;

		private static bool started = false;

		private static int warnings = 0, errors = 0;

		public static void AddRandomMemeFromUrl(String url)
		{
			String hashName = url.GetHashCode() + (url.GetHashCode() * 17 * 23).ToString("X")+".png";
			String fileName = DownloadToFile(url, hashName);
			using (AppContext db = new AppContext())
			{
				db.RandomMemes.Add(new RandomMeme(hashName, new RandomImage(fileName)));
				db.SaveChanges();
			}
		}
		
		public static void AddRandomMemeFromBase64(Base64File base64)
		{
			using (AppContext db = new AppContext())
			{
				db.RandomMemes.Add(new RandomMeme(base64.Name, new RandomImage(base64)));
				db.SaveChanges();
			}
		}

		private static String Base64ToFile(String base64, String fileName)
		{
			if (started)
			{
				String path = savePath;

				Bitmap bitmap;
				Stream base64Stream = base64.FromBase64().ToStream();
				bitmap = new Bitmap(base64Stream);

				try
				{
					if (bitmap != null)
					{
						bitmap.Save(fileName);
						return Path.Combine(path,fileName);
					}
				}
				catch (Exception e)
				{
					Logger.Error(e.Message);
				}

				base64Stream.Close(); 
			}
			else
			{
				NotRunning();
			}
			return null;
		}

		private static String DownloadToFile(String url, String fileName)
		{
			if(started)
			{
				String path = savePath;
				try
				{
					WebClient client = new WebClient();
					Stream stream = client.OpenRead(url);
					Bitmap bitmap; bitmap = new Bitmap(stream);

					if (bitmap != null)
						bitmap.Save(fileName);

					stream.Flush();
					stream.Close();
					client.Dispose();

					return Path.Combine(path,fileName);
				}
				catch (Exception e)
				{
					Logger.Error(e.Message);
				}
			}
			else
			{
				NotRunning();
			}

			return null;
		}

		private static void NotRunning()
		{
			Logger.Warning("Поставщик изображений ещё не запущен");
		}
	}
}
