using System;

namespace Interfaces
{
    public interface IGameState
    {
        void Install(ref IGameState oldState);
        void Upgrade(ref IGameState oldState);
        void Update(ref IGameState oldState);
        void Start(ref IGameState oldState);
        void UnInstall(ref IGameState oldState);
    }

    public class WrongStateException : Exception
    {
        public WrongStateException(){}
        public WrongStateException(string message) : base(message) { }
    }
}
