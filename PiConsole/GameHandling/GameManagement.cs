﻿using System;
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
        private static List<IGame> _savedAvailableGames;
        public static List<IGame> SavedAvailableGames => _savedAvailableGames ?? (_savedAvailableGames = SerializeHandling.Deserialize(Constants.GamesFile).Cast<IGame>().ToList());
        private static List<IGame> _allAvailableGames;
        public static List<IGame> AllAvailableGames
        {
            get
            {
                if (_allAvailableGames == null) SyncGames();
                return _allAvailableGames;
            }
        }

        public static void SyncGames()
        {
            RuntimeAddGame.SyncDll();
            List<IGame> runtimeLoadedGames = RuntimeAddGame.RuntimeLoadedGames;
            List<IGame> notYetSavedGames = runtimeLoadedGames
                .Where(runtime => SavedAvailableGames.All(saved => saved.Name != runtime.Name))
                .ToList();
            //List<IGame> notYetSavedGames = runtimeLoadedGames.Except(SavedAvailableGames).ToList();
            notYetSavedGames.ForEach(_savedAvailableGames.Add);
            _allAvailableGames = SavedAvailableGames.Union(_allAvailableGames ?? new List<IGame>()).ToList();
            SerializeHandling.Serialize(_allAvailableGames);
        }

        public static void AddGame(IGame newGame)
        {
            _savedAvailableGames.Add(newGame);
            SerializeHandling.Serialize(_savedAvailableGames);
        }

        public static IGame ChooseGame(string gameName, bool ownGames, User currentUser)
        {
            List<IGame> listToUse = ownGames ? currentUser.OwnedGames : AllAvailableGames;
            return listToUse?.Find(game => game.Name == gameName);
        }
    }
}
