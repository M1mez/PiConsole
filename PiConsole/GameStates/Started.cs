using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace PiConsole.GameStates
{
    [Serializable]
    class Started : IGameState
    {
        private readonly IGameStateSetter _setter;

        public Started(IGameStateSetter setter)
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
            Console.Write("Will check for updated and restart...");
            _setter.GameState = new Started(_setter);
        }

        public void Start()
        {
            Console.Write("Game will restart...");
            _setter.GameState = new Started(_setter);
        }

        public void Uninstall()
        {
            throw new WrongStateException("Game is running!");
        }
    }
}
