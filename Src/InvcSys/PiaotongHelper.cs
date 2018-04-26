/***************************************************************************
 * 文件名:             PiaotongHelper.cs
 * 作者：              Aaron
 * QQ   :                 991258519
 * 日期：              2018-04-24
 * 描述：              票通接口封装
***************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Piaotong.OpenApi;
using NLog;

namespace InvcSys
{
    internal class PiaotongHelper
    {
        internal static event Action<string> OnHttpPost;
        /// <summary>
        /// 开具电子蓝票
        /// </summary>
        /// <param name="viewModel"></param>
        internal static void DrawElectronicBlue(MainViewModel viewModel)
        {
            if (!apiInit())
            {
                return;
            }
            string url = ConfigurationManager.AppSettings["ElectronicBlue"];
            postBlueJson(viewModel, url, "电子");
        }
        /// <summary>
        /// 开具纸质蓝票
        /// </summary>
        /// <param name="viewModel"></param>
        internal static void DrawPaperBlue(MainViewModel viewModel)
        {
            if (!apiInit())
            {
                return;
            }
            string url = ConfigurationManager.AppSettings["PaperBlue"];
            postBlueJson(viewModel, url, "纸质");
        }
        
        /// <summary>
        /// 开具电子红票
        /// </summary>
        /// <param name="viewModel"></param>
        internal static void DrawElectronicRed(MainViewModel viewModel)
        {
            if (!apiInit())
            {
                return;
            }
            string url = ConfigurationManager.AppSettings["ElectronicRed"];
            postRedJson(viewModel, url,"电子");
        }

        /// <summary>
        /// 开具纸质红票
        /// </summary>
        /// <param name="viewModel"></param>
        internal static void DrawPaperRed(MainViewModel viewModel)
        {
            if (!apiInit())
            {
                return;
            }
            string url = ConfigurationManager.AppSettings["PaperRed"];
            postRedJson(viewModel, url,"纸质");
        }

        #region 类私有
        static OpenAPI mApi;
        static string mcPrefix, mcDesKey;
        static Logger mLogger;
        
        static bool apiInit()
        {
            if (mApi == null)
            {
                mcDesKey=ConfigurationManager.AppSettings["3DESKey"];
                string rsaPrivateKey=ConfigurationManager.AppSettings["RSAPrivateKey"];
                string rsaPublicKey=ConfigurationManager.AppSettings["RSAPublicKey"];
                string platformCode=ConfigurationManager.AppSettings["platformCode"];
                mcPrefix = ConfigurationManager.AppSettings["Prefix"];

                try
                {
                    // 当前私钥时java格式的，所以需要转化为.net 形式的
                    string csPrivateKey = RSAHelper.PrivateKeyJava2Dotnet(rsaPrivateKey);

                    mApi = new OpenAPI(mcDesKey, platformCode, mcPrefix, csPrivateKey);
                    mLogger = LogManager.GetCurrentClassLogger();
                }
                catch (Exception e)
                {
                    Task.Factory.StartNew(() =>
                    {
                        if (OnHttpPost != null)
                        {
                            OnHttpPost(e.Message);
                        }
                    });
                    return false;
                }
                
        }
            return true;
        }

        static void postBlueJson(MainViewModel viewModel,string url,string type)
        {
            InvoiceBlue invoice = new InvoiceBlue();
            invoice.taxpayerNum = "110101201702071";
            invoice.invoiceReqSerialNo = SerialNoHelper.GetInvoiceReqSerialNo(mcPrefix);
            // todo:设置字段
            invoice.buyerName = viewModel.Name;
            // 纸质蓝票的分机号时必填项
            if (type == "纸质")
            {
                invoice.extensionNum = "1";
            }
            
            List<Goods> goodsList = new List<Goods>();
            
            foreach (InvoiceItem item in viewModel.InvoiceItems)
            {
                // 只添加被选中的项目
                if (!item.IsSelected)
                {
                    continue;
                }
                goodsList.Add(new Goods()
                {
                    // todo:设置字段值
                    goodsName = "购买方名称1",
                    quantity=item.Quantity.ToString(),
                    //quantity ="1.00",
                    unitPrice=item.Price.ToString(),
                    //unitPrice ="56.64",
                    invoiceAmount=item.TransactionAmount.ToString(),
                   // invoiceAmount ="56.64",
                    includeTaxFlag="1",
                    taxRateValue="0.13",
                    taxClassificationCode = "1010101020000000000"
                });
            }
            // 项目列表
            invoice.itemList = JsonHelper.Object2String<List<Goods>>(goodsList);
            // 过滤业务内容串 
            string content = JsonHelper.Object2String<InvoiceBlue>(invoice).
                Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]");
            // 记录业务报文json串到日志中
            mLogger.Info("蓝票业务内容JSON串:"+content);
            
            Task.Factory.StartNew(() =>
            {
                MsgResponse rsp = mApi.PostJson(url, content);
                if (OnHttpPost != null)
                {
                    OnHttpPost(type+"蓝票开票"+rsp.msg);
                }
            });
        }

        static void postRedJson(MainViewModel viewModel, string url,string type)
        {
            InvoiceRed invoice = new InvoiceRed();
            invoice.taxpayerNum = "";
            invoice.invoiceReqSerialNo = SerialNoHelper.GetInvoiceReqSerialNo(mcPrefix);
            // todo：设置字段值
            
            string content = JsonHelper.Object2String<InvoiceRed>(invoice).Replace("\\","");
            mLogger.Info("红票业务内容JSON串");

            Task.Factory.StartNew(() =>
            {
                MsgResponse rsp = mApi.PostJson(url, content);
                if (OnHttpPost != null)
                {
                    OnHttpPost(type + "红票开票" + rsp.msg);
                }
            });
        }
        #endregion 类私有
    }
}
