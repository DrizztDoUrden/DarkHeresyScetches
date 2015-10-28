using HeresyAuthService.Authentication;
using HeresyAuthService.Authorization;
using HeresyAuthService.ServiceInterfaces;
using HeresyCore.Entities;

namespace HeresyAuthService.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HeresyAuthService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HeresyAuthService.svc or HeresyAuthService.svc.cs at the Solution Explorer and start debugging.
    public class InternalAuthService: AuthService, IInternalAuthService
    {
        public string GetId(Token token, string appSecret)
        {
            if (!AppSecretManager.Default.HasRight(appSecret, Rights.InternalAuth.Common))
                return null;

            var user = LoginHandler.GetUserByToken(token);

            return user?.ID;
        }
    }
}
