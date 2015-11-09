using HeresyCore.Entities.Enums;
using System.Collections.Generic;

namespace HeresyCore.Entities.Data.Interfaces
{
    public interface IStatsContainer
    {
        string GroupTypeName { get; }

        Dictionary<ECharacterStat, Dice> Stats { get; }
    }
}
