using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace PiConsole.FileHandling
{
    public static class SerializeHandling
    {
        public static void Serialize(List<User> users) =>
            Serialize(new List<object>(users), Constants.UsersFile);
        public static void Serialize(List<IGame> games) =>
            Serialize(new List<object>(games), Constants.GamesFile);
        public static void Serialize (List<object> list, string filePath)
        {
            //if (!File.Exists(filePath)) File.Create(filePath).Dispose();
            //if (filePath == null) filePath = Constants.UsersFile;
            if (filePath != Constants.UsersFile && filePath != Constants.GamesFile) return;
            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, list);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"in SerializeHandling.Serialize, path was \"{filePath}\".");
                Console.WriteLine(e.Message);
            }
        }

        public static List<object> Deserialize(string filePath)
        {
            if (filePath != Constants.UsersFile && filePath != Constants.GamesFile) return null;
            if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
            {
                var newList = new List<object>();
                if (filePath == Constants.UsersFile) newList = new List<object>() {Constants.TestUser};
                if (filePath == Constants.GamesFile)newList = new List<object> {Constants.TestGame};
                Serialize(newList, filePath);
                return newList;
            }

            //if (!(File.Exists(filePath) && new FileInfo(filePath).Length == 0)) return null;
            var list = new List<object>();
            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    list = (List<object>) bin.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"in SerializeHandling.Deserialize, path was \"{filePath}\".");
                Console.WriteLine(e.Message);
            }

            return list;
        }
    }
}
