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
using SimonTathamsPortablePuzzleCollection.Games.Fifteen;
using SimonTathamsPortablePuzzleCollection.Games.Sixteen;

namespace SimonTathamsPortablePuzzleCollection
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<IGame> gameList = new List<IGame>() { new FifteenGameView()};
        public MainWindow()
        {
            InitializeComponent();
            //CreateCollectionGrid();

        }

        private void CreateCollectionGrid()
        {
            foreach (IGame usercontrol in gameList)
            {
                StackPanel GameInfo = new StackPanel() { Orientation = Orientation.Vertical, Margin = new Thickness(30)};
                usercontrol.Thumbnail.Style = this.FindResource("ImageCollectionStyle") as Style;
                GameInfo.Children.Add(usercontrol.Thumbnail);

                TextBlock GameName = new TextBlock() { Text = usercontrol.GameTitle };
                GameName.Style = this.FindResource("GameNameCollectionSyle") as Style;
                GameName.TextAlignment = TextAlignment.Center;
                GameInfo.Children.Add(GameName);

                GameCollection.Children.Add(GameInfo);
            }
        }
    }
}
