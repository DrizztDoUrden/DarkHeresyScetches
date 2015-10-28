using HeresyAuthService.InternalHeresyService;
using HeresyCore.Entities;
using HeresyCore.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HeresyAuthService.Authentication
{
    public class LoginHandler
    {
        private IDictionary<string, Login> _logins;
        private IDictionary<string, Login> _loginsByTokens;

        public LoginSettings DefaultLoginSettings { get; set; }

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
            _loginsByTokens = new Dictionary<string, Login>();
            DefaultLoginSettings = new LoginSettings
            {
                AutoResetToken = true,
                TokenLifeTime = new TimeSpan(72, 0, 0),
            };
        }

        [DebuggerStepThrough]
        public LoginHandler()
        {
            Init();
            _logins = new Dictionary<string, Login>();
        }

        [DebuggerStepThrough]
        public LoginHandler(IDictionary<string, Login> loginProvider)
        {
            Init();
            _logins = loginProvider;
        }

        #endregion

        #region Authentication

        public Token Login(string loginHash, string passHash)
        {
            Login login;

            if (string.IsNullOrEmpty(loginHash)
                || string.IsNullOrEmpty(passHash)
                || !_logins.TryGetValue(loginHash, out login)
                || !login.Authorize(loginHash, passHash))
                return null;

            _loginsByTokens[login.Token] = login;
            return login.Token;
        }

        public bool Register(string loginHash, string passHash)
        {
            if (string.IsNullOrEmpty(loginHash)
                || string.IsNullOrEmpty(passHash)
                || _logins.ContainsKey(loginHash))
                return false;

            var login = new Login(loginHash, passHash, DefaultLoginSettings);
            _logins.Add(loginHash, login);

            using (var heresyService = new InternalHeresyServiceClient())
            {
                heresyService.RegisterUser(login.Id, AppSecret.Get());
            }

            return true;
        }

        public bool Logout(Token token)
        {
            var login = GetLoginByToken(token);

            if (login == null)
                return false;

            login.Token = null;
            _loginsByTokens.Remove(token);
            return true;
        }

        public bool IsLogged(Token token)
        {
            return GetLoginByToken(token) != null;
        }

        #endregion

        public Login GetLoginByToken(Token token)
        {
            Login login;

            if (!_loginsByTokens.TryGetValue(token, out login))
                return null;

            if (login.Token != token)
            {
                _loginsByTokens.Remove(token);
                return null;
            }

            var now = DateTime.Now;
            if (login.Token <= now)
            {
                login.Token = null;
                _loginsByTokens.Remove(token);
                return null;
            }

            return login;
        }
    }
}