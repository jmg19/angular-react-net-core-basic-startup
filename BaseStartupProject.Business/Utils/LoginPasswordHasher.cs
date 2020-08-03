using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BaseStartupProject.Business.Utils
{
    public class LoginPasswordHasher : ILoginPasswordHasher
    {
        public string GenerateUserHash(int id, string username, string password)
        {
            string string_to_hash = string.Format("SUPER-SECRET-{0}-{1}-{2}", id, username, password);

            byte[] data = System.Text.Encoding.UTF8.GetBytes(string_to_hash);
            SHA512Managed shaM = new SHA512Managed();
            byte[] codigoXato = shaM.ComputeHash(data);

            StringBuilder sb = new StringBuilder();

            foreach (byte hex in codigoXato) 
            {
                sb.Append(hex.ToString("x2"));
            }


            return sb.ToString();
        }
    }
}
