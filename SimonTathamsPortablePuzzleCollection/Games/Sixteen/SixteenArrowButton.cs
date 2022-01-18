using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SimonTathamsPortablePuzzleCollection.Games.Sixteen
{
    class SixteenArrowButton : Button
    {
        int verticalShift;

        int VerticalShift {
            get
            {
                return verticalShift;
            }
            set
            {
                verticalShift = value;
            }          
        }

        int horizontalShift;

        int HorizontalShift
        {
            get
            {
                return horizontalShift;
            }
            set
            {
                horizontalShift = value;
            }
        }

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


        public SixteenArrowButton(int row, int col,int verticalShift = 0, int horizontalShift = 0)
        {
            Row = row;
            Col = col;
            VerticalShift = verticalShift;
            HorizontalShift = horizontalShift;
        }
    }
}
