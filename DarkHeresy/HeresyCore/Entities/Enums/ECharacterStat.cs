using System.Runtime.Serialization;

namespace HeresyCore.Entities.Enums
{
    [DataContract]
    public enum ECharacterStat
    {
        [EnumMember]
        WeaponSkill = 10,
        [EnumMember]
        BallisticSkill = 20,

        [EnumMember]
        Strength = 30,
        [EnumMember]
        Toughness = 40,
        [EnumMember]
        Agility = 50,

        [EnumMember]
        Intelligence = 60,
        [EnumMember]
        Perception = 70,
        [EnumMember]
        Willpower = 80,
        [EnumMember]
        Fellowship = 90,
    }
}
