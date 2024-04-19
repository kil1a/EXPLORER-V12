using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Model;

namespace WpfApp1
{
    public partial class UCCreateFolderDialogWindow : UserControl
    {
        public UCCreateFolderDialogWindow()
        {
            InitializeComponent();
            Color background = Color.FromRgb(11, 67, 127);
            CreateFolderWindowCanvas.Background = new SolidColorBrush(background);
        }

        public void Cancel(object sender, EventArgs E)
        {
            Visibility = Visibility.Collapsed;
        }

        public void CreateNewFolder(object sender, EventArgs e)
        {
            FileSystemService.CreateFolder(NewFolderName.Text);
        }
    }
}
