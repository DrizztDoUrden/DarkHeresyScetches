using HeresyCore.Entities.Data;
using HeresyCore.Entities.Data.Traits;
using HeresyCore.Entities.Enums;
using HeresyCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace HeresyCore.Descriptions
{
    [DataContract, KnownType("GetKnownTypes")]
    public class FreebieDescription
    {
        [DataMember]
        public ICollection<IDictionary<string, object>> Choises { get; set; } =
            new List<IDictionary<string, object>>();

        public static Type[] GetKnownTypes() => new[]
        {
            typeof(EntityDescription),
            typeof(ESkillMastery),
        };

        public FreebieDescription() { }

        public FreebieDescription(Freebie freebie)
        {
            Choises = freebie.Options
                .Select(options => options.Remap(option =>
                {
                    if (option is Trait)
                        return (EntityDescription)((Trait)option);
                    return option;
                }))
                .ToArray();
        }

        public static implicit operator FreebieDescription(Freebie freebie) => new FreebieDescription(freebie);
    }
}
