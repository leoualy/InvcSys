using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace InvcSys
{
    public class TransactionCodesEntity
    {
        public string Trn_Code { get; set; }
        public string Description { get; set; }

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
            string sql = "select Trn_Code,Description from TransationCodesEntity";
            DataTable dt = SqlHelper.ExecDQLForDataTable(sql);
            if (mTCodeEntitys == null)
            {
                mTCodeEntitys = new List<TransactionCodesEntity>();
            }
            foreach (DataRow row in dt.Rows)
            {
                TransactionCodesEntity entity = SqlHelper.Row2Model<TransactionCodesEntity>(row);
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
                    return "保存设置成功!";
                }

                if (retSelect.ToString() == "1")
                {
                    if (SqlHelper.ExecDML(updateSql, parms) <= 0)
                    {
                        return "保存设置失败!";
                    }
                    return "保存设置成功!";
                }

                return "其他错误";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}
