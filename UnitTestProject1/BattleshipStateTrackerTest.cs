using BattleShip;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class BattleShipStateTrackerTest
    {

        [TestMethod]
        public void TestCreateBoard() //testing add board
        {
            //arrange
            int rows_expected = 10;
            int cols_expected = 10;
            var board = new Board();

            //act
            int board_rows_actual = board.GetRows();
            int board_cols_actual = board.GetColms();

            //assert
            Assert.AreEqual(rows_expected, board_rows_actual);
            Assert.AreEqual(cols_expected, board_cols_actual);
        }

        [TestMethod]
        public void TestAddShip() //test add ship. since adding ships are random, we can check if player status if false.
        {
            //arrange
            string expected_status = "false";
            Board board = new Board();

            //act
            board.Add();//add ship
            string actual_status = board.Status().ToString().ToLower();

            //assert
            Assert.AreEqual(expected_status, actual_status);


        }

        [TestMethod]
        public void TestAttack() //test attack 
        {
            //arrange
            int x = 5;
            int y = 7;
            string expected_attack = "miss";
            var board = new Board();

            //act
            string actual_attack = board.Attack(x, y);

            //assert
            Assert.AreEqual(expected_attack, actual_attack);

        }

        [TestMethod]
        public void TestStatus() //test status of board 
        {
            //arrange
            string expected_status = "true";//initially status is true because player has empty board
            Board board = new Board();

            //act
            string actual_status = board.Status().ToString().ToLower();

            //assert
            Assert.AreEqual(expected_status, actual_status);
        }

    }
}
