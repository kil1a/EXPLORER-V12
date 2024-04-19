using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Model;

namespace WpfApp1
{
    public partial class UCFolder : UserControl, INotifyPropertyChanged
    {
        private string _folderName;
        bool isFavorite = false;
        private string path { get; set; }
        public string LabelFolderName
        {
            get { return _folderName; }
            set
            {
                _folderName = value;
                OnPropertyChanged("LabelFolderName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Open(object sender, EventArgs e)
        {
            FileSystemService.OpenDirectory(LabelFolderName);
        }

        public void MouseEnterGridBG(object sender, EventArgs e)
        {
            Color background = Color.FromArgb(24, 99, 233, 219);
            grid.Background = new SolidColorBrush(background);
        }

        public void MouseLeaveGridBG(object sender, EventArgs e)
        {
            Color myColor = Color.FromArgb(0, 0, 0, 0);
            grid.Background = new SolidColorBrush(myColor);
        }

        public UCFolder()
        {
            InitializeComponent();
            DataContext = this;
        }

        public UCFolder(string folderName)
        {
            InitializeComponent();
            DataContext = this;
            LabelFolderName = folderName;
        }

        public void AddToDeleteList(object o, EventArgs args)
        {
            FileSystemService.AddToDeleteList(LabelFolderName);
        }

        public void DeleteFunc(object o, EventArgs args)
        {
            FileSystemService.DeleteFromList();
        }
    }
}
