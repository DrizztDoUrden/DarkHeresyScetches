using HeresyCore.Entities;
using HeresyCore.Entities.Data;
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
            var sc = new SoundConstitution();
            var c = new Character();

            var r = new Race
            {
                Id = "Race-Human",
                Name = "Человек",
                WoundsBase = new Dice(1, 5),

                Skills =
                {
                    ["Climb"] = ESkillMastery.Basic,
                    ["Swim"] = ESkillMastery.Basic,
                    ["Inquiry"] = ESkillMastery.Basic,
                    ["Intimidate"] = ESkillMastery.Basic,
                    ["Charm"] = ESkillMastery.Basic,
                    [@"Lang\LowGothic"] = ESkillMastery.Common,
                },
            };

            var w = new World
            {
                Id = "World-Imperial",
                Name = "Имперский мир",
                WoundsBase = 8,
                FateRolls = { [0] = 1, [1] = 1, [2] = 1, [3] = 1, [4] = 1, [5] = 1, [6] = 1, [7] = 1, [8] = 1, [9] = 1, },
                Stats = { [ECharacterStat.Willpower] = 3, },
                Skills = { [@"CL\ImperialCreed"] = ESkillMastery.Common, },
                Traits = { sc, },
            };

            c   .AddGroup(r)
                .AddGroup(w);
            
            c.MaxWounds.Moddifiers.Add("Test", (PropertyModdifier<int>)TestWoundModdifier);
            c.MaxWounds.Moddifiers.Add("Test2", 1);
        }

        public static void Main()
        {
            var token = TestAuth();
            TestService(token);
            TestCore();
        }
    }
}
