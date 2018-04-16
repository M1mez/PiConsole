using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace PiConsole.GameHandling
{
    class GameStateContext : IGameState
    {
        public IGameState GameState;

        public void Install(ref IGameState oldState)
        {
            const string method = "Install: ";
            Console.Write(method);
            try
            {
                GameState.Install(ref GameState);
                PrintCurrentState(method.Length);
            }
            catch (WrongStateException e)
            {
                PrintExceptionBeautifully(e, method.Length);
            }
        }

        public void Upgrade(ref IGameState oldState)
        {
            const string method = "Upgrade: ";
            Console.Write(method);
            try
            {
                GameState.Upgrade(ref GameState);
                PrintCurrentState(method.Length);
            }
            catch (WrongStateException e)
            {
                PrintExceptionBeautifully(e, method.Length);
            }
        }

        public void Update(ref IGameState oldState)
        {
            const string method = "Update: ";
            Console.Write(method);
            try
            {
                GameState.Update(ref GameState);
                PrintCurrentState(method.Length);
            }
            catch (WrongStateException e)
            {
                PrintExceptionBeautifully(e, method.Length);
            }
        }

        public void Start(ref IGameState oldState)
        {
            const string method = "Start: ";
            Console.Write(method);
            try
            {
                GameState.Start(ref GameState);
                PrintCurrentState(method.Length);
            }
            catch (WrongStateException e)
            {
                PrintExceptionBeautifully(e, method.Length);
            }
        }

        public void UnInstall(ref IGameState oldState)
        {
            const string method = "UnInstall: ";
            Console.Write(method);
            try
            {
                GameState.UnInstall(ref GameState);
                PrintCurrentState(method.Length);
            }
            catch (WrongStateException e)
            {
                PrintExceptionBeautifully(e, method.Length);
            }
        }
        
        private void PrintCurrentState(int spacing) =>
            Console.WriteLine($"\n{new string('¯', spacing - 1)} State is now: {GameState.GetType().Name}");
        private static void PrintExceptionBeautifully(WrongStateException e, int spacing)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Exception!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n{new string('¯', spacing - 1)} {e.Message}");
        }
    }
}
