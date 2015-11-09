using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data
{
    [DataContract]
    public class Freebie
    {
        [DataMember]
        public ICollection<IEnumerable<object>> Choises { get; } =
            new List<IEnumerable<object>>();
    }
}
