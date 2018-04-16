using System;
using System.IO;

namespace PiConsole
{
    public static class Constants
    {
        private static readonly string DeployPath = Environment.CurrentDirectory;
        public static string GamePath
        {
            get
            {
                const string dirName = "games";
                var dirPath = Path.Combine(DeployPath, dirName);
                Directory.CreateDirectory(dirPath);
                return dirPath;
            }
        }
        public static string PluginPath
        {
            get
            {
                const string dirName = "plugins";
                var dirPath = Path.Combine(DeployPath, dirName);
                Directory.CreateDirectory(dirPath);
                return dirPath;
            }
        }
    }
}
