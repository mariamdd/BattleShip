using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip
{
    //--------------------------------------------------------------------------------------------------------
    // Board: Represents a Battleship game board. For this task purposes all the battleship have no class yet
    //        of their own because the task specification has not specified the names or spcific length for 
    //        each battlship. The Board is a two-dimensional array that has length 10x10
    //--------------------------------------------------------------------------------------------------------
    public class Board
    {
        private int[,] _board;


        //The length and width of board. Currently is 10x10
        private const int BOARD_ROW = 10;
        private const int BOARD_COLMS = 10;

        //Representing a Board position: 
        //It can be empty i.e. have sea. It can be HIT (meaning a ship or part of it is hit) or OCCUPIED (a ship or part of it is present which is not hit)
        private const int SEA = 0;
        private const int HIT = 1;
        private const int OCCUPIED = 2;

        //Representing the vertical and horizontal direction of a ship on the board.
        private const string NORTH = "NORTH";
        private const string SOUTH = "SOUTH";
        private const string EAST = "EAST";
        private const string WEST = "WEST";

        //presents the maximum ship length 'n'
        private const int MAX_SHIP_LENGTH = 6;
        private const int MIN_SHIP_LENGTH = 1;

        public Board()
        {
            this._board = new int[BOARD_ROW, BOARD_COLMS];
        }

        //returns the length and width of board for testing purposes.
        public int GetRows()
        {
            return BOARD_ROW;
        }

        public int GetColms()
        {
            return BOARD_COLMS;
        }

        //Adds a battleship randomly to the board as specified by task requirement (only one ship is placed on board)
        public void Add()
        {
            //Get random location on board
            Random random = new Random();
            int ran_x = GenerateRandom(0, BOARD_ROW, random);
            int ran_y = GenerateRandom(0, BOARD_COLMS, random);

            //Get random direction NORTH, SOUTH, EAST and WEST
            string rand_direction = GetRandDirection();

            //Get random size of battleship
            int ship_length = GenerateRandom(MIN_SHIP_LENGTH, MAX_SHIP_LENGTH, random);

            //Add the battleship randomly on baord and check if adding was successful. If not try adding another battleship.
            bool is_ship_added = AddShip(ran_x, ran_y, rand_direction, ship_length);
            while (!is_ship_added)
            {
                is_ship_added = AddShip(ran_x, ran_y, rand_direction, ship_length);
                ran_x = GenerateRandom(0, BOARD_ROW, random);
                ran_y = GenerateRandom(0, BOARD_COLMS, random);
                rand_direction = GetRandDirection();
                ship_length = random.Next(MIN_SHIP_LENGTH, MAX_SHIP_LENGTH);
            }
        }

        //Places a battleship with specified length and direction to board if the given x and y positions are valid and available.  
        private bool AddShip(int x, int y, string rand_direction, int ship_length)
        {
            bool is_ship_added = false;
            int ran_x = x;
            int ran_y = y;

            bool is_coord_free = true; //assume initially that coordinate is free until we check
            int places_found_so_far = 0;//number of empty positions needed

            while (places_found_so_far < ship_length)
            {
                if (IsCoordsValid(ran_x, ran_y))
                {
                    if (IsCoordsEmpty(ran_x, ran_y))
                    {
                        places_found_so_far = places_found_so_far + 1;
                        ran_x = GetRow(ran_x, rand_direction);//get x neighbor at given direction from current position so we check at availability and validity
                        ran_y = GetColumn(ran_y, rand_direction);//get y neighbor at the given direction from current position so we can check availability and validity
                        is_coord_free = CheckNeighbor(ran_x, ran_y, rand_direction);//check if neighbor is free at given direction 
                    }
                    else
                    {
                        //the given position is not empty, find new position
                        break;
                    }
                }
                else
                {
                    //the given position is not valid, find new position
                    break;
                }
            }

            if (places_found_so_far == ship_length) //here we know all coords are valid so we can mark them now on board as occupied
            {
                ran_x = x;
                ran_y = y;
                places_found_so_far = 0;

                while (places_found_so_far < ship_length)
                {
                    MarkOccupied(ran_x, ran_y);
                    places_found_so_far = places_found_so_far + 1;
                    ran_x = GetRow(ran_x, rand_direction);
                    ran_y = GetColumn(ran_y, rand_direction);
                }

                is_ship_added = true;
            }
            else
            {
                is_ship_added = false;
            }

            return is_ship_added;
        }

        //Returns a random number
        private int GenerateRandom(int min_number, int max_number, Random random)
        {
            return random.Next(min_number, max_number);
        }

        //Attacks a board at the given location and reports whether the given attack has been a "hit" or a "miss"
        public string Attack(int x, int y)
        {
            string result = "miss";
            if (_board[x, y] == HIT)
            {
                result = "hit";
            }
            else if (_board[x, y] == OCCUPIED)
            {
                _board[x, y] = HIT;
                result = "hit";
            }
            else
            {
                result = "miss";
            }

            return result;
        }

        //Creates a new board
        public void Create()
        {
            for (int row = 0; row < BOARD_ROW; row++)
            {
                for (int col = 0; col < BOARD_COLMS; col++)
                {
                    this._board[row, col] = SEA;
                }
            }

        }

        //Returns whether the player has lost the game yet i.e. If all battleships are sunk.
        public bool Status()
        {
            bool lost = false;

            for (int row = 0; row < BOARD_ROW; row++)
            {
                lost = CheckColmsIfEmpty(row);   //check if there is any ship or part of ship
                if (lost == false)
                {
                    break;
                }
                else
                {
                    lost = true;
                }
            }

            return lost;
        }

        //Checks if all columns for the given row are empty
        private bool CheckColmsIfEmpty(int row)
        {
            bool is_empty = true;
            for (int col = 0; col < BOARD_COLMS; col++)
            {

                if (_board[row, col] == OCCUPIED)
                {
                    is_empty = false;
                    break;
                }
            }
            return is_empty;
        }

        //Checks if given location is occupied by a battleship assuming position is within range to avoid out of range program exception.
        private bool IsCoordsEmpty(int x, int y)
        {
            if (this._board[x, y] == OCCUPIED)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        //Checks if given location is valid i.e. its not out of board boundaries 
        private bool IsCoordsValid(int x, int y)
        {
            if ((x < 0) || (x > (BOARD_ROW - 1)) || (y < 0) || (y > (BOARD_COLMS - 1)))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        //Marks given position on board as OCCUPIED assuming position is within board range
        private void MarkOccupied(int x, int y)
        {
            this._board[x, y] = OCCUPIED;
        }

        //Checks if neighbours point at specified direction of given position (x, y) is empty  
        private bool CheckNeighbor(int x, int y, string direction)
        {
            bool free = false;

            //we calculate if point is free according to given "direction"
            switch (direction)
            {
                case NORTH:
                    if ((x - 1) >= 0)
                    {
                        if (IsCoordsEmpty(x - 1, y))
                        {
                            free = true;
                        }
                    }
                    break;
                case SOUTH:
                    if ((x + 1) <= (BOARD_ROW - 1))
                    {
                        if (IsCoordsEmpty(x + 1, y))
                        {
                            free = true;
                        }
                    }
                    break;
                case EAST:
                    if ((y + 1) <= (BOARD_COLMS - 1))
                    {
                        if (IsCoordsEmpty(x, y + 1))
                        {
                            free = true;

                        }
                    }
                    break;
                case WEST:
                    if ((y - 1) >= 0)
                    {
                        if (IsCoordsEmpty(x, y - 1))
                        {
                            free = true;
                        }
                    }
                    break;
            }

            return free;
        }

        //Returns neighboring point for given position in the direction given. This will help us determine the NORTH or SOUTH neighbor
        private int GetRow(int pos, string direction)
        {
            int neighbor_pos = pos;

            switch (direction)
            {
                case NORTH:
                    neighbor_pos = neighbor_pos - 1;
                    if (neighbor_pos < 0)
                    {
                        neighbor_pos = -1;
                    }
                    break;
                case SOUTH:
                    neighbor_pos = neighbor_pos + 1;
                    if (neighbor_pos > (BOARD_ROW - 1))
                    {
                        neighbor_pos = -1;
                    }
                    break;
                case EAST:
                    break;
                case WEST:
                    break;
            }
            return neighbor_pos;
        }

        //Returns neighboring point for given position in the direction given. This will help us determine the EAST or WEST neighbor
        private int GetColumn(int pos, string direction)
        {

            int neighbor_pos = pos;

            switch (direction)
            {
                case NORTH:
                    break;
                case SOUTH:
                    break;
                case EAST:
                    neighbor_pos = neighbor_pos + 1;
                    if (neighbor_pos > (BOARD_ROW - 1))
                    {
                        neighbor_pos = -1;
                    }
                    break;
                case WEST:
                    neighbor_pos = neighbor_pos - 1;
                    if (neighbor_pos < 0)
                    {
                        neighbor_pos = -1;
                    }
                    break;
            }
            return neighbor_pos;
        }

        //Returns a random direction. A direction can be either "NORTH", "SOUTH", "EAST" and "WEST" 
        private string GetRandDirection()
        {
            Random random = new Random();

            //get random direction NORTH, SOUTH, EAST and WEST
            int ran_direction = random.Next(0, 4);

            string direction = "NONE";
            switch (ran_direction)
            {
                case 0:
                    direction = "NORTH";
                    break;
                case 1:
                    direction = "SOUTH";
                    break;
                case 2:
                    direction = "EAST";
                    break;
                case 3:
                    direction = "WEST";
                    break;
                default:
                    break;
            }

            return direction;
        }
    }
}
