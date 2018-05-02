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
