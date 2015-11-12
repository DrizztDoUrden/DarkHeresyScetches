using HeresyCore.Entities.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HeresyCore.Descriptions
{
    [DataContract]
    public class WorldDescription : GroupDescription
    {
        [DataMember]
        public IDictionary<int, Dice> FateRolls { get; set; }

        public WorldDescription() { }

        public WorldDescription(World world) : base(world)
        {
            FateRolls = world.FateRolls;
        }

        public static implicit operator WorldDescription(World w) =>
            new WorldDescription(w);
    }
}
