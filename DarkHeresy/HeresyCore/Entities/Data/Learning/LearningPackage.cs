using HeresyCore.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data.Learning
{
    [DataContract, KnownType("GetKnownTypes")]
    public class LearningPackage : Entity
    {
        [DataMember]
        public List<LearningItemBase> ItemsToLearn { get; set; } = new List<LearningItemBase>();

        [DataMember]
        public int MinRank { get; set; }

        [DataMember]
        public int MaxRank { get; set; }

        [DataMember]
        public IDictionary<ECharacterStat, int> StatCosts { get; set; }

        protected virtual bool IsAvaibleCore(Character character) => true;

        public virtual string CustomRequirements => null;

        public LearningPackage() { }

        public LearningPackage(LearningPackage original)
        {
            Id = original.Id;
            Name = original.Name;
            ItemsToLearn = new List<LearningItemBase>(original.ItemsToLearn);
            StatCosts = original.StatCosts;
        }

        public bool IsAvaible(Character character)
        {
            var rank = character.RanksTaken + 1;
            return MinRank <= rank
                && rank <= MaxRank
                && IsAvaibleCore(character);
        }

        public void Apply(Character character)
        {
            character.LearningPackages.Add(new LearningPackage(this));
            character.RanksTaken++;
        }

        public static Type[] GetKnownTypes() => new[]
        {
            typeof(SkillToLearn),
            typeof(TraitToLearn),
        };
    }
}
