using HeresyCore.Entities;
using System;

namespace HeresyAuthService.Authentication
{
    public class Login
    {
        public string ID { get; set; }
        public string LoginHash { get; set; }
        public string PasswordHash { get; set; }
        public Token Token { get; set; }
        public LoginSettings Settings { get; set; }

        #region Constructors

        public Login(string loginHash, string passHash, LoginSettings defaults)
        {
            LoginHash = loginHash;
            PasswordHash = passHash;

            Settings = new LoginSettings(defaults);
            ID = Guid.NewGuid().ToString("D");
        }

        #endregion

        public bool Authorize(string loginHash, string passHash)
        {
            if (LoginHash != loginHash || PasswordHash != passHash)
                return false;

            var now = DateTime.Now;
            if (Token == null || Token.Expiration < now)
            {
                var tokenBase = $"{ID}@{LoginHash}";
                Token = TokenFactory.Default.GenerateToken(tokenBase, now, Settings.TokenLifeTime);
            }
            
            return true;
        }

        private void TryUpdateToken()
        {
            if (Settings.AutoResetToken)
            {
                Token.Expiration = DateTime.Now + Settings.TokenLifeTime;
            }
        }
    }
}