using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace PiConsole.GameStates
{
    class Full_Installation : IGameState
    {
        public void Install(ref IGameState oldState)
        {
            throw new WrongStateException("Full Game is already installed!");
        }

        public void Upgrade(ref IGameState oldState)
        {
            throw new WrongStateException("Game is already fully upgraded!");
        }

        public void Update(ref IGameState oldState)
        {
            Console.Write("Will check for Updates.");
        }

        public void Start(ref IGameState oldState)
        {
            Console.Write("Game is starting...");
            oldState = new Full_Started();
        }

        public void UnInstall(ref IGameState oldState)
        {
            Console.Write("Will uninstall Game...");
            oldState = new Full_Uninstalled();
        }
    }
}
