﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Piaotong.OpenApi
{
    internal class ECB3DES
    {
        #region 3DES
        internal static string Encrypt(string key, string srcString)
        {
            try
            {
                var des = new TripleDESCryptoServiceProvider()
                {
                    Key = Encoding.UTF8.GetBytes(key),
                    Mode = CipherMode.ECB
                };
                ICryptoTransform desEncrypt = des.CreateEncryptor();
                byte[] buf = Encoding.UTF8.GetBytes(srcString);
                return Convert.ToBase64String(desEncrypt.TransformFinalBlock(buf, 0, buf.Length));

            }
            catch (Exception e)
            {
                throw new Exception(string.Format("3DES加密时出错,异常消息:{0}",e.Message));
            }
        }
        internal static string Decrypt(string key, string srcString)
        {
            try
            {
                var des = new TripleDESCryptoServiceProvider
                {
                    Key = Encoding.UTF8.GetBytes(key),
                    Mode = CipherMode.ECB
                };
                ICryptoTransform desDescrypt = des.CreateDecryptor();
                byte[] buf = Convert.FromBase64String(srcString);
                return Encoding.UTF8.GetString(desDescrypt.TransformFinalBlock(buf, 0, buf.Length));
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("3DES解密出错，异常消息:{0}", e.Message));
            }
        }
        #endregion 3DES

    }
}