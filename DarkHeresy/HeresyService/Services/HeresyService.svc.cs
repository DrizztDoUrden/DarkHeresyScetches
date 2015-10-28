using HeresyCore.Entities;
using HeresyCore.Utilities;
using HeresyService.Entities;
using HeresyService.InternalAuthService;
using HeresyService.ServiceInterfaces;
using System.Collections.Generic;

namespace HeresyService.Services
{
    public class HeresyService: IHeresyService
    {
        #region Users

        protected static object _lock = new object();

        private static IDictionary<string, User> _users;
        protected static IDictionary<string, User> Users
        {
            get
            {
                lock (_lock)
                {
                    return _users ?? (_users = new Dictionary<string, User>());
                }
            }
        }

        #endregion

        public IDictionary<string, Character> GetCharacterList(Token token)
        {
            string id;
            User user;

            using (var auth = new InternalAuthServiceClient())
            {
                id = auth.GetId(token, AppSecret.Get());
            }

            if (id == null
                || !Users.TryGetValue(id, out user))
                return null;

            return user.Characters;
        }
    }
}
