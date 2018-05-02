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
using System.Diagnostics;

namespace Startup
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public string RESV_Name_ID { get; set; }
        private void btn_start(object sender, RoutedEventArgs e)
        {
            
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "InvcSys.exe"; //启动的应用程序名称  
            startInfo.Arguments = RESV_Name_ID;
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            Process.Start(startInfo);  
        }
    }
}
