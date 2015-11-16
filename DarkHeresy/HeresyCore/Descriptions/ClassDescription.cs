using HeresyCore.Descriptions.Learning;
using HeresyCore.Entities.Data;
using HeresyCore.Entities.Enums;
using HeresyCore.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace HeresyCore.Descriptions
{
    [DataContract]
    public class ClassDescription : GroupDescription
    {
        [DataMember]
        public IDictionary<ECharacterStat, int> StatCosts { get; set; }

        [DataMember]
        public IDictionary<int, IEnumerable<LearningPackageDescription>> LearningPackages { get; set; }

        public ClassDescription() { }

        public ClassDescription(Class world) : base(world)
        {
            StatCosts = world.StatCosts;
            LearningPackages = world.LearningPackages.Remap(l => l.Select(p => (LearningPackageDescription)p));
        }

        public static implicit operator ClassDescription(Class c) =>
            new ClassDescription(c);
    }
}
