using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvcSys
{
    public class QueryViewModel:ViewModel
    {
        public QueryViewModel()
        {
            mTaxpayerNum = "110101201702071";
            mInvoiceReqSerialNo = "DEMO2490591817271482";

            mInvoiceNo = "无";
            mInvoiceType = "无";
            mTradeNo = "无";
            mInvoiceDate = "0000-00-00";
            mDownloadUrl = "http://";
            mCode = "无";
            

        }
        string mTaxpayerNum;
        public string TaxpayerNum
        {
            get { return mTaxpayerNum; }
            set
            {
                mTaxpayerNum = value;
                OnPropertyChanged("TaxpayerNum");
            }
        }
        string mInvoiceReqSerialNo;
        public string InvoiceReqSerialNo
        {
            get { return mInvoiceReqSerialNo; }
            set
            {
                mInvoiceReqSerialNo = value;
                OnPropertyChanged("InvoiceReqSerialNo");
            }
        }


        #region 查询响应获取的字段
        string mInvoiceType;
        public string InvoiceType
        {
            get { return mInvoiceType; }
            set { mInvoiceType = value;
            OnPropertyChanged("InvoiceType");
            }
        }

        string mCode;
        public string Code
        {
            get { return mCode; }
            set
            {
                mCode = value;
                OnPropertyChanged("Code");
            }
        }

        string mInvoiceNo;
        public string InvoiceNo
        {
            get { return mInvoiceNo; }
            set
            {
                mInvoiceNo = value;
                OnPropertyChanged("InvoiceNo");
            }
        }


        string mTradeNo;
        public string TradeNo
        {
            get { return mTradeNo; }
            set
            {
                mTradeNo = value;
                OnPropertyChanged("TradeNo");
            }
        }

        string mInvoiceDate;
        public string InvoiceDate
        {
            get { return mInvoiceDate; }
            set
            {
                mInvoiceDate = value;
                OnPropertyChanged("InvoiceDate");
            }
        }

        string mDownloadUrl;
        public string DownloadUrl
        {
            get { return mDownloadUrl; }
            set { mDownloadUrl = value;
            OnPropertyChanged("DownloadUrl");
            }
        }



        #endregion 查询响应获取的字段



    }
}
