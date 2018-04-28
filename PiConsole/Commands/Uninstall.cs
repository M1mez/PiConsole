using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using PiConsole.GameHandling;

namespace PiConsole.Commands
{
    class Uninstall : ICommand
    {
        private IGame _game;

        public Uninstall(IGame game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.Context.Uninstall();
        }

        public void Undo()
        {
            _game.Context.Install();
        }
    }
}
