using HeresyCore.Entities;
using HeresyCore.Entities.Data;
using HeresyCore.Entities.Properties;
using HeresyCore.Utilities;
using ServiceTester.HeresyAuthService;
using ServiceTester.HeresyService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace ServiceTester
{
    public static class Program
    {
        private static readonly Random _rnd = new Random();
        private static readonly string _testLogin = ConfigurationManager.AppSettings["TestLogin"];
        private static readonly string _testPassword = ConfigurationManager.AppSettings["TestPassword"];

        private static int TestWoundModdifier(int baseValue, ICollection<string> conditions) => baseValue + 1;

        private static Token TestAuth()
        {
            Token token = null;

            WcfExtensions.Using<AuthServiceClient>(auth =>
            {
                token = auth.Login(_testLogin, _testPassword);

                if (token != null)
                    return;

                if (!auth.Register(_testLogin, _testPassword))
                    throw new Exception("Не удалось зарегистрироваться");

                token = auth.Login(_testLogin, _testPassword);
            });

            if (token == null)
                throw new Exception("Не удалось войти");

            return token;
        }

        private static Group GetRndGroup<T>(IEnumerable<T> groups) where T : Group =>
            groups.ElementAt(_rnd.Next(groups.Count()));

        private static Character AddRndGroup<T>(this Character c, IDictionary<string, T> groups) where T : Group =>
            c.AddGroup(GetRndGroup(groups.Values));

        private static void TestService(Token token)
        {
            var c = new Character();

            WcfExtensions.Using<HeresyServiceClient>(service =>
            {
                var chars = service.GetCharacterList(token);

                if (chars == null)
                    throw new Exception("Не удалось получить список персонажей");

                c.AddRndGroup(service.GetRaces());
                c.AddRndGroup(service.GetWorlds());
                c.AddRndGroup(service.GetClasses());
                c.AddRndGroup(service.GetBackgrounds());
            });

            c.MaxWounds.Moddifiers.Add("Test", (PropertyModdifier<int>)TestWoundModdifier);
            c.MaxWounds.Moddifiers.Add("Test2", 1);
        }

        public static void Main()
        {
            var token = TestAuth();
            TestService(token);
        }
    }
}
