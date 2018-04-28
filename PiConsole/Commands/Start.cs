using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using PiConsole.GameHandling;

namespace PiConsole.Commands
{
    class Start : ICommand
    {
        private IGame _game;

        public Start(IGame game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.Context.Start();
        }

        public void Undo()
        {
            
        }
    }
}
