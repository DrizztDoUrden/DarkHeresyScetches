using HeresyCore.Entities.Enums;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data.Learning
{
    [DataContract]
    public class SkillToLearn : LearningItemBase
    {
        [DataMember]
        public string SkillId { get; set; }
        [DataMember]
        public ESkillMastery Mastery { get; set; }

        public SkillToLearn() { }

        public SkillToLearn(string id, ESkillMastery mastery)
        {
            SkillId = id;
            Mastery = mastery;
        }
    }
}
