using System;
using System.Collections.Generic;

namespace HeresyCore.Entities.Properties.Moddifiers
{
    public sealed class CustomPropertyModdifier<TContent>: PropertyModdifier<TContent>
    {
        public Func<TContent, ICollection<string>, TContent> ModdifyHandler { get; set; }

        #region Constructors

        public CustomPropertyModdifier()
        {

        }

        public CustomPropertyModdifier(Func<TContent, ICollection<string>, TContent> moddifyHandler)
        {
            ModdifyHandler = moddifyHandler;
        }

        #endregion

        public static implicit operator CustomPropertyModdifier<TContent>(Func<TContent, ICollection<string>, TContent> moddifyHandler) =>
            new CustomPropertyModdifier<TContent>(moddifyHandler);

        public override TContent Moddify(TContent baseValue, ICollection<string> conditions)
        {
            return ModdifyHandler(baseValue, conditions);
        }
    }
}
