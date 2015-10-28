using HeresyAuthService.Authorization;
using HeresyService.Entities;
using HeresyService.InternalAuthService;
using HeresyService.ServiceInterfaces;

namespace HeresyService.Services
{
    public class InternalHeresyService: HeresyService, IInternalHeresyService
    {
        public void RegisterUser(string id, string appSecret)
        {
            using (var auth = new InternalAuthServiceClient())
            {
                if (!auth.ValidateAppSecret(appSecret, Rights.CoreService.Common))
                    return;
            }

            var user = new User(id);
            Users.Add(id, user);
        }
    }
}
