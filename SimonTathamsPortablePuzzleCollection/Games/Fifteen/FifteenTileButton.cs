using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SimonTathamsPortablePuzzleCollection.Games.Fifteen
{
    class FifteenTileButton : Button
    {

        public int Row;
        public int Col;

        public FifteenTileButton(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}
