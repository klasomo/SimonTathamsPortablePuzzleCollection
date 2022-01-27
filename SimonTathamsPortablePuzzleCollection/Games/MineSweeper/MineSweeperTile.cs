using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimonTathamsPortablePuzzleCollection.Games.MineSweeper
{
    [Serializable()]
    class MineSweeperTile
    {
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
