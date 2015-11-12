using HeresyCore.Entities.Data;
using System.Runtime.Serialization;

namespace HeresyCore.Descriptions
{
    [DataContract]
    public class RaceDescription : GroupDescription
    {
        public RaceDescription() { }

        public RaceDescription(Race race) : base(race) { }

        public static implicit operator RaceDescription(Race r) =>
            new RaceDescription(r);
    }
}
