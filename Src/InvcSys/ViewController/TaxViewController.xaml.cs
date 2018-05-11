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
    /// TaxViewController.xaml 的交互逻辑
    /// </summary>
    public partial class TaxViewController : UserControl
    {

        public TaxViewController()
        {
            InitializeComponent();
            this.Loaded+=TaxViewController_Loaded;
            
        }

        TaxViewModel taxViewModel;
        void TaxViewController_Loaded(object sender, RoutedEventArgs e)
        {
            taxViewModel = new TaxViewModel();
            taxViewModel.LoadTCodeEntitys();
            taxViewModel.LoadTaxClass();
            this.DataContext = taxViewModel;
        }

        private void btn_save(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(taxViewModel.Save());
        }

    }
}
