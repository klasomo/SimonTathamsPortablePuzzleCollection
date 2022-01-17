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
    public partial class MineSweeperGameView : UserControl
    {
        public MineSweeperGameView()
        {
            InitializeComponent();
            DisplayBoard(10, 10);
        }

        public void DisplayBoard(int rows, int cols)
        {
            CreateGrid(rows, cols);
            CreateTiles(rows, cols);
        }
        public void CreateGrid(int rows, int cols)
        {
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


        public void CreateTiles(int rows, int cols)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    MineSweeperTileButton btn = new MineSweeperTileButton(r, c);
                    btn.Click += new RoutedEventHandler(Tile_Click);
                    GridMineSweeperGame.Children.Add(btn);
                    Grid.SetRow(btn, r);
                    Grid.SetColumn(btn, c);
                }
            }
        }

        public void Tile_Click(object sender, RoutedEventArgs e)
        {
            MineSweeperTileButton btn = sender as MineSweeperTileButton;
            int testRow = btn.Row;
            int testCol = btn.Col;

            
        }
    }
}
