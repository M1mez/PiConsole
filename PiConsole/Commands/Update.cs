using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using PiConsole.GameHandling;

namespace PiConsole.Commands
{
    class Update : ICommand
    {
        private IGame _game;

        public Update(IGame game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.Context.Update();
        }

        public void Undo()
        {
            //throw new WrongCommandException("Cannot undo Update!");
        }
    }
}
