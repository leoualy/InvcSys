using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvcSys.Model
{
    public class BillEntity
    {
        public int Bill_Rowid { get; set; }

        public int Res_RowID { get; set; }

        public string Room { get; set; }

        public int ORes_RowID { get; set; }

        public string ORoom { get; set; }

        public DateTime TransactionDate { get; set; }

        public DateTime TransactionTime { get; set; }

        public int Trn_Code_RowID { get; set; }

        public string TransactionCode { get; set; }

        public int ArticlesCode_RowID { get; set; }

        public string ArticlesCode { get; set; }

        public int Ar_Code_Rowid { get; set; }

        public string Ar_Code { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TransactionAmount { get; set; }

        public string CheckNo { get; set; }

        public string Supplement { get; set; }

        public string Reference { get; set; }

        public int Window { get; set; }

        public int Covers { get; set; }

        public string ArAccount { get; set; }

        public string MemberCard { get; set; }

        public string CreditCardNo { get; set; }

        public string CreditCardExpireDate { get; set; }

        public int Cashier_ID { get; set; }

        public string Cashier_Name { get; set; }

        public DateTime CashiserCloseTime { get; set; }

        public int NoTransfer { get; set; }

        public int Is_AR_OK { get; set; }

        public int Is_Close { get; set; }

        public string CloseCode { get; set; }

        public int resort { get; set; }

        public DateTime insert_date { get; set; }

        public int insert_user { get; set; }

        public DateTime update_date { get; set; }

        public int update_user { get; set; }

        public int AdjustmentCode_RowID { get; set; }

        public string AdjustmentCode { get; set; }

        public int NoShow { get; set; }

        public string insert_user_name { get; set; }

        public string update_user_name { get; set; }

        public string OLastName { get; set; }

        public string LastName { get; set; }

        public string TrnDescription { get; set; }

        public int ARAdjARBill_RowID { get; set; }

        public int BaseORes_ID { get; set; }

        public string Market_Code { get; set; }

        public string Res_Status { get; set; }

        public string ArticleDescription { get; set; }

        public string SubCode { get; set; }

        public decimal CRMPoints { get; set; }

        public int CRMType { get; set; }

        public bool ShowFirst { get; set; }

    }
}