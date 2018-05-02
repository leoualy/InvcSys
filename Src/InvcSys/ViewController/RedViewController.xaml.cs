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

namespace InvcSys.ViewController
{
    /// <summary>
    /// RedViewController.xaml 的交互逻辑
    /// </summary>
    public partial class RedViewController : UserControl
    {
        public RedViewController()
        {
            InitializeComponent();
            mViewModel = new RedViewModel();
            this.DataContext = mViewModel;
        }
        RedViewModel mViewModel;
        private void btn_Electronic(object sender, RoutedEventArgs e)
        {
            if (!tbxCheck())
            {
                MessageBox.Show("请检查必填项");
                return;
            }
            if (!StringChecking.TaxpayerNum(mViewModel.TaxpayerNum))
            {
                MessageBox.Show("纳税人识别号格式错误,必须为15-20位的大写字母或者数字");
                return;
            }
           
            PiaotongHelper.DrawElectronicRed(mViewModel);
        }

        private void btn_Paper(object sender, RoutedEventArgs e)
        {
            if (!tbxCheck())
            {
                MessageBox.Show("请检查必填项");
                return;
            }
            if (!StringChecking.TaxpayerNum(mViewModel.TaxpayerNum))
            {
                MessageBox.Show("纳税人识别号格式错误,必须为15-20位的大写字母或者数字");
                return;
            }
            PiaotongHelper.DrawPaperRed(mViewModel);
        }

        bool tbxCheck()
        {
            if(mViewModel.InvoiceCode==""
                ||mViewModel.InvoiceNo==""
                ||mViewModel.TaxpayerNum==""
                ||mViewModel.RedReason==""
                ||mViewModel.Amount==""
                )
            {
                return false;
            }
            return true;
        }


    }
}
