using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimonTathamsPortablePuzzleCollection.Games.Fifteen
{
    class FifteenGameController
    {
        public List<List<int>> Board = new List<List<int>>();


        FifteenTileButton EmptySquare;

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


        public FifteenGameController(int row, int col)
        {
            RowCount = row;
            ColCount = col;
            EmptySquare = new FifteenTileButton(row-1, col-1);
            SetupBoard();
            ShuffleBoard();
        }

        private void SetupBoard()
        {
            int cellValue = 1;
            Board.Clear();
            EmptySquare.Row = RowCount-1;
            EmptySquare.Col = ColCount-1;
            
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
                if (EmptySquare.Row + i >= 0 && EmptySquare.Row + i < RowCount)
                {
                    validMoves.Add(new List<int>() { EmptySquare.Row + i, EmptySquare.Col });
                }

            }
            for (int i = -1; i <= 1; i += 2)
            {
                if (EmptySquare.Col + i >= 0 && EmptySquare.Col + i < ColCount)
                {
                    validMoves.Add(new List<int>() { EmptySquare.Row, EmptySquare.Col + i });
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
            Board[Clickedrow][Clickedcol] = Board[EmptySquare.Row][EmptySquare.Col];
            Board[EmptySquare.Row][EmptySquare.Col] = tmp;
            EmptySquare.Row = Clickedrow;
            EmptySquare.Col = Clickedcol;
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
