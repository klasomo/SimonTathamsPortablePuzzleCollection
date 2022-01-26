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

namespace SimonTathamsPortablePuzzleCollection.Games.Fifteen
{
    /// <summary>
    /// Interaktionslogik für FifteenGameView.xaml
    /// </summary>
    public partial class FifteenGameView : UserControl, IGame
    {
        private FifteenGameController Fifteen;
        private Image thumbnail = new Image() { Source = new BitmapImage(new Uri("../../Games/Fifteen/Thumbnail_Fifteen.png", UriKind.Relative))};

        private string gameTitle = "Fifteen";
        private string gameInfo = "FifteenInfo";

        public Image Thumbnail
        {
            get
            {
                return thumbnail;
            }
            set
            {
                thumbnail = value;
            }
        }
        public string GameTitle
        {
            get
            {
                return gameTitle;
            }
            set
            {
                gameTitle = value;
            }
        }
        public string GameInfo
        {
            get
            {
                return gameInfo;
            }
            set
            {
                gameInfo = value;
            }
        }

        public void SolveGame()
        {
            Fifteen.SolveGame();
            UpdateBoard();
        }

        public void NewGame()
        {
            Fifteen.NewGame();
            UpdateBoard();
        }

        public FifteenGameView()
        {
            ToolBarView test = new ToolBarView(SolveGame, NewGame);
          
            string directory = Directory.GetCurrentDirectory();
            Fifteen = new FifteenGameController(4, 4);
            InitializeComponent();
            DockPanel.SetDock(test, Dock.Top);
            MainDockPanel.Children.Add(test);
            CreateGrid(4, 4);
            UpdateBoard();
        }

        

        public void CreateGrid(int rows, int cols)
        {
            for (int i = 0; i < cols; i++)
            {
                ColumnDefinition Column = new ColumnDefinition();
                GridFifteenBoard.ColumnDefinitions.Add(Column);

            }
            for (int j = 0; j < rows; j++)
            {
                RowDefinition row = new RowDefinition();
                GridFifteenBoard.RowDefinitions.Add(row);
            }
        }

        public void UpdateBoard()
        {
            GridFifteenBoard.Children.Clear();
            for (int r = 0; r < Fifteen.RowCount; r++)
            {
                for (int c = 0; c < Fifteen.ColCount; c++)
                {
                    SetTile(r, c);
                }
            }
        }

        private void SetTile(int r, int c)
        {
            if (Fifteen.Board[r][c] == Fifteen.RowCount*Fifteen.ColCount)
            {
                return;
            }
            FifteenTileButton btn = new FifteenTileButton(r, c);

            btn.Content = Fifteen.Board[r][c].ToString();

            btn.Click += new RoutedEventHandler(Square_Click);

            GridFifteenBoard.Children.Add(btn);
            Grid.SetRow(btn, r);
            Grid.SetColumn(btn, c);
        }

        public void Square_Click(Object sender, RoutedEventArgs e)
        {
            FifteenTileButton ClickedTile = sender as FifteenTileButton;
            Fifteen.ClickCell(ClickedTile.Row, ClickedTile.Col);
            UpdateBoard();
        }

    }
}
