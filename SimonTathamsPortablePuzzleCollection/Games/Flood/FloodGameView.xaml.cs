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

namespace SimonTathamsPortablePuzzleCollection.Games.Flood
{
    /// <summary>
    /// Interaktionslogik für FloodGameView.xaml
    /// </summary>
    public partial class FloodGameView : UserControl, IGame
    {

        FloodGameControll Flood;

        private List<string> options = new List<String>() { "5x5","10x10", "15x15" };

        private string thumbnailPath = "../../Games/Flood/Thumbnail_Flood.png";
        private string saveFilePath = "../../Saves/Flood/";
        private string gameTitle = "Flood";
        private string gameInfo = "Try to Fill the entire Board with one color";

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


        public FloodGameView()
        {
            Flood = new FloodGameControll(10, 10);
            InitializeComponent();
            ShowToolBar();
            CreateGrid();
            UpdateBoard();
        }

        public void CreateGrid()
        {
            GridFloodBoard.Children.Clear();
            GridFloodBoard.RowDefinitions.Clear();
            GridFloodBoard.ColumnDefinitions.Clear();
            for (int i = 0; i < Flood.ColCount; i++)
            {
                ColumnDefinition Column = new ColumnDefinition();
                GridFloodBoard.ColumnDefinitions.Add(Column);

            }
            for (int j = 0; j < Flood.RowCount; j++)
            {
                RowDefinition row = new RowDefinition();
                GridFloodBoard.RowDefinitions.Add(row);
            }
        }

        public void UpdateBoard()
        {
            GridFloodBoard.Children.Clear();
            for (int r = 0; r < Flood.RowCount; r++)
            {
                for (int c = 0; c < Flood.ColCount; c++)
                {
                    SetTile(r, c);
                }
            }
        }

        private void SetTile(int r, int c)
        {
            Rectangle Square = new Rectangle();
            
            Square.Height = GridFloodBoard.Height/Flood.RowCount;
            Square.Width = GridFloodBoard.Width/Flood.ColCount;
            Square.Fill = (Brush)new BrushConverter().ConvertFrom(Flood.Board[r][c]);

            Square.MouseDown += new MouseButtonEventHandler(Square_Click);

            GridFloodBoard.Children.Add(Square);
            Grid.SetRow(Square, r);
            Grid.SetColumn(Square, c);
        }

        public void Square_Click(Object sender, RoutedEventArgs e)
        {
            Rectangle ClickedRectangle = sender as Rectangle;
            Flood.FloodFill(0,0,Flood.Board[0][0], new ColorConverter().ConvertToString(ClickedRectangle.Fill));
            UpdateBoard();
        }

        public void NewGame()
        {
            Flood = new FloodGameControll(Flood.RowCount, Flood.ColCount);
            UpdateBoard();
        }

        public void SolveGame()
        {
            Flood.SolveBoard();
            UpdateBoard();
        }

        public void LoadGame()
        {
            SaveFileWindow LoadWindow = new SaveFileWindow(SaveFilePath, Flood.GetType(), Flood);
            LoadWindow.ShowDialog();
            Flood = new FloodGameControll((FloodGameControll)LoadWindow.gameControllerObject);

            UpdateBoard();
        }

        public void SaveGame()
        {
            SaveFileWindow SaveWindow = new SaveFileWindow(SaveFilePath, Flood.GetType(), Flood);
            SaveWindow.ShowDialog();
        }

        public void ChangeType(string selectedOption)
        {
            string[] values = selectedOption.Split('x');
            Flood = new FloodGameControll(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]));
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
    }
}
