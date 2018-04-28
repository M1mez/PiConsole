using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiConsole
{
    public static class Printer
    {
        public static void PrintUnderlined(string message, char c = 'º') => Console.WriteLine($"{message}\n{new string(c, message.Length)}");
        
    }
}
