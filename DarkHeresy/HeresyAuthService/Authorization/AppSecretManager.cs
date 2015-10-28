using System.Collections.Generic;
using System.Linq;

namespace HeresyAuthService.Authorization
{
    public class AppSecretManager
    {
        private IDictionary<string, ICollection<string>> _rights;

        #region Singleton

        private static object _lock = new object();
        private static AppSecretManager _default;

        public static AppSecretManager Default
        {
            get
            {
                lock (_lock)
                {
                    if (_default == null)
                    {
                        _default = new AppSecretManager(DefaultRightsProvider);
                    }
                }
                return _default;
            }
        }

        #endregion

        #region Constructors

        public AppSecretManager(IDictionary<string, ICollection<string>> rightsProvider)
        {
            _rights = rightsProvider;
        }

        #endregion

        #region Rights

        public void AddApp(string appSecret)
        {
            _rights[appSecret] = new List<string>();
        }

        public ICollection<string> GetRights(string appSecret)
        {
            ICollection<string> appRights;

            if (!_rights.TryGetValue(appSecret, out appRights))
                return null;

            return appRights;
        }

        public bool HasRight(string appSecret, string right)
        {
            var appRights = GetRights(appSecret);

            if (appRights == null)
                return false;

            return appRights.Contains(right.ToLower());
        }

        public void AddRight(string appSecret, string right)
        {
            var appRights = GetRights(appSecret);

            if (appRights == null)
                return;

            appRights.Add(right);
        }

        #endregion

        private static IDictionary<string, ICollection<string>> DefaultRightsProvider => new Dictionary<string, ICollection<string>>
        {
            ["ServiceTester_SuperSecret"] = Rights.FullRights.ToList(),
            ["HeresyService_SuperSecret"] = Rights.FullRights.ToList(),
        };
    }
}