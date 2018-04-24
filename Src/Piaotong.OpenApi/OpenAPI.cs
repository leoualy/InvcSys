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
        
        public string BuildRequestJSON(string content)
        {
            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg.Add("platformCode", mPlatformCode);
            msg.Add("signType", "RSA");
            msg.Add("format", "JSON");
            msg.Add("version", "1.0");
            msg.Add("content",ECB3DES.Encrypt(mPasswd,content));
            msg.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            msg.Add("serialNo", SerialNoUtil.GetSerialNo(mPrefix));
            string sign = generateSignContent(msg);
            Console.WriteLine(sign);
            msg.Add("sign", RSAHelper.Sign(sign, mPrivateKey));

            return JsonHelper.Object2String<Dictionary<string,string>>(msg);
        }


        string generateSignContent(Dictionary<string,string> hash)
        {
            // 将键值按字母升序排序
            //ArrayList keys = new ArrayList(hash.Keys);
            //keys.Sort();
            //hash.Keys.ToList().Sort();
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


    }
}
