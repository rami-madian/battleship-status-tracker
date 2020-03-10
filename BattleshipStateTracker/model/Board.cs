using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipStateTracker.model
{
    public class Board
    {
        private List<BoardPanel> gameBoard;
        private Random rand;
        private readonly int HARD_LIMIT = 100;

        public int Id { get; }

        public int BoardDimensions { get; }

        public Board(int size)
        {
            if (size <= 0) throw new Exception("Invalid board size!");
            this.BoardDimensions = size;
            CreateGameBoard();
            rand = new Random(Guid.NewGuid().GetHashCode());
            this.Id = rand.Next();
        }

        public bool PlaceShip(Ship ship)
        {
            bool isOpen = true;
            var tryingCount = 0;
            while (isOpen)
            {
                if (tryingCount++ > HARD_LIMIT)
                {
                    return false;
                }
                var startcolumn = rand.Next(1, BoardDimensions + 1);
                var startrow = rand.Next(1, BoardDimensions + 1);
                int endrow = startrow, endcolumn = startcolumn;
                var orientation = ship.Layout;

                List<int> panelNumbers = new List<int>();
                if (orientation == ShipLayoutEnum.Horizontal)
                {
                    for (int i = 1; i < ship.ShipLength; i++)
                    {
                        endrow++;
                    }
                }
                else
                {
                    for (int i = 1; i < ship.ShipLength; i++)
                    {
                        endcolumn++;
                    }
                }

                //Check for the boundaries of the board
                if (endrow > BoardDimensions || endcolumn > BoardDimensions)
                {
                    isOpen = true;
                    continue; //Restart the while loop to select a new random panel
                }

                //Check if specified panels are occupied
                var affectedPanels = PanelRange(startrow, startcolumn, endrow, endcolumn);
                if (affectedPanels.Any(x => x.Occupation != null))
                {
                    isOpen = true;
                    continue;
                }

                foreach (var panel in affectedPanels)
                {
                    panel.Occupation = ship.ShipSymbol;
                }
                isOpen = false;
            }
            return true;
        }

        public bool Fire(BoardPanel boardPanel)
        {
            var targetPanel = gameBoard.Find(panel => panel.X == boardPanel.X
                                                && panel.Y == boardPanel.Y);
            if (targetPanel != null && targetPanel.Occupation != null)
            {
                targetPanel.Hit = true;
                return true;
            }
            return false;

        }


        private void CreateGameBoard()
        {
            gameBoard = new List<BoardPanel>();
            for (int i = 1; i <= this.BoardDimensions; i++)
            {
                for (int j = 1; j <= this.BoardDimensions; j++)
                {
                    gameBoard.Add(new BoardPanel {
                        X = i, Y = j,
                        Occupation = null,
                        Hit = false
                    });
                }
            }
        }

        private List<BoardPanel> PanelRange(int startRow, int startColumn, int endRow, int endColumn)
        {
            return gameBoard.Where(panel => panel.X >= startRow
                                        && panel.Y >= startColumn
                                        && panel.X <= endRow
                                        && panel.Y <= endColumn).ToList();
        }
    }
}