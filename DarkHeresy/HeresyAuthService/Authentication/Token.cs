using System;
using System.Runtime.Serialization;

namespace HeresyAuthService.Authentication
{
    [DataContract]
    public class Token
    {
        [DataMember]
        public string StrToken { get; set; }

        [DataMember]
        public DateTime Expiration { get; set; }

        public Token(string strToken, DateTime expiration)
        {
            StrToken = strToken;
            Expiration = expiration;
        }

        public override bool Equals(object obj)
        {
            var tok = obj as Token;

            if (tok != null)
            {
                return StrToken == tok.StrToken;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return StrToken.GetHashCode();
        }
    }
}