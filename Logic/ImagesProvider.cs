using MematorSQL.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MematorSQL.Logic
{
    public static class ImagesProvider
    {
        private const String _assetsFolderName = "Assets";
        private const String _memesFolderName = "Memes";
        private const String _randomMemesFolderName = "RandomMemes";
        private const String _savedImagesFolderName = "Saved";

        private static String currentPath = Directory.GetCurrentDirectory();

        private static String memesPath, randomPath, savePath, assetsPath;

        private static bool started = false;

        private static int warnings = 0;
        public static void Start()
        {
            Prepare();

            Logger.Log("Starting Images Provider...");
            if (!started)
            {
                

                Logger.Log("Checking database...");
                using (AppContext db = new AppContext())
                {
                    Regex regex = new Regex(@"([^1-9]*)([1-9]\d*)?(\.[a-zA-Z]*)");
                    if (db.Memes.Count() == 0)
                    {
                        Logger.Log("Database is empty. Scanning for MeMeS...");
                        var files = new List<String>(Directory.GetFiles(memesPath));

                        var memes = new List<Meme>();

                        foreach (var file in files)
                        {
                            MatchCollection matches = regex.Matches(Path.GetFileName(file));
                            var match = matches.First();
                            var groups = match.Groups.Values.ToList();
                            string _grp = "";
                            foreach (var group in groups)
                            {
                                _grp += group.Value + (groups.Last() == group ? "" : ", ");
                            }
                            Logger.Debug("Match: \""+match.Value+"\". Groups: "+_grp);
                            var memeName = groups[1].Value;
                            var foundMeme = memes.Find(m => m.Name.ToLower() == memeName);

                            if (foundMeme != null)
                            {
                                foundMeme.Images.Add(new Image(file));
                            }
                        }
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                Logger.Warning("Something tried to call ImagesProvider.Start() when Images Provider was already running.");
            }

        }




        private static void Prepare()
        {
            Logger.Debug("Getting paths...");
            assetsPath = Path.Combine(currentPath, _assetsFolderName);

            memesPath = Path.Combine(assetsPath, _memesFolderName);
            randomPath = Path.Combine(assetsPath, _randomMemesFolderName);
            savePath = Path.Combine(assetsPath, _savedImagesFolderName);

            Logger.Debug("Checking directories...");
            EnsureExists(assetsPath);
            EnsureExists(memesPath);
            EnsureExists(randomPath);
            EnsureExists(savePath);
        }

        private static void EnsureExists(String path)
        {
            if (!Directory.Exists(path))
            {
                Logger.Debug($"[{path}] does not exists. Creating...");
                Directory.CreateDirectory(path);
                return;
            }
            Logger.Debug($"[{path}] exists.");
        }
    }
}
