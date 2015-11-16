using HeresyCore.Entities.Data.Learning;
using HeresyCore.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace HeresyCore.Descriptions.Learning
{
    [DataContract, KnownType("GetKnownTypes")]
    public class LearningPackageDescription : EntityDescription
    {
        [DataMember]
        public IEnumerable<LearningItemBase> ItemsToLearn { get; set; } = new List<LearningItemBase>();

        [DataMember]
        public IDictionary<ECharacterStat, int> StatCosts { get; set; }

        [DataMember]
        public int MinRank { get; set; }

        [DataMember]
        public int MaxRank { get; set; }

        private static LearningItemBase GetItemDescription(LearningItemBase item)
        {
            if (item is TraitToLearn)
                return (TraitToLearnDescription)item;
            return item;
        }

        public LearningPackageDescription() { }

        public LearningPackageDescription(LearningPackage package) : base(package)
        {
            ItemsToLearn = package.ItemsToLearn.Select(GetItemDescription);
            StatCosts = package.StatCosts;
            MinRank = package.MinRank;
            MaxRank = package.MaxRank;
        }

        public static implicit operator LearningPackageDescription(LearningPackage package) => new LearningPackageDescription(package);

        public static Type[] GetKnownTypes() => new[]
        {
            typeof(SkillToLearn),
            typeof(TraitToLearnDescription),
        };
    }
}
