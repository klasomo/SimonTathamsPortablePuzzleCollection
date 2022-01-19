using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SimonTathamsPortablePuzzleCollection.Games.Flip
{
    class FlipBoardTileButton: Button
    {
        private int row;
        private int col;

        public int Row
        {
            get { return row; }
            set { row = value; }
        }
        public int Col
        {
            get { return col; }
            set { col = value; }
        }

        public FlipBoardTileButton(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}
