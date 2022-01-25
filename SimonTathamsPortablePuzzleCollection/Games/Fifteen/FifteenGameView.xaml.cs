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

namespace SimonTathamsPortablePuzzleCollection.Games.Fifteen
{
    /// <summary>
    /// Interaktionslogik für FifteenGameView.xaml
    /// </summary>
    public partial class FifteenGameView : UserControl
    {
        private FifteenGameController Fifteen;

        public FifteenGameView()
        {
            Fifteen = new FifteenGameController(4, 4);
            InitializeComponent();
            CreateGrid(4,4);
            UpdateBoard();
        }

        

        public void CreateGrid(int rows, int cols)
        {
            for (int i = 0; i < cols; i++)
            {
                ColumnDefinition Column = new ColumnDefinition();
                GridFifteenBoard.ColumnDefinitions.Add(Column);

            }
            for (int j = 0; j < rows; j++)
            {
                RowDefinition row = new RowDefinition();
                GridFifteenBoard.RowDefinitions.Add(row);
            }
        }

        public void UpdateBoard()
        {
            GridFifteenBoard.Children.Clear();
            for (int r = 0; r < Fifteen.RowCount; r++)
            {
                for (int c = 0; c < Fifteen.ColCount; c++)
                {
                    SetTile(r, c);
                }
            }
        }

        private void SetTile(int r, int c)
        {
            if (Fifteen.Board[r][c] == Fifteen.RowCount*Fifteen.ColCount)
            {
                return;
            }
            FifteenTileButton btn = new FifteenTileButton(r, c);

            btn.Content = Fifteen.Board[r][c].ToString();

            btn.Click += new RoutedEventHandler(Square_Click);

            GridFifteenBoard.Children.Add(btn);
            Grid.SetRow(btn, r);
            Grid.SetColumn(btn, c);
        }

        public void Square_Click(Object sender, RoutedEventArgs e)
        {
            FifteenTileButton ClickedTile = sender as FifteenTileButton;
            Fifteen.ClickCell(ClickedTile.Row, ClickedTile.Col);
            UpdateBoard();
        }

    }
}
