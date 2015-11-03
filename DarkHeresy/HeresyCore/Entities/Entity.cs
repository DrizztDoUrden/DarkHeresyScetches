using System.Runtime.Serialization;

namespace HeresyCore.Entities
{
    [DataContract]
    public abstract class Entity
    {
        [DataMember]
        public string Id { get; set; }
    }
}
