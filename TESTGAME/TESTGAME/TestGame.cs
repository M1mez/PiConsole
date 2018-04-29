
using System;
using System.Runtime.Serialization;
using Interfaces;
using PiConsole.GameHandling;

namespace TestGame
{
    [Serializable]
    public class TestGame : AbstractIGame
    {
        public TestGame()
        {
            Name = "TestGame";
        }
    }
}
