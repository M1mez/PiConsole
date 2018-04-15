using System;

namespace Interfaces
{
    public interface IGameState
    {
        void Install(bool isDemo = true);
        void Upgrade();
        void Update();
        void Start();
        void UnInstall();
    }
}
