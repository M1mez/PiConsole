using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using PiConsole.GameHandling;

namespace PiConsole.Commands
{
    class Buy : ICommand
    {
        private IGame _game;

        public Buy(IGame game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.Context.Buy();
        }

        public void Undo()
        {

        }
    }
}
