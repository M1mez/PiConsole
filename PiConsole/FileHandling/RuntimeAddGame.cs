using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using PiConsole.GameHandling;
using PiConsole.GameStates;

namespace PiConsole.FileHandling
{
    public static class RuntimeAddGame
    {
        public static int SyncDll()
        {
            int outcome = 0;
            ExtractIfZipped(DirArchives);

            DirGames.ForEach(game => LoadAtRuntime(game, true));
            DirPlugins.ForEach(plugin => LoadAtRuntime(plugin, false));

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                //Console.WriteLine(assembly.FullName);
            }



            return outcome;
        }

        public static List<IPlugin> LoadedPlugins = new List<IPlugin>();

        public static List<IGame> LoadedGames = GameManagement.SavedAvailableGames;
        //public List<IPlugin> LoadedPlugins = new List<IPlugin>();

        private static void LoadAtRuntime(string dllPath, bool isGame)
        {
            var DLL = Assembly.LoadFile(dllPath);

            foreach (Type type in DLL.GetExportedTypes())
            {
                var c = Activator.CreateInstance(type);
                if (isGame && c is IGame game)
                {
                    if (LoadedGames.All(loaded => game.Name != loaded.Name))
                    {
                        //game.Context = new GameStateContext();
                        LoadedGames.Add(game);
                    }
                }
                else LoadedPlugins.Add((IPlugin) c);
            }

            //Console.ReadLine();
        }

        private static List<string> _loadedDll = new List<string>();

        private static List<string> DirGames => Directory.GetFiles(Constants.GamePath, "*.dll").Select(Path.GetFullPath).ToList();
        private static List<string> DirPlugins => Directory.GetFiles(Constants.PluginPath, "*.dll").Select(Path.GetFullPath).ToList();
        private static List<string> DirArchives
        {
            get
            {
                var zipList = new List<string>();
                zipList.AddRange(Directory.GetFiles(Constants.PluginPath, "*.plugin").Select(Path.GetFullPath).ToList());
                zipList.AddRange(Directory.GetFiles(Constants.GamePath, "*.game").Select(Path.GetFullPath).ToList());
                return zipList;
            }
        }

        private static List<string> _pluginList = new List<string>();
        private static List<string> _gamesList = new List<string>();

        private static List<string> ExtractIfZipped(List<string> archivesList)
        {
            var fileList = new List<string>();
            foreach (var zipPath in archivesList)
            {
                var isGame = zipPath.EndsWith(".game", StringComparison.OrdinalIgnoreCase);
                var isPlugin = zipPath.EndsWith(".plugin", StringComparison.OrdinalIgnoreCase);
                if (!isGame && !isPlugin) continue;
                var extractPath = isGame ? Constants.GamePath : Constants.PluginPath;

                using (var archive = ZipFile.OpenRead(zipPath))
                {
                    foreach (var entry in archive.Entries)
                    {
                        if (entry.FullName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                        {
                            var newFullPath = Path.Combine(extractPath, entry.FullName);
                            if (File.Exists(newFullPath)) continue;
                            entry.ExtractToFile(newFullPath);
                            fileList.Add(newFullPath);
                        }
                    }

                }
            }
            return fileList;
        }
    }
}
