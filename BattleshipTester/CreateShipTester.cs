using System;
using System.Collections.Generic;
using BattleshipStateTracker.model;
using Xunit;

namespace BattleshipTester
{
    public class CreateShipTester
    {
        [Fact]
        public void CreateOneShip()
        {
            Board board = new Board(3);
            Assert.Equal(3, board.BoardDimensions);
            Assert.True(board.Id > 0);
            Ship ship = new Ship
            {
                Layout = ShipLayoutEnum.Vertical,
                ShipLength = 2,
                ShipSymbol = "Ship1"
            };
            Assert.True(board.PlaceShip(ship));
        }

        [Fact]
        public void CreateBoardWithInvalidSize()
        {
            try
            {
                Board board = new Board(-3);
                Assert.False(true);
            }
            catch (Exception exp)
            {
                Assert.Equal("Invalid board size!", exp.Message);
            }
        }

        [Fact]
        public void CreateTenShips()
        {
            Board board = new Board(5);
            Assert.Equal(5, board.BoardDimensions);
            Assert.True(board.Id > 0);
            var ships = new List<Ship>();
            for(int i = 0; i < 10; i++)
            {
                ships.Add(new Ship
                {
                    Layout = ShipLayoutEnum.Vertical,
                    ShipLength = 2,
                    ShipSymbol = string.Format("Ship %d", i)
                });             
            }
            foreach(Ship s in ships)
            {
                Assert.True(board.PlaceShip(s));
            }
            //Creat one extra ship than the 5 X 5 can fit
            Assert.False(board.PlaceShip(new Ship {
                Layout = ShipLayoutEnum.Vertical,
                ShipLength = 2,
                ShipSymbol = "Ship 11"
            }));
        }

        [Fact]
        public void FireOnAnEmptyBoard()
        {
            Board board = new Board(5);
            Assert.Equal(5, board.BoardDimensions);
            Assert.True(board.Id > 0);
            var boardPanel = new BoardPanel
            {
                X = 1,
                Y = 1
            };
            Assert.False(board.Fire(boardPanel));
        }

        [Fact]
        public void FillBoardWithShipsAndFire()
        {
            Board board = new Board(5);
            Assert.Equal(5, board.BoardDimensions);
            Assert.True(board.Id > 0);
            var ships = new List<Ship>();
            for (int i = 0; i < 10; i++)
            {
                ships.Add(new Ship
                {
                    Layout = ShipLayoutEnum.Horizontal,
                    ShipLength = 2,
                    ShipSymbol = string.Format("Ship %d", i)
                });
            }
            foreach (Ship s in ships)
            {
                Assert.True(board.PlaceShip(s));
            }
            var boardPanel = new BoardPanel
            {
                X = 1,
                Y = 1
            };

            Assert.True(board.Fire(boardPanel));
        }
    }
}
