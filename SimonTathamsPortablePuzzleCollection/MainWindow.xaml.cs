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
using System.IO;
using SimonTathamsPortablePuzzleCollection.Games;
using SimonTathamsPortablePuzzleCollection.Games.Guess;
using SimonTathamsPortablePuzzleCollection.Games.Fifteen;
using SimonTathamsPortablePuzzleCollection.Games.Flip;
using SimonTathamsPortablePuzzleCollection.Games.Sixteen;
using SimonTathamsPortablePuzzleCollection.Games.Sudoku;
using SimonTathamsPortablePuzzleCollection.Games.Flood;

namespace SimonTathamsPortablePuzzleCollection
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<IGame> gameList = new List<IGame>() { new FifteenGameView(), new FlipGameView(), new GuessGameView(), new SudokuGameView(), new SixteenGameView(), new FloodGameView()};

        public MainWindow()
        {
            InitializeComponent();
            ShowMainMenu();
            ToolBarView.EventshowMainMenu += ShowMainMenu;
        }

        private void ShowMainMenu()
        {
            MainGrid.Children.Clear();
            WrapPanel WPGameCollection = new WrapPanel();
            WPGameCollection.Children.Clear();

            foreach (IGame usercontrol in gameList)
            {
                StackPanel GameInfo = new StackPanel() { Orientation = Orientation.Vertical, Margin = new Thickness(30) };
                GameInfo.Children.Clear();
                Image thumbnail = new Image() { Source = new BitmapImage(new Uri(usercontrol.ThumbnailPath, UriKind.Relative)) };
                thumbnail.Style = this.FindResource("ImageCollectionStyle") as Style;
                GameInfo.Children.Add(thumbnail);

                TextBlock GameName = new TextBlock() { Text = usercontrol.GameTitle };
                GameName.Style = this.FindResource("GameNameCollectionSyle") as Style;
                GameName.TextAlignment = TextAlignment.Center;
                GameInfo.Children.Add(GameName);
                GameInfo.MouseDown += GameCollection_Clicked;

                WPGameCollection.Children.Add(GameInfo);
            }
            MainGrid.Children.Add(WPGameCollection);
        }

        private void ShowGameUsercontroll(UserControl gameUserControl)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(gameUserControl);
        }

        private void GameCollection_Clicked(Object sender, MouseButtonEventArgs e)
        {
            StackPanel ClickedGameStackPanel = sender as StackPanel;
            IGame selectedGame = gameList.Find(x => x.GameTitle == ((TextBlock)ClickedGameStackPanel.Children[1]).Text);
            ShowGameUsercontroll((UserControl)selectedGame);
        }
    }
}
