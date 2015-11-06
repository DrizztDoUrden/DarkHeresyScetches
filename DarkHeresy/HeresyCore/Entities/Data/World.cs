using System;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data
{
    [DataContract]
    public class World : Group
    {
        [DataMember]
        public Dice[] FateRolls { get; } = new Dice[10];

        protected override string GroupTypeName => "Мир";

        protected override void AddCore(Character character)
        {
            var fateRoll = new Random().Next(0, 9);
            var fate = FateRolls?[fateRoll] ?? 0;
            
            character.MaxFatePoints.Moddifiers.Add("World", fate.Roll().Sum);
        }
    }
}
