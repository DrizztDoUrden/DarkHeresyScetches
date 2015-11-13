using HeresyCore.Entities.Enums;
using System.Collections.Generic;

namespace HeresyCore.Entities.Data.Interfaces
{
    public interface IStatsContainer
    {
        string GroupTypeName { get; }

        IDictionary<ECharacterStat, Dice> Stats { get; }
    }
}
