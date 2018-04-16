using System;
using Interfaces;

namespace PiConsole.GameStates
{
    class Full_Started : IGameState
    {
        public void Install(ref IGameState oldState)
        {
            throw new WrongStateException("Game is already installed! And started!");
        }

        public void Upgrade(ref IGameState oldState)
        {
            Console.Write("Will check for Upgrades...");
        }

        public void Update(ref IGameState oldState)
        {
            Console.Write("Will check for Updates...");
        }

        public void Start(ref IGameState oldState)
        {
            throw new WrongStateException("Game is already started!");
        }

        public void UnInstall(ref IGameState oldState)
        {
            Console.Write("Game will quit and then uninstall.");
            oldState = new Full_Uninstalled();
        }
    }
}
