/***************************************************************************
 * 文件名:             RSAHelper.cs
 * 作者：              Liuy
 * 日期：              2018-04-15
 * 描述：              RSA 加签验签
***************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;

namespace Piaotong.OpenApi
{
    internal class RSAHelper
    {
        #region 使用java提供的密钥对进行加签验签
        static readonly string Sign_Algorithm = "SHA1WITHRSA";
        /// <summary>
        /// RSA加签
        /// </summary>
        /// <param name="content"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        internal static string SignByJavaPrivateKey(string content, string javaPrivateKey)
        {
            try
            {
                RsaKeyParameters privateKeyParam = (RsaKeyParameters)PrivateKeyFactory.CreateKey(
                Convert.FromBase64String(javaPrivateKey));

                ISigner signer = SignerUtilities.GetSigner(Sign_Algorithm);
                signer.Init(true, privateKeyParam);//参数为true验签，参数为false加签  
                var dataByte = Encoding.UTF8.GetBytes(content);
                signer.BlockUpdate(dataByte, 0, dataByte.Length);
                //return Encoding.GetEncoding(encoding).GetString(signer.GenerateSignature()); //签名结果 非Base64String  
                return Convert.ToBase64String(signer.GenerateSignature()); 
            }
            catch(Exception e)
            {
                throw new Exception("数据加签时出错:" + e.Message);
            }
            

        }

        /// <summary>
        /// RSA验签
        /// </summary>
        /// <param name="content"></param>
        /// <param name="signedString"></param>
        /// <param name="javaPublicKey"></param>
        /// <returns></returns>
        internal static bool VerifyByJavaPublicKey(string content, string signedString, string javaPublicKey)
        {
            try
            {
                RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(javaPublicKey));
                ISigner signer = SignerUtilities.GetSigner(Sign_Algorithm);
                signer.Init(false, publicKeyParam);
                byte[] dataByte = Encoding.UTF8.GetBytes(content);
                signer.BlockUpdate(dataByte, 0, dataByte.Length);

                byte[] signatureByte = Convert.FromBase64String(signedString);
                return signer.VerifySignature(signatureByte);
            }
            catch (Exception e)
            {
                throw new Exception("数据验签时出错:" + e.Message);
            }
        }

        #endregion 使用java提供的密钥对进行加签验签
        /// <summary>
        /// 生成明文的待加签串
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        internal static string GenerateSignContent(Dictionary<string, string> hash)
        {
            List<string> keys = hash.Keys.ToList();
            keys.Sort();
            StringBuilder builder = new StringBuilder();
            string val;
            for (int i = 0; i < keys.Count; i++)
            {
                hash.TryGetValue(keys[i], out val);
                builder.Append(string.Format("{0}={1}", keys[i], val));
                if (i < keys.Count - 1)
                {
                    builder.Append("&");
                }
            }
            return builder.ToString();
        }


        #region 暂时未使用的方法
        private static RSAParameters ConvertFromPublicKey(string pemFileConent)
        {

            byte[] keyData = Convert.FromBase64String(pemFileConent);
            if (keyData.Length < 162)
            {
                throw new ArgumentException("pem file content is incorrect.");
            }
            byte[] pemModulus = new byte[128];
            byte[] pemPublicExponent = new byte[3];
            Array.Copy(keyData, 29, pemModulus, 0, 128);
            Array.Copy(keyData, 159, pemPublicExponent, 0, 3);
            RSAParameters para = new RSAParameters();
            para.Modulus = pemModulus;
            para.Exponent = pemPublicExponent;
            return para;
        }


        public static void CreateKeys()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            
            using (StreamWriter writer = new StreamWriter(@"J:\GitHub\InvcSys\PrivateKey.xml"))
            {
                writer.WriteLine(rsa.ToXmlString(true));
            }
            using (StreamWriter writer = new StreamWriter(@"J:\GitHub\InvcSys\PublicKey.xml"))
            {
                writer.WriteLine(rsa.ToXmlString(false));
            }
        }

        public static string PrivateKeyJava2Dotnet(string keyJava)
        {
            try
            {
                RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)
                PrivateKeyFactory.CreateKey(Convert.FromBase64String(keyJava));
                return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
                Convert.ToBase64String(privateKeyParam.Modulus.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.PublicExponent.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.P.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.Q.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.DP.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.DQ.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.QInv.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.Exponent.ToByteArrayUnsigned()));    
            }
            catch (Exception e)
            {
                throw new Exception("java格式私钥转化为.net格式时出错:" + e.Message);
            }
            
        }
        public static string PublicKeyJava2Dotnet(string keyJava)
        {
            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(keyJava));
            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
                Convert.ToBase64String(publicKeyParam.Modulus.ToByteArrayUnsigned()),
                Convert.ToBase64String(publicKeyParam.Exponent.ToByteArrayUnsigned()));    
        }

        public static string SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature)
        {
            //byte[] rgbHash = Convert.FromBase64String(m_strHashbyteSignature);
            byte[] rgbHash = Encoding.UTF8.GetBytes(m_strHashbyteSignature);
            RSACryptoServiceProvider key = new RSACryptoServiceProvider();
            key.FromXmlString(p_strKeyPrivate);
            RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);
            formatter.SetHashAlgorithm("SHA1withRSA");
            byte[] inArray = formatter.CreateSignature(rgbHash);
            return Convert.ToBase64String(inArray);
        }
        #endregion 暂时未使用的方法


    }
}
