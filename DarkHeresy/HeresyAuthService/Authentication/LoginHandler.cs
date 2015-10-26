using System.Collections.Generic;

namespace HeresyAuthService.Authentication
{
    public class LoginHandler
    {
        private IDictionary<string, User> _users;
        private IDictionary<Token, User> _usersByTokens;

        #region Singleton

        private static object _lock = new object();
        private static LoginHandler _default;

        public static LoginHandler Default
        {
            get
            {
                lock (_lock)
                {
                    if (_default == null)
                    {
                        _default = new LoginHandler();
                    }
                }
                return _default;
            }
        }

        #endregion

        public LoginHandler()
        {
            _users = new Dictionary<string, User>();
            _usersByTokens = new Dictionary<Token, User>();
        }

        public Token Login(string loginHash, string passHash)
        {
            User user;

            if (!_users.TryGetValue(loginHash, out user))
                return null;

            if (!user.Authorize(loginHash, passHash))
                return null;

            return user.Token;
        }
    }
}