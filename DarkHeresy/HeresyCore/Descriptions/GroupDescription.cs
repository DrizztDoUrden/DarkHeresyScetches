using HeresyCore.Entities.Data;
using HeresyCore.Entities.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace HeresyCore.Descriptions
{
    [DataContract]
    public abstract class GroupDescription : EntityDescription
    {
        [DataMember]
        public string GroupTypeName { get; set; }

        [DataMember]
        public Dice WoundsBase { get; set; }

        [DataMember]
        public IDictionary<ECharacterStat, Dice> Stats { get; set; }

        [DataMember]
        public IEnumerable<EntityDescription> Traits { get; set; }

        [DataMember]
        public IEnumerable<Freebie> Freebies { get; set; }

        [DataMember]
        public IDictionary<string, ESkillMastery> Skills { get; set; }

        public GroupDescription() { }

        public GroupDescription(Group group) : base(group)
        {
            GroupTypeName = group.GroupTypeName;
            WoundsBase = group.WoundsBase;
            Stats = group.Stats;
            Traits = group.Traits.Select(t => t.GetDescription());
            Freebies = group.Freebies;
            Skills = group.Skills;
        }
    }
}
