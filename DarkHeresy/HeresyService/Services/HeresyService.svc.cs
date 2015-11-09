using HeresyCore.Entities;
using HeresyService.Entities;
using HeresyService.ServiceInterfaces;
using HeresyService.Utilities;
using System.Collections.Generic;

namespace HeresyService.Services
{
    public partial class HeresyService : IHeresyService
    {
        protected static class Users
        {
            private static object _lock = new object();

            private static IDictionary<string, User> _users;

            public static IDictionary<string, User> All
            {
                get
                {
                    lock (_lock)
                    {
                        return _users ?? (_users = new Dictionary<string, User>());
                    }
                }
            }
        }

        #region Constructors

        public HeresyService()
        {
            InitCharacterCreation();
        }

        #endregion

        public IDictionary<string, Character> GetCharacterList(Token token) =>
            Users.All
                .GetUser(token)
                ?.Characters;

        public Character GetCharacter(Token token, string charId)
        {
            var chars = GetCharacterList(token);
            Character c;

            if (chars == null
                || !chars.TryGetValue(charId, out c))
                return null;

            return c;
        }
    }
}
