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
    public partial class FlipGameView : UserControl
    {
        FlipBoard board = null;
        public FlipGameView()
        {
            InitializeComponent();
            board = new FlipBoard(20, 20);
            SetupGrid();
            UpdateGrid();
        }

        private void SetupGrid()
        {
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

        private void UpdateGrid()
        {
            for(int row = 0; row< board.RowCount; row++)
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
