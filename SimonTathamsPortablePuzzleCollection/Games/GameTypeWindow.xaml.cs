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
using System.Windows.Shapes;

namespace SimonTathamsPortablePuzzleCollection.Games
{
    /// <summary>
    /// Interaktionslogik für GameTypeWindow.xaml
    /// </summary>
    public partial class GameTypeWindow : Window
    {
        public GameTypeWindow(List<string> givenOptions)
        {
            InitializeComponent();
            ShowGivenOptions(givenOptions);
            //ShowCustomOptions(customOptions);
        }

        private void ShowGivenOptions(List<string> givenOptions)
        {
            for(int i =0; i < givenOptions.Count; i++)
            {
                ListBox LBgivenOptions = new ListBox();
                LBgivenOptions.Height = 100;
                TextBlock option = new TextBlock() { Text = givenOptions[i] };
                LBgivenOptions.Items.Add(option);
            }
        }

        private void ShowCustomOptions(List<string> customOptions)
        {
            StackPanel SPcustomOptions = new StackPanel();
            for (int i= 0; i< customOptions.Count; i++)
            {
                
            }
        }
    }
}
