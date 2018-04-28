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
        private readonly IGameStateSetter _setter;

        public Demo_Downloaded(IGameStateSetter setter)
        {
            _setter = setter;
        }
        
        public void Install()
        {
            Console.Write("Demo downloaded, will install now. ");
            _setter.GameState = new Demo_Installation(_setter);
        }

        public void Upgrade()
        {
            throw new WrongStateException("Cannot upgrade to Full Installation if Demo is not installed!");
        }

        public void Update()
        {
            throw new WrongStateException("Cannot Update if not installed!");
        }

        public void Start()
        {
            throw new WrongStateException("Install the game first!");
        }

        public void UnInstall()
        {
            throw new WrongStateException("Cannot uninstall game if not previously installed!");
        }
    }
}
