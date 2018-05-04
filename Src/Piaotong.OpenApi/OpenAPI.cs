/***************************************************************************
 * 文件名:             OpenAPI.cs
 * 作者：              Liuy
 * 日期：              2018-04-15
 * 描述：              OpenApi实现
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Piaotong.OpenApi
{
    public class OpenAPI
    {
        public OpenAPI(string passwd, string platformCode, string prefix, string privateKey,string publicKey)
        {
            this.mPlatformCode = platformCode;
            this.mPrefix = prefix;
            this.mPasswd = passwd;
            this.mPrivateKey = privateKey;
            this.mPublicKey = publicKey;
        }
        public OpenAPI(string version, string passwd, string platformCode, string prefix, string privateKey,string publicKey)
            : this(passwd, platformCode, prefix, privateKey,publicKey)
        {
            this.mVersion = version;
        }
        
        string mPrefix;
        string mPasswd;
        string mVersion = "1.0";
        string mPublicKey;
        string mPrivateKey;
        
        string mPlatformCode;
        /// <summary>
        /// 提交开票的json
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public MsgResponse PostJson(string url, string content)
        {
            MsgResponse rsp = null;
            try
            {
                string json = BuildRequestJSON(content);
                string jsonRep = HttpHelper.Post(url, Encoding.UTF8.GetBytes(json));
                rsp = JsonHelper.String2Object<MsgResponse>(jsonRep);
                Dictionary<string,string> dic=JsonHelper.String2Object<Dictionary<string,string>>(jsonRep);
                
                dic.Remove("sign");
                string contentResp = RSAHelper.GenerateSignContent(dic);
                // 验签结果
                bool ret = RSAHelper.VerifyByJavaPublicKey(contentResp, rsp.sign,mPublicKey);
                if (!ret)
                {
                    throw new Exception("接口返回的数据验签失败!");
                }
                rsp.content = DESHelper.Decrypt(mPasswd, rsp.content);
                rsp.ContentResponse = JsonHelper.String2Object<ContentResponse>(rsp.content);
                return rsp;
            }
            catch (Exception e)
            {
                if (rsp == null)
                {
                    rsp = new MsgResponse();
                }
                rsp.msg=e.Message;
                return rsp;
            }
        }
        /// <summary>
        /// 提交查询发票的Json
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public MsgResponse PostQueryJson(string url, string content)
        {
            MsgResponse rsp = null;
            try
            {
                string json = BuildRequestJSON(content);
                string jsonRep = HttpHelper.Post(url, Encoding.UTF8.GetBytes(json));
                rsp = JsonHelper.String2Object<MsgResponse>(jsonRep);
                Dictionary<string, string> dic = JsonHelper.String2Object<Dictionary<string, string>>(jsonRep);

                dic.Remove("sign");
                string contentResp = RSAHelper.GenerateSignContent(dic);
                // 验签结果
                bool ret = RSAHelper.VerifyByJavaPublicKey(contentResp, rsp.sign, mPublicKey);
                if (!ret)
                {
                    throw new Exception("接口返回的数据验签失败!");
                }
                rsp.content = DESHelper.Decrypt(mPasswd, rsp.content);
                rsp.QueryContent = JsonHelper.String2Object<QueryContent>(rsp.content);
                return rsp;
            }
            catch (Exception e)
            {
                if (rsp == null)
                {
                    rsp = new MsgResponse();
                }
                rsp.msg = e.Message;
                return rsp;
            }
        }

        #region 私有
        /// <summary>
        /// 创建json
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        string BuildRequestJSON(string content)
        {

            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg.Add("platformCode", mPlatformCode);
            msg.Add("signType", "RSA");
            msg.Add("format", "JSON");
            msg.Add("version", this.mVersion);
            try
            {
                msg.Add("content", DESHelper.Encrypt(mPasswd, content));
                msg.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                msg.Add("serialNo", SerialNoHelper.GetSerialNo(mPrefix));
                string sign =RSAHelper.GenerateSignContent(msg);
                msg.Add("sign", RSAHelper.SignByJavaPrivateKey(sign, mPrivateKey));
                return JsonHelper.Object2String<Dictionary<string, string>>(msg);
            }
            catch (Exception e)
            {
                throw new Exception("生成完整JSON时出错:" + e.Message);
            }
        }
        #endregion 私有
    }
}
