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

namespace SimonTathamsPortablePuzzleCollection.Games.MineSweeper
{
    /// <summary>
    /// Interaktionslogik für MineSweeperGameView.xaml
    /// </summary>
    public partial class MineSweeperGameView : UserControl, IGame
    {
        private string thumbnailPath = "../../Games/MineSweeper/Thumbnail_MineSweeper.png";


        private string gameTitle = "MineSweeper";
        private string gameInfo = "MineSweeperInfo";
        private string saveFilePath = "../../Saves/MineSweeper/";

        private List<string> options = new List<String>() { "Beginner", "Intermediate", "Expert"};
        public string SaveFilePath
        {
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
            board.SolveGame();
            UpdateBoard();
        }

        public void NewGame()
        {
            board.NewGame();
            UpdateBoard();
        }

        public void LoadGame()
        {
            SaveFileWindow LoadWindow = new SaveFileWindow(SaveFilePath, board.GetType(), board);
            LoadWindow.ShowDialog();
            board = new MineSweeperBoard((MineSweeperBoard)LoadWindow.gameControllerObject);

            UpdateBoard();
        }

        public void SaveGame()
        {
            SaveFileWindow SaveWindow = new SaveFileWindow(SaveFilePath, board.GetType(), board);
            SaveWindow.ShowDialog();
        }

        public void ChangeType(string selectedOption)
        {
            switch (selectedOption)
            {
                case "Beginner":
                    board = new MineSweeperBoard(9, 9, 10);
                    break;
                case "Intermediate":
                    board = new MineSweeperBoard(16, 16, 40);
                    break;
                case "Expert":
                    board = new MineSweeperBoard(30, 16, 99);
                    break;
 
            }
            DisplayBoard();
        }

        private void ShowToolBar()
        {
            ToolBarView _toolBar = new ToolBarView(SolveGame, NewGame, LoadGame, SaveGame, options);
            _toolBar.EventGameTypeChanged += ChangeType;
            DockPanel.SetDock(_toolBar, Dock.Top);
            MainDockPanel.Children.Add(_toolBar);
        }

        MineSweeperBoard board;


        public MineSweeperGameView()
        {
            board = new MineSweeperBoard(16, 16, 40);
            InitializeComponent();
            ShowToolBar();
            DisplayBoard();
        }

        public void DisplayBoard()
        {
            CreateGrid(board.RowCount, board.ColCount);
            UpdateBoard();
        }

        public void CreateGrid(int rows, int cols)
        {
            GridMineSweeperGame.Children.Clear();
            GridMineSweeperGame.RowDefinitions.Clear();
            GridMineSweeperGame.ColumnDefinitions.Clear();

            for (int i = 0; i < cols; i++)
            {
                ColumnDefinition Column = new ColumnDefinition();
                GridMineSweeperGame.ColumnDefinitions.Add(Column);

            }
            for (int j = 0; j < rows; j++)
            {
                RowDefinition row = new RowDefinition();
                GridMineSweeperGame.RowDefinitions.Add(row);
            }
        }

        public void UpdateBoard()
        {

            GridMineSweeperGame.Children.Clear();
            for (int r = 0; r < board.RowCount; r++)
            {
                for (int c = 0; c < board.ColCount; c++)
                {
                    SetTile(r, c);
                }
            }
        }


        private void SetTile(int r, int c)
        {
            MineSweeperTileButton btn = new MineSweeperTileButton(r, c);
            if (board.Board[r][c].isRevealed)
            {
                int cellValue = board.Board[r][c].value;
                btn.Background = Brushes.White;
                switch (cellValue)
                {
                    case -1:
                        Image bomb = new Image() { Source = new BitmapImage(new Uri("../../Games/MineSweeper/Assets/Mine.png", UriKind.Relative))};
                        btn.Content = bomb;
                        break;
                    case 0:
                        floodFill(r, c);
                        break;
                    case 1:
                        btn.Foreground = Brushes.Blue;
                        break;
                    case 2:
                        btn.Foreground = Brushes.Green;
                        break;
                    case 3:
                        btn.Foreground = Brushes.Red;
                        break;
                    case 4:
                        btn.Foreground = Brushes.DarkBlue;
                        break;
                    case 5:
                        btn.Foreground = Brushes.Brown;
                        break;
                    case 6:
                        btn.Foreground = Brushes.Cyan;
                        break;
                    case 7:
                        btn.Foreground = Brushes.Black;
                        break;
                    case 8:
                        btn.Foreground = Brushes.DarkGray;
                        break;
                }
                if(cellValue != -1 && cellValue != 0)
                {
                    btn.Content = cellValue.ToString();
                }
                    
                
            }
            else
            {
                if (board.Board[r][c].isFlagMarked)
                {
                    Image flag = new Image() { Source = new BitmapImage(new Uri("../../Games/MineSweeper/Assets/Flag.png", UriKind.Relative)) };
                    btn.Content = flag;
                }
                else
                {
                    btn.Click += new RoutedEventHandler(Tile_Click);
                }
                btn.MouseRightButtonUp += new MouseButtonEventHandler(Tile_RightClick);
            }
            GridMineSweeperGame.Children.Add(btn);
            Grid.SetRow(btn, r);
            Grid.SetColumn(btn, c);
        }

        private void UpdateTile(int row, int col)
        {
            for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for (int colOffset = -1; colOffset <= 1; colOffset++)
                {
                    if (row + rowOffset > -1 && row + rowOffset < board.Board.Count && col + colOffset > -1 && col + colOffset < board.Board[0].Count)
                    {
                        SetTile(row + rowOffset, col + colOffset);
                    }
                }
            }
        }

        private void floodFill(int row, int col)
        {
            for(int rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for(int colOffset = -1; colOffset <= 1; colOffset++)
                {
                    if (row + rowOffset > -1 && row + rowOffset < board.RowCount && col + colOffset > -1 && col + colOffset < board.ColCount)
                    {
                        if (board.Board[row + rowOffset][col + colOffset].value != -1 && board.Board[row + rowOffset][col + colOffset].isRevealed == false)
                        {
                            board.Board[row + rowOffset][col + colOffset].isRevealed = true;
                            UpdateTile(row, col);
                        }
                    }
                }
            }  
        }


        public void Tile_Click(object sender, RoutedEventArgs e)
        {
            MineSweeperTileButton btn = sender as MineSweeperTileButton;
            if(!board.Board[btn.Row][btn.Col].isRevealed)
            {
                board.Board[btn.Row][btn.Col].isRevealed = true;
                UpdateBoard();
                if (board.Board[btn.Row][btn.Col].value == -1)
                {
                    MessageBox.Show("You Lost");
                }
            }
        }

        public void Tile_RightClick(object sender, RoutedEventArgs e)
        {
            MineSweeperTileButton btn = sender as MineSweeperTileButton;
            if (!board.Board[btn.Row][btn.Col].isRevealed)
            {
                board.Board[btn.Row][btn.Col].isFlagMarked= !board.Board[btn.Row][btn.Col].isFlagMarked;
            }
            UpdateBoard();
        }
    }
}
