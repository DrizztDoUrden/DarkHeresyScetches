using HeresyCore.Entities.Data.Traits;
using System.Runtime.Serialization;

namespace HeresyCore.Descriptions
{
    [DataContract]
    public abstract class TraitDataDescription
    {
        [DataMember]
        public EntityDescription Trait { get; set; }

        public TraitDataDescription(TraitData traitData)
        {
            Trait = traitData.Trait;
        }
    }

    [DataContract]
    public class TraitDataDescription<TContent> : TraitDataDescription
    {
        public TContent Content { get; set; }

        public TraitDataDescription(TraitData<TContent> data) : base(data)
        {
            Content = data.Content;
        }

        public static implicit operator TraitDataDescription<TContent>(TraitData<TContent> data) =>
            new TraitDataDescription<TContent>(data);
    }
}
