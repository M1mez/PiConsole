﻿using System;
using Interfaces;

namespace PiConsole.GameStates
{
    class NeedsUpdate : IGameState
    {
        public void Install(ref IGameState oldState)
        {
            throw new WrongStateException("Game is already installed!");
        }

        public void Upgrade(ref IGameState oldState)
        {
            Console.Write("Will update and check for upgrades.");
            oldState = new Full_Installation();
        }

        public void Update(ref IGameState oldState)
        {
            Console.Write("Will Update Game...");
            oldState = new Full_Installation();
        }

        public void Start(ref IGameState oldState)
        {
            Console.Write("Game will update and then start.");
            oldState = new Full_Started();
        }

        public void UnInstall(ref IGameState oldState)
        {
            Console.Write("Game will uninstall...");
            oldState = new Full_Uninstalled();
        }
    }
}
