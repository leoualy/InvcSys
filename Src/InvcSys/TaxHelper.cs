using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvcSys
{
    internal class TaxHelper
    {
        /// <summary>
        /// 计算税额
        /// </summary>
        /// <param name="quantity">物品数量</param>
        /// <param name="price">价格</param>
        /// <param name="taxRate">税率</param>
        /// <returns></returns>
        internal static decimal GetTaxAmount(int quantity, decimal price, decimal taxRate)
        {
            decimal amount = decimal.Multiply(quantity, price);
            return decimal.Multiply(amount, taxRate);
        }
    }
}
