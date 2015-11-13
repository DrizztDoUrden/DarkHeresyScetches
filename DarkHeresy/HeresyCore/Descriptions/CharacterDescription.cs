using HeresyCore.Entities;
using HeresyCore.Entities.Enums;
using HeresyCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace HeresyCore.Descriptions
{
    [DataContract, KnownType("GetKnownTypes")]
    public class CharacterDescription : EntityDescription
    {
        [DataMember]
        public ECreationStage CreationStage { get; set; }

        [DataMember]
        public int FreeExp { get; set; }
        [DataMember]
        public int SpentExp { get; set; }

        [DataMember]
        public int Wounds { get; set; }
        [DataMember]
        public int MaxWounds { get; set; }

        [DataMember]
        public int FatePoints { get; set; }
        [DataMember]
        public int MaxFatePoints { get; set; }

        [DataMember]
        public IDictionary<ECharacterStat, int> Stats { get; set; }

        [DataMember]
        public IDictionary<ECharacterStat, int> StatCosts { get; set; }

        [DataMember]
        public IDictionary<string, string> Groups { get; set; }

        [DataMember]
        public IDictionary<string, ESkillMastery> Skills { get; set; }

        [DataMember]
        public IDictionary<string, int> TestBonuses { get; set; }

        [DataMember]
        public IDictionary<string, TraitDataDescription> Traits { get; set; }

        [DataMember]
        public IEnumerable<FreebieDescription> Freebies { get; set; }

        public CharacterDescription() { }

        public CharacterDescription(Character character) : base(character)
        {
            CreationStage = character.CreationStage;
            FreeExp = character.FreeExp;
            SpentExp = character.SpentExp;
            FatePoints = character.FatePoints;
            Freebies = character.Freebies.Select(f => (FreebieDescription)f);
            Groups = character.Groups;
            MaxFatePoints = character.MaxFatePoints;
            MaxWounds = character.MaxWounds;
            Skills = character.Skills;
            Stats = character.Stats.ToPlainDictionary();
            StatCosts = character.StatCosts;
            TestBonuses = character.TestBonuses.ToPlainDictionary();
            Traits = character.Traits.Remap(data => (TraitDataDescription)data);
            Wounds = character.Wounds;
        }

        public static implicit operator CharacterDescription(Character c) => new CharacterDescription(c);

        public static Type[] GetKnownTypes() => new[]
        {
            TraitDataDescription.GenericsFamily,
        }.SelectMany(t => t).ToArray();
    }
}
