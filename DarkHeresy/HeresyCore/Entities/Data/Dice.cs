using System;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data
{
    [DataContract]
    public class Dice
    {
        [DataMember]
        public int DieNumber { get; set; }
        [DataMember]
        public int DieSides { get; set; }
        [DataMember]
        public int Constant { get; set; }

        public Roll Roll()
        {
            var roll = new Roll();
            var rnd = new Random();

            for (var i = DieNumber; i > 0; i--)
            {
                roll.Rolls.Add(rnd.Next(1, DieSides + 1));
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
