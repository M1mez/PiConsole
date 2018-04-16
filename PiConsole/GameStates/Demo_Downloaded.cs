using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace PiConsole.GameStates
{
    class Demo_Downloaded : IGameState
    {
        public void Install(ref IGameState oldState)
        {
            Console.Write("Demo downloaded, will install now. ");
            oldState = new Demo_Installation();
        }

        public void Upgrade(ref IGameState oldState)
        {
            throw new WrongStateException("Cannot upgrade to Full Installation if Demo is not installed!");
        }

        public void Update(ref IGameState oldState)
        {
            throw new WrongStateException("Cannot Update if not installed!");
        }

        public void Start(ref IGameState oldState)
        {
            throw new WrongStateException("Install the game first!");
        }

        public void UnInstall(ref IGameState oldState)
        {
            throw new WrongStateException("Cannot uninstall game if not previously installed!");
        }
    }
}
