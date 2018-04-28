
using System;
using System.Runtime.Serialization;
using Interfaces;
using PiConsole.GameHandling;

namespace TestGame
{
    [Serializable]
    public class TestGame : IGame
    {
        public Guid? Owner { get; set; }
        public Guid? CurrentUser { get; set; }
        public Guid? _userWhoWantsToBorrow { get; set; }
        public string Name { get; } = "TestGame";
        public IGameStateContext Context { get; set; }
    }
}
