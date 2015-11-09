using System.Runtime.Serialization;

namespace HeresyCore.Entities.Enums
{
    [DataContract]
    public enum ECreationStage
    {
        [EnumMember]
        RaceSelection = 10,
        [EnumMember]
        StatReroll = 20,
        [EnumMember]
        WorldSelection = 30,
        [EnumMember]
        ClassSelection = 40,
        [EnumMember]
        BackgroundSelection = 50,



        [EnumMember]
        Finished = 100,
    }
}
