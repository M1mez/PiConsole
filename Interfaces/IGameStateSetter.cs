using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Interfaces
{
    public interface IGameStateSetter
    {
        IGameState GameState { set; }
    }
}
