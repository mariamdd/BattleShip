using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip
{
    public interface IGameService
    {
        void Create();
        void Add();
        string Attack(int x, int y);
        bool Status();

    }
}
