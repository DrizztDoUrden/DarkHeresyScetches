using HeresyCore.Entities.Data.Traits;
using HeresyCore.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data
{
    [DataContract, KnownType("GetKnownTypes")]
    public class Freebie
    {
        [DataMember]
        public List<IDictionary<string, object>> Options { get; } =
            new List<IDictionary<string, object>>();

        public static Type[] GetKnownTypes() => new[]
        {
            typeof(Trait),
            typeof(ESkillMastery),
        };

        public Freebie() { }

        public Freebie(List<IDictionary<string, object>> options)
        {
            Options = options;
        }

        public static implicit operator Freebie(List<IDictionary<string, object>> options) => new Freebie(options);
        public static implicit operator Freebie(IDictionary<string, object>[] options) => new Freebie(options.ToList());
    }
}
