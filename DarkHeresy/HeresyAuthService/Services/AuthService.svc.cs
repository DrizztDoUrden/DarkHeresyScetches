﻿using HeresyAuthService.Authentication;
using HeresyAuthService.ServiceInterfaces;
using HeresyCore.Entities;

namespace HeresyAuthService.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HeresyAuthService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HeresyAuthService.svc or HeresyAuthService.svc.cs at the Solution Explorer and start debugging.
    public class AuthService: IAuthService
    {
        protected static LoginHandler LoginHandler => LoginHandler.Default;

        public bool Register(string loginHash, string passHash) => LoginHandler.Register(loginHash, passHash);
        public Token Login(string loginHash, string passHash) => LoginHandler.Login(loginHash, passHash);
        public bool Logout(Token token) => LoginHandler.Logout(token);
        public bool IsLogged(Token token) => LoginHandler.IsLogged(token);
    }
}
