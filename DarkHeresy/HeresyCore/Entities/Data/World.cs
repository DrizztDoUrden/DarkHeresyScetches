using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data
{
    [DataContract]
    public class World : Group
    {
        public static string GroupType = "World";

        [DataMember]
        public Dictionary<int, Dice> FateRolls { get; set; } = new Dictionary<int, Dice>();

        public override string GroupTypeName => GroupType;

        protected override void AddCore(Character character)
        {
            var fateRoll = new Random().Next(0, 9);
            Dice fate;

            if (FateRolls.TryGetValue(fateRoll, out fate))
            {
                character.MaxFatePoints.Moddifiers.Add(GroupTypeName, fate.Roll().Sum);
            }
        }
    }
}
