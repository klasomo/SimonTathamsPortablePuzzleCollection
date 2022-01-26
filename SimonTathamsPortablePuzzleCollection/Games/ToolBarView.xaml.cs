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
    /// <summary>
    /// Interaktionslogik für ToolBarView.xaml
    /// </summary>
    public partial class ToolBarView : UserControl
    {
        public delegate void BtnDelegate();

        BtnDelegate solveGame;
        BtnDelegate newGame;
        BtnDelegate loadGame;
        BtnDelegate saveGame;
        public ToolBarView(Action solveGameAction, Action newGameAction, Action loadGameAction, Action saveGameAction)
        {
            
            InitializeComponent();
            solveGame = new BtnDelegate(solveGameAction);
            newGame = new BtnDelegate(newGameAction);
            loadGame = new BtnDelegate(loadGameAction);
            saveGame = new BtnDelegate(saveGameAction);
        }

        private void BtnMainMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSaveGame_Click(object sender, RoutedEventArgs e)
        {
            saveGame();
        }

        private void BtnLoadGame_Click(object sender, RoutedEventArgs e)
        {
            loadGame();
        }

        private void BtnSolveGame_Click(object sender, RoutedEventArgs e)
        {
            solveGame();
        }

        private void BTnType_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void BtnNewGame_Click(object sender, RoutedEventArgs e)
        {
            newGame();
        }
    }
}
