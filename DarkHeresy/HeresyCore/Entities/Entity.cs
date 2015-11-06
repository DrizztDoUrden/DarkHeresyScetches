using System.Runtime.Serialization;

namespace HeresyCore.Entities
{
    [DataContract]
    public abstract class Entity
    {
        [DataMember]
        public virtual string Id { get; set; }

        [DataMember]
        public virtual string Name { get; set; }

        public string GetDebugIdString()
        {
            return $"<{Name}[{Id}]>";
        }
    }
}
