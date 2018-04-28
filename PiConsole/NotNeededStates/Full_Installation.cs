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
        private readonly IGameStateSetter _setter;

        public Full_Installation(IGameStateSetter setter)
        {
            _setter = setter;
        }

        public void Install()
        {
            throw new WrongStateException("Full Game is already installed!");
        }

        public void Upgrade()
        {
            throw new WrongStateException("Game is already fully upgraded!");
        }

        public void Update()
        {
            Console.Write("Will check for Updates.");
        }

        public void Start()
        {
            Console.Write("Game is starting...");
            _setter.GameState = new Full_Started(_setter);
        }

        public void UnInstall()
        {
            Console.Write("Will uninstall Game...");
            _setter.GameState = new Full_Uninstalled(_setter);
        }
    }
}
