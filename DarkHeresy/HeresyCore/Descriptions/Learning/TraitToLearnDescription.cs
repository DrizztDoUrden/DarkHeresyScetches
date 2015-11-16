using HeresyCore.Entities.Data.Learning;
using System.Runtime.Serialization;

namespace HeresyCore.Descriptions.Learning
{
    [DataContract]
    public class TraitToLearnDescription : LearningItemBase
    {
        [DataMember]
        public EntityDescription Trait { get; set; }

        public TraitToLearnDescription() { }

        public TraitToLearnDescription(TraitToLearn trait)
        {
            ExpCost = trait.ExpCost;
            StatRequirements = trait.StatRequirements;
            SkillRequirements = trait.SkillRequirements;
            TraitRequirements = trait.TraitRequirements;
            Trait = trait.Trait;
        }
    }
}
