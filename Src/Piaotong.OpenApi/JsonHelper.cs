/***************************************************************************
 * 文件名:             JsonHelper
 * 作者：              Liuy
 * 日期：              2018-04-15
 * 描述：              Newtonsoft 方法封装
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Piaotong.OpenApi
{
    public class JsonHelper
    {
        public static string Object2String<T>(T t)
        {
            try
            {
                return JsonConvert.SerializeObject(t);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("转换json时出错,异常消息:{0}", e.Message));
            }
        }
    }
}
