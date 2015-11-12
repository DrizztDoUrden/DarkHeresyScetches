using HeresyCore.Entities;
using HeresyCore.Entities.Data;
using HeresyCore.Entities.Data.Traits;
using HeresyCore.Entities.Enums;
using HeresyCore.Utilities;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HeresyCore.Descriptions
{
    [DataContract]
    public class CharacterDescription : EntityDescription
    {
        [DataMember]
        public ECreationStage CreationStage { get; set; }

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
        public IDictionary<string, string> Groups { get; set; }

        [DataMember]
        public IDictionary<string, ESkillMastery> Skills { get; set; }

        [DataMember]
        public IDictionary<string, int> TestBonuses { get; set; }

        [DataMember]
        public IDictionary<string, TraitData> Traits { get; set; }

        [DataMember]
        public ICollection<Freebie> Freebies { get; set; }

        public CharacterDescription() { }

        public CharacterDescription(Character character) : base(character)
        {
            CreationStage = character.CreationStage;
            FatePoints = character.FatePoints;
            Freebies = character.Freebies;
            Groups = character.Groups;
            MaxFatePoints = character.MaxFatePoints;
            MaxWounds = character.MaxWounds;
            Skills = character.Skills;
            Stats = character.Stats.ToPlainDictionary();
            TestBonuses = character.TestBonuses.ToPlainDictionary();
            Traits = character.Traits;
            Wounds = character.Wounds;
        }

        public static implicit operator CharacterDescription(Character c) =>
            new CharacterDescription(c);
    }
}
