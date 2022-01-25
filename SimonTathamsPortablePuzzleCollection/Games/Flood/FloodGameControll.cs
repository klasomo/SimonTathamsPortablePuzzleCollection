using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SimonTathamsPortablePuzzleCollection.Games.Flood
{
    class FloodGameControll
    {
        public List<List<Brush>> Board = new List<List<Brush>>();
        public int RowCount { get; private set; }
        public int ColCount { get; private set; }

        public List<Brush> ColorPallet = new List<Brush>() { Brushes.Red, Brushes.Blue, Brushes.Gray, Brushes.Yellow, Brushes.Orange };

        public FloodGameControll(int row, int col)
        {
            RowCount = row;
            ColCount = col;
            CreateBoard();
        }

        private void CreateBoard()
        {
            Random rand = new Random();
            for (int i = 0; i < RowCount; i++)
            {
                Board.Add(new List<Brush>());
                for(int j = 0; j < ColCount; j++)
                {
                    
                    int randIndex = rand.Next(ColorPallet.Count);
                    Board[i].Add(ColorPallet[randIndex]);
                }
            }
        }

        public void FloodFill(int row, int col, Brush Oldcolor, Brush NewColor)
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

    }
}
