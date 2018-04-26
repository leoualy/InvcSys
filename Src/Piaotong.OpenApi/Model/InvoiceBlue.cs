using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Piaotong.OpenApi
{
    public class Goods
    {
        /// <summary>
        /// 货物名称 必填
        /// </summary>
        public string goodsName { get; set; }
        /// <summary>
        /// 对应税收分类 必填
        /// </summary>
        public string taxClassificationCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 对应规格型号
        /// </summary>
        public string specificationModel { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 单位
        /// </summary>
        public string meteringUnit { get; set; }
        /// <summary>
        /// 数量 必填
        /// </summary>
        public string quantity { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 含税标示
        /// </summary>
        public string includeTaxFlag { get; set; }
        /// <summary>
        /// 单价 必填
        /// </summary>
        public string unitPrice { get; set; }
        /// <summary>
        /// 金额 必填
        /// </summary>
        public string invoiceAmount { get; set; }
        /// <summary>
        /// 税率 必填
        /// </summary>
        public string taxRateValue { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 税额 
        /// </summary>
        public string taxRateAmount { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 折扣金额
        /// </summary>
        public string discountAmount { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 折扣税额
        /// </summary>
        public string discountTaxRateAmount { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 折扣率值
        /// </summary>
        public string discountRateValue { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 差额开票抵扣金额
        /// </summary>
        public string deductionAmount { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 优惠政策标识
        /// </summary>
        public string preferentialPolicyFlag { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 增值税特殊管理
        /// </summary>
        public string vatSpecialManage { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 零税率标识
        /// </summary>
        public string zeroTaxFlag { get; set; }
    }
    public class InvoiceBlue
    {
        /// <summary>
        /// 纳税人识别号 必填
        /// </summary>
        public string taxpayerNum { get; set; }
        /// <summary>
        /// 发票请求流水号 必填
        /// </summary>
        public string invoiceReqSerialNo { get; set; }
        /// <summary>
        /// 购买方名称 必填
        /// </summary>
        public string buyerName { get; set; }

        /// <summary>
        /// 开票项目列表 必填
        /// </summary>
        public string itemList { get; set; }

        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        /// <summary>
        /// 购买方纳税人
        /// </summary>
        public string buyerTaxpayerNum { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 购买方地址
        /// </summary>
        public string buyerAddress { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 购买方电话 
        /// </summary>
        public string buyerTel { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 购买方开户行
        /// </summary>
        public string buyerBankName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 购买方银行账号
        /// </summary>
        public string buyerBankAccount { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 销货方地址
        /// </summary>
        public string sellerAddress { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 销货方电话
        /// </summary>
        public string sellerTel { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 销货方开户行
        /// </summary>
        public string sellerBankName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 销货方开户行账号
        /// </summary>
        public string sellerBankAccount { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 主开票项目名称
        /// </summary>
        public string itemName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 收款人名称
        /// </summary>
        public string casherName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 复核人名称
        /// </summary>
        public string reviewerName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 开票人名称
        /// </summary>
        public string drawerName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 收票人名称
        /// </summary>
        public string takerName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 收票人手机号
        /// </summary>
        public string takerTel { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 收票人邮箱
        /// </summary>
        public string takerEmail { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

       
        

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 代开标示
        /// </summary>
        public string agentInvoiceFlag { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 分机号
        /// </summary>
        public string extensionNum { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 订单号
        /// </summary>
        public string tradeNo { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// <summary>
        /// 自定义数据
        /// </summary>
        public string definedData { get; set; }
    }
}
