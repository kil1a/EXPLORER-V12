using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class UCActionMenu : UserControl
    {
        public UCActionMenu()
        {
            InitializeComponent();
            Color background = Color.FromArgb(0, 28, 8, 221);
            ActionMenu.Background = new SolidColorBrush(background);
        }

        public void ShowCreateFileDialogWindow(object sender, EventArgs e)
        {
            ActionMenu.Visibility = Visibility.Collapsed;
            CreateFileDialog.Visibility = Visibility.Visible;
        }

        public void ShowCreateFolderDialogWindow(object sender, EventArgs e)
        {
            ActionMenu.Visibility = Visibility.Collapsed;
            CreateFolderDialog.Visibility = Visibility.Visible;
        }
    }
}
