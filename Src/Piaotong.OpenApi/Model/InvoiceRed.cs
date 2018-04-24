using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Piaotong.OpenApi
{
    public class InvoiceRed
    {
        /// <summary>
        /// 纳税人标识号 必填
        /// </summary>
        public string taxpayerNum { get; set; }
        /// <summary>
        /// 发票请求流水号 必填
        /// </summary>
        public string invoiceReqSerialNo { get; set; }
        /// <summary>
        /// 发票代码 必填
        /// </summary>
        public string invoiceCode { get; set; }
        /// <summary>
        /// 发票号码 必填
        /// </summary>
        public string invoiceNo { get; set; }
        /// <summary>
        /// 冲红原因 必填
        /// </summary>
        public string redReason { get; set; }
        /// <summary>
        /// 价税合计金额 必填
        /// </summary>
        public string amount { get; set; }

        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        /// <summary>
        /// 自定义数据
        /// </summary>
        public string definedData { get; set; }
    }
}
