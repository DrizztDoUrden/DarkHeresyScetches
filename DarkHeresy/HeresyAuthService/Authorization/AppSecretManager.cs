﻿using HeresyCore.Authorization;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace HeresyAuthService.Authorization
{
    public class AppSecretManager
    {
        private readonly IDictionary<string, ICollection<string>> _rights;

        #region Singleton

        private static readonly object _lock = new object();
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

            _rights.TryGetValue(appSecret, out appRights);

            return appRights;
        }

        public bool HasRight(string appSecret, string right)
        {
            var appRights = GetRights(appSecret);

            return appRights != null
                && appRights.Contains(right.ToLower());
        }

        public void AddRight(string appSecret, string right)
        {
            var appRights = GetRights(appSecret);

            appRights?.Add(right);
        }

        #endregion

        private static IDictionary<string, ICollection<string>> DefaultRightsProvider =>
            ConfigurationManager.AppSettings["FullRightApps"]
            .Split(';', ',')
            .Select(s => s.Trim())
            .ToDictionary(
                appSecret => appSecret,
                appSecret => (ICollection<string>)Rights.FullRights.ToList()
            );
    }
}