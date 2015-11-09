using System.Runtime.Serialization;

namespace HeresyCore.Entities.Enums
{
    [DataContract]
    public enum ESkillMastery
    {
        [EnumMember]
        Basic = 10,
        [EnumMember]
        Common = 20,
        [EnumMember]
        Skilled = 30,
        [EnumMember]
        Master = 40,
    }
}
