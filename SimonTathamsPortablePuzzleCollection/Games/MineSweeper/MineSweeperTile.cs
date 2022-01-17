using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimonTathamsPortablePuzzleCollection.Games.MineSweeper
{
    class MineSweeperTile
    {
        private enum CellValue
        {
            Bomb = -1,
            Empty,
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight 
        }
        public bool isRevealed { get; private set; }
        public int value { get; private set; }
    }
}
