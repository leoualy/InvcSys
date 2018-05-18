using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Data;

namespace InvcSys
{
    public class TransactionCodesEntity:ViewModel
    {
        public string Trn_Code { get; set; }
        public string Description { get; set; }
        string status;
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }
        string mBtnTxt;

        public string BtnTxt
        {
            get { return mBtnTxt; }
            set { mBtnTxt = value;
            OnPropertyChanged("BtnTxt");
            }
        }

    }
    public class GoodsTaxRate:ViewModel
    {
        public string TaxClassificationCode { get; set; }
        public string GoodsName { get; set; }
        public string ShortGoodsName { get; set; }

        public string Instruction { get; set; }
     
        public double TaxRate { get; set; }
    }


    public class TaxViewModel:ViewModel
    {
        public TaxViewModel()
        {
            ViewSource = new CollectionViewSource();
            ViewSource.Filter += ViewSource_Filter;
            SelectedGoodsTaxRate = null;
        }
        List<TransactionCodesEntity> mTCodeEntitys;
        public List<TransactionCodesEntity> TCodeEntitys
        {
            get { return mTCodeEntitys; }
            set
            {
                mTCodeEntitys = value;
                OnPropertyChanged("TCodeEntitys");
            }
        }

        List<GoodsTaxRate> mGoodsTaxRates;
        public List<GoodsTaxRate> GoodsTaxRates
        {
            get { return mGoodsTaxRates; }
            set
            {
                mGoodsTaxRates = value;
                OnPropertyChanged("GoodsTaxRates");
            }
        }

        public TransactionCodesEntity SelectedTCodeEntity { get; set; }

        GoodsTaxRate mSelectGoodsTaxRate;
        public GoodsTaxRate SelectedGoodsTaxRate
        {
            get { return mSelectGoodsTaxRate; }
            set
            {
                mSelectGoodsTaxRate = value;
                OnPropertyChanged("SelectedGoodsTaxRate");
            }
        }





        public void LoadTCodeEntitys()
        {
            string sql = @"select TCE.Trn_Code,TCE.Description,ISNULL(TTR.TaxClassificationCode,0)as Status from 
TrnCodeTaxRate TTR right join TransationCodesEntity TCE on TTR.TrnCode=TCE.Trn_Code";
            DataTable dt = SqlHelper.ExecDQLForDataTable(sql);
            if (mTCodeEntitys == null)
            {
                mTCodeEntitys = new List<TransactionCodesEntity>();
            }
            foreach (DataRow row in dt.Rows)
            {
                TransactionCodesEntity entity = SqlHelper.Row2Model<TransactionCodesEntity>(row);
                entity.Status = entity.Status == "0" ? "未设置" : "已设置";
                entity.BtnTxt = entity.Status == "未设置" ? "设定" : "更新";
                mTCodeEntitys.Add(entity);
            }
        }
        public void  LoadTaxClass()
        {
            string sql = @"select GoodsName,ShortGoodsName,TaxRate,TaxClassificationCode,Instruction from GoodsTaxRate 
where GoodsName is not null ";
            DataTable dt = SqlHelper.ExecDQLForDataTable(sql);
            if (mGoodsTaxRates == null)
            {
                mGoodsTaxRates = new List<GoodsTaxRate>();
            }
            foreach (DataRow row in dt.Rows)
            {
                GoodsTaxRate entity = SqlHelper.Row2Model<GoodsTaxRate>(row);
                mGoodsTaxRates.Add(entity);
            }
        }

        public string Save()
        {
            if (SelectedTCodeEntity==null)
            {
                return "请选择货物类型!";
            }
            if (SelectedGoodsTaxRate == null)
            {
                return "请选择税率类型";
            }

            

            // 查重的sql
            string selectSql = "select 1 from TrnCodeTaxRate where TrnCode=@TrnCode";
            // 更新的sql
            string updateSql = @"update TrnCodeTaxRate set TrnCode=@TrnCode,TaxClassificationCode=@TaxClassificationCode,TaxRate=@TaxRate
where TrnCode=@TrnCode";
            // 添加的sql
            string insertSql = @"insert into TrnCodeTaxRate (TrnCode,TaxClassificationCode,TaxRate) 
            values(@TrnCode,@TaxClassificationCode,@TaxRate)";
            
            try
            {
                object retSelect = SqlHelper.ExecDQL(selectSql, new List<IDataParameter>(){
                SqlHelper.CreateDataParameter("@TrnCode",SelectedTCodeEntity.Trn_Code)
                });

                List<IDataParameter> parms = new List<IDataParameter>(){
                    SqlHelper.CreateDataParameter("@TrnCode",SelectedTCodeEntity.Trn_Code),
                    SqlHelper.CreateDataParameter("@TaxClassificationCode",SelectedGoodsTaxRate.TaxClassificationCode),
                    SqlHelper.CreateDataParameter("@TaxRate",SelectedGoodsTaxRate.TaxRate)
                };
                if (retSelect == null)
                {
                    if (SqlHelper.ExecDML(insertSql, parms) <= 0)
                    {
                        return "保存设置失败!";
                    }
                    SelectedTCodeEntity.Status = "已设置";
                    SelectedTCodeEntity.BtnTxt = "更新";
                    //SelectedGoodsTaxRate = null;
                    return "保存设置成功!";
                }

                if (retSelect.ToString() == "1")
                {
                    if (SqlHelper.ExecDML(updateSql, parms) <= 0)
                    {
                        return "保存设置失败!";
                    }
                    SelectedTCodeEntity.Status = "已设置";
                    //SelectedGoodsTaxRate = null;
                    return "保存设置成功!";

                }

                return "其他错误";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        #region 税率类型显示
        string mTextFilter;
        public string TextFilter
        {
            get { return mTextFilter; }
            set
            {
                mTextFilter = value;
                OnPropertyChanged("TextFilter");
                ViewSource.View.Refresh();
            }
        }

        CollectionViewSource mViewSource;

        public CollectionViewSource ViewSource
        {
            get { return mViewSource; }
            set
            {
                mViewSource = value;
                OnPropertyChanged("CollectionViewSource");
            }
        }
        void ViewSource_Filter(object sender, FilterEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextFilter))
            {
                var val = e.Item as GoodsTaxRate;
                e.Accepted = val != null && val.GoodsName.Contains(TextFilter.Trim());
            }
        }
        #endregion 税率类型显示


    }
}
