using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvcSys.Model
{
    public class RESERVATION_NAME
    {
        public int resv_name_id { get; set; }

        public int resort { get; set; }

        public int name_id { get; set; }

        public string alt_name { get; set; }

        public string last_name { get; set; }

        public string first_name { get; set; }

        public string language { get; set; }

        public string confirmation_no { get; set; }

        public string resv_status { get; set; }

        public string resv_type { get; set; }

        public string rate_code { get; set; }

        public decimal rate_finally { get; set; }

        public decimal rate_not_discount { get; set; }

        public string fixed_rate { get; set; }

        public string Packages { get; set; }

        public string rtc { get; set; }

        public string block { get; set; }

        public string is_block { get; set; }

        public string room_class { get; set; }

        public string room_type { get; set; }

        public string room_no { get; set; }

        public int room_counter { get; set; }

        public string floor { get; set; }

        public string company { get; set; }

        public string group { get; set; }

        public string source { get; set; }

        public string agent { get; set; }

        public string contact { get; set; }

        public string member_type { get; set; }

        public string member_no { get; set; }

        public string membership_level { get; set; }

        public string market_code { get; set; }

        public string source_code { get; set; }

        public string country { get; set; }

        public string state { get; set; }

        public string city { get; set; }

        public string credit_card { get; set; }

        public string credit_card_holder { get; set; }

        public string credit_card_id { get; set; }

        public string credit_card_date { get; set; }

        public string vip_level { get; set; }

        public string title { get; set; }

        public int nights { get; set; }

        public int adults { get; set; }

        public int children { get; set; }

        public DateTime begin_date { get; set; }

        public DateTime end_date { get; set; }

        public string payment_method { get; set; }

        public string Approlcal_code { get; set; }

        public string special_need { get; set; }

        public string goods_used { get; set; }

        public string promotion { get; set; }

        public string message { get; set; }

        public int no_post { get; set; }

        public int party_code { get; set; }

        public int walkin_yn { get; set; }

        public string do_not_move { get; set; }

        public DateTime original_end_date { get; set; }

        public DateTime actual_check_in_date { get; set; }

        public DateTime actual_check_out_date { get; set; }

        public string channel_rowid { get; set; }

        public string channel_code { get; set; }

        public string reservation_comments { get; set; }

        public string cashier_comments { get; set; }

        public string caller_name { get; set; }

        public string caller_phone { get; set; }

        public decimal discount_amount { get; set; }

        public decimal discount_precent { get; set; }

        public string discount_reason { get; set; }

        public string discount_comments { get; set; }

        public string room_features { get; set; }

        public string group_commission_code { get; set; }

        public decimal group_commission_amout { get; set; }

        public decimal group_commission_paid { get; set; }

        public string commission_code { get; set; }

        public decimal commission_amount { get; set; }

        public decimal commission_paid { get; set; }

        public string address_id { get; set; }

        public string address { get; set; }

        public string phone { get; set; }

        public string pre_charge_yn { get; set; }

        public string post_charge_yn { get; set; }

        public DateTime business_date_created { get; set; }

        public int print_rate_yn { get; set; }

        public string extra_text1 { get; set; }

        public string extra_text2 { get; set; }

        public string extra_text3 { get; set; }

        public DateTime search_date { get; set; }

        public DateTime insert_date { get; set; }

        public int insert_user { get; set; }

        public DateTime update_date { get; set; }

        public int update_user { get; set; }

        public int company_id { get; set; }

        public int block_id { get; set; }

        public int source_id { get; set; }

        public int agent_id { get; set; }

        public DateTime NA_Date { get; set; }

        public int Is_PM { get; set; }

        public int Is_freeRoom { get; set; }

        public int Is_HouseUse { get; set; }

        public string rtc_reason { get; set; }

        public int Is_OpenBalance { get; set; }

        public string WaitReason { get; set; }

        public int WaitPriority { get; set; }

        public string WaitDescription { get; set; }

        public string insert_user_name { get; set; }

        public string update_user_name { get; set; }

        public string cancel_reason { get; set; }

        public decimal rate { get; set; }

        public string checkinTime { get; set; }

        public string checkoutTime { get; set; }

        public string crs_id { get; set; }

        public int ShareCode { get; set; }

        public decimal Balance { get; set; }

        public decimal TotAmount { get; set; }

        public int group_id { get; set; }

        public string change_reason { get; set; }

        public string seller { get; set; }

        public DateTime original_begin_date { get; set; }

        public string sellerRowId { get; set; }

        public decimal PAD { get; set; }

        public string extra_text4 { get; set; }

        public int superblock { get; set; }

        public string caller_member { get; set; }

        public string caller_idcard { get; set; }

        public string identityCard { get; set; }

    }
}