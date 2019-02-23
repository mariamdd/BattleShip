using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip
{
    //--------------------------------------------------------------------------------------------------------
    // Player: represents a Battleship game player
    //--------------------------------------------------------------------------------------------------------
    public class Player
    {
        private IGameService _service;

        public Player(IGameService service)
        {
            this._service = service;
        }

        public void Create()
        {
            this._service.Create();
        }

        public void Add()
        {
            this._service.Add();
        }

        public string Attack(int x, int y)
        {
            return this._service.Attack(x, y);
        }

        public bool Status()
        {
            return this._service.Status();
        }

    }

}
