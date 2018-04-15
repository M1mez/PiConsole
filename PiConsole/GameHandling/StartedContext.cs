using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace PiConsole.GameHandling
{
    class GameStateContext : IGameState
    {
        public IGameState GameStateState;
        public void Install(bool isDemo = true) => GameStateState.Install(isDemo);
        public void Upgrade() => GameStateState.Upgrade();
        public void Update() => GameStateState.Update();
        public void Start() => GameStateState.Start();
        public void UnInstall() => GameStateState.UnInstall();
    }
}
