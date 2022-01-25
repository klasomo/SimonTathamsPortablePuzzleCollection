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
    public partial class FloodGameView : UserControl
    {

        FloodGameControll Flood;
        public FloodGameView()
        {
            Flood = new FloodGameControll(12, 12);
            InitializeComponent();
            CreateGrid(12,12);
            UpdateBoard();
        }

        public void CreateGrid(int rows, int cols)
        {
            for (int i = 0; i < cols; i++)
            {
                ColumnDefinition Column = new ColumnDefinition();
                GridFloodBoard.ColumnDefinitions.Add(Column);

            }
            for (int j = 0; j < rows; j++)
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
            
            Square.Height = 50;
            Square.Width = 50;
            Square.Fill = Flood.Board[r][c];

            Square.MouseDown += new MouseButtonEventHandler(Square_Click);

            GridFloodBoard.Children.Add(Square);
            Grid.SetRow(Square, r);
            Grid.SetColumn(Square, c);
        }

        public void Square_Click(Object sender, RoutedEventArgs e)
        {
            Rectangle ClickedRectangle = sender as Rectangle;
            Flood.FloodFill(0,0,Flood.Board[0][0],ClickedRectangle.Fill);
            UpdateBoard();
        }

        

    }
}
