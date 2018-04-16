using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace PiConsole.GameStates
{
    class Demo_Installation : IGameState
    {
        public void Install(ref IGameState oldState)
        {
            throw new WrongStateException("Game already installed as Demo! Did you mean to call \"Upgrade\"?");
        }

        public void Upgrade(ref IGameState oldState)
        {
            Console.Write("Game will now be upgraded to full Installation! ");
            oldState = new Full_Installation();
        }

        public void Update(ref IGameState oldState)
        {
            Console.Write("Will check for updates for Demo now. ");
        }

        public void Start(ref IGameState oldState)
        {
            Console.Write("Demo is starting. ");
            oldState = new Demo_Started();
        }

        public void UnInstall(ref IGameState oldState)
        {
            Console.Write("Game will now be uninstalled. ");
            oldState = new Demo_Uninstalled();
        }
    }
}
