/***************************************************************************
 * 文件名:             PiaotongHelper.cs
 * 作者：              Aaron
 * QQ   :                 991258519
 * 日期：              2018-04-24
 * 描述：              票通接口封装
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Piaotong.OpenApi;

namespace InvcSys
{
    internal class PiaotongHelper
    {
        /// <summary>
        /// 开具电子蓝票
        /// </summary>
        /// <param name="viewModel"></param>
        internal static void DrawElectronicBlue(MainViewModel viewModel)
        {
            apiInit();
            string url = ConfigurationManager.AppSettings["ElectronicBlue"];
            postBlueJson(viewModel, url);
        }
        /// <summary>
        /// 开具纸质蓝票
        /// </summary>
        /// <param name="viewModel"></param>
        internal static void DrawPaperBlue(MainViewModel viewModel)
        {
            apiInit();
            string url = ConfigurationManager.AppSettings["PaperBlue"];
            postBlueJson(viewModel, url);
        }
        
        /// <summary>
        /// 开具电子红票
        /// </summary>
        /// <param name="viewModel"></param>
        internal static void DrawElectronicRed(MainViewModel viewModel)
        {
            apiInit();
            string url = ConfigurationManager.AppSettings["ElectronicRed"];
            postRedJson(viewModel, url);
        }

        /// <summary>
        /// 开具纸质红票
        /// </summary>
        /// <param name="viewModel"></param>
        internal static void DrawPaperRed(MainViewModel viewModel)
        {
            apiInit();
            string url = ConfigurationManager.AppSettings["PaperRed"];
            postRedJson(viewModel, url);
        }


        #region 类私有
        static OpenAPI mApi;
        // http 报文内容类型
        static readonly string ContentType = "application/json";
        static void apiInit()
        {
            if (mApi == null)
            {
                string desKey=ConfigurationManager.AppSettings["3DESKey"];
                string rsaPrivateKey=ConfigurationManager.AppSettings["RSAPrivateKey"];
                string rsaPublicKey=ConfigurationManager.AppSettings["RSAPublicKey"];
                string platformCode=ConfigurationManager.AppSettings["platformCode"];
                string prefix=ConfigurationManager.AppSettings["Prefix"];
                mApi = new OpenAPI(desKey, platformCode, prefix, rsaPrivateKey);
            }
        }

        static void postBlueJson(MainViewModel viewModel,string url)
        {
            InvoiceBlue invoice = new InvoiceBlue();
            invoice.taxpayerNum = "";
            // todo:设置字段值


            List<Goods> goodsList = new List<Goods>();
            if (viewModel.InvoiceItems == null || viewModel.InvoiceItems.Count <= 0)
            {
                throw new Exception("没有选中的消费项目");
            }

            foreach (InvoiceItem item in viewModel.InvoiceItems)
            {
                goodsList.Add(new Goods()
                {
                    // todo:设置字段值
                    goodsName = "测试物品",
                    
                });
            }
            // 项目列表
            invoice.itemList = JsonHelper.Object2String<List<Goods>>(goodsList);
            string json = mApi.BuildRequestJSON(JsonHelper.Object2String<InvoiceBlue>(invoice));
            HttpHelper.Post(url, Encoding.UTF8.GetBytes(json));
        }

        static void postRedJson(MainViewModel viewModel, string url)
        {
            InvoiceRed invoice = new InvoiceRed();
            invoice.taxpayerNum = "";
            // todo：设置字段值
            
            string json = mApi.BuildRequestJSON(JsonHelper.Object2String<InvoiceRed>(invoice));
            HttpHelper.Post(url, Encoding.UTF8.GetBytes(json));
        }
        #endregion 类私有
    }
}
