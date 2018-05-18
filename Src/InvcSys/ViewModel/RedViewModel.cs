using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvcSys
{
    public class RedViewModel:ViewModel
    {
        public RedViewModel()
        {
            mTaxpayerNum = "110101201702071";
            mInvoiceReqSerialNo = "DEMO2490591817271482";
            mInvoiceCode = "050003522444";
            mInvoiceNo = "11302054";
            mRedReason = "冲红";
            mAmount = "-56.64";
        }

        public string DrawElectricRed()
        {
            string ret = checkRed();
            if (!string.IsNullOrWhiteSpace(ret))
            {
                return ret;
            }
            HomeWindow.ShowDrawStatus();
            PiaotongHelper.DrawElectronicRed(this);
            return null;
        }
        public string DrawPaperRed()
        {
            string ret = checkRed();
            if (!string.IsNullOrWhiteSpace(ret))
            {
                return ret;
            }
            HomeWindow.ShowDrawStatus();
            PiaotongHelper.DrawPaperRed(this);
            return null;
        }

        public void ClearData()
        {
            TaxpayerNum = string.Empty;
            InvoiceReqSerialNo = string.Empty;
            InvoiceCode = string.Empty;
            InvoiceNo = string.Empty;
            RedReason = string.Empty;
            Amount = string.Empty;
        }

        string checkRed()
        {
            if (string.IsNullOrWhiteSpace(this.InvoiceCode))
            {
                return "发票代码不能为空";
            }
            if (string.IsNullOrWhiteSpace(this.InvoiceNo))
            {
                return "发票号码不能为空";
            }
            if (string.IsNullOrWhiteSpace(this.TaxpayerNum))
            {
                return "纳税人识别号不能为空";
            }
            if (string.IsNullOrWhiteSpace(this.RedReason))
            {
                return "冲红原因不能为空";
            }
            if (string.IsNullOrWhiteSpace(this.Amount))
            {
                return "价税合计金额不能为空";
            }
            if (!StringChecking.TaxpayerNum(this.TaxpayerNum))
            {
                return "纳税人识别号格式错误,必须为15-20位的大写字母或者数字";
            }
            return null;

        }



        string mInvoiceCode;
        public string InvoiceCode
        {
            get { return mInvoiceCode; }
            set
            {
                mInvoiceCode = value;
                OnPropertyChanged("InvoiceCode");
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

        string mAmount;
        public string Amount
        {
            get { return mAmount; }
            set
            {
                mAmount = value;
                OnPropertyChanged("Amount");
            }
        }

        string mTaxpayerNum;
        public string TaxpayerNum
        {
            get { return mTaxpayerNum; }
            set { mTaxpayerNum = value;
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


        string mRedReason;
        public string RedReason
        {
            get { return mRedReason; }
            set
            {
                mRedReason = value;
                OnPropertyChanged("RedReason");
            }
        }

        

        
    }
}
