using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace PiConsole.Commands
{
    class CommandContext : ICommand
    {
        private ICommand currentCommand;
        public CommandContext(ICommand newCommand) => currentCommand = newCommand;
        public void Execute() => currentCommand.Execute();
        public void Undo() => currentCommand.Undo();
    }
}
