using System;
using Interfaces;

namespace PiConsole.GameStates
{
    class NeedsUpdate : IGameState
    {
        private readonly IGameStateSetter _setter;

        public NeedsUpdate(IGameStateSetter setter)
        {
            _setter = setter;
        }

        public void Install()
        {
            throw new WrongStateException("Game is already installed!");
        }

        public void Upgrade()
        {
            Console.Write("Will update and check for upgrades.");
            _setter.GameState = new Full_Installation(_setter);
        }

        public void Update()
        {
            Console.Write("Will Update Game...");
            _setter.GameState = new Full_Installation(_setter);
        }

        public void Start()
        {
            Console.Write("Game will update and then start.");
            _setter.GameState = new Full_Started(_setter);
        }

        public void UnInstall()
        {
            Console.Write("Game will uninstall...");
            _setter.GameState = new Full_Uninstalled(_setter);
        }
    }
}
