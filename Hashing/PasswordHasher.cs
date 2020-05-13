using System;
using System.Security.Cryptography;
using System.Text;

namespace SPWPF.Hashing
{
    public class Hasher
    {
        public string GetHashedString(string str)
        {
            if (str == null)
                return null;

            MD5 md5 = MD5.Create();
            return Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(str)));
        }
    }
}
