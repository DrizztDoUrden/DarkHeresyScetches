using HeresyCore.Entities;
using HeresyService.Entities;
using HeresyService.InternalAuthService;
using HeresyService.ServiceInterfaces;
using System.Collections.Generic;
using System.Configuration;

namespace HeresyService.Services
{
    public class HeresyService: IHeresyService
    {
        private static object _lock = new object();

        private static IDictionary<string, User> _users;
        private static IDictionary<string, User> Users
        {
            get
            {
                lock (_lock)
                {
                    if (_users == null)
                    {
                        _users = TestUsers;
                    }
                }
                return _users;
            }
        }

        public IDictionary<string, Character> GetCharacterList(Token token)
        {
            string id;
            User user;

            using (var auth = new InternalAuthServiceClient())
            {
                id = auth.GetId(token, ConfigurationManager.AppSettings["AppSecret"]);
            }

            if (id == null
                || !Users.TryGetValue(id, out user))
                return null;

            return user.Characters;
        }

        private static IDictionary<string, User> TestUsers => new Dictionary<string, User>
        {
            ["Test"] = new User
            {
                Characters = new Dictionary<string, Character>
                {
                    { "Test", new Character() },
                },
            },
        };
    }
}
