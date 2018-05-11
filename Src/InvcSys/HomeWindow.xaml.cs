using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InvcSys.ViewController;

namespace InvcSys
{
    /// <summary>
    /// HomeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HomeWindow : Window
    {
        
        public HomeWindow()
        {
            InitializeComponent();
            this.Loaded += HomeWindow_Loaded;
        }

        
        void HomeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.tabBlue.Content = new BlueViewController();
            this.tabRed.Content = new RedViewController();
            this.tabQuery.Content = new QueryViewController();
            this.tabTax.Content = new TaxViewController();
            mcGridMask = this.gridMask;
            mcTbxStatus = this.tbxDrawStatus;
            gridMask.Visibility = Visibility.Collapsed;

            PiaotongHelper.OnHttpPost += PiaotongHelper_OnHttpPost;
            // 启动api服务
            try
            {
                ApiServer.Start();  
            }
            catch(Exception eStart)
            {
                MessageBox.Show(eStart.Message);
                return;
            }
            DataManager.RegistHomeWindow(this);
        }

        void PiaotongHelper_OnHttpPost(string obj)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.tbxDrawStatus.Text = obj;
            });
            Thread.Sleep(3000);
            this.Dispatcher.Invoke(() =>
            {
                this.gridMask.Visibility = Visibility.Collapsed;
            });
        }
        /// <summary>
        /// 实现假边框拖拽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn_exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        static Grid mcGridMask;
        static TextBlock mcTbxStatus;
        internal static void ShowDrawStatus()
        {
            mcGridMask.Visibility = System.Windows.Visibility.Visible;
            mcTbxStatus.Text = "正在发送开票请求...";
        }
    }
}
