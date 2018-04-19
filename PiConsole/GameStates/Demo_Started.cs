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
        private readonly IGameStateSetter _setter;

        public Demo_Started(IGameStateSetter setter)
        {
            _setter = setter;
        }

        public void Install()
        {
            throw new WrongStateException("Demo already installed. And running by the way. ");
        }

        public void Upgrade()
        {
            Console.Write("Game will shut down and upgrade to Full Installation. ");
            _setter.GameState = new Full_Installation(_setter);
        }

        public void Update()
        {
            Console.Write("Will check for new Updates for Demo now. ");
        }

        public void Start()
        {
            throw new WrongStateException("Demo already started! ");
        }

        public void UnInstall()
        {
            Console.Write("Quit Demo... ");
            _setter.GameState = new Demo_Uninstalled(_setter);
        }
    }
}
