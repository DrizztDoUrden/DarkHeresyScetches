using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data.Traits
{
    [DataContract]
    public abstract class TraitData
    {
        [DataMember]
        public Trait Trait { get; private set; }

        public abstract object TraitContent { get; set; }

        #region Constructors

        public TraitData(Trait trait)
        {
            Trait = trait;
        }

        public TraitData(Trait trait, object content) : this(trait)
        {
            TraitContent = content;
        }

        #endregion
    }

    [DataContract]
    public class TraitData<TContent> : TraitData
    {
        [DataMember]
        public TContent Content { get; set; }

        public sealed override object TraitContent
        {
            get
            {
                return Content;
            }
            set
            {
                Content = (TContent)value;
            }
        }

        #region Constructors

        public TraitData(Trait trait, TContent content) : base(trait)
        {
            Content = content;
        }

        public TraitData(Trait trait, object content) : base(trait, content)
        {

        }

        public TraitData(Trait trait) : base(trait)
        {

        }

        #endregion
    }
}
