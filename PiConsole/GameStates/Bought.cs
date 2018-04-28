using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace PiConsole.GameStates
{
    [Serializable]
    class Bought : IGameState
    {
        private readonly IGameStateSetter _setter;

        public Bought(IGameStateSetter setter)
        {
            _setter = setter;
        }

        public void Buy()
        {
            throw new WrongStateException("Game already bought!");
        }

        public void Download()
        {
            Console.Write("Will Download!");
            _setter.GameState = new Downloaded(_setter);
        }

        public void Install()
        {
            throw new WrongStateException("Download first!");
        }

        public void Update()
        {
            throw new WrongStateException("Download first!");
        }

        public void Start()
        {
            throw new WrongStateException("Download first!");
        }

        public void Uninstall()
        {
            throw new WrongStateException("Game not even downloaded!");
        }
    }
}
