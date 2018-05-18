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
            string ret = mViewModel.DrawElectricRed();
            if (!string.IsNullOrWhiteSpace(ret))
            {
                MessageBox.Show(ret);
            }
        }

        private void btn_Paper(object sender, RoutedEventArgs e)
        {
            string ret = mViewModel.DrawPaperRed();
            if (!string.IsNullOrWhiteSpace(ret))
            {
                MessageBox.Show(ret);
            }
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

        private void btn_Clear(object sender, RoutedEventArgs e)
        {
            mViewModel.ClearData();
            txtDefineData.Clear();
        }


    }
}
