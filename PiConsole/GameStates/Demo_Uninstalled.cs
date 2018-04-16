using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace PiConsole.GameStates
{
    class Demo_Uninstalled : IGameState
    {
        public void Install(ref IGameState oldState)
        {
            Console.Write("Demo will now be installed. ");
            oldState = new Demo_Installation();
        }

        public void Upgrade(ref IGameState oldState)
        {
            Console.Write("Demo will now be upgraded to Full Installation, then installed. ");
            oldState = new Full_Installation();
        }

        public void Update(ref IGameState oldState)
        {
            throw new WrongStateException("Demo cannot be updated, not installed yet!");
        }

        public void Start(ref IGameState oldState)
        {
            throw new WrongStateException("Demo cannot be started, not installed yet!");
        }

        public void UnInstall(ref IGameState oldState)
        {
            throw new WrongStateException("Demo is already uninstalled!");
        }
    }
}
