using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Properties
{
    [DataContract]
    public class Property<TContent>
    {
        [DataMember]
        public TContent PureContent { get; set; }
        [DataMember]
        public TContent Content => GetContent(null);
        public IDictionary<string, PropertyModdifier<TContent>> Moddifiers { get; private set; }

        #region Constructors

        public Property()
        {
            Moddifiers = new Dictionary<string, PropertyModdifier<TContent>>();
        }

        public Property(TContent content) : this()
        {
            PureContent = content;
        }

        #endregion

        #region Conversions

        public static implicit operator TContent(Property<TContent> property) => property.Content;
        public static implicit operator Property<TContent>(TContent content) => new Property<TContent>(content);

        #endregion

        public TContent GetContent(ICollection<string> conditions)
        {
            var content = PureContent;

            foreach (var mod in Moddifiers.Values)
            {
                content = mod.Moddify(content, conditions);
            }

            return content;
        }
    }
}
