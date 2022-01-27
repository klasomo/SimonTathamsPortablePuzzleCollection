using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace SimonTathamsPortablePuzzleCollection.Games.Guess
{
    [Serializable()]
    /// <summary>
    /// Defines the Buttons from the Playground.
    /// </summary>
    class PlaygroundEllipse : Button
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

        public PlaygroundEllipse(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}
