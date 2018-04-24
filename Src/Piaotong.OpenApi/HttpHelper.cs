using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Piaotong.OpenApi
{
    public class HttpHelper
    {
        public static string Post(string url,byte[] content)
        {
            HttpWebRequest rq = WebRequest.Create(url) as HttpWebRequest;
            rq.Accept = "*";
            rq.Method = "POST";
            rq.ContentType = "application/json";
            rq.ContentLength = content.Length;

            try
            {
                using (Stream s = rq.GetRequestStream())
                {
                    s.Write(content, 0, content.Length);
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("向请求流写入数据时出错,异常消息:{0}", e.Message));
            }
            try
            {
                using (HttpWebResponse rp = rq.GetResponse() as HttpWebResponse)
                {
                    using (Stream s = rp.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(s, Encoding.UTF8);
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("发送POST请求时出错,异常消息:{0}", e.Message));
            }
        }
    }
}
