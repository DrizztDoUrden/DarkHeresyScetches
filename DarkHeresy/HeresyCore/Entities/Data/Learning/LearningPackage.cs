using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data.Learning
{
    [DataContract, KnownType("GetKnownTypes")]
    public class LearningPackage
    {
        [DataMember]
        public List<LearningItemBase> ItemsToLearn { get; set; } = new List<LearningItemBase>();

        public static Type[] GetKnownTypes() => new[]
        {
            typeof(SkillToLearn),
            typeof(TraitToLearn),
        };
    }
}
