using System;
using Interfaces;

namespace PiConsole.GameStates
{
    [Serializable]
    class NeedsUpdate : IGameState
    {
        private readonly IGameStateSetter _setter;

        public NeedsUpdate(IGameStateSetter setter)
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
            Console.Write("Will Update Game...");
            _setter.GameState = new Installed(_setter);
        }

        public void Start()
        {
            Console.Write("Game will update and then start.");
            _setter.GameState = new Started(_setter);
        }

        public void Uninstall()
        {
            Console.Write("Game will uninstall...");
            _setter.GameState = new Bought(_setter);
        }
    }
}
