using System;
using System.IO;
using Interfaces;

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
        public static string SerializePath
        {
            get
            {
                const string dirName = "serialized";
                var dirPath = Path.Combine(DeployPath, dirName);
                Directory.CreateDirectory(dirPath);
                return dirPath;
            }
        }

        public static string UsersFile => Path.Combine(SerializePath, "users.bin");
        public static string GamesFile => Path.Combine(SerializePath, "games.bin");
        public static Guid TestGuid = Guid.Parse("9245fe4a-d402-451c-b9ed-9c1a04247482");
        public static IGame TestGame => new GamePlaceHolder("TestGame", TestGuid);
        public static User TestUser => new User("Admin", TestGuid);
    }
}
