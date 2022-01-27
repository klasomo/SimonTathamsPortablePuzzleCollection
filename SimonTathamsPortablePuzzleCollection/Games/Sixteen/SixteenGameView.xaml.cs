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

namespace SimonTathamsPortablePuzzleCollection.Games.Sixteen
{
    /// <summary>
    /// interaction logistics for SixteenGameView.xaml
    /// </summary>
    public partial class SixteenGameView : UserControl, IGame
    {
        #region IGameImplementation
        private string thumbnailPath = "../../Games/Sixteen/Thumbnail_Sixteen.png";
        private string saveFilePath = "../../Saves/Sixteen/";
        private string gameTitle = "Sixteen";
        private string gameInfo = "In this Game you have a frid with numbers in it. In the end of the game they have to be in the right order, but you can only move a hole row or a hole column. Good Luck!";
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
        #endregion
        SixteenGameController Sixteen;
        public SixteenGameView()
        {
            Sixteen = new SixteenGameController(4, 4);
            InitializeComponent();
            ShowToolBar();
            DisplayBoard(Sixteen.RowCount, Sixteen.ColCount);
        }

        /// <summary>
        /// displays the hole board on the window
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        public void DisplayBoard(int rows, int cols)
        {
            Sixteen.ShuffleBoard();
            CreateGrid(rows, cols);
            CreateTiles(rows, cols);
        }
        /// <summary>
        /// creates the grid with its row and columns
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        public void CreateGrid(int rows, int cols)
        {
            GridSixteenBoard.Children.Clear();
            GridSixteenBoard.RowDefinitions.Clear();
            GridSixteenBoard.ColumnDefinitions.Clear();

            for (int i = 0; i < cols + 2; i++)
            {
                ColumnDefinition Column = new ColumnDefinition();
                GridSixteenBoard.ColumnDefinitions.Add(Column);

            }
            for (int j = 0; j < rows + 2; j++)
            {
                RowDefinition row = new RowDefinition();
                GridSixteenBoard.RowDefinitions.Add(row);
            }
        }

        /// <summary>
        /// Creates the single fields of the game including the numbers
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        public void CreateTiles(int rows, int cols)
        {
            GridSixteenBoard.Children.Clear();
            for (int r = 0; r < rows + 2; r++)
            {
                for (int c = 0; c < cols + 2; c++)
                {
                    SixteenArrowButton btn = null;

                    if (r == 0 && c != 0 && c != cols + 1) //Arrow Up
                    {
                        btn = new SixteenArrowButton(0, c - 1, 1, 0);
                        btn.Content = "5";
                    }
                    else if (r == rows + 1 && c != 0 && c != cols + 1) //Arrow Down
                    {
                        btn = new SixteenArrowButton(Sixteen.RowCount, c - 1, -1, 0);
                        btn.Content = "6";
                    }
                    else if (c == 0 && r != rows + 1 && r != 0) //Arrow Left
                    {
                        btn = new SixteenArrowButton(r - 1, 0, 0, 1);
                        btn.Content = "3";
                    }
                    else if (c == cols + 1 && r != rows + 1 && r != 0) //Arrow Right
                    {
                        btn = new SixteenArrowButton(r - 1, Sixteen.ColCount, 0, -1);
                        btn.Content = "4";
                    }

                    if (btn != null) // wenn ein Arrow geklickt worden ist
                    {
                        btn.FontSize = 40;  //Pfeilgröße
                        btn.FontFamily = new FontFamily("Marlett");
                        btn.Click += new RoutedEventHandler(Arrow_Click);
                        GridSixteenBoard.Children.Add(btn);
                        Grid.SetRow(btn, r);
                        Grid.SetColumn(btn, c);
                    }
                    else if (r == 0 || r == rows + 1 || c == 0 || c == cols + 1) // Die 4 Ecken
                    {
                        //conrer Piece
                    }
                    else
                    {
                        TextBlock number = new TextBlock();
                        number.FontSize = 20;
                        number.VerticalAlignment = VerticalAlignment.Center;
                        number.HorizontalAlignment = HorizontalAlignment.Center;
                        //Zahl die momentagn drin steht
                        //number.Text = (r * rows + c).ToString();
                        number.Text = Sixteen.Board[r - 1][c - 1].ToString();
                        GridSixteenBoard.Children.Add(number);
                        Grid.SetRow(number, r);
                        Grid.SetColumn(number, c);
                    }
                }
            }
        }

        /// <summary>
        /// checks if a button has been clicked 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Arrow_Click(object sender, RoutedEventArgs e)
        {
            SixteenArrowButton btn = sender as SixteenArrowButton;
            if (btn.VerticalShift == 0)
            {
                Sixteen.ShiftRow(btn.Row, btn.HorizontalShift);
                CreateTiles(Sixteen.RowCount, Sixteen.ColCount);
            }
            else
            {
                Sixteen.ShiftColumn(btn.Col, btn.VerticalShift);
                CreateTiles(Sixteen.RowCount, Sixteen.ColCount);
            }
        }
        public void SolveGame()
        {
            Sixteen.SolveBoard();
            CreateTiles(Sixteen.RowCount, Sixteen.ColCount);
        }




        /// <summary>
        /// Checks wether the numbers are in the correct order and displays a message if the player has won
        /// </summary>
        public void CheckGameSolved()
        {
            int counter = 1;
            for (int i = 0; i < Sixteen.RowCount; i++)
            {
                for (int j = 0; j < Sixteen.ColCount; j++)
                {
                    if (Sixteen.Board[i][j] == counter)
                    {
                        counter++;
                    }
                }
            }
            if (counter == Sixteen.RowCount*Sixteen.ColCount+1)
                MessageBox.Show("Gewonnen! ☜(ﾟヮﾟ☜)");
        }



        /// <summary>
        /// Creates a new Game with new shuffled numbers
        /// </summary>
        public void NewGame()
        {
            Sixteen.ShuffleBoard();
            CreateTiles(Sixteen.RowCount, Sixteen.ColCount);
        }

        public void LoadGame()
        {
            SaveFileWindow LoadWindow = new SaveFileWindow(SaveFilePath, Sixteen.GetType(), Sixteen);
            LoadWindow.ShowDialog();
            Sixteen = new SixteenGameController((SixteenGameController)LoadWindow.gameControllerObject);

            CreateTiles(Sixteen.RowCount, Sixteen.ColCount);
        }

        public void SaveGame()
        {
            SaveFileWindow SaveWindow = new SaveFileWindow(SaveFilePath, Sixteen.GetType(), Sixteen);
            SaveWindow.ShowDialog();
        }

        public void ChangeType(string selectedOption)
        {
            switch (selectedOption)
            {
                case "4x4":
                    Sixteen = new SixteenGameController(4, 4);
                    break;
                case "5x5":
                    Sixteen = new SixteenGameController(5, 5);
                    break;
                case "6x6":
                    Sixteen = new SixteenGameController(6, 6);
                    break;
                case "7x7":
                    Sixteen = new SixteenGameController(7, 7);
                    break;
            }
            DisplayBoard(Sixteen.RowCount, Sixteen.ColCount);
        }

        private void ShowToolBar()
        {
            ToolBarView _toolBar = new ToolBarView(SolveGame, NewGame, LoadGame, SaveGame, options);
            _toolBar.EventGameTypeChanged += ChangeType;
            DockPanel.SetDock(_toolBar, Dock.Top);
            MainDockPanel.Children.Add(_toolBar);
        }

    }
}


