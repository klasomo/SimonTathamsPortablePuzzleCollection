using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimonTathamsPortablePuzzleCollection.Games.Sudoku
{
    [Serializable()]
    class SudokuBoard
    {
        public List<List<int>> Board = new List<List<int>>();
        public int RowCount { get; private set; }
        public int ColCount { get; private set; }
        public int Difficulty { get; set; }

        public void ResetBoard()
        {

        }

        public SudokuBoard()
        {
            InitBoard();
            Difficulty = 4;
        }
        public SudokuBoard(SudokuBoard SBoardCopy)
        {
            this.RowCount = SBoardCopy.RowCount;
            this.ColCount = SBoardCopy.ColCount;
            this.Board = SBoardCopy.Board;
            this.Difficulty = SBoardCopy.Difficulty;
            

        }
        private void InitBoard()
        {
            Board.Clear();
            for(int i = 0; i < 9; i++)
            {
                Board.Add(new List<int>());
                for(int j= 0; j<9; j++)
                {
                    Board[i].Add(0);
                }
            }
        }


        public bool CheckLegalMove(int col, int row, int c)
        {
            for (int i = 0; i < 9; i++)
            {
                //check row  
                if (i != row && Board[i][col] == c)
                    return false;
                //check column  
                if (i != col && Board[row][i] == c)
                    return false;
                //check 3*3 block 
                if (Board[(row / 3) * 3 + (i % 3)][(col / 3) * 3 + (i / 3)] == c && ((row / 3) * 3 + (i % 3) != row || (col / 3) * 3 + (i / 3) != col))
                    return false;
            }
            return true;
        }

        public bool CheckWinningCondition()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Board[i][j] == -1)
                        return false;
                }
            }
            for (int j = 1; j <= 7; j += 3)
            {
                for (int i = 1; i <= 9; i++)
                {
                    if (!CheckLegalMove(j, j,i))
                        return false;
                }
            }
            return true;
        }

        public bool SolveBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Board[i][j] == 0)
                    {
                        for (int c = 1; c <= 9; c++)
                        {
                            if (CheckLegalMove(j, i, c))
                            {
                                Board[i][j] = c;

                                if (SolveBoard())
                                {
                                    return true;
                                }
                                else
                                {
                                    Board[i][j] = 0;
                                }
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }
        public void FillDiagonal()
        {
            Random rand = new Random();
            for (int i = 0; i < 9; i++)
            {
                int j = rand.Next(1, 10);
                if (CheckLegalMove(i, i, j))
                {
                    Board[i][i] = j;
                }
                else
                    i--;
            }
        }
        public void RemoveDigits()
        {
            Random rand = new Random();
            int RemoveNumberCounter = Difficulty * 9;
            while(RemoveNumberCounter > 0)
            {
                int randRow = rand.Next(0, 9);
                int randCol = rand.Next(0, 9);
                if (Board[randRow][randCol] != 0)
                {
                    Board[randRow][randCol] = 0;
                    RemoveNumberCounter--;
                }
            }
        }
        public void ClearBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Board[i][j] = 0;
                }
            }
        }

        public void GenerateSudoku()
        {
            ClearBoard();
            FillDiagonal();
            SolveBoard();
            RemoveDigits();
        }
    }

}
