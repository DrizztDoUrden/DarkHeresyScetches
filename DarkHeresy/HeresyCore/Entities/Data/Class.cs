using HeresyCore.Entities.Enums;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data
{
    [DataContract]
    public class Class : Group
    {
        public const string GroupType = "Class";

        [DataMember]
        public IDictionary<ECharacterStat, int> StatCosts { get; set; } = new Dictionary<ECharacterStat, int>();

        public override string GroupTypeName => GroupType;

        protected override Dice GetDefaultStatValue(ECharacterStat stat) => 0;
    }
}
