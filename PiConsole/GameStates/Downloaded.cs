using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace PiConsole.GameStates
{
    [Serializable]
    class Downloaded : IGameState
    {
        private readonly IGameStateSetter _setter;

        public Downloaded(IGameStateSetter setter)
        {
            _setter = setter;
        }

        public void Buy()
        {
            throw new WrongStateException("Game already bought!");
        }

        public void Download()
        {
            throw new WrongStateException("Game already Downloaded!");
        }

        public void Install()
        {
            Console.Write("Game will install!");
            _setter.GameState = new Installed(_setter);
        }

        public void Update()
        {
            throw new WrongStateException("Game not installed!");
        }

        public void Start()
        {
            throw new WrongStateException("Game not installed!");
        }

        public void Uninstall()
        {
            throw new WrongStateException("Game not installed!");
        }
    }
}
