using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiConsole
{
    public static class Constants
    {
        private static string _deployPath;
        private static string DeployPath => _deployPath ?? (_deployPath = Environment.CurrentDirectory);

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
