using HeresyCore.Descriptions;
using HeresyCore.Entities.Enums;
using ServiceTester.HeresyService;
using ServiceTester.Utilities;
using ServiceTester.Utilities.Interfaces;
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

        private static TGroup GetRndGroup<TKey, TGroup>(this IDictionary<TKey, TGroup> groups) where TGroup : GroupDescription =>
            groups.Values.ElementAt(_rnd.Next(groups.Count()));

        private static void Login(AuthorizedContext auth)
        {
            if (!auth.Login(_testLogin, _testPassword))
            {
                if (!auth.Register(_testLogin, _testPassword))
                    throw new Exception("Не удалось зарегистрироваться");

                if (!auth.Login(_testLogin, _testPassword))
                    throw new Exception("Не удалось войти после успешной регистрации");
            }
        }

        private static void CreateCharacter(AuthorizedContext auth, IHeresyService dataContext)
        {
            var race = dataContext.GetRaceDescriptions().GetRndGroup();
            var world = dataContext.GetWorldDescriptions().GetRndGroup();
            var cclass = dataContext.GetClassDescriptions().GetRndGroup();
            var background = dataContext.GetBackgroundDescriptions().GetRndGroup();
            var character = auth.CreateCharacter("Tester");

            character.SelectRace(race);
            character.RerollStat(ECharacterStat.WeaponSkill);
            character.SelectWorld(world);
            character.SelectClass(cclass);
            character.SelectBackground(background);
        }

        private static void ManageCharacter(ICharacterManagementContext character)
        {
            while (character.Character.Freebies.Any())
            {
                character.SelectFreebie(0, 0);
            }

            character.UpgradeStat(ECharacterStat.WeaponSkill, 1);
        }

        private static void TestService() => AuthorizedContext.Using((service, auth) =>
        {
            Login(auth);

            if (!auth.CharacterList.Any())
            {
                CreateCharacter(auth, service);
            }

            var c = auth.CharacterList.First().Value;
            ManageCharacter(c);
        });

        public static void Main()
        {
            TestService();
        }
    }
}
