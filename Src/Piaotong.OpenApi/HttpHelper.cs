/***************************************************************************
 * 文件名:             HttpHelper
 * 作者：              Liuy
 * 日期：              2018-04-15
 * 描述：              http实现
***************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Piaotong.OpenApi
{
    internal class HttpHelper
    {
        internal static string Post(string url,byte[] content)
        {
            HttpWebRequest rq = WebRequest.Create(url) as HttpWebRequest;
            rq.Accept = "*";
            rq.Method = "POST";
            rq.ContentType = "application/json";
            rq.ContentLength = content.Length;
            // 超时值为50毫秒
            rq.Timeout = 50000;
            try
            {
                using (Stream s = rq.GetRequestStream())
                {
                    s.Write(content, 0, content.Length);
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("向请求流写入数据失败:{0}",e.Message));
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
                throw new Exception(string.Format("获取http响应时出错:{0}", e.Message));
            }
        }

    }
}
