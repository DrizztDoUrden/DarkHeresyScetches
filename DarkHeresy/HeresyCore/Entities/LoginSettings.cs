using System;

namespace HeresyCore.Entities
{
    public class LoginSettings
    {
        public TimeSpan TokenLifeTime { get; set; }
        public bool AutoResetToken { get; set; }

        public LoginSettings()
        {

        }

        public LoginSettings(LoginSettings defaults)
        {
            TokenLifeTime = defaults.TokenLifeTime;
            AutoResetToken = defaults.AutoResetToken;
        }
    }
}
