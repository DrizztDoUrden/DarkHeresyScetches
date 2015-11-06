using HeresyCore.Entities.Data.Traits;
using HeresyCore.Entities.Enums;
using HeresyCore.Entities.Properties;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HeresyCore.Entities
{
    [DataContract]
    public class Character : Entity
    {
        #region Properties

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Property<int> Wounds { get; set; }
        [DataMember]
        public Property<int> MaxWounds { get; set; }

        [DataMember]
        public IDictionary<ECharacterStat, Property<int>> Stats { get; private set; }

        [DataMember]
        public IDictionary<string, ESkillMastery> Skills { get; private set; }

        [DataMember]
        public IDictionary<string, Property<int>> TestBonuses { get; private set; }

        [DataMember]
        public IDictionary<string, TraitData> Traits { get; private set; }

        #endregion

        #region Constructors

        public Character()
        {
            Stats = new Dictionary<ECharacterStat, Property<int>>
            {
                [ECharacterStat.WeaponSkill] = 30,
                [ECharacterStat.BallisticSkill] = 30,

                [ECharacterStat.Strength] = 30,
                [ECharacterStat.Toughness] = 30,
                [ECharacterStat.Agility] = 30,

                [ECharacterStat.Intelligence] = 30,
                [ECharacterStat.Perception] = 30,
                [ECharacterStat.Willpower] = 30,
                [ECharacterStat.Fellowship] = 30,
            };

            Skills = new Dictionary<string, ESkillMastery>
            {
                ["Swim"] = ESkillMastery.Basic,
                ["Climb"] = ESkillMastery.Basic,
                ["Inquiry"] = ESkillMastery.Basic,
                ["Intimidate"] = ESkillMastery.Basic,
                ["Charm"] = ESkillMastery.Basic,
            };

            TestBonuses = new Dictionary<string, Property<int>>();
            Traits = new Dictionary<string, TraitData>();
        }

        #endregion
    }
}