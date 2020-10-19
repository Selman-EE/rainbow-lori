using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class Encryption
    {
        private static readonly byte[] Vector = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private static readonly byte[] _key = Encoding.UTF8.GetBytes("Ff!.678?");

        public static string Encrypt(this string text)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(text))
                    return text;

                var inputArr = Encoding.UTF8.GetBytes(text);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, new DESCryptoServiceProvider().CreateEncryptor(_key, Vector), CryptoStreamMode.Write);
                cs.Write(inputArr, 0, inputArr.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch
            {
                return null;
            }
        }

        public static string Decrypt(this string text)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(text))
                    return text;

                var inputArr = Convert.FromBase64String(text);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, new DESCryptoServiceProvider().CreateDecryptor(_key, Vector), CryptoStreamMode.Write);
                cs.Write(inputArr, 0, inputArr.Length);
                cs.FlushFinalBlock();
                return Encoding.UTF8.GetString(ms.ToArray()).Replace(" ", "+").Trim();
            }
            catch
            {
                return null;
            }
        }
    }
}
