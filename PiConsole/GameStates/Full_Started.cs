using System;
using Interfaces;

namespace PiConsole.GameStates
{
    class Full_Started : IGameState
    {
        private readonly IGameStateSetter _setter;

        public Full_Started(IGameStateSetter setter)
        {
            _setter = setter;
        }

        public void Install()
        {
            throw new WrongStateException("Game is already installed! And started!");
        }

        public void Upgrade()
        {
            Console.Write("Will check for Upgrades...");
        }

        public void Update()
        {
            Console.Write("Will check for Updates...");
        }

        public void Start()
        {
            throw new WrongStateException("Game is already started!");
        }

        public void UnInstall()
        {
            Console.Write("Game will quit and then uninstall.");
            _setter.GameState = new Full_Uninstalled(_setter);
        }
    }
}
