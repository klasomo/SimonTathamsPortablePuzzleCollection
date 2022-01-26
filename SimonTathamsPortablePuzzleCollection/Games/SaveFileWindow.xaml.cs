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
using System.Runtime.Serialization.Formatters.Binary;

namespace SimonTathamsPortablePuzzleCollection.Games
{
    /// <summary>
    /// Interaktionslogik für SaveFileWindow.xaml
    /// </summary>
    public partial class SaveFileWindow : Window
    {
        public Object gameControllerObject { get; set; }
        private Type gameControllerType { get; set; }
        private string saveFilePath { get; set; }
        public SaveFileWindow(string _saveFilePath, Type _gameControllerObjectType, Object GameObjec)
        {
            gameControllerType = _gameControllerObjectType;
            gameControllerObject = GameObjec;

            saveFilePath = _saveFilePath;
            InitializeComponent();
            DisplayFilesFromDirectory(saveFilePath);
            if((new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name == "LoadGame")
            {
                SubmitButton.Click += LoadGame;
                SubmitButton.Content = "Load";
            }
            else
            {
                SubmitButton.Click += SaveGame;
                SubmitButton.Content = "Save";
            }
        }

        private void DisplayFilesFromDirectory(string saveFilePath)
        {
            DirectoryInfo d = new DirectoryInfo(saveFilePath);

            FileInfo[] Files = d.GetFiles("*.dat");

            foreach (FileInfo file in Files)
            {
                DockPanel FileInfo = new DockPanel();

                Rectangle divider = new Rectangle() { Width = 350, Height = 2, Fill = Brushes.Gray };
                DockPanel.SetDock(divider, Dock.Bottom);

                TextBlock fileName = new TextBlock() { FontSize = 15 };
                fileName.Text = (file.Name.Split('.'))[0];
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

        public void ListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DockPanel SelectedDockPanel = (DockPanel)((ListBox)sender).SelectedItem;
            SelectedFileName.Text = ((TextBlock)SelectedDockPanel.Children[1]).Text.ToString();
            
        }

        public void LoadGame(Object sender, RoutedEventArgs e)
        {
            gameControllerObject = SaveLoadFileController.LoadGame(saveFilePath + SelectedFileName.Text + ".dat", gameControllerType);
        }
        public void SaveGame(Object sender, RoutedEventArgs e)
        {
            SaveLoadFileController.SaveGame(saveFilePath+SelectedFileName.Text+".dat", gameControllerObject);
        }
    }
}

