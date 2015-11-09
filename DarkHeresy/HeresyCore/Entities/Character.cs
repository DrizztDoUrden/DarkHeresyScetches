using HeresyCore.Entities.Data;
using HeresyCore.Entities.Data.Traits;
using HeresyCore.Entities.Enums;
using HeresyCore.Entities.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace HeresyCore.Entities
{
    [DataContract]
    public class Character : Entity
    {
        #region Properties

        [DataMember]
        public ECreationStage CreationStage { get; set; } = ECreationStage.RaceSelection;

        [DataMember]
        public int Wounds { get; set; } = 1;
        [DataMember]
        public Property<int> MaxWounds { get; set; } = 1;

        [DataMember]
        public int FatePoints { get; set; } = 0;
        [DataMember]
        public Property<int> MaxFatePoints { get; set; } = 0;

        [DataMember]
        public IDictionary<ECharacterStat, Property<int>> Stats { get; } = Enum.GetValues(typeof(ECharacterStat))
            .Cast<ECharacterStat>()
            .ToDictionary(stat => stat, stat => new Property<int>());

        [DataMember]
        public IDictionary<string, string> Groups { get; } = new Dictionary<string, string>();

        [DataMember]
        public IDictionary<string, ESkillMastery> Skills { get; } = new Dictionary<string, ESkillMastery>();

        [DataMember]
        public IDictionary<string, Property<int>> TestBonuses { get; } = new Dictionary<string, Property<int>>();

        [DataMember]
        public IDictionary<string, TraitData> Traits { get; } = new Dictionary<string, TraitData>();
        
        [DataMember]
        public ICollection<Freebie> Freebies { get; } = new List<Freebie>();

        #endregion

        #region helpers

        public Character AddTrait(Trait trait) => trait.Add(this);

        public Character AddGroup(Group group) => group.Add(this);

        #endregion
    }
}