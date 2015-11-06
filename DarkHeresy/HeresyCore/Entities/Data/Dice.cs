using System;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data
{
    [DataContract]
    public class Dice
    {
        private static Random _random = new Random();

        [DataMember]
        public int DieNumber { get; set; }
        [DataMember]
        public int DieSides { get; set; }
        [DataMember]
        public int Constant { get; set; }

        public Roll Roll()
        {
            var roll = new Roll(this);

            for (var i = DieNumber; i > 0; i--)
            {
                roll.Rolls.Add(_random.Next(1, DieSides + 1));
            }

            return roll;
        }

        #region Constructors

        public Dice()
        {

        }

        public Dice(int number, int sides, int constant = 0)
        {
            DieNumber = number;
            DieSides = sides;
            Constant = constant;
        }

        #endregion

        #region Converters

        public static implicit operator Dice(int constant) => new Dice(0, 0, constant);

        #endregion
    }
}
