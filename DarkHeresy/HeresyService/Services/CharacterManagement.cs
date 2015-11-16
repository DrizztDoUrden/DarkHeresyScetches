using HeresyCore.Descriptions.Learning;
using HeresyCore.Entities;
using HeresyCore.Entities.Data.Extensions;
using HeresyCore.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeresyService.Services
{
    public partial class HeresyService
    {
        private bool CharacterManagement(Token token, string charId, Func<Character, bool> condition = null, Action<Character> action = null) => CharacterManagement(token, charId, c =>
        {
            if (condition != null && !condition.Invoke(c)) return false;
            action(c);
            return true;
        });

        private TResult CharacterManagement<TResult>(Token token, string charId, Func<Character, TResult> action)
        {
            var c = GetCharacter(token, charId);
            if (c == null) return default(TResult);
            return action(c);
        }

        public bool SelectFreebie(Token token, string charId, int freebieId, int optionId) => CharacterManagement(token, charId,
            action: c => c.SelectFreebie(freebieId, optionId)
        );

        public bool UpgradeStat(Token token, string charId, ECharacterStat stat, int points) => CharacterManagement(token, charId,
            c => c.IsStatUpgradeAvaible(stat, points),
            c => c.UpgradeStat(stat, points)
        );

        public IEnumerable<LearningPackageDescription> GetAvaibleLearningPackages(Token token, string charId) => CharacterManagement(token, charId, c =>
            c.GetAvaibleLearningPackages(GetClass)
            .Select(p => (LearningPackageDescription)p)
        );

        public bool SelectLearningPackages(Token token, string charId, string packageId) => CharacterManagement(token, charId,
            c => c.IsLearningPackageAvaible(GetClass, packageId),
            c => c.SelectLearningPackage(GetClass, packageId)
        );
    }
}
