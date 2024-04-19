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
using WpfApp1.Model;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для UCDownloadImage.xaml
    /// </summary>
    public partial class UCDownloadImage : UserControl
    {
        public UCDownloadImage()
        {
            InitializeComponent();
        }

        public void DownloadImage(object o, EventArgs e)
        {
            FileSystemService.DownloadImage("https://searchthisweb.com/wallpaper/thumb/main_memory_2560x1440_oj0sa.jpg", @"C:\images\ad.jpg");
        }
    }
}
