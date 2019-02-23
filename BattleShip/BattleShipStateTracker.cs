/* Created Feb 2019 By Mariam D. 

   This program is based on the classic game Battleship.

   The program supports the following logic:
   - Create a 10x10 board for a player.

   - Add a battleship to the board. The battleship is placed arbitrary on the board either vertically or horizontally with a length of 1xn
     For the purpose of this task a battleship can have size up to 5. No specific battleship types are defined yet.
     When a board has a battleship or part of a ship the board will be marked as OCCUPIED. The board does not need to know what type of battleship is
     present at that location yet. When part of a battleship is hit the board will be marked as HIT. Otherwise the location is empty i.e. sea is present
   
   - Take an "attack" at a given position, and report back whether the attack resulted in a "hit" (if a ship or part of ship occupies the position) or
     a "miss" (these is sea).
   
   - Return whether the player has lost the game yet (i.e. all battleships are sunk)    
*/
using System;

namespace BattleShip
{

    //--------------------------------------------------------------------------------------------------------
    // Program: Implements the Battleship state tracker for a single player
    //--------------------------------------------------------------------------------------------------------

    class BattleShipStateTracker
    {
        static void Main(string[] args)
        {

            //Battleship state tracker for a single player that supports:
            //- Create a board
            Player p = new Player(new BattleShipService());
            p.Create();

            // - Adds a battleship to the board vertically or horizontally with size 1xn where n is 5 for the purpose of this task.
            p.Add();

            //- Attack a given position and reports whether the attack is "miss" or "hit". For this task purposes, position (6,4) will be attacked.
            string result_of_attack = p.Attack(6, 4);


            //- Returns whether the player has lost the game yet (i.e. all battleships are sunk)
            bool status_of_player = p.Status();
        }
    }
}
