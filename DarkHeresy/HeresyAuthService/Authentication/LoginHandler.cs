using HeresyCore.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HeresyAuthService.Authentication
{
    public class LoginHandler
    {
        private IDictionary<string, User> _users;
        private IDictionary<string, User> _usersByTokens;

        public UserSettings DefaultUserSettings { get; set; }

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
            _usersByTokens = new Dictionary<string, User>();
            DefaultUserSettings = new UserSettings
            {
                AutoResetToken = true,
                TokenLifeTime = new TimeSpan(72, 0, 0),
            };
        }

        [DebuggerStepThrough]
        public LoginHandler()
        {
            Init();
            _users = new Dictionary<string, User>();
        }

        [DebuggerStepThrough]
        public LoginHandler(IDictionary<string, User> userProvider)
        {
            Init();
            _users = userProvider;
        }

        #endregion

        #region Authentication

        public Token Login(string loginHash, string passHash)
        {
            User user;

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

            _users.Add(loginHash, new User(loginHash, passHash, DefaultUserSettings));

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

        public User GetUserByToken(Token token)
        {
            User user;

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