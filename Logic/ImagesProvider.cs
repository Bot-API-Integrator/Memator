using MematorSQL.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

		public static void Base64ToFile(String fileName, String base64)
		{

		}
	}
}
