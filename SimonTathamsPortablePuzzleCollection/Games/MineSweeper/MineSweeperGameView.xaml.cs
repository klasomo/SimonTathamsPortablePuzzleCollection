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
            board = new MineSweeperBoard(16, 16, 35);
            InitializeComponent();
            DisplayBoard(16, 16);
        }

        public void DisplayBoard(int rows, int cols)
        {
            CreateGrid(rows, cols);
            UpdateBoard();
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

        public void UpdateBoard()
        {
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
                if (cellValue == 0)
                {
                    floodFill(r, c);
                }
                else
                {
                    btn.Content = cellValue.ToString();
                }
                
            }
            else
            {
                if (board.Board[r][c].isFlagMarked)
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
                            Console.WriteLine($"{row + rowOffset}, {col + colOffset}");
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
