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
                new Demo_Downloaded(context),
                new Full_Downloaded(context),
                new Demo_Installation(context),
                new Full_Installation(context),
                new Demo_Started(context),
                new Full_Started(context),
                new Demo_Uninstalled(context),
                new Full_Uninstalled(context),
                new NeedsUpdate(context)
            };
            stateList.ForEach(state =>
            {
                var stateName = state.GetType().Name;
                Console.WriteLine($"╔{new string('═', stateName.Length + 2)}╗");
                Console.WriteLine("║ " + stateName + " ║");
                Console.WriteLine($"╚{new string('═', stateName.Length + 2)}╝");
                context.GameState = state;
                context.Install();
                context.GameState = state;
                context.Start();
                context.GameState = state;
                context.UnInstall();
                context.GameState = state;
                context.Update();
                context.GameState = state;
                context.Upgrade();
                Console.ReadLine();
            });
        }
    }
}
