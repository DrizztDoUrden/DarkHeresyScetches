using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data
{
    [DataContract]
    public class Roll
    {
        [DataMember]
        public Dice Dice { get; set; }

        [DataMember]
        public ICollection<int> Rolls { get; } = new List<int>();

        public int Sum => Rolls.Sum() + Dice.Constant;

        public Roll(Dice dice)
        {
            Dice = dice;
        }
    }
}
