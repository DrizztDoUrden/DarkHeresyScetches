using System;

namespace HeresyCore.Entities
{
    public class UserSettings
    {
        public TimeSpan TokenLifeTime { get; set; }
        public bool AutoResetToken { get; set; }

        public UserSettings()
        {

        }

        public UserSettings(UserSettings defaults)
        {
            TokenLifeTime = defaults.TokenLifeTime;
            AutoResetToken = defaults.AutoResetToken;
        }
    }
}
