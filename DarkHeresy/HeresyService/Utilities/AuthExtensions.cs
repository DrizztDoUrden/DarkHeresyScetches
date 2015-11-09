using HeresyCore.Entities;
using HeresyCore.Utilities;
using HeresyService.Entities;
using HeresyService.InternalAuthService;
using System.Collections.Generic;

namespace HeresyService.Utilities
{
    public static class AuthExtensions
    {
        public static string GetUserId(this Token token)
        {
            string id = null;

            WcfExtensions.Using<InternalAuthServiceClient>(auth =>
                id = auth.GetId(token, AppSecret.Get())
            );

            return id;
        }

        public static User GetUser(this IDictionary<string, User> usersCollection, Token token)
        {
            var id = token.GetUserId();
            User user = null;

            if (id == null
                || !usersCollection.TryGetValue(id, out user))
                return null;

            return user;
        }
    }
}