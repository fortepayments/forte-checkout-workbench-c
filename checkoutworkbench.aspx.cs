using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;


namespace Workbench
{
    public partial class CheckoutWorkbench : System.Web.UI.Page
     {

         protected static string key = "YourSecureTransKey";    //Change to your Secure Transaction Key

        [WebMethod]
        public static string sign(string Attributes, string Hash_Method = "MD5")
        {
            // Check for valid session

            string Signature = "error";    
            if ((Attributes != null) && !string.IsNullOrEmpty(Attributes))
                {
                    

                    System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                    byte[] keyByte = encoding.GetBytes(key);
                    byte[] messageBytes = encoding.GetBytes(Attributes);
                    byte[] hashmessage;

                    try
                    {
                        switch ((Hash_Method).ToUpper())
                        {

                            case "SHA1":
                                hashmessage = null;
                                Signature = null;
                                HMACSHA1 hmacsha1 = new HMACSHA1(keyByte);
                                hashmessage = hmacsha1.ComputeHash(messageBytes);
                                Signature = ByteToString(hashmessage);
                                break;

                            case "SHA256":
                                hashmessage = null;
                                Signature = null;
                                HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);
                                hashmessage = hmacsha256.ComputeHash(messageBytes);
                                Signature = ByteToString(hashmessage);
                                break;

                            default:
                            case "MD5":
                                hashmessage = null;
                                Signature = null;
                                HMACMD5 hmacmd5 = new HMACMD5(keyByte);
                                hashmessage = hmacmd5.ComputeHash(messageBytes);
                                Signature = ByteToString(hashmessage);
                                break;
                        }

                 }
                 catch (Exception e)
                 {
                    return e.Message;
                }
            }
            return Signature;
        }

        public static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }
    }

}