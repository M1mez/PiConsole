using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class WrongCommandException : Exception
    {
        public WrongCommandException() { }
        public WrongCommandException(string message) : base(message) { }
    }
}
