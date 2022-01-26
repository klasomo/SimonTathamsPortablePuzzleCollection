using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimonTathamsPortablePuzzleCollection.Games
{
    interface IGame
    {
        Image Thumbnail { get;  set; }
        string GameTitle { get; set; }
        string GameInfo { get; set; }
        string SaveFilePath { get; set; }

        void NewGame();
        void SolveGame();
    }
}
