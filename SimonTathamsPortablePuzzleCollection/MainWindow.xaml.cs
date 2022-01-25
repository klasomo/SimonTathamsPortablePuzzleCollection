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
using System.IO;

namespace SimonTathamsPortablePuzzleCollection
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<UserControl> gameList = new List<UserControl>();
        public MainWindow()
        {
            GetThumbnails();
            InitializeComponent();
            
        }

        private void GetThumbnails()
        {
            string folderPath = "../../Games";
            foreach (string file in Directory.GetFiles(folderPath,"Thumbnail*.*",SearchOption.AllDirectories))
            {
                Image Thumbnail = new Image();
                Uri uri = new Uri(file,UriKind.Relative);
                Thumbnail.Source = new BitmapImage(uri);
            }
        }
    }
}
