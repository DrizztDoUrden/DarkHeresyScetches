using HeresyCore.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data
{
    [DataContract]
    public class World : Entity
    {
        private const int DefaultStatValue = 20;

        [DataMember]
        public IDictionary<ECharacterStat, int> Stats { get; private set; }

        [DataMember]
        public int WoundsBase { get; private set; }

        [DataMember]
        public int[] FateRolls { get; private set; }

        public World()
        {
            Stats = Enum.GetValues(typeof(ECharacterStat))
                .Cast<ECharacterStat>()
                .ToDictionary(
                    stat => stat,
                    stat => DefaultStatValue
                );
        }
    }
}
