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
            this.DataContext = mMainViewModel;
        }

        
        string privateKey = @"MIICdQIBADANBgkqhkiG9w0BAQEFAASCAl8wggJbAgEAAoGBAIVLAoolDaE7m5oMB1ZrILHkMXMF6qmC8I/FCejz4hwBcj59H3rbtcycBEmExOJTGwexFkNgRakhqM+3uP3VybWu1GBYNmqVzggWKKzThul9VPE3+OTMlxeG4H63RsCO1//J0MoUavXMMkL3txkZBO5EtTqek182eePOV8fC3ZxpAgMBAAECgYBp4Gg3BTGrZaa2mWFmspd41lK1E/kPBrRA7vltMfPj3P47RrYvp7/js/Xv0+d0AyFQXcjaYelTbCokPMJT1nJumb2A/Cqy3yGKX3Z6QibvByBlCKK29lZkw8WVRGFIzCIXhGKdqukXf8RyqfhInqHpZ9AoY2W60bbSP6EXj/rhNQJBAL76SmpQOrnCI8Xu75di0eXBN/bE9tKsf7AgMkpFRhaU8VLbvd27U9vRWqtu67RY3sOeRMh38JZBwAIS8tp5hgcCQQCyrOS6vfXIUxKoWyvGyMyhqoLsiAdnxBKHh8tMINo0ioCbU+jc2dgPDipL0ym5nhvg5fCXZC2rvkKUltLEqq4PAkAqBf9b932EpKCkjFgyUq9nRCYhaeP6JbUPN3Z5e1bZ3zpfBjV4ViE0zJOMB6NcEvYpy2jNR/8rwRoUGsFPq8//AkAklw18RJyJuqFugsUzPznQvad0IuNJV7jnsmJqo6ur6NUvef6NA7ugUalNv9+imINjChO8HRLRQfRGk6B0D/P3AkBt54UBMtFefOLXgUdilwLdCUSw4KpbuBPw+cyWlMjcXCkj4rHoeksekyBH1GrBJkLqDMRqtVQUubuFwSzBAtlc";
        string publicKey = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCJkx3HelhEm/U7jOCor29oHsIjCMSTyKbX5rpoAY8KDIs9mmr5Y9r+jvNJH8pK3u5gNnvleT6rQgJQW1mk0zHuPO00vy62tSA53fkSjtM+n0oC1Fkm4DRFd5qJgoP7uFQHR5OEffMjy2qIuxChY4Au0kq+6RruEgIttb7wUxy8TwIDAQAB";
        string password = "lsBnINDxtct8HZB7KCMyhWSJ";
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string csPrivateKey = RSAHelper.PrivateKeyJava2Dotnet(privateKey);
            OpenAPI api = new OpenAPI(password, "11111111", "DEMO", csPrivateKey);
            

            //return;
            List<Goods> goods = new List<Goods>();

            Goods good_eg = new Goods();
            good_eg.goodsName = "牙刷";
            good_eg.taxRateValue = "0.13";
            good_eg.invoiceAmount = 56.64m;
            good_eg.quantity = "1111.00";
            good_eg.unitPrice = "2900.0000";
            goods.Add(good_eg);
            goods.Add(good_eg);
            goods.Add(good_eg);
            goods.Add(good_eg);
            goods.Add(good_eg);
            goods.Add(good_eg);

            InvoiceBlue eg = new InvoiceBlue();
            eg.taxpayerNum = "110101201702071";
            eg.invoiceReqSerialNo = "DEMO0000000000000024";

            eg.itemList = JsonHelper.Object2String<List<Goods>>(goods);
            //Console.WriteLine(JsonHelper.Object2String<InvoiceBlue>(eg));
            
            
            
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("taxpayerNum", "110101201702071");
            //dic.Add("invoiceReqSerialNo", "DEMO5678902234568903");
            //dic.Add("invoiceCode", "123456789012");
            //dic.Add("invoiceNo", "12345678");
            //dic.Add("redReason", "冲红");
            //dic.Add("amount", "-51.22");
            string str = api.BuildRequestJSON(JsonHelper.Object2String<InvoiceBlue>(eg));
            Console.WriteLine(str);
            try
            {
                string strresp = HttpHelper.Post("http://fpkj.testnw.vpiaotong.cn/tp/openapi/invoicePaperCommon.pt", Encoding.UTF8.GetBytes(str));
                Console.WriteLine("*****************************************");
                Console.WriteLine(strresp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        MainViewModel mMainViewModel;
    }
}
