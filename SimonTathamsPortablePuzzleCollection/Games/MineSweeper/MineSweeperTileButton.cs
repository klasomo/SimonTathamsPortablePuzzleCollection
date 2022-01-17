using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SimonTathamsPortablePuzzleCollection.Games.MineSweeper
{
    public class MineSweeperTileButton : Button
    {
        private int id;
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

        public MineSweeperTileButton(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}
