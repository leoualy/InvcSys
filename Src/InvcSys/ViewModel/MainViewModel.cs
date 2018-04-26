using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InvcSys.Model;
using System.Data;
using System.Windows;

namespace InvcSys
{
    public class InvoiceItem:ViewModel
    {
        // 是否选中该开票项
        bool mIsSelected;
        public bool IsSelected
        {
            get { return mIsSelected; }
            set { mIsSelected = value;
            OnPropertyChanged("IsSelected");
            }
        }


        public string ArticlesCode { get; set; }
        public int Quantity { get; set; }
        //单价
        public decimal Price { get; set; }
        // 金额
        public decimal TransactionAmount { get; set; }
    }
    
    public class MainViewModel:ViewModel
    {
        public MainViewModel()
        {
            mInvoiceItems = new List<InvoiceItem>();
        }

        public void LoadInvoiceItems()
        {
            string sql = @"select rname.company,rname.address,rname.phone,
be.* from RESERVATION_NAME rname,BillEntity be where 
be.Res_RowID=rname.resv_name_id and rname.resv_name_id=@resv_name_id";

            DataTable dt = SqlHelper.ExecDQLForDataTable(sql,
                new List<IDataParameter>() { SqlHelper.CreateDataParameter("@resv_name_id", 39320) });
            foreach (DataRow dr in dt.Rows)
            {
                mInvoiceItems.Add(SqlHelper.Row2Model<InvoiceItem>(dr));
            }
            Name = dt.Rows[0]["company"].ToString();
            Address = dt.Rows[0]["address"].ToString();
            Phone = dt.Rows[0]["phone"].ToString();
        }


        List<InvoiceItem> mInvoiceItems;
        public List<InvoiceItem> InvoiceItems
        {
            get { return mInvoiceItems; }
            set
            {
                mInvoiceItems = value;
                OnPropertyChanged("InvoiceItems");
            }
        }

        string name;
        public string Name
        {
            get { return name; }
            set { name = value;
            OnPropertyChanged("Name");
            }
        }

        string taxPayerNum;
        public string TaxPayerNum
        {
            get { return taxPayerNum; }
            set
            {
                taxPayerNum = value;
                OnPropertyChanged("TaxPayerNum");
            }
        }

        string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }
        string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }


        Visibility visibilityStatus;
        public Visibility VisibilityStatus
        {
            get { return visibilityStatus; }
            set
            {
                visibilityStatus = value;
                OnPropertyChanged("VisibilityStatus");
            }
        }

        string drawStatus;
        public string DrawStatus
        {
            get { return drawStatus; }
            set
            {
                drawStatus = value;
                OnPropertyChanged("DrawStatus");
            }
        }

        
    }
}
