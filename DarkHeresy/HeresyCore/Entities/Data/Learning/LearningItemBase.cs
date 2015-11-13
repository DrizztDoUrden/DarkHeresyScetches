using HeresyCore.Entities.Data.Traits;
using HeresyCore.Entities.Enums;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data.Learning
{
    [DataContract]
    public abstract class LearningItemBase
    {
        [DataMember]
        public int ExpCost { get; set; }

        [DataMember]
        public IDictionary<ECharacterStat, int> StatRequirements { get; set; } = new Dictionary<ECharacterStat, int>();

        [DataMember]
        public IDictionary<string, ESkillMastery> SkillRequirements { get; set; } = new Dictionary<string, ESkillMastery>();

        [DataMember]
        public List<Trait> TraitRequirements { get; set; } = new List<Trait>();
    }
}
