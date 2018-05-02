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
    /// QueryViewController.xaml 的交互逻辑
    /// </summary>
    public partial class QueryViewController : UserControl
    {
        public QueryViewController()
        {
            InitializeComponent();
            mQueryViewModel = new QueryViewModel();
            this.DataContext = mQueryViewModel;
        }
        QueryViewModel mQueryViewModel;

        private void btn_queryPaper(object sender, RoutedEventArgs e)
        {
            if (!tbxCheck())
            {
                MessageBox.Show("请检查必填项");
                return;
            }

            if (!StringChecking.TaxpayerNum(mQueryViewModel.TaxpayerNum))
            {
                MessageBox.Show("纳税人识别号格式错误,必须为15-20位的大写字母或者数字");
                return;
            }
            if (!StringChecking.InvoiceReqSerialNo(mQueryViewModel.InvoiceReqSerialNo))
            {
                MessageBox.Show("请求流水号格式错误,必须为20位的字母或者数字");
                return;
            }

            HomeWindow.ShowDrawStatus();
            PiaotongHelper.QueryPaper(mQueryViewModel);
        }

        private void btn_queryElectronic(object sender, RoutedEventArgs e)
        {
            if (!tbxCheck())
            {
                MessageBox.Show("请检查必填项");
                return;
            }

            if (!StringChecking.TaxpayerNum(mQueryViewModel.TaxpayerNum))
            {
                MessageBox.Show("纳税人识别号格式错误,必须为15-20位的大写字母或者数字");
                return;
            }
            if (!StringChecking.InvoiceReqSerialNo(mQueryViewModel.InvoiceReqSerialNo))
            {
                MessageBox.Show("请求流水号格式错误,必须为20位的字母或者数字");
                return;
            }
            HomeWindow.ShowDrawStatus();
            PiaotongHelper.QueryElectronic(mQueryViewModel);
        }
        bool tbxCheck()
        {
            if(mQueryViewModel.InvoiceReqSerialNo==""
                || mQueryViewModel.TaxpayerNum == "")
            {
                return false;
            }
            return true;
        }
    }
}
