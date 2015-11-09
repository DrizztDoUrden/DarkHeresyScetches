using HeresyCore.Entities.Enums;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data
{
    [DataContract]
    public class Race : Group
    {
        public const string GroupType = "Race";

        private const int DefaultStatDice = 2;
        private const int DefaultStatDieSides = 10;
        private const int DefaultStatValue = 20;

        public override string GroupTypeName => GroupType;

        protected override Dice GetDefaultStatValue(ECharacterStat stat)
            => new Dice(DefaultStatDice, DefaultStatDieSides, DefaultStatValue);
    }
}
