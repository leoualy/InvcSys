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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Piaotong.OpenApi;
using System.Collections;

namespace InvcSys.ViewController
{
    /// <summary>
    /// MainViewController.xaml 的交互逻辑
    /// </summary>
    public partial class MainViewController : UserControl
    {
        public MainViewController()
        {
            InitializeComponent();
            mMainViewModel = new MainViewModel();
            mMainViewModel.LoadInvoiceItems();
            mMainViewModel.VisibilityStatus = Visibility.Collapsed;
            this.DataContext = mMainViewModel;
            PiaotongHelper.OnHttpPost += PiaotongHelper_OnHttpPost;
        }

        void PiaotongHelper_OnHttpPost(string obj)
        {
            mMainViewModel.DrawStatus = obj;
            Thread.Sleep(3000);
            mMainViewModel.VisibilityStatus = Visibility.Collapsed;
            mMainViewModel.DrawStatus = "正在发送开票请求...";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Btn_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        MainViewModel mMainViewModel;

        string mBlueAndRed = "", mElectronicAndPaper = "";

        private void rdiobtn_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton btn = sender as RadioButton;
            string name = btn.Content.ToString();
            if (name == "普票" || name == "专票")
            {
                mBlueAndRed = name;
                return;
            }
            if (name == "电票" || name == "纸票")
            {
                mElectronicAndPaper = name;
                return;
            }
        }

        private void btn_Draw(object sender, RoutedEventArgs e)
        {
            if (mBlueAndRed == "")
            {
                MessageBox.Show("请选择普票或者专票!");
                return;
            }
            if (mElectronicAndPaper == "")
            {
                MessageBox.Show("请选择电票或者纸票!");
                return;
            }

            var itemsSelected = mMainViewModel.InvoiceItems.Where(m => m.IsSelected);
            if (itemsSelected == null || itemsSelected.Count() <= 0)
            {
                MessageBox.Show("请选择开票项目");
                return;
            }

            mMainViewModel.VisibilityStatus = Visibility.Visible;
            mMainViewModel.DrawStatus = "正在发送开票请求...";
            if (mBlueAndRed == "普票")
            {
                if (mElectronicAndPaper == "电票")
                {
                    PiaotongHelper.DrawElectronicBlue(mMainViewModel);
                    return;
                }
                PiaotongHelper.DrawPaperBlue(mMainViewModel);
                //MessageBox.Show(mBlueAndRed + mElectronicAndPaper);
                return;
            }

            if (mElectronicAndPaper == "电票")
            {
                PiaotongHelper.DrawElectronicRed(mMainViewModel);
                //MessageBox.Show(mBlueAndRed + mElectronicAndPaper);
                return;
            }
            PiaotongHelper.DrawPaperRed(mMainViewModel);
            //MessageBox.Show(mBlueAndRed + mElectronicAndPaper);
            return;
        }
    }
}
