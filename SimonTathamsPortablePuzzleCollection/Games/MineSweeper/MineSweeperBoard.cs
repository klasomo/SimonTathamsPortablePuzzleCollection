using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimonTathamsPortablePuzzleCollection.Games.MineSweeper
{
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
            SetEmptyBoard();
            DistributeMinesRandomly();
            CalculateCellValues();
        }

        public void SetEmptyBoard()
        {
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
            for(int row = 1; row < RowCount-1; row++)
            {
                for(int col = 1; col < ColCount-1; col++)
                {
                    if (Board[row][col].value != -1)
                    {
                        for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
                        {
                            for (int colOffset = -1; colOffset <= 1; colOffset++)
                            {
                                if(Board[row + rowOffset][col + colOffset].value == -1)
                                {
                                    Board[row][col].value++;
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
                int row = r.Next(1,Board.Count-1);
                int col = r.Next(1, Board[0].Count-1);
                if (Board[row][col].value == 0)
                {
                    Board[row][col].value = -1;
                    minesPlaced++;
                }
            }
        }
    }
}
