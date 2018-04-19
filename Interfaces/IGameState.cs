using System;

namespace Interfaces
{
    public interface IGameState
    {
        void Install();
        void Upgrade();
        void Update();
        void Start();
        void UnInstall();
    }

    public class WrongStateException : Exception
    {
        public WrongStateException(){}
        public WrongStateException(string message) : base(message) { }
    }
}
