using System;
using Interfaces;

namespace PiConsole.GameStates
{
    class Full_Uninstalled : IGameState
    {
        private readonly IGameStateSetter _setter;

        public Full_Uninstalled(IGameStateSetter setter)
        {
            _setter = setter;
        }

        public void Install()
        {
            Console.Write("Game will install with old Savestates.");
            _setter.GameState = new Full_Installation(_setter);
        }

        public void Upgrade()
        {
            throw new WrongStateException("Game is not installed!");
        }

        public void Update()
        {
            throw new WrongStateException("Game is not installed!");
        }

        public void Start()
        {
            throw new WrongStateException("Game is not installed!");
        }

        public void UnInstall()
        {
            throw new WrongStateException("Game is not installed!");
        }
    }
}
