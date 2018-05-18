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
        public string Description { get; set; }
        public int Quantity { get; set; }
        //单价
        public decimal Price { get; set; }
        // 单位
        public string MeteringUnit { get; set; }
        // 金额
        public decimal TransactionAmount { get; set; }
        //string taxClassificationCode;
        // 税收分类代码
        public string TaxClassificationCode { get; set; }
        // 税率
        public decimal TaxRate { get; set; }
        // 税额
        public decimal TaxRateAmount { get; set; }
        
    }


    

    public class BlueViewModel:ViewModel
    {
        public BlueViewModel()
        {
            //mBlueGoodses = new List<BlueGoods>();
        }
        #region sql
        // 查询购买方公司信息
        string resSql = @"select RN.company,RN.address,RN.phone,P.email from RESERVATION_NAME RN,PROFILE P 
where RN.resv_name_id=@resv_name_id and RN.name_id=P.name_id";
        // 消费统计sql
        string billSql = @"select TCE.Description,TCTR.TaxRate,TCTR.TaxClassificationCode,BE.Price,
SUM(BE.Quantity)as Quantity ,SUM(BE.TransactionAmount)as TransactionAmount
from BillEntity BE,TransationCodesEntity TCE,TrnCodeTaxRate TCTR 
where BE.Res_RowID=@resv_name_id and BE.Window=@Window and BE.Price>0 and BE.Quantity>0 and 
TCE.Trn_Code=BE.TransactionCode and TCTR.TrnCode=BE.TransactionCode
group by TCE.Description,TCTR.TaxRate,TCTR.TaxClassificationCode,BE.Price";
        #endregion sql


        void clearUIData()
        {
            BlueGoodses = null;
            BuyerName = string.Empty;
            BuyerAddress = string.Empty;
            BuyerTel = string.Empty;
            BuyerEmail = string.Empty;

            // 待修改
            //TaxPayerNum = "110101201702071";
            TaxPayerNum = string.Empty;
        }

        public void UpdateData(int id, int window)
        {

            DataTable resDt = SqlHelper.ExecDQLForDataTable(resSql, new List<IDataParameter>(){
                SqlHelper.CreateDataParameter("@resv_name_id",id)
            });
            if (resDt == null || resDt.Rows.Count <= 0)
            {
                clearUIData();
                return;
            }
            List<IDataParameter> billParams = new List<IDataParameter>() 
            { 
                SqlHelper.CreateDataParameter("@resv_name_id",id),
                SqlHelper.CreateDataParameter("@Window",window) 
            };

            DataTable dt = SqlHelper.ExecDQLForDataTable(billSql, billParams);
            if (dt == null || dt.Rows.Count <= 0)
            {
                clearUIData();
                return;
            }
            List<BlueGoods> goods = new List<BlueGoods>();
            foreach (DataRow dr in dt.Rows)
            {
                BlueGoods goodes = SqlHelper.Row2Model<BlueGoods>(dr);
                goodes.TaxRateAmount = TaxHelper.GetTaxAmount(goodes.Quantity, goodes.Price, goodes.TaxRate);
                goods.Add(goodes);
            }

            BlueGoodses = goods;
            BuyerName = resDt.Rows[0]["company"].ToString();
            BuyerAddress = resDt.Rows[0]["address"].ToString();
            BuyerTel = resDt.Rows[0]["phone"].ToString();
            BuyerEmail = resDt.Rows[0]["email"].ToString();
            
            // 待修改
            TaxPayerNum = "110101201702071";
        }

        public string DrawElectricCommon()
        {
            string ret = checkCommon();
            if (ret != null)
            {
                return ret;
            }
            HomeWindow.ShowDrawStatus();
            PiaotongHelper.DrawElectronicBlue(this);
            return ret;
        }
        public string DrawPaperCommon()
        {
            string ret = checkCommon();
            if (ret != null)
            {
                return ret;
            }
            HomeWindow.ShowDrawStatus();
            PiaotongHelper.DrawPaperCommon(this);
            return ret;
        }

        public string DrawPaperSpecial()
        {
            string ret = checkSpecial();
            if (ret != null)
            {
                return ret;
            }
            HomeWindow.ShowDrawStatus();
            PiaotongHelper.DrawPaperSpecial(this);

            return null;
        }


        string checkCommon()
        {
            if (string.IsNullOrWhiteSpace(this.BuyerName))
            {
                return "公司名称不能为空";
            }
            if (string.IsNullOrWhiteSpace(this.TaxPayerNum))
            {
                return "纳税人识别号不能为空";
            }
            if (!StringChecking.TaxpayerNum(this.TaxPayerNum))
            {
                return "纳税人识别号格式错误,必须为15-20位的大写字母或者数字";
            }
            var selectTmp = this.BlueGoodses.Where(m => m.IsSelected);
            if (selectTmp == null || selectTmp.Count() <= 0)
            {
                return "请至少选择一个开票项";
            }
            return null;
        }
        string checkSpecial()
        {
            string ret = checkCommon();
            if (ret != null)
            {
                return ret;
            }
            if (string.IsNullOrWhiteSpace(this.SellerAddress))
            {
                return "销货方地址不能为空";
            }
            if (string.IsNullOrWhiteSpace(this.SellerBankAccount))
            {
                return "销货方银行账户不能为空";
            }
            if (string.IsNullOrWhiteSpace(this.SellerBankName))
            {
                return "销货方开户行名称不能为空";
            }
            if (string.IsNullOrWhiteSpace(this.SellerTel))
            {
                return "销货方电话不能为空";
            }
            if (string.IsNullOrWhiteSpace(this.BuyerBankAccount))
            {
                return "购货方银行账号不能为空";
            }
            if (string.IsNullOrWhiteSpace(this.BuyerBankName))
            {
                return "购货方开户行名称不能为空";
            }
            if (string.IsNullOrWhiteSpace(this.BuyerAddress))
            {
                return "购货方地址不能为空";
            }
            if (string.IsNullOrWhiteSpace(this.BuyerTel))
            {
                return "购货方电话不能为空";
            }
            return null;
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

        string mBuyerName;
        public string BuyerName
        {
            get { return mBuyerName; }
            set
            {
                mBuyerName = value;
                OnPropertyChanged("BuyerName");
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

        
        string mBuyerAddress;
        public string BuyerAddress
        {
            get { return mBuyerAddress; }
            set
            {
                mBuyerAddress = value;
                OnPropertyChanged("BuyerAddress");
            }
        }

        string mBuyerTel;
        public string BuyerTel
        {
            get { return mBuyerTel; }
            set
            {
                mBuyerTel = value;
                OnPropertyChanged("BuyerTel");
            }
        }


        string mBuyerEmail;
        public string BuyerEmail
        {
            get { return mBuyerEmail; }
            set
            {
                mBuyerEmail = value;
                OnPropertyChanged("BuyerEmail");
            }
        }



        // 开户行
        public string BuyerBankName { get; set; }
        // 开户行账号
        public string BuyerBankAccount { get; set; }
        public string SellerAddress { get; set; }
        public string SellerTel { get; set; }
        public string SellerBankName { get; set; }
        public string SellerBankAccount { get; set; }

    }
}
