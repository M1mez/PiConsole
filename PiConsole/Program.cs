using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiConsole.FileHandling;

namespace PiConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            RuntimeAddGame.SyncDll();
            Console.Read();
        }
    }
}
