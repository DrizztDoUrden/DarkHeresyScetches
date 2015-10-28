using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace HeresyCore.Entities
{
    [DataContract]
    public class Token
    {
        [DataMember]
        public string StrToken { get; set; }

        [DataMember]
        public DateTime Expiration { get; set; }

        public static implicit operator string(Token token)
        {
            return token.StrToken;
        }

        public static implicit operator DateTime (Token token)
        {
            return token.Expiration;
        }

        #region Constructors

        [DebuggerStepThrough]
        public Token(string strToken, DateTime expiration)
        {
            StrToken = strToken;
            Expiration = expiration;
        }

        #endregion

        #region Equality and hash

        [DebuggerStepThrough]
        public static bool operator ==(Token left, Token right)
        {
            return ReferenceEquals(left, null) && ReferenceEquals(right, null)
                || !ReferenceEquals(left, null) && !ReferenceEquals(right, null) && left.Equals(right);
        }

        [DebuggerStepThrough]
        public static bool operator !=(Token left, Token right)
        {
            return ReferenceEquals(left, null) && !ReferenceEquals(right, null)
                || !ReferenceEquals(left, null) && ReferenceEquals(right, null)
                || !ReferenceEquals(left, null) && !ReferenceEquals(right, null) && !left.Equals(right);
        }

        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            var tok = obj as Token;

            if (tok != null)
            {
                return StrToken == tok.StrToken;
            }

            return false;
        }

        [DebuggerStepThrough]
        public override int GetHashCode()
        {
            return StrToken.GetHashCode();
        }

        #endregion
    }
}