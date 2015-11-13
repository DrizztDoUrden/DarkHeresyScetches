using HeresyCore.Entities;
using HeresyCore.Entities.Data;
using HeresyCore.Entities.Data.Extensions;
using HeresyCore.Entities.Enums;
using HeresyService.Utilities;
using System;

namespace HeresyService.Services
{
    public partial class HeresyService
    {
        public string CreateCharacter(Token token, string name)
        {
            var user = Users.All.GetUser(token);

            if (user == null)
                return null;

            var cid = Guid.NewGuid().ToString("N");
            var c = new Character
            {
                Id = cid,
                Name = name,
            };
            user.Characters.Add(cid, c);

            return cid;
        }

        #region Creation stages helpers

        #region Group selecting helpers

        private delegate bool GroupGetter(string id, out Group group);
        private delegate bool GroupExtGetter<T>(string id, out T result) where T : Group;

        private bool SelectGroup(Token token, string charId, string groupId, ECreationStage stage, GroupGetter getter)
        {
            Group group = null;

            return CreationStage(token, charId, stage,
                c => getter(groupId, out group),
                c => c.AddGroup(group)
            );
        }

        private GroupGetter CreateGroupGetter<T>(GroupExtGetter<T> getter) where T : Group => (string id, out Group group) =>
        {
            T result;
            var found = getter(id, out result);
            group = result;

            return found;
        };

        private GroupGetter RaceGetter;
        private GroupGetter WorldGetter;
        private GroupGetter ClassGetter;
        private GroupGetter BackgroundGetter;

        #endregion

        private bool CreationStage(Token token, string charId, ECreationStage stage, Func<Character, bool> condition = null, Action<Character> action = null)
        {
            var c = GetCharacter(token, charId);

            if (c == null
                || !(condition?.Invoke(c) ?? true)
                || !c.TryIncraseCreationStage(stage))
                return false;

            action?.Invoke(c);

            return true;
        }

        #endregion

        #region Initialization

        private void InitCharacterCreation()
        {
            RaceGetter = CreateGroupGetter<Race>(TryGetRace);
            WorldGetter = CreateGroupGetter<World>(TryGetWorld);
            ClassGetter = CreateGroupGetter<Class>(TryGetClass);
            BackgroundGetter = CreateGroupGetter<Background>(TryGetBackground);
        }

        #endregion

        public bool SelectRace(Token token, string charId, string raceId) =>
            SelectGroup(token, charId, raceId, ECreationStage.RaceSelection, RaceGetter);

        public bool RerollStat(Token token, string charId, ECharacterStat stat) => CreationStage(token, charId, ECreationStage.StatReroll,
            action: c => c.RerollStat(stat)
        );

        public bool SkipReroll(Token token, string charId) =>
            CreationStage(token, charId, ECreationStage.StatReroll);

        public bool SelectWorld(Token token, string charId, string worldId) =>
            SelectGroup(token, charId, worldId, ECreationStage.WorldSelection, WorldGetter);

        public bool SelectClass(Token token, string charId, string classId) =>
            SelectGroup(token, charId, classId, ECreationStage.ClassSelection, ClassGetter);

        public bool SelectBackground(Token token, string charId, string backgroundId) =>
            SelectGroup(token, charId, backgroundId, ECreationStage.BackgroundSelection, BackgroundGetter);

        public bool SkipBackground(Token token, string charId) =>
            CreationStage(token, charId, ECreationStage.BackgroundSelection);
    }
}