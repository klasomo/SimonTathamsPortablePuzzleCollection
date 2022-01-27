using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimonTathamsPortablePuzzleCollection.Games.Sixteen
{
    [Serializable()]
    class SixteenGameController
    {
        public List<List<int>> Board = new List<List<int>>();

        public int RowCount;
        public int ColCount;

        public SixteenGameController(int row, int col)
        {
            RowCount = row;
            ColCount = col;
            SolveBoard();
        }
        public SixteenGameController()
        {

        }
        public SixteenGameController(SixteenGameController _gameObject)
        {
            this.RowCount = _gameObject.RowCount;
            this.ColCount = _gameObject.ColCount;
            this.Board = _gameObject.Board;
        }

        public void SolveBoard()
        {
            Board.Clear();
            int counter = 1;
            for(int i = 0; i < RowCount; i++)
            {
                Board.Add(new List<int>());
                for(int j = 0; j< ColCount; j++)
                {
                    Board[i].Add(counter);
                    counter++;
                }
            }
        }


        /// <summary>
        /// randomizes the numbers of the board
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        public void ShuffleBoard()
        {
            Random random = new Random();
            int lengthRow = ColCount;

            for (int i = 0; i < RowCount * ColCount; i++)
            {
                int i0 = i / lengthRow;
                int i1 = i % lengthRow;

                int j = random.Next(i + 1);
                int j0 = j / lengthRow;
                int j1 = j % lengthRow;

                int temp = Board[i0][i1];
                Board[i0][i1] = Board[j0][j1];
                Board[j0][j1] = temp;
            }
        }

        /// <summary>
        /// moves the row where the player has moved the row
        /// </summary>
        /// <param name="row"></param>
        /// <param name="horizontalShift"></param>
        public void ShiftRow(int row, int horizontalShift)
        {
            if (horizontalShift > 0)
            {
                int firstNumber = Board[row][0];
                for (int i = 0; i < RowCount; i++)
                {
                    if (i + 1 < RowCount)
                        Board[row][i] = Board[row][i + 1];
                    else if (i + 1 >= RowCount)
                        Board[row][i] = firstNumber;
                }
            }
            else
            {
                int firstNumber = Board[row][ColCount - 1];
                for (int i = RowCount - 1; i >= 0; i--)
                {
                    if (i - 1 >= 0)
                        Board[row][i] = Board[row][i - 1];
                    else if (i - 1 < 0)
                        Board[row][i] = firstNumber;
                }
            }
        }


        /// <summary>
        /// moves the column where the player has clicked the button
        /// </summary>
        /// <param name="col"></param>
        /// <param name="verticalShift"></param>
        public void ShiftColumn(int col, int verticalShift)
        {
            if (verticalShift > 0)
            {
                int firstNumber = Board[0][col];
                for (int i = 0; i < ColCount; i++)
                {
                    if (i + 1 < ColCount)
                        Board[i][col] = Board[i + 1][col];
                    else if (i + 1 >= ColCount)
                        Board[i][col] = firstNumber;
                }
            }
            else
            {
                int firstNumber = Board[RowCount - 1][col];
                for (int i = ColCount - 1; i >= 0; i--)
                {
                    if (i - 1 >= 0)
                        Board[i][col] = Board[i - 1][col];
                    else if (i - 1 < 0)
                        Board[i][col] = firstNumber;
                }
            }
        }
    }
}
