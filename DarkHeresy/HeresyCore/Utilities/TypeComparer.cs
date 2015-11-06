using System;
using System.Collections.Generic;

namespace HeresyCore.Utilities
{
    public class TypeComparer : IEqualityComparer<Type>
    {
        public bool Equals(Type x, Type y) => x.AssemblyQualifiedName == y.AssemblyQualifiedName;

        public int GetHashCode(Type obj) => obj.AssemblyQualifiedName.GetHashCode();
    }
}
