using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace PiConsole.GameStates
{
    [Serializable]
    class Installed : IGameState
    {
        private readonly IGameStateSetter _setter;

        public Installed(IGameStateSetter setter)
        {
            _setter = setter;
        }

        public void Buy()
        {
            throw new WrongStateException("Game already bought!");
        }

        public void Download()
        {
            throw new WrongStateException("Game already downloaded!");
        }

        public void Install()
        {
            throw new WrongStateException("Game already installed!");
        }

        public void Update()
        {
            Console.Write("Will check for updates!");
            _setter.GameState = new Installed(_setter);
        }

        public void Start()
        {
            Console.Write("Game starting...");
            _setter.GameState = new Started(_setter);
        }

        public void Uninstall()
        {
            Console.Write("Game will uninstall...");
            _setter.GameState = new Bought(_setter);
        }
    }
}
