﻿/***************************************************************************
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
        static PiaotongHelper()
        {
            if (mApi == null)
            {
                mcDesKey = ConfigurationManager.AppSettings["3DESKey"];
                string rsaPrivateKey = ConfigurationManager.AppSettings["RSAPrivateKey"];
                string rsaPublicKey = ConfigurationManager.AppSettings["RSAPublicKey"];
                string platformCode = ConfigurationManager.AppSettings["platformCode"];
                mcPrefix = ConfigurationManager.AppSettings["Prefix"];
                mcVersion = ConfigurationManager.AppSettings["Version"];

                try
                {
                    mApi = new OpenAPI(mcDesKey, platformCode, mcPrefix, rsaPrivateKey,rsaPublicKey);
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
                }

            }
        }

        internal static event Action<string> OnHttpPost;
       
        /// <summary>
        /// 开具电子蓝票
        /// </summary>
        /// <param name="viewModel"></param>
        internal static void DrawElectronicBlue(BlueViewModel viewModel)
        {
            string url = ConfigurationManager.AppSettings["ElectronicBlue"];
            postBlueJson(viewModel, url, "电子");
        }
        /// <summary>
        /// 开具纸质普票
        /// </summary>
        /// <param name="viewModel"></param>
        internal static void DrawPaperCommon(BlueViewModel viewModel)
        {
            string url = ConfigurationManager.AppSettings["PaperBlue"];
            postBlueJson(viewModel, url, "纸质");
        }
        internal static void DrawPaperSpecial(BlueViewModel viewModel)
        {
            string url = ConfigurationManager.AppSettings["PaperSpecial"];
            postBlueJson(viewModel, url, "纸质");
        }
        
        /// <summary>
        /// 开具电子红票
        /// </summary>
        /// <param name="viewModel"></param>
        internal static void DrawElectronicRed(RedViewModel viewModel)
        {
            string url = ConfigurationManager.AppSettings["ElectronicRed"];
            postRedJson(viewModel, url,"电子");
        }

        /// <summary>
        /// 开具纸质红票
        /// </summary>
        /// <param name="viewModel"></param>
        internal static void DrawPaperRed(RedViewModel viewModel)
        {
            string url = ConfigurationManager.AppSettings["PaperRed"];
            postRedJson(viewModel, url,"纸质");
        }

        internal static void QueryPaper(QueryViewModel viewModel)
        {
            string url = ConfigurationManager.AppSettings["PaperQuery"];
            postQueryJson(viewModel, url,"纸质");
        }
        internal static void QueryElectronic(QueryViewModel viewModel)
        {
            string url = ConfigurationManager.AppSettings["ElectronicQuery"];
            postQueryJson(viewModel, url, "电子");
        }



        #region 类私有
        static OpenAPI mApi;
        static string mcPrefix, mcDesKey, mcVersion;
        static Logger mLogger;


        static void postBlueJson(BlueViewModel viewModel,string url,string type)
        {
            InvoiceBlue invoice = new InvoiceBlue();
            // 纳税人识别号
            invoice.taxpayerNum = viewModel.TaxPayerNum;
            // 发票请求流水号
            invoice.invoiceReqSerialNo = SerialNoHelper.GetInvoiceReqSerialNo(mcPrefix);

            invoice.buyerName = viewModel.BuyerName;
            invoice.buyerAddress = viewModel.BuyerAddress;
            invoice.buyerTel = viewModel.BuyerTel;
            invoice.buyerBankName = viewModel.BuyerBankName;
            invoice.buyerBankAccount = viewModel.BuyerBankAccount;
            invoice.buyerTaxpayerNum = viewModel.TaxPayerNum;

            invoice.sellerAddress = viewModel.SellerAddress;
            invoice.sellerBankAccount = viewModel.SellerBankAccount;
            invoice.sellerBankName = viewModel.SellerBankName;
            invoice.sellerTel = viewModel.SellerTel;
            
            // 纸质蓝票的分机号时必填项
            if (type == "纸质")
            {
                invoice.extensionNum = "1";
            }
            
            List<Goods> goodsList = new List<Goods>();
            
            foreach (BlueGoods item in viewModel.BlueGoodses)
            {
                // 只添加被选中的项目
                if (!item.IsSelected)
                {
                    continue;
                }
                goodsList.Add(new Goods()
                {
                    goodsName =item.Description,
                    quantity=item.Quantity.ToString(),
                    //quantity ="1.00",
                    unitPrice=item.Price.ToString(),
                    //unitPrice ="56.64",
                    invoiceAmount=item.TransactionAmount.ToString(),
                   // invoiceAmount ="56.64",
                    includeTaxFlag=item.IsIncludeTax?"1":"0",
                    taxRateValue=item.TaxRate.ToString(),
                    // 0税率处理
                    zeroTaxFlag=item.TaxRate==0?"1":null,
                    // 税额的计算
                    taxRateAmount=Convert.ToDouble((item.TaxRateAmount==0?
                    TaxHelper.GetTaxAmount(item.Quantity,item.Price,item.TaxRate):item.TaxRateAmount)).ToString(),
                    
                    taxClassificationCode=item.TaxClassificationCode,
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

        static void postRedJson(RedViewModel viewModel, string url,string type)
        {
            InvoiceRed invoice = new InvoiceRed();
            invoice.taxpayerNum = viewModel.TaxpayerNum;
            invoice.invoiceReqSerialNo = SerialNoHelper.GetInvoiceReqSerialNo(mcPrefix);
            invoice.amount = viewModel.Amount;
            invoice.invoiceCode = viewModel.InvoiceCode;
            invoice.invoiceNo = viewModel.InvoiceNo;
            invoice.redReason = viewModel.RedReason;
            
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


        static void postQueryJson(QueryViewModel viewModel, string url, string type)
        {
            InvoiceQuery query = new InvoiceQuery();
            query.invoiceReqSerialNo = viewModel.InvoiceReqSerialNo;
            query.taxpayerNum = viewModel.TaxpayerNum;
            string content = JsonHelper.Object2String<InvoiceQuery>(query).Replace("\\", "");
            mLogger.Info("查询发票业务内容JSON串");

            Task.Factory.StartNew(() =>
            {
                MsgResponse rsp = mApi.PostQueryJson(url, content);
                Console.WriteLine(rsp.msg);
                if (OnHttpPost != null)
                {
                    OnHttpPost(type + "发票查询" + rsp.msg);
                }
                //if (OnQueryHttpPost != null)
                //{
                //    OnQueryHttpPost(type + "发票查询", rsp.code);
                //}


                if (rsp.code == "0000")
                {
                    viewModel.Code = rsp.QueryContent.code == "0000" ? "开票成功" : rsp.QueryContent.code == "9999" ? "开票中" : "失败";
                    viewModel.InvoiceDate =rsp.QueryContent.invoiceDate;
                    viewModel.InvoiceType =rsp.QueryContent.invoiceType == "1" ? "蓝票" : "红票";
                    viewModel.TradeNo =rsp.QueryContent.tradeNo;
                    viewModel.InvoiceNo = rsp.QueryContent.invoiceNo;
                    viewModel.DownloadUrl =rsp.QueryContent.downloadUrl;
                }
                else
                {
                    viewModel.Code ="空";
                    viewModel.InvoiceDate = "0000-00-00";
                    viewModel.InvoiceType = "空";
                    viewModel.TradeNo ="空";
                    viewModel.InvoiceNo ="空";
                    viewModel.DownloadUrl ="空";
                }


            });
        }
        #endregion 类私有
    }
}
