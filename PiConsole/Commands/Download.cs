using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using PiConsole.GameHandling;

namespace PiConsole.Commands
{
    class Download : ICommand
    {
        private IGame _game;

        public Download(IGame game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.Context.Download();
        }

        public void Undo()
        {

        }
    }
}
