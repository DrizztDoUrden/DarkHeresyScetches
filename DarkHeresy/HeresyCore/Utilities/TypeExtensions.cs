using System;
using System.Collections.Generic;
using System.Linq;

namespace HeresyCore.Utilities
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetGenericTypesFamily(this Type genericType, IEnumerable<Type[]> typeParameters) =>
            typeParameters.Select(types =>
                genericType.MakeGenericType(types)
            );
    }
}
