using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimonTathamsPortablePuzzleCollection.Games.Fifteen
{
    [Serializable()]
    class FifteenGameController
    {
        public List<List<int>> Board = new List<List<int>>();

        private int EmptySquareRow;
        private int EmptySquareCol;
        

        public int RowCount;
        public int ColCount;

        public void NewGame()
        {
            SetupBoard();
            ShuffleBoard();
        }

        public void SolveGame()
        {
            SetupBoard();
        }

        public FifteenGameController()
        {

        }

        public FifteenGameController(FifteenGameController gameObject)
        {
            this.ColCount = gameObject.ColCount;
            this.RowCount = gameObject.RowCount;
            this.Board = gameObject.Board;
            this.EmptySquareCol = gameObject.EmptySquareCol;
            this.EmptySquareRow = gameObject.EmptySquareRow;
        }

        public FifteenGameController(int row, int col)
        {
            RowCount = row;
            ColCount = col;
            EmptySquareRow = row - 1;
            EmptySquareCol = col - 1;
            NewGame();
        }

        private void SetupBoard()
        {
            int cellValue = 1;
            Board.Clear();
            EmptySquareRow = RowCount-1;
            EmptySquareCol = ColCount-1;
            
            for(int i = 0; i < RowCount; i++)
            {
                Board.Add(new List<int>());
                for(int j = 0; j< ColCount; j++)
                {
                    Board[i].Add(cellValue);
                    cellValue++;
                }
            }
        }

        private List<List<int>> GetValidMove()
        {
            List<List<int>> validMoves = new List<List<int>>();

            for (int i = -1; i <= 1; i += 2)
            {
                if (EmptySquareRow + i >= 0 && EmptySquareRow + i < RowCount)
                {
                    validMoves.Add(new List<int>() { EmptySquareRow + i, EmptySquareCol });
                }

            }
            for (int i = -1; i <= 1; i += 2)
            {
                if (EmptySquareCol + i >= 0 && EmptySquareCol + i < ColCount)
                {
                    validMoves.Add(new List<int>() { EmptySquareRow, EmptySquareCol + i });
                }

            }
            return validMoves;
        }

        private void ShuffleBoard()
        {
            Random random = new Random();

            for (int i = 0; i < 4*RowCount * ColCount; i++)
            {
                List<List<int>> validMoves = GetValidMove();
                int randomIndex = random.Next(validMoves.Count);
                SlideCell(validMoves[randomIndex][0], validMoves[randomIndex][1]);
            }
        }

        private void SlideCell(int Clickedrow, int Clickedcol)
        {
            int tmp = Board[Clickedrow][Clickedcol];
            Board[Clickedrow][Clickedcol] = Board[EmptySquareRow][EmptySquareCol];
            Board[EmptySquareRow][EmptySquareCol] = tmp;
            EmptySquareRow = Clickedrow;
            EmptySquareCol = Clickedcol;
        }

        public void ClickCell(int row, int col)
        {
            for(int i = -1; i <=1; i += 2)
            {
                if(row+i>=0 && row + i < RowCount)
                {
                    if (Board[row + i][col] == RowCount*ColCount)
                    {
                        SlideCell(row, col);
                    }
                }

            }
            for(int i = -1; i<=1; i += 2)
            {
                if (col + i >= 0 && col + i < ColCount)
                {
                    if (Board[row][col + i] == RowCount * ColCount)
                    {
                        SlideCell(row, col);
                    }
                }

            }
        }  
    }
}
