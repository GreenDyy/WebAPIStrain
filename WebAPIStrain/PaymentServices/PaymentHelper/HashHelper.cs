using System;
using System.Security.Cryptography;
using System.Text;

namespace WebAPIStrain.PaymentServices.PaymentHelper
{
    public class HashHelper
    {
        public static string HmacSHA256(string inputData, string key)
        {
            byte[] keyByte = Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                string hex = BitConverter.ToString(hashmessage);
                hex= hex.Replace("-", "").ToLower();
                return hex;
            }
        }
    }
}
