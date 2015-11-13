using HeresyCore.Entities.Enums;
using HeresyCore.Utilities;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data
{
    [DataContract]
    public class Class : Group
    {
        public const string GroupType = "Class";
        
        [DataMember]
        public IDictionary<ECharacterStat, int> StatCosts { get; set; } = ECharacterStatExtensions.GetStatsDictionary(stat => Character.DefaultStatCost);

        public override string GroupTypeName => GroupType;

        protected override Dice GetDefaultStatValue(ECharacterStat stat) => 0;

        protected override void AddCore(Character character)
        {
            character.StatCosts = StatCosts;
        }
    }
}
