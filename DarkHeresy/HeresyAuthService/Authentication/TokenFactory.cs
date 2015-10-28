using HeresyCore.Entities;
using System;
using System.Security.Cryptography;
using System.Text;

namespace HeresyAuthService.Authentication
{
    public class TokenFactory
    {
        public string StaticSeed { get; set; } = "TokenFactory";
        public HashAlgorithm HashGenerator { get; set; } = SHA1.Create();

        #region Singleton

        private static object _lock = new object();
        private static TokenFactory _default;

        public static TokenFactory Default
        {
            get
            {
                lock (_lock)
                {
                    if (_default == null)
                    {
                        _default = new TokenFactory();
                    }
                }
                return _default;
            }
        }

        #endregion

        public Token GenerateToken(string seed, DateTime time, TimeSpan lifeTime)
        {
            var baseStr = $"{seed}@{time}@{StaticSeed}";
            var baseBytes = Encoding.Unicode.GetBytes(baseStr);
            var tokenBytes = HashGenerator.ComputeHash(baseBytes);
            var token = Convert.ToBase64String(tokenBytes);

            return new Token(token, time + lifeTime);
        }
    }
}