using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;

namespace InvcSys
{
    public class BlueGoods : ViewModel
    {
        // 是否选中该开票项
        bool mIsSelected;
        public bool IsSelected
        {
            get { return mIsSelected; }
            set
            {
                mIsSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
        // 是否含税
        public bool IsIncludeTax { get; set; }


        public string ArticlesCode { get; set; }
        public int Quantity { get; set; }
        //单价
        public decimal Price { get; set; }
        // 单位
        public string MeteringUnit { get; set; }
        // 金额
        public decimal TransactionAmount { get; set; }

        // 税收分类代码
        public string TaxClassificationCode { get; set; }
        // 税率
        public string TaxRateValue { get; set; }
        // 税额
        public string TaxRateAmount { get; set; }
        // 折扣金额
        public string DiscountAmount { get; set; }
        // 折扣税额
        public string DiscountTaxRateAmount { get; set; }
        // 折扣率值
        public string DiscountRateValue { get; set; }


    }
    public class BlueViewModel:ViewModel
    {
        public BlueViewModel()
        {
            mBlueGoodses = new List<BlueGoods>();
        }

        public void LoadBlueGoodses()
        {
            string sql = @"select rname.company,rname.address,rname.phone,
be.* from RESERVATION_NAME rname,BillEntity be where 
be.Res_RowID=rname.resv_name_id and rname.resv_name_id=@resv_name_id";

            DataTable dt = SqlHelper.ExecDQLForDataTable(sql,
                new List<IDataParameter>() { SqlHelper.CreateDataParameter("@resv_name_id", 39320) });
            foreach (DataRow dr in dt.Rows)
            {
                BlueGoods goodes = SqlHelper.Row2Model<BlueGoods>(dr);
                goodes.TaxRateValue = "0.13";
                goodes.TaxClassificationCode = "1010101020000000000";
                mBlueGoodses.Add(goodes);
            }
            Name = dt.Rows[0]["company"].ToString();
            Address = dt.Rows[0]["address"].ToString();
            Phone = dt.Rows[0]["phone"].ToString();
            // 待修改
            TaxPayerNum = "110101201702071";
        }


        List<BlueGoods> mBlueGoodses;
        public List<BlueGoods> BlueGoodses
        {
            get { return mBlueGoodses; }
            set
            {
                mBlueGoodses = value;
                OnPropertyChanged("BlueGoodses");
            }
        }

        string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
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
        // 开户行
        public string BuyerBankName { get; set; }
        // 开户行账号
        public string BuyerBankAccount { get; set; }


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
