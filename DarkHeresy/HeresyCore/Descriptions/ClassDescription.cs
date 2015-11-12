using HeresyCore.Entities.Data;
using HeresyCore.Entities.Enums;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HeresyCore.Descriptions
{
    [DataContract]
    public class ClassDescription : GroupDescription
    {
        [DataMember]
        public Dictionary<ECharacterStat, int> StatCosts { get; set; }

        public ClassDescription() { }

        public ClassDescription(Class world) : base(world)
        {
            StatCosts = world.StatCosts;
        }

        public static implicit operator ClassDescription(Class c) =>
            new ClassDescription(c);
    }
}
