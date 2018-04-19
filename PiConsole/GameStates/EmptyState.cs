using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace PiConsole.GameStates
{
    class EmptyState : IGameState
    {
        private readonly IGameStateSetter _setter;

        public EmptyState(IGameStateSetter setter)
        {
            _setter = setter;
        }
        
        public void Install()
        {
            _setter.GameState = new Demo_Installation(_setter);
        }

        public void Upgrade()
        {
            _setter.GameState = new Full_Installation(_setter);
        }

        public void Update()
        {
            _setter.GameState = new Demo_Installation(_setter);
        }

        public void Start()
        {
            throw new WrongStateException("Empty GameState!");
        }

        public void UnInstall()
        {
            throw new WrongStateException("Empty GameState!");
        }
    }
}
