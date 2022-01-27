using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimonTathamsPortablePuzzleCollection.Games.MineSweeper
{
    [Serializable()]
    class MineSweeperBoard
    {
        public List<List<MineSweeperTile>> Board = new List<List<MineSweeperTile>>();
        public int RowCount { get; private set; }
        public int ColCount { get; private set; }
        public int MineCount { get; private set; }

        public MineSweeperBoard(int row, int col, int mineCount)
        {
            RowCount = row;
            ColCount = col;
            MineCount = mineCount;
            NewGame();
        }

        public MineSweeperBoard()
        {

        }

        public MineSweeperBoard(MineSweeperBoard _gameObject)
        {
            this.RowCount = _gameObject.RowCount;
            this.ColCount = _gameObject.ColCount;
            this.MineCount = _gameObject.MineCount;
            this.Board = _gameObject.Board;
        }

        public void SolveGame()
        {
            for (int i = 0; i < RowCount; i++)
            {
               
                for (int j = 0; j < ColCount; j++)
                {
                    Board[i][j].isRevealed = true;
                }
            }
        }

        public void NewGame()
        {
            SetEmptyBoard();
            DistributeMinesRandomly();
            CalculateCellValues();
        }

        public void SetEmptyBoard()
        {
            Board.Clear();
            for(int i = 0; i < RowCount; i++)
            {
                Board.Add(new List<MineSweeperTile>());
                for(int j = 0; j < ColCount; j++)
                {
                    Board[i].Add(new MineSweeperTile());
                }
            }
        }

        public void CalculateCellValues()
        {
            for(int row = 0; row < RowCount; row++)
            {
                for(int col = 0; col < ColCount; col++)
                {
                    if (Board[row][col].value != -1)
                    {
                        for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
                        {
                            for (int colOffset = -1; colOffset <= 1; colOffset++)
                            {
                                if(row+rowOffset > -1 && row+rowOffset < Board.Count && col+colOffset > -1 && col+colOffset < Board[0].Count)
                                {
                                    if (Board[row + rowOffset][col + colOffset].value == -1)
                                    {
                                        Board[row][col].value++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void DistributeMinesRandomly()
        {
            int minesPlaced = 0;
            Random r = new Random();

            while (minesPlaced < MineCount)
            {
                int row = r.Next(0,Board.Count);
                int col = r.Next(0, Board[0].Count);
                if (Board[row][col].value == 0)
                {
                    Board[row][col].value = -1;
                    minesPlaced++;
                }
            }
        }
    }
}
