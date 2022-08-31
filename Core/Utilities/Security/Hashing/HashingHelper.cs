using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string passwd, out byte[] passwdHash, out byte[] passwdSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwdSalt = hmac.Key;
                passwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwd));
            }
        }
        public static bool VerifyPasswdHash(string passwd, byte[] passwdHash, byte[] passwdSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwdSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwd));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwdHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
