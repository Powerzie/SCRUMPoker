using System;
using System.Security.Cryptography;
using System.Text;

namespace SPWPF.Hashing
{
    public interface IPasswordHandler
    {
        string GetHashedPassword(string pass);
    }
    public class UserPassword : IPasswordHandler
    {
        public string GetHashedPassword(string pass)
        {
            MD5 md5 = MD5.Create();
            return Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(pass)));
        }
    }
}
