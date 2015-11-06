using HeresyCore.Entities.Properties.Moddifiers;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HeresyCore.Entities.Properties
{
    public abstract class PropertyModdifier<TContent>
    {
        public abstract TContent Moddify(TContent baseValue, ICollection<string> conditions);

        #region Auto converters

        [DebuggerStepThrough]
        public static implicit operator PropertyModdifier<TContent>(Func<TContent, ICollection<string>, TContent> moddifyHandler) =>
            (CustomPropertyModdifier<TContent>)moddifyHandler;

        [DebuggerStepThrough]
        public static implicit operator PropertyModdifier<TContent>(TContent value) => AddModdifier<TContent>.Create(value);

        #endregion
    }
}
