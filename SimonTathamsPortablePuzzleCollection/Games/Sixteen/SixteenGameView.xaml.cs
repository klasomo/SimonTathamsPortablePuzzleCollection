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

namespace SimonTathamsPortablePuzzleCollection.Games.Sixteen
{
    /// <summary>
    /// Interaktionslogik für SixteenGameView.xaml
    /// </summary>
    public partial class SixteenGameView : UserControl, IGame
    {
        private Image thumbnail = new Image() { Source = new BitmapImage(new Uri("../../Games/Sixteen/Thumbnail_Sixteen.png", UriKind.Relative))};

        private string gameTitle = "Sixteen";
        private string gameInfo = "SixteenInfo";

        public Image Thumbnail
        {
            get
            {
                return thumbnail;
            }
            set
            {
                thumbnail = value;
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

        public void SolveGame()
        {

        }

        public void NewGame()
        {

        }
        public SixteenGameView()
        {
            InitializeComponent();
            DisplayBoard(7, 6);
        }

        public void DisplayBoard(int rows, int cols)
        {
            CreateGrid(rows, cols);
            CreateTiles(rows, cols);
        }

        public void CreateGrid(int rows, int cols)
        {
            for (int i = 0; i < cols+2; i++)
            {
                ColumnDefinition Column = new ColumnDefinition();
                GridSixteenBoard.ColumnDefinitions.Add(Column);

            }
            for (int j = 0; j < rows+2; j++)
            {
                RowDefinition row = new RowDefinition();
                GridSixteenBoard.RowDefinitions.Add(row);
            }
        }


        public void CreateTiles(int rows, int cols)
        {
            for (int r = 0; r < rows+2; r++)
            {

                for (int c = 0; c < cols+2; c++)
                {
                    SixteenArrowButton btn = null;
                    
                    if (r == 0 && c != 0 &&  c!=cols+1) //Arrow Up
                    {
                        btn = new SixteenArrowButton(0, r, 1, 0);
                        btn.Content = "5";
                    }
                    else if (r == rows + 1 && c!=0 && c!=cols+1) //Arrow Down
                    {
                        btn = new SixteenArrowButton(0, r, -1, 0);
                        btn.Content = "6";
                    }
                    else if (c == 0 && r!=rows+1 && r!=0) //Arrow Left
                    {
                        
                        btn = new SixteenArrowButton(c, 0, 0, 1);
                        btn.Content = "3";

                    }
                    else if(c == cols + 1 && r!=rows+1 && r!=0) //Arrow Right
                    {
                
                        btn = new SixteenArrowButton(0, r, 0, -1);
                        btn.Content = "4";
                    }

                    if(btn != null) //Wenn ein Arrow Gecklickt worden ist
                    {
                        btn.FontSize = 40; //Arrow Size
                        btn.FontFamily = new FontFamily("Marlett");
                        btn.Click += new RoutedEventHandler(Arrow_Click);
                        GridSixteenBoard.Children.Add(btn);
                        Grid.SetRow(btn, r);
                        Grid.SetColumn(btn, c);
                        
                    }else if(r==0 || r==rows+1 || c==0|| c==cols+1) //Die 4 Ecken
                    {
                        //conrer Piece
                    }
                    else
                    {
                        TextBlock number = new TextBlock();
                        number.FontSize = 20;
                        number.VerticalAlignment = VerticalAlignment.Center;
                        number.HorizontalAlignment = HorizontalAlignment.Center;

                        //Zahl die momentan drin steht
                        number.Text = (r * rows + c).ToString();
                        

                        GridSixteenBoard.Children.Add(number);
                        Grid.SetRow(number, r);
                        Grid.SetColumn(number, c);
                    }

                }
            }
        }


        public void Arrow_Click(object sender, RoutedEventArgs e)
        {
            SixteenArrowButton btn = sender as SixteenArrowButton;
            int testRow = btn.Row;
            int testCol = btn.Col;

        }
    }
}
