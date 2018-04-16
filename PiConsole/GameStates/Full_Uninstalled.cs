using System;
using Interfaces;

namespace PiConsole.GameStates
{
    class Full_Uninstalled : IGameState
    {
        public void Install(ref IGameState oldState)
        {
            Console.Write("Game will install with old Savestates.");
            oldState = new Full_Installation();
        }

        public void Upgrade(ref IGameState oldState)
        {
            throw new WrongStateException("Game is not installed!");
        }

        public void Update(ref IGameState oldState)
        {
            throw new WrongStateException("Game is not installed!");
        }

        public void Start(ref IGameState oldState)
        {
            throw new WrongStateException("Game is not installed!");
        }

        public void UnInstall(ref IGameState oldState)
        {
            throw new WrongStateException("Game is not installed!");
        }
    }
}
