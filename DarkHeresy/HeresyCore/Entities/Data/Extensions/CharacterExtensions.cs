using HeresyCore.Entities.Data.Interfaces;
using HeresyCore.Entities.Enums;

namespace HeresyCore.Entities.Data.Extensions
{
    public static class CharacterExtensions
    {
        public static Character AddStats(this Character character, IStatsContainer stats)
        {
            foreach (var stat in stats.Stats)
            {
                var charStat = character.Stats[stat.Key];
                var statRoll = stat.Value.Roll().Sum;

                charStat.Moddifiers.Add(stats.GroupTypeName, statRoll);
            }

            return character;
        }

        public static Character AddTraits(this Character character, ITraitsContainer traits)
        {
            foreach (var trait in traits.Traits)
            {
                trait.Add(character);
            }

            return character;
        }

        public static Character AddSkills(this Character character, ISkillsContainer skills)
        {
            foreach (var skill in skills.Skills)
            {
                ESkillMastery charSkill;

                if (!character.Skills.TryGetValue(skill.Key, out charSkill)
                    || skill.Value > charSkill)
                    character.Skills[skill.Key] = skill.Value;
            }

            return character;
        }
    }
}
