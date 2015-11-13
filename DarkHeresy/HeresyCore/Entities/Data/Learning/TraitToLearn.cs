using HeresyCore.Entities.Data.Traits;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data.Learning
{
    [DataContract]
    public class TraitToLearn : LearningItemBase
    {
        [DataMember]
        public Trait Trait { get; set; }

        public TraitToLearn() { }

        public TraitToLearn(Trait trait)
        {
            Trait = trait;
        }
    }
}
