using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using PiConsole.FileHandling;

namespace PiConsole.GameHandling
{
    public static class GameManagement
    {
        //AllOwnedGames needed? User stores owned IGames in own list and gets serialized with it
        private static List<IGame> _allOwnedGames;
        public static List<IGame> AllOwnedGames => _allOwnedGames ?? (_allOwnedGames = SerializeHandling.Deserialize(Constants.GamesFile).Cast<IGame>().ToList());
        public static List<IGame> AllAvailableGames
        {
            get
            {
                RuntimeAddGame.SyncDll();
                return RuntimeAddGame.LoadedGames;
            }
        }

        public static void AddGame(IGame newGame)
        {
            _allOwnedGames.Add(newGame);
            SerializeHandling.Serialize(_allOwnedGames);
        }
    }
}
