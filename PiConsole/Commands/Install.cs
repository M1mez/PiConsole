using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using PiConsole.GameHandling;

namespace PiConsole.Commands
{
    class Install : ICommand
    {
        private IGame _game;

        public Install(IGame game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.Context.Install();
        }

        public void Undo()
        {
            _game.Context.Uninstall();
        }
    }
}
