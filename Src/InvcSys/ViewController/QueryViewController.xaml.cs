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
            string ret = mQueryViewModel.QueryPaper();
            if (!string.IsNullOrWhiteSpace(ret))
            {
                MessageBox.Show(ret);
            }
        }

        private void btn_queryElectronic(object sender, RoutedEventArgs e)
        {
            string ret = mQueryViewModel.QueryElectric();
            if (!string.IsNullOrWhiteSpace(ret))
            {
                MessageBox.Show(ret);
            }
        }
        
    }
}
