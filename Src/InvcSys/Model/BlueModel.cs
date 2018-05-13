using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace InvcSys
{
    public class BlueModel
    {
        static string sql = @"
select RN.company as BuyerName,RN.address as BuyerAddress,RN.phone as BuyerTel,P.email as BuyerEmail,
TCE.Description,TCTR.TaxRate,TCTR.TaxClassificationCode,BE.Price,
SUM(BE.Quantity)as Quantity ,SUM(BE.TransactionAmount)as TransactionAmount

from BillEntity BE,TransationCodesEntity TCE,TrnCodeTaxRate TCTR,RESERVATION_NAME RN,PROFILE P  

where BE.Res_RowID=@ResID and BE.Window=@Window and BE.Price>0 and BE.Quantity>0 and 
TCE.Trn_Code=BE.TransactionCode and TCTR.TrnCode=BE.TransactionCode and 
RN.resv_name_id=@ResID and RN.name_id=P.name_id

group by TCE.Description,TCTR.TaxRate,TCTR.TaxClassificationCode,BE.Price,
RN.company,RN.address,RN.phone,P.email";
        
        public string BuyerName { get; set; }
        public string BuyerAddress { get; set; }
        public string BuyerTel { get; set; }
        public string BuyerEmail { get; set; }

        //货物和劳务名称
        public string Description { get; set; }
        // 税率
        public decimal TaxRate { get; set; }
        // 税率类型
        public string TaxClassification { get; set; }
        // 单价
        public decimal Price { get; set; }
        // 数量
        public int Quantity { get; set; }
        // 金额
        public decimal TransactionAmount { get; set; }


        public static List<BlueModel> GetBlueModel(int id,int window)
        {
            List<IDataParameter> parms = new List<IDataParameter>() 
            { 
                SqlHelper.CreateDataParameter("@ResID",id),
                SqlHelper.CreateDataParameter("@Window",window) 
            };
            DataTable dt = SqlHelper.ExecDQLForDataTable(sql,parms);
            if (dt == null || dt.Rows.Count <= 0)
            {
                return null;
            }
            List<BlueModel> list = new List<BlueModel>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(SqlHelper.Row2Model<BlueModel>(row));
            }

            return list;
        }



    }
}
