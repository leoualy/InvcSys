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

            taxViewModel.ViewSource.Source = taxViewModel.GoodsTaxRates;
            this.DataContext = taxViewModel;
        }
        

        private void btn_save(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string code = btn.CommandParameter.ToString();
            taxViewModel.SelectedTCodeEntity = taxViewModel.TCodeEntitys.First(m => m.Trn_Code == code);
            MessageBox.Show(taxViewModel.Save());
        }


        private void lv_selected(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = sender as ListView;
            
            try
            {
                GoodsTaxRate gtr = lv.SelectedItem as GoodsTaxRate;
                taxViewModel.SelectedGoodsTaxRate = gtr;
                txtFilter.Text = gtr.GoodsName;
            }
            catch
            {

            }
            

        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            taxViewModel.TextFilter = ((TextBox)sender).Text.Trim();
            if (string.IsNullOrWhiteSpace(taxViewModel.TextFilter))
            {
                taxViewModel.SelectedGoodsTaxRate = null;
            }

            if (lvTaxRate.SelectedItem!=null&&taxViewModel.SelectedGoodsTaxRate!=null
                && taxViewModel.TextFilter != taxViewModel.SelectedGoodsTaxRate.GoodsName)
            {
                lvTaxRate.SelectedItem = null;
            }
             
            
        }

        private void btn_clearTaxType(object sender, RoutedEventArgs e)
        {
            txtFilter.Clear();
            lvTaxRate.SelectedItem = null;
            taxViewModel.SelectedGoodsTaxRate = null;
        }

    }
}
