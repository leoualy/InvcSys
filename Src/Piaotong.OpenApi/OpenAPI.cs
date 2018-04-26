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
        public OpenAPI(string passwd, string platformCode, string prefix, string privateKey)
        {
            this.mPlatformCode = platformCode;
            this.mPrefix = prefix;
            this.mPasswd = passwd;
            this.mPrivateKey = privateKey;
        }
        
        string mPrefix;
        string mPasswd;
        string mPrivateKey;
        string mPlatformCode;
        
        

        public MsgResponse PostJson(string url, string content)
        {
            MsgResponse rsp = null;
            try
            {
                string json = BuildRequestJSON(content);
                string jsonRep = HttpHelper.Post(url, Encoding.UTF8.GetBytes(json));
                rsp = JsonHelper.String2Object<MsgResponse>(jsonRep);
                rsp.content = ECB3DES.Decrypt(mPasswd, rsp.content);
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

        #region 私有
        string BuildRequestJSON(string content)
        {

            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg.Add("platformCode", mPlatformCode);
            msg.Add("signType", "RSA");
            msg.Add("format", "JSON");
            msg.Add("version", "1.0");
            try
            {
                msg.Add("content", ECB3DES.Encrypt(mPasswd, content));
                msg.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                msg.Add("serialNo", SerialNoHelper.GetSerialNo(mPrefix));
                string sign = generateSignContent(msg);
                msg.Add("sign", RSAHelper.Sign(sign, mPrivateKey));
                return JsonHelper.Object2String<Dictionary<string, string>>(msg);
            }
            catch (Exception e)
            {
                throw new Exception("生成完整JSON时出错:" + e.Message);
            }
        }
        string generateSignContent(Dictionary<string,string> hash)
        {
            List<string> keys=hash.Keys.ToList();
            keys.Sort();
            StringBuilder builder = new StringBuilder();
            string val;
            for (int i = 0; i < keys.Count; i++)
            {
                hash.TryGetValue(keys[i],out val);
                builder.Append(string.Format("{0}={1}", keys[i], val));
                if (i < keys.Count - 1)
                {
                    builder.Append("&");
                }
            }
            return builder.ToString();
        }
        #endregion 私有
    }
}
