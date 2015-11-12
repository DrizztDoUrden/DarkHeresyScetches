using HeresyCore.Entities;
using System.Runtime.Serialization;

namespace HeresyCore.Descriptions
{
    [DataContract]
    public class EntityDescription
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        public EntityDescription() { }

        public EntityDescription(Entity entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }

        public static implicit operator EntityDescription(Entity entity) => new EntityDescription(entity);
    }
}
