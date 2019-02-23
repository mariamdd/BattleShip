using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip
{
    //--------------------------------------------------------------------------------------------------------
    // BattleShipService
    //--------------------------------------------------------------------------------------------------------

    public class BattleShipService : IGameService
    {

        private Board _board;

        public BattleShipService()
        {
            this._board = new Board();
        }
        public void Add()
        {
            this._board.Add();
        }

        public string Attack(int x, int y)
        {
            return this._board.Attack(x, y);
        }

        public void Create()
        {
            this._board.Create();
        }

        public bool Status()
        {
            return this._board.Status();
        }

    }
}
