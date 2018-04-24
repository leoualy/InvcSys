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
                string sql = "select * from BillEntity where Res_RowID=@Res_RowID";
                DataTable dt = SqlHelper.ExecDQLForDataTable(sql,
                    new List<IDataParameter>() { SqlHelper.CreateDataParameter("@Res_RowID", 39616) });
                foreach (DataRow dr in dt.Rows)
                {
                    mInvoiceItems.Add(SqlHelper.Row2Model<InvoiceItem>(dr));
                }
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
    }
}
