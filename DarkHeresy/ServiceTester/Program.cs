using HeresyCore.Entities;
using ServiceTester.HeresyAuthService;
using ServiceTester.HeresyService;
using System;
using System.Configuration;

namespace ServiceTester
{
    public class Program
    {
        private static string _testLogin = ConfigurationManager.AppSettings["TestLogin"];
        private static string _testPassword = ConfigurationManager.AppSettings["TestPassword"];

        public static void Main()
        {
            Token token;

            using (var auth = new AuthServiceClient())
            {
                token = auth.Login(_testLogin, _testPassword);

                if (token == null)
                {
                    if (!auth.Register(_testLogin, _testPassword))
                        throw new Exception("Не удалось зарегистрироваться");

                    token = auth.Login(_testLogin, _testPassword);

                    if (token == null)
                        throw new Exception("Не удалось войти после регистрации");
                }
            }

            using (var service = new HeresyServiceClient())
            {
                var chars = service.GetCharacterList(token);
            }
        }
    }
}
