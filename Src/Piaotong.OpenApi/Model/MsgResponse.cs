using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piaotong.OpenApi
{
    public class ContentResponse
    {
        /// <summary>
        /// 发票请求流水号 必填
        /// </summary>
        public string invoiceReqSerialNo { get; set; }
        /// <summary>
        /// 二维码url 必填
        /// </summary>
        public string qrCodePath { get; set; }
        /// <summary>
        /// 二维码图片 Base64 必填
        /// </summary>
        public string qrCode { get; set; }
    }

    public class QueryContent
    {
        public string taxpayerNum { get; set; }
        public string invoiceReqSerialNo { get; set; }
        public string invoiceType { get; set; }

        public string code { get; set; }
        public string msg { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string tradeNo { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string securityCode { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string qrCode { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string invoiceCode { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string invoiceNo { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string invoiceDate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string noTaxAmount { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string taxAmount { get; set; }

        #region 纸质发票查询响应报文项
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string invoiceKindCode { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string cipherText { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string diskNo { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string definedData { get; set; }
        #endregion 纸质发票查询响应报文项

        #region 电子发票查询响应报文项
        public string invoicePdf { get; set; }
        public string downloadUrl { get; set; }
        #endregion 电子发票查询响应报文项


    }


    public class MsgResponse
    {
        /// <summary>
        /// 响应状态 必填
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 响应消息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 签名串 必填
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 交易请求流水号 必填
        /// </summary>
        public string serialNo { get; set; }
        /// <summary>
        /// 业务报文内容 必填
        /// </summary>
        public string content { get; set; }

        public ContentResponse ContentResponse { get; set; }
        public QueryContent QueryContent { get; set; }
    }
}
