using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace PiConsole.GameStates
{
    class Uninstalled : IGameState
    {
        public void Install(bool isDemo = true)
        {
            throw new NotImplementedException();
        }

        public void Upgrade()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void UnInstall()
        {
            throw new NotImplementedException();
        }
    }
}
