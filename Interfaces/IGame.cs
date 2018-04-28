using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Interfaces
{
    public interface IGame
    {
        Guid? Owner { get; set; }
        Guid? CurrentUser { get; set; }
        Guid? _userWhoWantsToBorrow { get; set; }
        string Name { get; }
        IGameStateContext Context { get; set; }
    }
}
