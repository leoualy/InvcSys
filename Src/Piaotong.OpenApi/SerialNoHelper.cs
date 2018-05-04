/***************************************************************************
 * 文件名:             SerialNoHelper.cs
 * 作者：              Liuy
 * 日期：              2018-04-15
 * 描述：              序列号生成工具
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piaotong.OpenApi
{
    public class SerialNoHelper
    {
        /// <summary>
        /// 序列号：4位平台简介+14位日期+8位随机数
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        internal static string GetSerialNo(string prefix)
        {
            Random rand = CreateRandom();
            double val = rand.Next(10000000, 99999999);
            
            return string.Format("{0}{1}{2}", prefix, DateTime.Now.ToString("yyyyMMddHHmmss"),val);
        }

        /// <summary>
        /// 生成发票请求流水号
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string GetInvoiceReqSerialNo(string prefix)
        {
            Random rand = CreateRandom();
            double val = rand.Next(10000000, 99999999);
            double val1 = rand.Next(10000000, 99999999);
            
            return string.Format("{0}{1}{2}", prefix, val,val1);
        }

        static Random CreateRandom()
        {
            long tick = DateTime.Now.Ticks;
            int realTick = (int)(tick & 0xffffffffL) | (int)(tick >> 32);
            Random rd = new Random(realTick);
            return rd;
        }
    }
}
