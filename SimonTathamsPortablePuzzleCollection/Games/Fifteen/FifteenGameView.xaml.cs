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
        private string thumbnailPath = "../../Games/Fifteen/Thumbnail_Fifteen.png";
        

        private string gameTitle = "Fifteen";
        private string gameInfo = "FifteenInfo";
        private string saveFilePath = "../../Saves/Fifteen/";
        private List<string> options = new List<String>() {"4x4","5x5","6x6","7x7"};
        public string SaveFilePath {
            get
            {
                return saveFilePath;
            }
            set
            {
                saveFilePath = value;
            }
        }

        public string ThumbnailPath
        {
            get
            {
                return thumbnailPath;
            }
            set
            {
                thumbnailPath = value;
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

        public void LoadGame()
        {
            SaveFileWindow LoadWindow = new SaveFileWindow(SaveFilePath, Fifteen.GetType(),Fifteen);
            LoadWindow.ShowDialog();
            Fifteen = new FifteenGameController((FifteenGameController)LoadWindow.gameControllerObject);
            
            UpdateBoard();
        }

        public void SaveGame()
        {
            SaveFileWindow SaveWindow = new SaveFileWindow(SaveFilePath, Fifteen.GetType(),Fifteen);
            SaveWindow.ShowDialog();
        }

        public void ChangeType(string selectedOption)
        {
            switch (selectedOption)
            {
                case "4x4":
                    Fifteen = new FifteenGameController(4, 4);
                    break;
                case "5x5":
                    Fifteen = new FifteenGameController(5, 5);
                    break;
                case "6x6":
                    Fifteen = new FifteenGameController(6, 6);
                    break;
                case "7x7":
                    Fifteen = new FifteenGameController(7, 7);
                    break;
            }
            UpdateBoard();
        }

        public FifteenGameView()
        { 
            Fifteen = new FifteenGameController(4, 4);
            
            InitializeComponent();
            ShowToolBar();
            UpdateBoard();
        }

        private void ShowToolBar()
        {
            ToolBarView _toolBar = new ToolBarView(SolveGame, NewGame, LoadGame, SaveGame, options);
            _toolBar.EventGameTypeChanged += ChangeType;
            DockPanel.SetDock(_toolBar, Dock.Top);
            MainDockPanel.Children.Add(_toolBar);
        }

        

        public void CreateGrid(int rows, int cols)
        {
            GridFifteenBoard.Children.Clear();
            GridFifteenBoard.ColumnDefinitions.Clear();
            GridFifteenBoard.RowDefinitions.Clear();
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
            CreateGrid(Fifteen.RowCount, Fifteen.ColCount);
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
