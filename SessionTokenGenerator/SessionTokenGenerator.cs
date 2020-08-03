using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Global.SessionTokenGeneratorPack
{
    public class SessionTokenGenerator : ISessionTokenGenerator
    {
        private string key;

        public SessionTokenGenerator(string key)
        {
            this.key = key;
        }
        public SessionToken Decrypt(string tokenString)
        {
            RijndaelManaged AES = new RijndaelManaged();
            MD5CryptoServiceProvider Hash_AES = new MD5CryptoServiceProvider();            

            Byte[] hash = new Byte[32];
            Byte[] temp = Hash_AES.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
            Array.Copy(temp, 0, hash, 0, 16);
            Array.Copy(temp, 0, hash, 15, 16);
            AES.Key = hash;
            AES.Mode = CipherMode.ECB;
            ICryptoTransform DESDecrypter = AES.CreateDecryptor();
            Byte[] Buffer = Convert.FromBase64String(tokenString);
            string decrypted = ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));

            string[] aux_array_string_token = decrypted.Split("<SPLIT>");
            SessionToken token = new SessionToken() {
                dateTime = Convert.ToDateTime(aux_array_string_token[0]),
                daysToExpire = Convert.ToInt32(aux_array_string_token[1]),
                UUID = aux_array_string_token[2],
                userId = Convert.ToInt32(aux_array_string_token[3]),
                userName = aux_array_string_token[4]
            };

            return token;
        }

        public string Encript(SessionToken token)
        {
            string aux_string_token = $"{token.dateTime}<SPLIT>{token.daysToExpire}<SPLIT>{token.UUID}<SPLIT>{token.userId}<SPLIT>{token.userName}";
            var AES = new RijndaelManaged();
            var Hash_AES = new MD5CryptoServiceProvider();
            
            var hash = new byte[32];
            byte[] temp = Hash_AES.ComputeHash(Encoding.ASCII.GetBytes(key));
            Array.Copy(temp, 0, hash, 0, 16);
            Array.Copy(temp, 0, hash, 15, 16);
            AES.Key = hash;
            AES.Mode = CipherMode.ECB;
            ICryptoTransform DESEncrypter = AES.CreateEncryptor();
            var Buffer = Encoding.ASCII.GetBytes(aux_string_token);
            string encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));
            return encrypted;
        }
    }
}
