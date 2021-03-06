﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace PiConsole.GameStates
{
    [Serializable]
    class EmptyState : IGameState
    {
        private readonly IGameStateSetter _setter;

        public EmptyState(IGameStateSetter setter)
        {
            _setter = setter;
        }

        public void Buy()
        {
            Console.Write("Will buy Game!");
            _setter.GameState = new Bought(_setter);
        }

        public void Download()
        {
            throw new WrongStateException("Buy first!");
        }

        public void Install()
        {
            throw new WrongStateException("Buy first!");
        }

        public void Update()
        {
            throw new WrongStateException("Buy first!");
        }

        public void Start()
        {
            throw new WrongStateException("Buy first!");
        }

        public void Uninstall()
        {
            throw new WrongStateException("Game not even bought!");
        }
    }
}
