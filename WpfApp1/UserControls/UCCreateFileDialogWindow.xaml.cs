using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Model;

namespace WpfApp1
{
    public partial class UCCreateFileDialogWindow : UserControl
    {
        public UCCreateFileDialogWindow()
        {
            InitializeComponent();
            Color background = Color.FromArgb(11, 67, 127, 200);
            CreateFileWindowCanvas.Background = new SolidColorBrush(background);
        }

        public void Cancel(object sender, EventArgs e)
        {
           Visibility = Visibility.Collapsed;
        }

        public void CreateNewFile(object sender, EventArgs e)
        {
            FileSystemService.CreateFile(NewFileName.Text, MenuExaption.Header.ToString());
        }

        public void SelectExaption(object sender, EventArgs e)
        {
            Label label = sender as Label;
            if (label != null)
            {
                MenuExaption.Header = label.Content;
            }
        }
    }
}
