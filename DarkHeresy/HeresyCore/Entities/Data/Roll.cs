using System.Collections.Generic;
using System.Diagnostics;
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
        
        public int PureSum => Rolls.Sum();
        
        public int Sum => PureSum + Dice.Constant;

        [DebuggerStepThrough]
        public Roll(Dice dice)
        {
            Dice = dice;
        }

        public static implicit operator int(Roll roll)
            => roll.Sum;
    }
}
