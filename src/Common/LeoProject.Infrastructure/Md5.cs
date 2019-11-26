using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LeoProject.Infrastructure
{
    public class Md5
    {
        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encrypt(string str)
        {
            MD5 md5Hash = MD5.Create();

            // 编码UTF8/Unicode　
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(str));
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
