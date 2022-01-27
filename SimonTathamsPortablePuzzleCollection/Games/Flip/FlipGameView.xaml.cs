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

namespace SimonTathamsPortablePuzzleCollection.Games.Flip
{

    /// <summary>
    /// Interaktionslogik für FlipGameView.xaml
    /// </summary>
    public partial class FlipGameView : UserControl, IGame
    {
        FlipBoard board = null;

        private string thumbnailPath = "../../Games/Flip/Thumbnail_Flip.png";


        private string gameTitle = "Flip";
        private string gameInfo = "FlipInfo";
        private string saveFilePath = "../../Saves/Flip/";
        private List<string> options = new List<String>() { "4x4", "5x5", "6x6", "7x7" };
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
            board = new FlipBoard((FlipBoard)LoadWindow.gameControllerObject);

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
                case "4x4":
                    board = new FlipBoard(4, 4);
                    break;
                case "5x5":
                    board = new FlipBoard(5, 5);
                    break;
                case "6x6":
                    board = new FlipBoard(6, 6);
                    break;
                case "7x7":
                    board = new FlipBoard(7, 7);
                    break;
            }
            UpdateBoard();
        }

        
        public FlipGameView()
        {
            board = new FlipBoard(5, 5);
            InitializeComponent();
            ShowToolBar();
            
            CreateGrid();
            UpdateBoard();
        }

        private void ShowToolBar()
        {
            ToolBarView _toolBar = new ToolBarView(SolveGame, NewGame, LoadGame, SaveGame, options);
            DockPanel.SetDock(_toolBar, Dock.Top);
            _toolBar.EventGameTypeChanged += ChangeType;
            MainDockPanel.Children.Add(_toolBar);
        }

        private void CreateGrid()
        {
            GridFlipGame.RowDefinitions.Clear();
            GridFlipGame.ColumnDefinitions.Clear();
            GridFlipGame.Children.Clear();

            for(int row = 0; row < board.RowCount; row++)
            {
                RowDefinition _row = new RowDefinition();
                GridFlipGame.RowDefinitions.Add(_row);
            }
            for (int col = 0; col < board.ColCount; col++)
            {
                ColumnDefinition _col= new ColumnDefinition();
                GridFlipGame.ColumnDefinitions.Add(_col);
            }
        }

        private void UpdateBoard()
        {
            CreateGrid();
            for (int row = 0; row< board.RowCount; row++)
            {
                for(int col= 0; col< board.ColCount; col++)
                {
                    UpdateCell(row, col);
                }
            }
        }

        private void UpdateCell(int row, int col)
        {
            board.FlipCell(row, col);
            FlipBoardTileButton btn = new FlipBoardTileButton(row, col);
            btn.Background = board.getCellColor(row, col);
            btn.Click += new RoutedEventHandler(Tile_Click);
            GridFlipGame.Children.Add(btn);
            Grid.SetRow(btn, row);
            Grid.SetColumn(btn, col);
        }

        public void Tile_Click(object sender, RoutedEventArgs e)
        {
            FlipBoardTileButton btn = sender as FlipBoardTileButton;
            UpdateCellAndNeighbours(btn.Row, btn.Col);
            if (board.checkBoardisSolved())
            {
                MessageBox.Show("Gewonnen");
            }
        }

        private void UpdateCellAndNeighbours(int row, int col)
        {
            for (int offset = -1; offset <= 1; offset += 2)
            {
                if (row + offset >= 0 && row + offset < board.RowCount)
                {
                    UpdateCell(row + offset, col);
                }
                if (col + offset >= 0 && col + offset < board.ColCount)
                {
                    UpdateCell(row, col + offset);
                }
            }
            UpdateCell(row, col);
        }
    }
}
