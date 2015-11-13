using HeresyCore.Entities;
using HeresyCore.Entities.Data.Extensions;
using HeresyCore.Entities.Enums;
using System;

namespace HeresyService.Services
{
    public partial class HeresyService
    {
        private bool CharacterManagement(Token token, string charId, Func<Character, bool> condition = null, Action<Character> action = null)
        {
            var c = GetCharacter(token, charId);

            if (c == null
                || !(condition?.Invoke(c) ?? true))
                return false;

            action(c);
            return true;
        }

        public bool SelectFreebie(Token token, string charId, int freebieId, int optionId) => CharacterManagement(token, charId, action: c =>
            c.SelectFreebie(freebieId, optionId)
        );

        public bool UpgradeStat(Token token, string charId, ECharacterStat stat, int points) => CharacterManagement(token, charId,
            c => c.IsStatUpgradeAvaible(stat, points),
            c => c.UpgradeStat(stat, points)
        );
    }
}