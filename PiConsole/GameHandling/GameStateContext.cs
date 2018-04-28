using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using PiConsole.GameStates;

namespace PiConsole.GameHandling
{
    [Serializable]
    public class GameStateContext : IGameStateContext
    {
        public IGameState GameState { private get; set; }

        public GameStateContext()
        {
            GameState = new EmptyState(this);
        }

        public void Buy()
        {
            const string method = "Buy: ";
            Console.Write(method);
            try
            {
                GameState.Buy();
                PrintGameState(method.Length);
            }
            catch (WrongStateException e)
            {
                PrintExceptionBeautifully(e, method.Length);
            }
        }

        public void Download()
        {
            const string method = "Download: ";
            Console.Write(method);
            try
            {
                GameState.Download();
                PrintGameState(method.Length);
            }
            catch (WrongStateException e)
            {
                PrintExceptionBeautifully(e, method.Length);
            }
        }

        public void Install()
        {
            const string method = "Install: ";
            Console.Write(method);
            try
            {
                GameState.Install();
                PrintGameState(method.Length);
            }
            catch (WrongStateException e)
            {
                PrintExceptionBeautifully(e, method.Length);
            }
        }

        /*public void Upgrade()
        {
            const string method = "Upgrade: ";
            Console.Write(method);
            try
            {
                GameState.Upgrade();
                PrintGameState(method.Length);
            }
            catch (WrongStateException e)
            {
                PrintExceptionBeautifully(e, method.Length);
            }
        }*/

        public void Update()
        {
            const string method = "Update: ";
            Console.Write(method);
            try
            {
                GameState.Update();
                PrintGameState(method.Length);
            }
            catch (WrongStateException e)
            {
                PrintExceptionBeautifully(e, method.Length);
            }
        }

        public void Start()
        {
            const string method = "Start: ";
            Console.Write(method);
            try
            {
                GameState.Start();
                PrintGameState(method.Length);
            }
            catch (WrongStateException e)
            {
                PrintExceptionBeautifully(e, method.Length);
            }
        }

        public void Uninstall()
        {
            const string method = "Uninstall: ";
            Console.Write(method);
            try
            {
                GameState.Uninstall();
                PrintGameState(method.Length);
            }
            catch (WrongStateException e)
            {
                PrintExceptionBeautifully(e, method.Length);
            }
        }
        
        private void PrintGameState(int spacing)
        {
            Console.Write($"\n{new string('¯', spacing - 1)} State is now: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(GameState.GetType().Name);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void PrintExceptionBeautifully(WrongStateException e, int spacing)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Exception!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n{new string('¯', spacing - 1)} {e.Message}");
        }

    }
}
