using System;

namespace HeresyAuthService.Authentication
{
    public class User
    {
        public string ID { get; set; }
        public string LoginHash { get; set; }
        public string PasswordHash { get; set; }

        public Token Token { get; set; }
        public TimeSpan TokenLifeTime { get; set; }

        public bool Authorize(string loginHash, string passHash)
        {
            if (LoginHash != loginHash || PasswordHash != passHash)
                return false;

            var now = DateTime.Now;
            if (Token == null || Token.Expiration < now)
            {
                var tokenBase = $"{ID}@{LoginHash}";
                Token = TokenFactory.Default.GenerateToken(tokenBase, now, TokenLifeTime);
            }
            
            return true;
        }
    }
}