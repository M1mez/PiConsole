using System;

namespace Interfaces
{
    public interface IGameState
    {
        void Buy();
        void Download();
        void Install();
        void Start();
        void Uninstall();
        void Update();
    }

    public class WrongStateException : Exception
    {
        public WrongStateException(){}
        public WrongStateException(string message) : base(message) { }
    }
}
