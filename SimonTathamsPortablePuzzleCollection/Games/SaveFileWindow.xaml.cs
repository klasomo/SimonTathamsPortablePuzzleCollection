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
using System.IO;

namespace SimonTathamsPortablePuzzleCollection.Games
{
    /// <summary>
    /// Interaktionslogik für SaveFileWindow.xaml
    /// </summary>
    public partial class SaveFileWindow : Window
    {
        public SaveFileWindow(string saveFilePath)
        {
            InitializeComponent();
            DisplayFilesFromDirectory(saveFilePath);
        }

        private void DisplayFilesFromDirectory(string saveFilePath)
        {
            DirectoryInfo d = new DirectoryInfo(saveFilePath);

            FileInfo[] Files = d.GetFiles("*.txt");

            foreach (FileInfo file in Files)
            {
                DockPanel FileInfo = new DockPanel();

                Rectangle divider = new Rectangle() { Width = 350, Height = 2, Fill = Brushes.Gray };
                DockPanel.SetDock(divider, Dock.Bottom);

                TextBlock fileName = new TextBlock() { FontSize = 15};
                fileName.Text = file.Name;
                DockPanel.SetDock(fileName, Dock.Left);

                TextBlock fileDate = new TextBlock() { FontSize = 12};
                fileDate.Text = file.LastWriteTime.ToString();
                DockPanel.SetDock(fileDate, Dock.Right);

                TextBlock space = new TextBlock() { Width = 150 };

                FileInfo.Children.Add(divider);
                FileInfo.Children.Add(fileName);
                FileInfo.Children.Add(fileDate);
                FileInfo.Children.Add(space);

                LBFiles.Items.Add(FileInfo);
            }
        }
    }


}
