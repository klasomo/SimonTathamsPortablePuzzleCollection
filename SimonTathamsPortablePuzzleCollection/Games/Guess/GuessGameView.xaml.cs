using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

namespace SimonTathamsPortablePuzzleCollection.Games.Guess
{
    /// <summary>
    /// Interaction logic for GuessGameView.xaml
    /// </summary>
    public partial class GuessGameView : UserControl, IGame
    {
        ColorConverter ColCon= new ColorConverter();
        private GuessBoard Guess;
        private bool isSolved = false;
        private List<string> options = new List<String>() { "Standard", "Super"};

        private string thumbnailPath = "../../Games/Guess/Thumbnail_Guess.png";
        private string saveFilePath = "../../Saves/Guess/";
        private string gameTitle = "Guess";
        private string gameInfo = "Try to guess the hidden combination of colours. You will be given limited information about each guess you make, enabling you to refine the next guess.";

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
        public void NewGame()
        {
            isSolved = false;

            Guess = new GuessBoard(Guess.RowCount, Guess.ColCount, Guess.AmountOfColors);
            DisplayBoard(Guess.RowCount, Guess.ColCount, Guess.AmountOfColors);
        }
        public void SolveGame()
        {
            DisplayHiddenCode(true);
            DisplayGameSolved();
        }
        public void LoadGame()
        {
            SaveFileWindow LoadWindow = new SaveFileWindow(SaveFilePath, Guess.GetType(), Guess);
            LoadWindow.ShowDialog();
            Guess = new GuessBoard((GuessBoard)LoadWindow.gameControllerObject);

            DisplayBoard(Guess.RowCount, Guess.ColCount, Guess.AmountOfColors);
        }

        public void SaveGame()
        {
            SaveFileWindow SaveWindow = new SaveFileWindow(SaveFilePath, Guess.GetType(), Guess);
            SaveWindow.ShowDialog();
        }

        public void ChangeType(string selectedOption)
        {
            switch (selectedOption)
            {
                case "Standard":
                    Guess = new GuessBoard(10, 4, 6);
                    break;
                case "Super":
                    Guess = new GuessBoard(12, 5, 8);
                    break;
            }
            DisplayBoard(Guess.RowCount, Guess.ColCount, Guess.AmountOfColors);
        }

        private void ShowToolBar()
        {
            ToolBarView _toolBar = new ToolBarView(SolveGame, NewGame, LoadGame, SaveGame, options);
            DockPanel.SetDock(_toolBar, Dock.Top);
            _toolBar.EventGameTypeChanged += ChangeType;
            MainDockPanel.Children.Add(_toolBar);
        }


        public GuessGameView()
        {
            Guess = new GuessBoard(10, 4, 6);
            InitializeComponent();
            ShowToolBar();
           
            
            NewGame();
        }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                            Display 
        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        /// <summary>
        /// Calls the Displayfunctions to print the whole game.
        /// ColorPalette, MainGame and ValidationArea (in the window from left to right)
        /// </summary>
        /// <param name="rows">Sets the amount of rows of the playground.</param>
        /// <param name="cols">Sets the amount of columns of the playground.</param>
        /// <param name="colors">Sets the amount of colors of the ColorPalette</param>
        public void DisplayBoard(int rows, int cols, int colors)
        {
            DisplayColorPalette(colors);
            DisplayMainGame(rows, cols);
            DisplayValidationArea(cols);
        }

        public void DisplayColorPalette(int amount)
        {
            LBpalette.Items.Clear();
            Brush tmpColor;
            for (int i = 0; i < amount; i++)
            {
                tmpColor = (Brush)new BrushConverter().ConvertFrom(GuessColorPalette.ColorPalette[i]);

                Ellipse tmpEllipse = new Ellipse();
                tmpEllipse.Fill = tmpColor;
                tmpEllipse.Width = 30;
                tmpEllipse.Height = 30;
                LBpalette.Items.Add(tmpEllipse);
            }
        }
        public void DisplayMainGame(int row, int col)
        {
            CreateGrid(row, col, GridPlayground);
            CreateGrid(1, col, GridHiddenCode);

            DisplayPlayground();
            DisplayHiddenCode(false);
        }
        public void DisplayPlayground()
        {
            for (int i = 0; i < Guess.RowCount; i++)
            {
                DisplayPlaygroundRow(i);
            }
            DisplayCurrentRow();
        }
        public void DisplayPlaygroundRow(int row)
        {
            for (int col = 0; col < Guess.ColCount; col++)
            {
                PlaygroundEllipse playgroundEllipse = new PlaygroundEllipse(row, col);
                playgroundEllipse.Click += new RoutedEventHandler(BtnPlayground_Click);

                Border BtnBorder = new Border();
                BtnBorder.CornerRadius = new CornerRadius(15);
                BtnBorder.Background = (Brush)new BrushConverter().ConvertFrom(Guess.Playground[row][col]);
                BtnBorder.Width = 30;
                BtnBorder.Height = 30;

                playgroundEllipse.Content = BtnBorder;
                playgroundEllipse.Background = Brushes.Transparent;

                Grid.SetRow(playgroundEllipse, row);
                Grid.SetColumn(playgroundEllipse, col);

                GridPlayground.Children.Add(playgroundEllipse);
            }
        }
        public void DisplayValidationArea(int amount)
        {
            GuessValidation.Children.Clear();

            for (int row = 0; row < Guess.RowCount; row++)
            {
                Grid GridValidation = new Grid();
                GridValidation.MouseDown += new MouseButtonEventHandler(GridValidation_MouseDown);
                GridValidation.Margin = new Thickness(2);

                CreateGrid(2, (int)Math.Ceiling(Guess.ColCount / 2.0), GridValidation);

                for (int col = 0; col < Guess.ColCount; col++)
                {
                    int tmpRow, tmpCol;

                    Border ValidationBorder = new Border();
                    ValidationBorder.CornerRadius = new CornerRadius(7.5);
                    ValidationBorder.Width = 15;
                    ValidationBorder.Height = 15;
                    ValidationBorder.Background = (Brush)new BrushConverter().ConvertFrom(Guess.ValidationArea[row][col]);

                    if (col < (int)Math.Ceiling(Guess.ColCount / 2.0))
                    {
                        //the rounded amount of columns from the playground belong to the first row
                        tmpRow = 0;
                    }
                    else
                    {
                        //the rest belongs to the second row
                        tmpRow = 1;
                    }

                    //arrangement of the ellipses
                    tmpCol = col - tmpRow * (int)Math.Ceiling(Guess.ColCount / 2.0);

                    Grid.SetRow(ValidationBorder, tmpRow);
                    Grid.SetColumn(ValidationBorder, tmpCol);
                    GridValidation.Children.Add(ValidationBorder);
                }

                GuessValidation.Children.Add(GridValidation);
            }
        }
        /// <summary>
        /// Shows the hidden code which the player have to guess.
        /// reveal == false -> the code will not be revealed
        /// reveal == true -> the code will be displayed
        /// </summary>
        /// <param name="reveal">Decides if we should reveal the code or not.</param>
        public void DisplayHiddenCode(bool reveal)
        {
            GridHiddenCode.Children.Clear();

            for (int i = 0; i < Guess.ColCount; i++)
            {
                Border BtnBorder = new Border();
                BtnBorder.Margin = new Thickness(2);
                BtnBorder.CornerRadius = new CornerRadius(15);
                BtnBorder.Width = 30;
                BtnBorder.Height = 30;

                if (reveal)
                {
                    //the code will be shown
                    BtnBorder.Background = (Brush)new BrushConverter().ConvertFrom(Guess.HiddenCode[i]);
                }
                else
                {
                    //the code will be not shown
                    BtnBorder.Background = Brushes.Gray;
                }

                Grid.SetRow(BtnBorder, 0);
                Grid.SetColumn(BtnBorder, i);
                GridHiddenCode.Children.Add(BtnBorder);
            }
        }

        /// <summary>
        /// Shows the player in which row he has to place his guess
        /// </summary>
        public void DisplayCurrentRow()
        {
            for (int col = 0; col < Guess.ColCount; col++)
            {
                if (Guess.CurrrentRow != 0)
                    ((PlaygroundEllipse)GridPlayground.Children[(Guess.CurrrentRow - 1) * Guess.ColCount + col]).Background = Brushes.Transparent;
                ((PlaygroundEllipse)GridPlayground.Children[Guess.CurrrentRow * Guess.ColCount + col]).Background = Brushes.CornflowerBlue;
            }
        }

        /// <summary>
        /// When the player finishes the game the board will be locked, so he can't click anything and have to start a new game.
        /// </summary>
        public void DisplayGameSolved()
        {
            MessageBox.Show("Start new Game");
            isSolved = true;
        }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                        Update 
        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        /// <summary>
        /// If the player selected the colors of the current row and wants to validate his guess this function gives feedback.
        /// This function
        /// </summary>
        /// <param name="row"></param>
        public void UpdateValidationArea()
        {
            List<int> CheckList = CheckRow();
            int CorrectPositions = CheckList[0];            //black - right colour in the right place
            int CorrectColors = CheckList[1];               //white - right colours but in the wrong place
            Grid tmp = (Grid)GuessValidation.Children[Guess.CurrrentRow];

            //draw the borders in the right color
            int col = 0;
            foreach (Border border in tmp.Children)
            {
                
                if (CorrectPositions > 0)
                {
                    //black - right colour in the right place
                    border.Background = Brushes.Black;
                    CorrectPositions--;
                }
                else if (CorrectColors > 0)
                {
                    //white - right colours but in the wrong place
                    border.Background = Brushes.LightGray;
                    CorrectColors--;
                }
                Guess.ValidationArea[Guess.CurrrentRow][col] = ColCon.ConvertToString(border.Background);
                col++;
            }

            //after each row we have to proof if the game is solved
            CheckGameSolved(CheckList[0]);
            Guess.CurrrentRow++;
        }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                        Create 
        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        public void CreateGrid(int row, int cols, Grid tmpGrid)
        {
            tmpGrid.Children.Clear();

            for (int i = 0; i < cols; i++)
            {
                ColumnDefinition Column = new ColumnDefinition();
                tmpGrid.ColumnDefinitions.Add(Column);
            }
            for (int j = 0; j < row; j++)
            {
                RowDefinition Row = new RowDefinition();
                tmpGrid.RowDefinitions.Add(Row);
            }
        }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                            Check 
        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        /// <summary>
        /// Determinates correct guesses.
        /// </summary>
        /// <returns>A List which gives informations about the amount of correct Position-guesses and Color-guesses</returns>
        public List<int> CheckRow()
        {
            List<int> CheckList = new List<int>();
            bool[] CorrectPositions = new bool[Guess.ColCount];     //stores if we recorded a black hit
            bool[] CorrectColors = new bool[Guess.ColCount];        //stores if we recorded a white hit

            int PositionHits = 0;
            int ColorHits = 0;

            //compare for PositionHits
            for (int i = 0; i < Guess.ColCount; i++)
            {
                //compare the playground with the code in same location to see if PositionHit
                if (Guess.HiddenCode[i] == Guess.Playground[Guess.CurrrentRow][i])
                {
                    PositionHits++;
                    CorrectPositions[i] = true;
                    continue;
                }
            }

            //compare for ColorHits
            for (int i = 0; i < Guess.ColCount; i++)
            {
                //if guess generated a PositionHit, no need to see if it will be a ColorHit
                if (CorrectPositions[i])
                    continue;

                for (int j = 0; j < Guess.ColCount; j++)
                {
                    //already checked
                    if (j == i)
                        continue;
                    //no CorrectPosition and no CorrectColor at this index and the same color --> must be a ColorHit
                    if (!CorrectPositions[j] && !CorrectColors[j] && Guess.HiddenCode[j] == Guess.Playground[Guess.CurrrentRow][i])
                    {
                        ColorHits++;
                        CorrectColors[j] = true;
                        break;
                    }
                }
            }

            CheckList.Add(PositionHits);
            CheckList.Add(ColorHits);

            return CheckList;
        }

        /// <summary>
        /// If the amount of correct positions is equal to the amount of the columns the game is solved.
        /// </summary>
        /// <param name="correctPositions"></param>
        public void CheckGameSolved(int correctPositions)
        {
            if (correctPositions == Guess.ColCount)
            {
                MessageBox.Show("You won!!");
                DisplayHiddenCode(true);
                DisplayGameSolved();
            }
            else if (Guess.CurrrentRow >= (Guess.RowCount - 1) && correctPositions != Guess.ColCount)
            {
                MessageBox.Show("You lost!!");
                DisplayHiddenCode(true);
                DisplayGameSolved();
            }
        }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                        Events 
        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        /// <summary>
        /// Fills the selected Plagroundellipse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnPlayground_Click(Object sender, RoutedEventArgs e)
        {
            if (!isSolved)
            {
                PlaygroundEllipse Btn = sender as PlaygroundEllipse;

                if (Btn.Row != Guess.CurrrentRow)
                {
                    return;
                }
                if(LBpalette.SelectedItem != null)
                {
                    ((Border)Btn.Content).Background = ((Ellipse)LBpalette.SelectedItem).Fill;
                    Guess.Playground[Btn.Row][Btn.Col] = ColCon.ConvertToString(((Border)Btn.Content).Background);
                }
            }
        }
        /// <summary>
        /// Inizialises the validation of the current guessing row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GridValidation_MouseDown(Object sender, RoutedEventArgs e)
        {
            if (!isSolved)
            {
                bool isIncompleteRow = false;

                //only when the row has been completely filled it possible to start the validation
                for (int i = 0; i < Guess.ColCount; i++)
                {
                    if (Guess.Playground[Guess.CurrrentRow][i] == ColCon.ConvertToString(Brushes.Gray))
                        isIncompleteRow = true;
                }

                if (!isIncompleteRow)
                    UpdateValidationArea();

                DisplayCurrentRow();
            }
        }

    }
}
