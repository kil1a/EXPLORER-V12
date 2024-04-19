using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using WpfApp1.Model;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            GetFilesInDirectory();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        bool isVisible = false;
        int margin = 10;
        public void GetFilesInDirectory()
        {
            Wrap.Children.Clear();
            FileSystemService.GetFilesInDirectory();
        }

        public void GoToParentDirectory(object sender, EventArgs e)
        {
            FileSystemService.GoToParentDirectory();
        }

        public void AddToWrap(string name, string type)
        {
            if (type == "file")
            {
                var userControl = new UCFile(name);
                userControl.Margin = new Thickness(margin);
                Wrap.Children.Add(userControl);
            }
            if (type == "folder")
            {
                var userControl = new UCFolder(name);
                userControl.Margin = new Thickness(margin);
                Wrap.Children.Add(userControl);
            }
            else if (type == "drive")
            {
                var userControl = new UCDrive(name);
                userControl.Margin = new Thickness(margin);
                Wrap.Children.Add(userControl);
            }
        }

        public void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        public void MinimizeWindow(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        public void MaximizedWindow(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
                WindowState = WindowState.Maximized;
        }

        public void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        public void VisibleFavoriteWrap(object sender, MouseButtonEventArgs e)
        {
            if (Wrap.Visibility == Visibility.Visible)
            {
                Wrap.Visibility = Visibility.Collapsed;
                FavoriteWrap.Visibility = Visibility.Visible;
            }
            else
            {
                Wrap.Visibility = Visibility.Visible;
                FavoriteWrap.Visibility = Visibility.Collapsed;
            }
        }

        public void ShowCreateFileDialogWindow(object sender, EventArgs e)
        {
            CreateFileDialog.Visibility = Visibility.Visible;
        }

        public void ShowCreateFolderDialogWindow(object sender, EventArgs e)
        {
            CreateFolderDialog.Visibility = Visibility.Visible;
        }

        public void ChekBoxVisible(object sender, EventArgs e)
        {
            DeleteLabel.Visibility = Visibility.Visible;
            foreach (var child in Wrap.Children)
            {
                if (child is UCFile file)
                {
                    // Здесь вы можете изменить свойство в каждом UserControl
                    file.CBDelete.Visibility = Visibility.Visible;
                }
                else if (child is UCFolder folder)
                {
                    // Здесь вы можете изменить свойство в каждом UserControl
                    folder.CBDelete.Visibility = Visibility.Visible;
                }
            }
        }

        public void DeleteFunc(object o, EventArgs args)
        {
            FileSystemService.DeleteFromList();
        }

        public void SelectExaption()
        {
            
        }

        public void ShowDownloadImage(object sender, EventArgs e)
        {
            ShowDownloadImage.Visibility = Visibility.Visible;
        }
    }
}
