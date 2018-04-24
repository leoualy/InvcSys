using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piaotong.OpenApi
{
    internal class SerialNoUtil
    {
        internal static string GetSerialNo(string prefix)
        {
            return string.Format("{0}{1}", prefix, DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}
