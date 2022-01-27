using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SimonTathamsPortablePuzzleCollection.Games.Guess
{
    [Serializable()]
    /// <summary>
    /// This class contains the possible colors for the ColorPalette.
    /// The colors red, yellow, green, blue, orange and purple belongs to the level "Standard"
    /// and the level "Super" additionally includes brown and lightblue.
    /// </summary>
    class GuessColorPalette
    {
        public static readonly List<string> ColorPalette = new List<string>() { "#FFFF0000", "#FFFFFF00", "#FF008000", "#FF0000FF", "#FFFFA500", "#FF800080", "#FFBC8F8F", "#FFADD8E6" };

        public GuessColorPalette()
        {

        }
    }
}
