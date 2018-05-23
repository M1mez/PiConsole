using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using PiConsole.Commands;
using PiConsole.FileHandling;
using PiConsole.GameHandling;
using PiConsole.GameStates;

namespace PiConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("#Wondergreat© #Wonderconsole© by #Wondermiel© and #Wondermed©\n\n");

            //var y = RuntimeAddGame.RuntimeLoadedGames;
            //var x = GameManagement.AllAvailableGames;
            //ShowCommands();
            UserManagement.UserInteraction();
            //RuntimeAddGame.SyncDll();
            Console.ReadLine();
        }
        
        private static void ShowStates()
        {
            var context = new GameStateContext();
            var stateList = new List<IGameState>()
            {
                new EmptyState(context),
                new Bought(context),
                new Downloaded(context),
                new Installed(context),
                new NeedsUpdate(context),
                new Started(context)
            };

            stateList.ForEach(state =>
            {
                var stateName = state.GetType().Name;
                Console.WriteLine($"╔{new string('═', stateName.Length + 2)}╗");
                Console.WriteLine("║ " + stateName + " ║");
                Console.WriteLine($"╚{new string('═', stateName.Length + 2)}╝");
                context.GameState = state;
                context.Buy();
                context.GameState = state;
                context.Download();
                context.GameState = state;
                context.Install();
                context.GameState = state;
                context.Update();
                context.GameState = state;
                context.Start();
                context.GameState = state;
                context.Uninstall();
                Console.ReadLine();
            });
        }

        private static void ShowCommands()
        {
            User miel = new User("Miel");
            IGame BoI = new GamePlaceHolder(Constants.TestGame, miel.ID);

            Buy buyGame = new Buy(BoI);
            Download downloadGame = new Download(BoI);
            Install installGame = new Install(BoI);
            Start startGame = new Start(BoI);
            Uninstall uninstallGame = new Uninstall(BoI);
            Update updateGame = new Update(BoI);


            buyGame.Execute();
            Console.ReadLine();
            downloadGame.Execute();
            Console.ReadLine();
            installGame.Execute();
            Console.ReadLine();
            startGame.Execute();
            Console.ReadLine();
            uninstallGame.Execute();
            Console.ReadLine();
            updateGame.Execute();
            Console.ReadLine();

            CommandContext uniCmd = new CommandContext(buyGame);
            uniCmd.Execute();
            Console.ReadLine();
            uniCmd = new CommandContext(downloadGame);
            uniCmd.Execute();
            Console.ReadLine();
            uniCmd = new CommandContext(installGame);
            uniCmd.Execute();
            Console.ReadLine();
            uniCmd = new CommandContext(startGame);
            uniCmd.Execute();
            Console.ReadLine();
            uniCmd = new CommandContext(uninstallGame);
            uniCmd.Execute();
            Console.ReadLine();
            uniCmd = new CommandContext(updateGame);
            uniCmd.Execute();
            Console.ReadLine();
        }
    }
}
