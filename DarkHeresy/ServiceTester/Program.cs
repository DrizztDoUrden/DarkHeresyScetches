using HeresyCore.Entities;
using HeresyCore.Entities.Enums;
using HeresyCore.Entities.Properties;
using HeresyCore.Utilities;
using ServiceTester.HeresyAuthService;
using ServiceTester.HeresyService;
using System;
using System.Collections.Generic;
using System.Configuration;
using TestContent.Traits;

namespace ServiceTester
{
    public static class Program
    {
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

        private static void TestService(Token token)
        {
            WcfExtensions.Using<HeresyServiceClient>(service =>
            {
                var chars = service.GetCharacterList(token);

                if (chars == null)
                    throw new Exception("Не удалось получить список персонажей");
            });
        }

        private static void TestCore()
        {
            var rnd = new Random();

            var c = new Character
            {
                MaxWounds = rnd.Next(1, 5),

                Stats = {
                    [ECharacterStat.WeaponSkill] = 40,
                    [ECharacterStat.BallisticSkill] = 40,
                },

                Skills = {
                    ["Lang_LowGothic"] = ESkillMastery.Common,
                },
            };
            
            c.MaxWounds.Moddifiers.Add("Test", (PropertyModdifier<int>)TestWoundModdifier);
            c.MaxWounds.Moddifiers.Add("Test2", 1);
            c.MaxWounds.Moddifiers.Add("Test2", 2);

            var sc = new SoundConstitution();

            c   .AddTrait(sc)
                .AddTrait(sc)
                .AddTrait(sc)
                .AddTrait(sc)
                .AddTrait(sc);
        }

        public static void Main()
        {
            var token = TestAuth();
            TestService(token);
            TestCore();
        }
    }
}
