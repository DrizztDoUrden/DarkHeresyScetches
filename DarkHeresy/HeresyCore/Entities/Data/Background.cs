using HeresyCore.Entities.Enums;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data
{
    [DataContract]
    public class Background : Group
    {
        public const string GroupType = "Background";

        public override string GroupTypeName => GroupType;

        protected override Dice GetDefaultStatValue(ECharacterStat stat) => 0;
    }
}
