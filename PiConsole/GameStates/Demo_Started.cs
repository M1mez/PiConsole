using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace PiConsole.GameStates
{
    class Demo_Started : IGameState
    {
        public void Install(ref IGameState oldState)
        {
            throw new WrongStateException("Demo already installed. And running by the way. ");
        }

        public void Upgrade(ref IGameState oldState)
        {
            Console.Write("Game will shut down and upgrade to Full Installation. ");
            oldState = new Full_Installation();
        }

        public void Update(ref IGameState oldState)
        {
            Console.Write("Will check for new Updates for Demo now. ");
        }

        public void Start(ref IGameState oldState)
        {
            throw new WrongStateException("Demo already started! ");
        }

        public void UnInstall(ref IGameState oldState)
        {
            Console.Write("Quit Demo... ");
            oldState = new Demo_Uninstalled();
        }
    }
}
