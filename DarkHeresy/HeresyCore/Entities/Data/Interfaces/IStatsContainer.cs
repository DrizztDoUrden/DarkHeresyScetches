using HeresyCore.Entities.Enums;
using System.Collections.Generic;

namespace HeresyCore.Entities.Data.Interfaces
{
    public interface IStatsContainer
    {
        IDictionary<ECharacterStat, Dice> Stats { get; }
    }
}
