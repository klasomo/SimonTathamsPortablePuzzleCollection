using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimonTathamsPortablePuzzleCollection.Games.MineSweeper
{
    class MineSweeperBoard
    {
        public List<List<MineSweeperTile>> Board = new List<List<MineSweeperTile>>();
        public int RowCount { get; private set; }
        public int ColCount { get; private set; }
        public int MineCount { get; private set; }

        public void ResetBoard()
        {

        }
    }
}
