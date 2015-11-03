using HeresyCore.Entities;
using HeresyCore.Entities.Enums;
using HeresyCore.Entities.Properties;
using HeresyCore.Utilities;
using ServiceTester.HeresyAuthService;
using ServiceTester.HeresyService;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace ServiceTester
{
    public class Program
    {
        private static string _testLogin = ConfigurationManager.AppSettings["TestLogin"];
        private static string _testPassword = ConfigurationManager.AppSettings["TestPassword"];

        public static int TestWoundModdifier(int baseValue, ICollection<string> conditions) => baseValue + 1;

        private static Token TestAuth()
        {
            Token token = null;

            WcfExtensions.Using<AuthServiceClient>(auth =>
            {
                token = auth.Login(_testLogin, _testPassword);

                if (token == null)
                {
                    if (!auth.Register(_testLogin, _testPassword))
                        throw new Exception("Не удалось зарегистрироваться");

                    token = auth.Login(_testLogin, _testPassword);
                }
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
            var c = new Character
            {
                Wounds = 5,
                MaxWounds = 5,

                Stats = {
                    [ECharacterStat.WeaponSkill] = 40,
                    [ECharacterStat.BallisticSkill] = 40,
                },

                Skills = {
                    ["Lang_LowGothic"] = ESkillMastery.Common,
                },
            };

            c.Wounds.Moddifiers.Add("Test", (PropertyModdifier<int>)TestWoundModdifier);
            c.MaxWounds.Moddifiers.Add("Test", (PropertyModdifier<int>)TestWoundModdifier);
            c.MaxWounds.Moddifiers.Add("Test2", (PropertyModdifier<int>)TestWoundModdifier);
        }

        public static void Main()
        {
            var token = TestAuth();
            TestService(token);
            TestCore();
        }
    }
}
