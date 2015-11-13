using HeresyCore.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeresyCore.Utilities
{
    public static class ECharacterStatExtensions
    {
        public static Dictionary<ECharacterStat, TValue> GetStatsDictionary<TValue>(Func<ECharacterStat, TValue> valuesGetter) => Enum.GetValues(typeof(ECharacterStat))
            .Cast<ECharacterStat>()
            .ToDictionary(stat => stat, valuesGetter);
    }
}
