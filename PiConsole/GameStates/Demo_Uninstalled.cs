using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace PiConsole.GameStates
{
    class Demo_Uninstalled : IGameState
    {
        private readonly IGameStateSetter _setter;

        public Demo_Uninstalled(IGameStateSetter setter)
        {
            _setter = setter;
        }
        
        public void Install()
        {
            Console.Write("Demo will now be installed. ");
            _setter.GameState = new Demo_Installation(_setter);
        }

        public void Upgrade()
        {
            Console.Write("Demo will now be upgraded to Full Installation, then installed. ");
            _setter.GameState = new Full_Installation(_setter);
        }

        public void Update()
        {
            throw new WrongStateException("Demo cannot be updated, not installed yet!");
        }

        public void Start()
        {
            throw new WrongStateException("Demo cannot be started, not installed yet!");
        }

        public void UnInstall()
        {
            throw new WrongStateException("Demo is already uninstalled!");
        }
    }
}
