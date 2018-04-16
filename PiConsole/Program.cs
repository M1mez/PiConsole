using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using PiConsole.FileHandling;
using PiConsole.GameHandling;
using PiConsole.GameStates;

namespace PiConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            RuntimeAddGame.SyncDll();
            Console.ReadLine();

            var context = new GameStateContext();
            var stateList = new List<IGameState>()
            {
                new Demo_Downloaded(),
                new Full_Downloaded(),
                new Demo_Installation(),
                new Full_Installation(),
                new Demo_Started(),
                new Full_Started(),
                new Demo_Uninstalled(),
                new Full_Uninstalled(),
                new NeedsUpdate()
            };
            stateList.ForEach(state =>
            {
                var stateName = state.GetType().Name;
                Console.WriteLine($"╔{new string('═', stateName.Length + 2)}╗");
                Console.WriteLine("║ " + stateName + " ║");
                Console.WriteLine($"╚{new string('═', stateName.Length + 2)}╝");
                context.GameState = state;
                context.Install(ref context.GameState);
                context.GameState = state;
                context.Start(ref context.GameState);
                context.GameState = state;
                context.UnInstall(ref context.GameState);
                context.GameState = state;
                context.Update(ref context.GameState);
                context.GameState = state;
                context.Upgrade(ref context.GameState);
                Console.ReadLine();
            });
        }
    }
}
