using HeresyCore.Descriptions;
using HeresyCore.Entities;
using HeresyCore.Entities.Enums;
using ServiceTester.HeresyService;
using ServiceTester.Utilities.Interfaces;
using System;

namespace ServiceTester.Utilities
{
    public class CharacterContext : ICharacterCreationContext, ICharacterManagementContext
    {
        private AuthorizedContext _auth;
        private HeresyServiceClient Main => _auth.Main;
        private Token Token => _auth.Token;

        public string Id => Character.Id;
        public CharacterDescription Character { get; private set; }

        public CharacterContext(AuthorizedContext auth, CharacterDescription character)
        {
            _auth = auth;
            Character = character;
        }

        #region Methods

        #region Helpers

        private TResult CharacterRelated<TResult>(Func<TResult> worker)
        {
            if (Main == null || Token == null)
                throw new Exception("Контекст не авторизован");

            var result = worker.Invoke();
            var character = Main.GetCharacterDescription(Token, Id);
            if (character == null)
                return result;

            Character = character;
            _auth.CharacterList[Id] = this;
            return result;
        }

        private bool CharacterRelated(Action worker = null)
        {
            if (Main == null || Token == null)
                throw new Exception("Контекст не авторизован");

            worker?.Invoke();
            var character = Main.GetCharacterDescription(Token, Id);
            if (character == null)
                return false;

            Character = character;
            _auth.CharacterList[Id] = this;
            return true;
        }

        #endregion

        #region Creation

        public bool SelectRace(RaceDescription race) => CharacterRelated(() =>
            Main.SelectRace(Token, Id, race.Id)
        );

        public bool RerollStat(ECharacterStat stat) => CharacterRelated(() =>
            Main.RerollStat(Token, Id, stat)
        );

        public bool SkipReroll() => CharacterRelated(() =>
            Main.SkipReroll(Token, Id)
        );

        public bool SelectWorld(WorldDescription world) =>CharacterRelated(() =>
            Main.SelectWorld(Token, Id, world.Id)
        );

        public bool SelectClass(ClassDescription cclass) => CharacterRelated(() =>
            Main.SelectClass(Token, Id, cclass.Id)
        );

        public bool SelectBackground(BackgroundDescription background) => CharacterRelated(() =>
            Main.SelectBackground(Token, Id, background.Id)
        );

        public bool SkipBackground() => CharacterRelated(() =>
            Main.SkipBackground(Token, Id)
        );

        #endregion

        #region Management

        public bool SelectFreebie(int freebieId, int optionId) => CharacterRelated(() =>
            Main.SelectFreebie(Token, Id, freebieId, optionId)
        );

        public bool UpgradeStat(ECharacterStat stat, int points) => CharacterRelated(() =>
            Main.UpgradeStat(Token, Id, stat, points)
        );

        #endregion

        #region Common

        public bool Update() => CharacterRelated();

        #endregion

        #endregion
    }
}
