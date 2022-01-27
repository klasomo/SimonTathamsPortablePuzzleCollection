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

namespace SimonTathamsPortablePuzzleCollection.Games.Sudoku
{
    /// <summary>
    /// Interaktionslogik für SudokuGameView.xaml
    /// </summary>
    public partial class SudokuGameView : UserControl, IGame
    {
        SudokuBoard SBoard;
        List<List<SudokuTileButton>> ButtonList = new List<List<SudokuTileButton>>();
        SudokuTileButton Previous = new SudokuTileButton(0, 0, 0);
        private string thumbnailPath = "../../Games/Sudoku/Thumbnail_Sudoku.png";


        private string gameTitle = "Sudoku";
        private string gameInfo = "Fill missing Numbers";
        private string saveFilePath = "../../Saves/Sudoku/";
        private List<string> options = new List<String>() { "Super Easy", "Easy", "Normal", "Difficult", "Hard", "Expert", "Ridiculous", "HAHA", "Empty" };
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



        public SudokuGameView()
        {
            SBoard = new SudokuBoard();
            InitializeComponent();
            ShowToolBar();
            
            SBoard.GenerateSudoku();
            DisplayBoard(9, 9);
        }


        public void DisplayBoard(int rows, int cols)
        {
            CreateGrid(rows, cols);
            CreateTiles(rows, cols);
        }
        public void CreateGrid(int rows, int cols)
        {
            GridSudokuGame.Children.Clear();
            GridSudokuGame.RowDefinitions.Clear();
            GridSudokuGame.ColumnDefinitions.Clear();
            for (int i = 0; i < cols; i++)
            {
                ColumnDefinition Column = new ColumnDefinition();
                GridSudokuGame.ColumnDefinitions.Add(Column);

            }
            for (int j = 0; j < rows; j++)
            {
                RowDefinition row = new RowDefinition();
                GridSudokuGame.RowDefinitions.Add(row);
            }
        }

        /// <summary>
        /// Creates the colored Tiles
        /// </summary>
        /// <param name="rows">Number of rows</param>
        /// <param name="cols">Number of columns</param>
        public void CreateTiles(int rows, int cols)
        {
            ButtonList.Clear();
            for (int r = 0; r < rows; r++)
            {
                ButtonList.Add(new List<SudokuTileButton>());
                for (int c = 0; c < cols; c++)
                {
                    SudokuTileButton btn = new SudokuTileButton(r, c, 0);
                    if (SBoard.Board[r][c] == 0)
                    {
                        btn.Text = "";
                    }
                    else if (SBoard.Board[r][c] > 0)
                    {
                        btn.Text = SBoard.Board[r][c].ToString();
                        btn.Foreground = Brushes.Black;
                    }
                    else
                    {
                        btn.Text = (-SBoard.Board[r][c]).ToString();
                        btn.Foreground = Brushes.Red;
                    }

                    if (((c / 3) % 2 == 1) && (((r / 3) == 0) || ((r / 3) == 2)))
                        btn.Background = System.Windows.Media.Brushes.LightGray;
                    if (((c / 3) % 2 == 0) && ((r / 3) == 1))
                        btn.Background = System.Windows.Media.Brushes.LightGray;
                    btn.TextAlignment = TextAlignment.Center;
                    btn.Height = 50;
                    btn.Width = 50;
                    btn.VerticalContentAlignment = VerticalAlignment.Center;
                    btn.FontWeight = FontWeights.Bold;
                    btn.FontSize = 18;
                    btn.KeyUp += new KeyEventHandler(SudokuTastenEingabe);
                    btn.MouseEnter += new MouseEventHandler(SudokuMouseHover);
                    btn.MouseLeave += new MouseEventHandler(SudokuNotMouseHover);
                    ButtonList[r].Add(btn);
                    GridSudokuGame.Children.Add(btn);
                    Grid.SetRow(btn, r);
                    Grid.SetColumn(btn, c);
                }
            }
        }
        /// <summary>
        /// Lets you Write in the Sudoku Board
        /// </summary>
        /// <param name="sender">The Tile that has been written into</param>
        /// <param name="e"></param>
        private void SudokuTastenEingabe(object sender, KeyEventArgs e)
        {
            SudokuTileButton btn = sender as SudokuTileButton;
            int CellValue = 0;
            if (btn.Text == "1" || btn.Text == "2" || btn.Text == "3" || btn.Text == "4" || btn.Text == "5" || btn.Text == "6" || btn.Text == "7" || btn.Text == "8" || btn.Text == "9")
            {
                CellValue = Convert.ToInt32(btn.Text);
                if (SBoard.CheckLegalMove(btn.Col, btn.Row, Convert.ToInt32(btn.Text)))
                {
                    btn.Foreground = Brushes.Black;
                    SBoard.Board[btn.Row][btn.Col] = CellValue;
                }
                else
                {
                    btn.Foreground = Brushes.Red;
                    SBoard.Board[btn.Row][btn.Col] = -CellValue;
                }
                if (SBoard.CheckWinningCondition())
                {
                    //TODO: Game Win Screen
                }
            }
            else
                btn.Text = "";

        }
        /// <summary>
        /// Highlights the Tile that the Mouse hovers on and the cross.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SudokuMouseHover(object sender, MouseEventArgs e)
        {
            SudokuTileButton btn = sender as SudokuTileButton;
            for (int i = 0; i < 9; i++)
            {
                ButtonList[i][btn.Col].Background = Brushes.LightSkyBlue;
                ButtonList[btn.Row][i].Background = Brushes.LightSkyBlue;

            }
            Previous = btn;
        }




        /// <summary>
        /// Paints the Tiles Back to its original colour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SudokuNotMouseHover(object sender, MouseEventArgs e)
        {
            SudokuTileButton btn = sender as SudokuTileButton;
            if (Previous.Col == btn.Col)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (Previous.Row / 3 == 1)
                    {
                        if (i / 3 == 1)
                        {
                            ButtonList[Previous.Row][i].Background = Brushes.White;
                        }
                        else
                        {
                            ButtonList[Previous.Row][i].Background = Brushes.LightGray;
                        }
                    }
                    else
                    {
                        if (i / 3 == 1)
                        {
                            ButtonList[Previous.Row][i].Background = Brushes.LightGray;
                        }
                        else
                        {
                            ButtonList[Previous.Row][i].Background = Brushes.White;
                        }
                    }
                }
            }
            if (Previous.Row == btn.Row)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (Previous.Col / 3 == 1)
                    {
                        if (i / 3 == 1)
                        {
                            ButtonList[i][Previous.Col].Background = Brushes.White;
                        }
                        else
                        {
                            ButtonList[i][Previous.Col].Background = Brushes.LightGray;
                        }
                    }
                    else
                    {
                        if (i / 3 == 1)
                        {
                            ButtonList[i][Previous.Col].Background = Brushes.LightGray;
                        }
                        else
                        {
                            ButtonList[i][Previous.Col].Background = Brushes.White;
                        }
                    }
                }
            }

        }


        public void NewGame()
        {
            SBoard.GenerateSudoku();
            CreateTiles(9, 9);
        }

        public void SolveGame()
        {
            SBoard.SolveBoard();
        }

        public void LoadGame()
        {
            SaveFileWindow LoadWindow = new SaveFileWindow(SaveFilePath, SBoard.GetType(), SBoard);
            LoadWindow.ShowDialog();
            SBoard = new SudokuBoard((SudokuBoard)LoadWindow.gameControllerObject);

            CreateTiles(9, 9);
        }

        public void SaveGame()
        {
            SaveFileWindow SaveWindow = new SaveFileWindow(SaveFilePath, SBoard.GetType(), SBoard);
            SaveWindow.ShowDialog();
        }

        public void ChangeType(string selectedOption)
        {

            SBoard.Difficulty = options.IndexOf(selectedOption) + 1;
            SBoard.GenerateSudoku();
            CreateTiles(9, 9);
        }


        private void ShowToolBar()
        {
            ToolBarView _toolBar = new ToolBarView(SolveGame, NewGame, LoadGame, SaveGame, options);
            DockPanel.SetDock(_toolBar, Dock.Top);
            _toolBar.EventGameTypeChanged += ChangeType;
            MainDockPanel.Children.Add(_toolBar);
        }
    }
}
