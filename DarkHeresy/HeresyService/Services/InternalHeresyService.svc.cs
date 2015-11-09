using HeresyCore.Authorization;
using HeresyCore.Utilities;
using HeresyService.Entities;
using HeresyService.InternalAuthService;
using HeresyService.ServiceInterfaces;

namespace HeresyService.Services
{
    public class InternalHeresyService: HeresyService, IInternalHeresyService
    {
        public void RegisterUser(string id, string appSecret)
        {
            var succes = false;

            WcfExtensions.Using<InternalAuthServiceClient>(auth =>
                succes = auth.ValidateAppSecret(appSecret, Rights.CoreService.Common)
            );

            if (!succes)
                return;

            var user = new User(id);
            Users.All.Add(id, user);
        }
    }
}
