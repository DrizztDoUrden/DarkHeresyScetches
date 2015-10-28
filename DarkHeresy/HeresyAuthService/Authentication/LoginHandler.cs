using HeresyCore.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HeresyAuthService.Authentication
{
    public class LoginHandler
    {
        private IDictionary<string, Login> _users;
        private IDictionary<string, Login> _usersByTokens;

        public LoginSettings DefaultUserSettings { get; set; }

        #region Singleton

        private static object _lock = new object();
        private static LoginHandler _default;

        public static LoginHandler Default
        {
            [DebuggerStepThrough]
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

        #region Constructors

        private void Init()
        {
            _usersByTokens = new Dictionary<string, Login>();
            DefaultUserSettings = new LoginSettings
            {
                AutoResetToken = true,
                TokenLifeTime = new TimeSpan(72, 0, 0),
            };
        }

        [DebuggerStepThrough]
        public LoginHandler()
        {
            Init();
            _users = new Dictionary<string, Login>();
        }

        [DebuggerStepThrough]
        public LoginHandler(IDictionary<string, Login> userProvider)
        {
            Init();
            _users = userProvider;
        }

        #endregion

        #region Authentication

        public Token Login(string loginHash, string passHash)
        {
            Login user;

            if (string.IsNullOrEmpty(loginHash)
                || string.IsNullOrEmpty(passHash)
                || !_users.TryGetValue(loginHash, out user)
                || !user.Authorize(loginHash, passHash))
                return null;

            _usersByTokens[user.Token] = user;
            return user.Token;
        }

        public bool Register(string loginHash, string passHash)
        {
            if (string.IsNullOrEmpty(loginHash)
                || string.IsNullOrEmpty(passHash)
                || _users.ContainsKey(loginHash))
                return false;

            _users.Add(loginHash, new Login(loginHash, passHash, DefaultUserSettings));

            return true;
        }

        public bool Logout(Token token)
        {
            var user = GetUserByToken(token);

            if (user == null)
                return false;

            user.Token = null;
            _usersByTokens.Remove(token);
            return true;
        }

        public bool IsLogged(Token token)
        {
            return GetUserByToken(token) != null;
        }

        #endregion

        public Login GetUserByToken(Token token)
        {
            Login user;

            if (!_usersByTokens.TryGetValue(token, out user))
                return null;

            if (user.Token != token)
            {
                _usersByTokens.Remove(token);
                return null;
            }

            var now = DateTime.Now;
            if (user.Token <= now)
            {
                user.Token = null;
                _usersByTokens.Remove(token);
                return null;
            }

            return user;
        }
    }
}