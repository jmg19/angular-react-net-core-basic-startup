using System;
using System.Collections.Generic;
using System.Text;

namespace Global.SessionTokenGeneratorPack
{
    public interface ISessionTokenGenerator
    {
        string Encript(SessionToken token);
        SessionToken Decrypt(string tokenString);
    }
}
