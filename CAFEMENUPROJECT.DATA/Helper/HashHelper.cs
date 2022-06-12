using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEMENUPROJECT.DATA.Helper
{
    public class HashHelper
    {
        public static string Hash(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            return passwordHash;
        }

        public static string Salt(string password)
        {
            password = password+ "a5da654";
            string passwordSalt = BCrypt.Net.BCrypt.HashPassword(password);

            return passwordSalt;
        }

        public static bool Verify(string password,string HashPassword,string SaltPassword)
        {
            string password2 = HashPassword + "a5da654";
            bool verified = BCrypt.Net.BCrypt.Verify(password,HashPassword);
            bool verified2 = BCrypt.Net.BCrypt.Verify(password2, SaltPassword);

            if (verified == true && verified2 == true)
            {
                return true;
            }

            return false;
        }
    }
}
