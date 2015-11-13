using HeresyCore.Entities.Data.Traits;
using HeresyCore.Utilities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HeresyCore.Descriptions
{
    [DataContract]
    public abstract class TraitDataDescription
    {
        [DataMember]
        public EntityDescription Trait { get; set; }

        [DataMember]
        public abstract object Content { get; set; }

        public TraitDataDescription(TraitData traitData)
        {
            Trait = traitData.Trait;
        }

        public TraitDataDescription() { }

        public static implicit operator TraitDataDescription(TraitData data) => data.GetDescription();

        #region GenericsFamily

        public static IEnumerable<Type[]> SupportedSubtypes => new[]
        {
            new [] { typeof(byte), },
            new [] { typeof(short), },
            new [] { typeof(int), },
            new [] { typeof(long), },

            new [] { typeof(ushort), },
            new [] { typeof(uint), },
            new [] { typeof(ulong), },

            new [] { typeof(float), },
            new [] { typeof(double), },

            new [] { typeof(string), },

            new [] { typeof(bool), },
        };

        public static IEnumerable<Type> GenericsFamily => typeof(TraitDataDescription<>).GetGenericTypesFamily(SupportedSubtypes);

        #endregion
    }

    [DataContract]
    public class TraitDataDescription<TContent> : TraitDataDescription
    {
        public TContent TraitContent { get; set; }

        public override object Content
        {
            get
            {
                return TraitContent;
            }
            set
            {
                TraitContent = (TContent)value;
            }
        }

        public TraitDataDescription(TraitData<TContent> data) : base(data)
        {
            TraitContent = data.Content;
        }

        public TraitDataDescription() { }

        public static implicit operator TraitDataDescription<TContent>(TraitData<TContent> data) => new TraitDataDescription<TContent>(data);
    }
}
