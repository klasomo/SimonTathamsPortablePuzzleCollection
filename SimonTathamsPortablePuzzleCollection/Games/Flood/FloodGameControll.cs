using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SimonTathamsPortablePuzzleCollection.Games.Flood
{
    [Serializable()]
    class FloodGameControll
    {
        public List<List<String>> Board = new List<List<String>>();
        public int RowCount { get; private set; }
        public int ColCount { get; private set; }

        public List<String> ColorPallet = new List<String>() { "#FFFF0000", "#FFFFFF00", "#FF008000", "#FF0000FF", "#FFFFA500" };

        public FloodGameControll(int row, int col)
        {
            RowCount = row;
            ColCount = col;
            CreateBoard();
        }
        public FloodGameControll()
        {

        }

        public FloodGameControll(FloodGameControll _gameObject)
        {
            this.RowCount = _gameObject.RowCount;
            this.ColCount = _gameObject.ColCount;
            this.Board = _gameObject.Board;
        }

        private void CreateBoard()
        {
            Random rand = new Random();
            for (int i = 0; i < RowCount; i++)
            {
                Board.Add(new List<String>());
                for(int j = 0; j < ColCount; j++)
                {
                    
                    int randIndex = rand.Next(ColorPallet.Count);
                    Board[i].Add(ColorPallet[randIndex]);
                }
            }
        }

        public void FloodFill(int row, int col, String Oldcolor, String NewColor)
        {
            if ((row < 0) || (row >= RowCount))
                return;
            if ((col < 0) || (col >= ColCount))
                return;
            if(Board[row][col] == Oldcolor)
            {
                Board[row][col] = NewColor;
                FloodFill(row + 1, col, Oldcolor, NewColor);
                FloodFill(row, col+1, Oldcolor, NewColor);
                FloodFill(row - 1, col, Oldcolor, NewColor);
                FloodFill(row, col-1, Oldcolor, NewColor);
            }
        }

        public void SolveBoard()
        {
            for(int i =0; i < RowCount; i++)
            {
                for(int j = 0; j < ColCount; j++)
                {
                    Board[i][j] = Board[0][0];
                }
            }
        }
    }
}
