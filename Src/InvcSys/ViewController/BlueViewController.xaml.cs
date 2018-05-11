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
using System.Text.RegularExpressions;

namespace InvcSys.ViewController
{
    /// <summary>
    /// BlueViewController.xaml 的交互逻辑
    /// </summary>
    public partial class BlueViewController : UserControl
    {
        public BlueViewController()
        {
            InitializeComponent();
            this.Loaded += BlueViewController_Loaded;
        }

        void BlueViewController_Loaded(object sender, RoutedEventArgs e)
        {
            mBlueViewModel = new BlueViewModel();
            
            this.DataContext = mBlueViewModel;
            // 向数据管理类注册viewmodel
            DataManager.RegistData(mBlueViewModel);
        }
        BlueViewModel mBlueViewModel;

        private void btn_ElectronicBlue(object sender, RoutedEventArgs e)
        {
            string ret = mBlueViewModel.DrawElectricCommon();
            if (!string.IsNullOrWhiteSpace(ret))
            {
                MessageBox.Show(ret);
            }
        }

        private void btn_PaperBlue(object sender, RoutedEventArgs e)
        {
            string ret = mBlueViewModel.DrawPaperCommon();
            if (!string.IsNullOrWhiteSpace(ret))
            {
                MessageBox.Show(ret);
            }
            
        }

        private void btn_PaperSpecial(object sender, RoutedEventArgs e)
        {
            string ret = mBlueViewModel.DrawPaperSpecial();
            if (!string.IsNullOrWhiteSpace(ret))
            {
                MessageBox.Show(ret);
            }
        }
    }
}
