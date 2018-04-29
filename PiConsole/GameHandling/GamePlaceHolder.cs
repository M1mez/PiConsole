using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using PiConsole.GameHandling;

namespace PiConsole
{
    [Serializable]
    public class GamePlaceHolder : IGame
    {
        public GamePlaceHolder() { }

        public GamePlaceHolder(string name, Guid userId)
        {
            Name = name;
            Owner = userId;
            CurrentUser = userId;
        }

        public GamePlaceHolder(IGame gameToRepresent, Guid userId)
        {
            Name = gameToRepresent.Name;
            Owner = userId;
            CurrentUser = userId;
            Context = new GameStateContext();
        }

        public Guid? Owner { get; set; }
        public Guid? CurrentUser { get; set; }
        public Guid? _userWhoWantsToBorrow { get; set; } = null;
        public string Name { get; }
        public IGameStateContext Context { get; set; }

        public bool IsLent => CurrentUser != null && Owner != CurrentUser;
    }
}
