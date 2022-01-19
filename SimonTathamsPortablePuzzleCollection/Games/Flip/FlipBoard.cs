using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SimonTathamsPortablePuzzleCollection.Games.Flip
{
    class FlipBoard
    {
        public List<List<bool>> board = new List<List<bool>>();
        public int RowCount { get; set; }
        public int ColCount { get; set; }


        public FlipBoard(int row, int col)
        {
            RowCount = row;
            ColCount = col;

            SetupBoard();
            ShuffleBoard();
        }

        private void SetupBoard()
        {
            for (int row = 0; row < RowCount; row++)
            {
                board.Add(new List<bool>());
                for (int col = 0; col < ColCount; col++)
                {
                    board[row].Add(false);
                }
            }
        }

        public void FlipCell(int row, int col)
        {
            board[row][col] = !board[row][col];
        }

        public SolidColorBrush getCellColor(int row, int col)
        {
            if (board[row][col])
            {
                return Brushes.White;
            }
            return Brushes.Black;
        }

        private void ShuffleBoard()
        {
            var rand = new Random();
            int StepCounter = ColCount * RowCount;
            for (int i = 0; i < StepCounter; i++)
            {
                int col = rand.Next(0, ColCount);
                int row = rand.Next(0, RowCount);

                CellClick(row, col);
            }
        }

        public void CellClick(int row, int col)
        {
            for(int offset=-1; offset <= 1; offset += 2)
            {
                if(row+offset >= 0 && row + offset < RowCount)
                {
                    board[row + offset][col] = !board[row + offset][col];
                }
                if (col + offset >= 0 && col + offset < ColCount)
                {
                    board[row][col+offset] = !board[row][col + offset];
                }
            }
            board[row][col] = !board[row][col];
        }


        public bool checkBoardisSolved()
        {
            bool isWinning = true;
            for(int row = 0; row < RowCount; row++)
            {
                for(int col = 0; col< ColCount; col++)
                {
                    isWinning = isWinning & board[row][col];
                    if (isWinning == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
