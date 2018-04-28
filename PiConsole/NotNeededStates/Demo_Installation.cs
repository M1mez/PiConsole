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
        private readonly IGameStateSetter _setter;

        public Demo_Installation(IGameStateSetter setter)
        {
            _setter = setter;
        }

        public void Install()
        {
            throw new WrongStateException("Game already installed as Demo! Did you mean to call \"Upgrade\"?");
        }

        public void Upgrade()
        {
            Console.Write("Game will now be upgraded to full Installation! ");
            _setter.GameState = new Full_Installation(_setter);
        }

        public void Update()
        {
            Console.Write("Will check for updates for Demo now. ");
        }

        public void Start()
        {
            Console.Write("Demo is starting. ");
            _setter.GameState = new Demo_Started(_setter);
        }

        public void UnInstall()
        {
            Console.Write("Game will now be uninstalled. ");
            _setter.GameState = new Demo_Uninstalled(_setter);
        }
    }
}
