using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SimonTathamsPortablePuzzleCollection.Games.Sudoku
{
    [Serializable()]
    public class SudokuTileButton : TextBox
    {
        private int row;
        private int col;
        private int valueButton;

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

        public int ValueButton
        {
            get { return valueButton; }
            set { valueButton = value; }
        }

        public SudokuTileButton(int row, int col, int value)
        {
            Row = row;
            Col = col;
            ValueButton = value;
        }


    }
}
