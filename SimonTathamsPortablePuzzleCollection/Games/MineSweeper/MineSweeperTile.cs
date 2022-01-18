using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimonTathamsPortablePuzzleCollection.Games.MineSweeper
{
    class MineSweeperTile
    {
        public enum CellValue
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
        public bool isRevealed { get; set; }
        public int value { get; set; }

        public bool isFlagMarked { get; set; }

        public MineSweeperTile()
        {
            isRevealed = false;
            value = 0;
            isFlagMarked = false;
        }
    }
}
