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
            mBlueViewModel.LoadBlueGoodses();
            
            this.DataContext = mBlueViewModel;
        }
        BlueViewModel mBlueViewModel;

        private void btn_ElectronicBlue(object sender, RoutedEventArgs e)
        {
            if (!tbxCheck())
            {
                MessageBox.Show("请检查必填项");
                return;
            }
            if (!StringChecking.TaxpayerNum(mBlueViewModel.TaxPayerNum))
            {
                MessageBox.Show("纳税人识别号格式错误,必须为15-20位的大写字母或者数字");
                return;
            }


            var itemsSelected = mBlueViewModel.BlueGoodses.Where(m => m.IsSelected);
            
            if (itemsSelected == null || itemsSelected.Count() <= 0)
            {
                MessageBox.Show("请选择开票项目");
                return;
            }
            PiaotongHelper.DrawElectronicBlue(mBlueViewModel);
        }

        private void btn_PaperBlue(object sender, RoutedEventArgs e)
        {
            if (!tbxCheck())
            {
                MessageBox.Show("请检查必填项");
                return;
            }
            if (!StringChecking.TaxpayerNum(mBlueViewModel.TaxPayerNum))
            {
                MessageBox.Show("纳税人识别号格式错误,必须为15-20位的大写字母或者数字");
                return;
            }

            var itemsSelected = mBlueViewModel.BlueGoodses.Where(m => m.IsSelected);
            
            if (itemsSelected == null || itemsSelected.Count() <= 0)
            {
                MessageBox.Show("请选择开票项目");
                return;
            }
            PiaotongHelper.DrawPaperBlue(mBlueViewModel);
        }

        bool tbxCheck()
        {
            if (mBlueViewModel.Name == ""||mBlueViewModel.Name==null||
                mBlueViewModel.TaxPayerNum==""||mBlueViewModel.TaxPayerNum==null)
            {
                return false;
            }
            return true;
            
        }
        

        
    }
}
