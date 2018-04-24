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
    public class RSAHelper
    {
        public static string Sign(string content, string privateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            SHA1 sh = new SHA1CryptoServiceProvider();
            byte[] bytesSigned = rsa.SignData(Encoding.UTF8.GetBytes(content), sh);
            
            return Convert.ToBase64String(bytesSigned);
        }
        //public static string Sign()
        




        public static bool Verify(string content, string signedString, string publicKey)
        {
            byte[] Data = Encoding.UTF8.GetBytes(content);
            byte[] data = Convert.FromBase64String(signedString);
            RSAParameters paraPub = ConvertFromPublicKey(publicKey);
            RSACryptoServiceProvider rsaPub = new RSACryptoServiceProvider();
            rsaPub.ImportParameters(paraPub);

            SHA1 sh = new SHA1CryptoServiceProvider();
            return rsaPub.VerifyData(data, sh, Data);
        }

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


       
    }
}
