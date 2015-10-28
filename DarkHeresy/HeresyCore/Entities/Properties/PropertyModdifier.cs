using HeresyCore.Entities.Properties.Moddifiers;
using System;
using System.Collections.Generic;

namespace HeresyCore.Entities.Properties
{
    public abstract class PropertyModdifier<TContent>
    {
        public abstract TContent Moddify(TContent baseValue, ICollection<string> conditions);

        public static implicit operator PropertyModdifier<TContent>(Func<TContent, ICollection<string>, TContent> moddifyHandler) =>
            (CustomPropertyModdifier<TContent>)moddifyHandler;
    }
}
