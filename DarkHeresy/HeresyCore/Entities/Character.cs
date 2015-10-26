using System.Runtime.Serialization;

namespace HeresyCore.Entities
{
    [DataContract]
    public class Character
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public int Wounds { get; set; }
        [DataMember]
        public int MaxWounds { get; set; }
    }
}