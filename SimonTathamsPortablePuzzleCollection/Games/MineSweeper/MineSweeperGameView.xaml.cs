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

        MineSweeperBoard board;
        public MineSweeperGameView()
        {
            board = new MineSweeperBoard(10, 10, 1);
            InitializeComponent();
            DisplayBoard(board.RowCount-2, board.ColCount-2);
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
        private Visibility BoolToVisibility(bool isVisible)
        {
            if (isVisible)
            {
                return Visibility.Visible;
            }
            return Visibility.Hidden;
        }

        public void CreateTiles(int rows, int cols)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    MineSweeperTileButton btn = new MineSweeperTileButton(r+1, c+1);
                    if(board.Board[r + 1][c + 1].isRevealed)
                    {
                        int cellValue = board.Board[r + 1][c + 1].value;
                        btn.Background = Brushes.White;
                        if (cellValue == 0)
                        {
                            floodFill(r+1, c+1);
                        }
                        else
                        {
                            btn.Content = cellValue.ToString();
                        }
                    }
                    else
                    {
                        if(board.Board[r + 1][c + 1].isFlagMarked)
                        {
                            btn.Background = Brushes.Red;
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
            }
        }

        private void floodFill(int row, int col)
        {
            if(row>0 && row< board.RowCount-1 && col > 0 && col < board.ColCount - 1)
            {
                if (board.Board[row][col].value == 0)
                {
                    board.Board[row][col].isRevealed = true;
                    floodFill(row + 1, col);
                    floodFill(row - 1, col);
                    floodFill(row, col - 1);
                    floodFill(row, col + 1);
                }
                else
                {
                    return;
                }

            }
            return;

        }


        public void Tile_Click(object sender, RoutedEventArgs e)
        {
            MineSweeperTileButton btn = sender as MineSweeperTileButton;
            if(!board.Board[btn.Row][btn.Col].isRevealed)
            {
                board.Board[btn.Row][btn.Col].isRevealed = true;
                CreateTiles(board.RowCount - 2, board.ColCount - 2);
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
            CreateTiles(board.RowCount - 2, board.ColCount - 2);
        }
    }
}
