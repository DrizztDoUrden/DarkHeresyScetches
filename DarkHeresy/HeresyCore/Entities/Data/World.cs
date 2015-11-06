using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data
{
    [DataContract]
    public class World : Group
    {
        [DataMember]
        public IDictionary<int, Dice> FateRolls { get; } = new Dictionary<int, Dice>();

        public override string GroupTypeName => "Мир";

        protected override void AddCore(Character character)
        {
            var fateRoll = new Random().Next(0, 9);
            Dice fate;

            if (FateRolls.TryGetValue(fateRoll, out fate))
            {
                character.MaxFatePoints.Moddifiers.Add("World", fate.Roll().Sum);
            }
        }
    }
}
