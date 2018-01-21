using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class TamperProofString
    {
        //Function to encode the string
        static public string TamperProofStringEncode(string value, string key)
        {
            System.Security.Cryptography.MACTripleDES mac3des = new System.Security.Cryptography.MACTripleDES();
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));
            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(value)) + System.Convert.ToChar("-") + System.Convert.ToBase64String(mac3des.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value)));
        }

        //Function to decode the string
        //Throws an exception if the data is corrupt
        static public string TamperProofStringDecode(string value, string key)
        {
            String dataValue = "";
            String calcHash = "";
            String storedHash = "";

            System.Security.Cryptography.MACTripleDES mac3des = new System.Security.Cryptography.MACTripleDES();
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));

            try
            {
                dataValue = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(value.Split(System.Convert.ToChar("-"))[0]));
                storedHash = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(value.Split(System.Convert.ToChar("-"))[1]));
                calcHash = System.Text.Encoding.UTF8.GetString(mac3des.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dataValue)));

                if (storedHash != calcHash)
                {
                    //Data was corrupted
                    throw new ArgumentException("Hash value does not match");
                    //This error is immediately caught below
                }

            }
            catch (System.Exception)
            {
                throw new ArgumentException("Invalid TamperProofString");
            }
            return dataValue;
        }

        static public string QueryStringEncode(string value)
        {
            return System.Web.HttpUtility.UrlEncode(TamperProofStringEncode(value.Trim(), ConfigurationManager.AppSettings["TamperProofKey"]));
        }

        static public string QueryStringDecode(string value)
        {
            return TamperProofStringDecode(value.Trim().Replace(" ", "+"), ConfigurationManager.AppSettings["TamperProofKey"]);
        }
    }
}
