using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace InvcSys
{
    internal class StringChecking
    {
        static string taxpayerNumPattern = "([A-Z]|[0-9]){15,20}";
        static string invoiceReqSerialPattern = "([a-z]|[A-Z]|[0-9]){20}";
        internal static bool TaxpayerNum(string input)
        {
            if(input.Length<15||
                input.Length>20||
                !Regex.IsMatch(input, taxpayerNumPattern))
            {
                return false;
            }
            return true;
        }

        internal static bool InvoiceReqSerialNo(string input)
        {
            if (input.Length!=20 ||
                !Regex.IsMatch(input,invoiceReqSerialPattern))
            {
                return false;
            }
            return true;
        }
    }
}
