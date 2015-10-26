using HeresyAuthService.Authentication;
using HeresyAuthService.ServiceInterfaces;
using System;

namespace HeresyAuthService.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HeresyAuthService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HeresyAuthService.svc or HeresyAuthService.svc.cs at the Solution Explorer and start debugging.
    public class HeresyAuthService: IHeresyAuthService
    {
        public bool Register(string loginHash, string passHash)
        {
            throw new NotImplementedException();
        }

        public Token Login(string loginHash, string passHash)
        {
            throw new NotImplementedException();
        }

        public string Logout(Token token)
        {
            throw new NotImplementedException();
        }

        public bool IsLogged(Token token)
        {
            throw new NotImplementedException();
        }

        public string GetId(Token token, string appSecret)
        {
            throw new NotImplementedException();
        }
    }
}
