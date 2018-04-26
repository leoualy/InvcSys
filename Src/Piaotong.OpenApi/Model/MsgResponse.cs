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
    }
}
