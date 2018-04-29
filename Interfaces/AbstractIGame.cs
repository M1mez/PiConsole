using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    [Serializable]
    public abstract class AbstractIGame : IGame
    {
        public Guid? Owner { get; set; }
        public Guid? CurrentUser { get; set; }
        public Guid? _userWhoWantsToBorrow { get; set; }
        public string Name { get; protected set; }
        public IGameStateContext Context { get; set; }
        public bool IsLent => CurrentUser != null && Owner != CurrentUser;
    }
}
