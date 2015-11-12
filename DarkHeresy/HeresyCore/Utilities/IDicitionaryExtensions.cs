using HeresyCore.Entities.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeresyCore.Utilities
{
    public static class IDicitionaryExtensions
    {
        public static IDictionary<TKey, TValue> Remap<TKey, TSValue, TValue>(this IDictionary<TKey, TSValue> dict, Func<TSValue, TValue> valueConverter)
            => dict.ToDictionary(kvp => kvp.Key, kvp => valueConverter(kvp.Value));

        public static IDictionary<TKey, TValue> ToPlainDictionary<TKey, TValue>(this IDictionary<TKey, Property<TValue>> dict)
            => dict.Remap(value => (TValue)value);

        public static bool TryGetConverted<TKey, TSValue, TValue>(this IDictionary<TKey, TSValue> dict, TKey key, out TValue value, Func<TSValue, TValue> valueConverter)
        {
            TSValue raw;

            var result = dict.TryGetValue(key, out raw);
            value = valueConverter(raw);

            return result;
        }
    }
}
