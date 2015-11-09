using HeresyCore.Entities.Data.Traits;
using System.Collections.Generic;

namespace HeresyCore.Entities.Data.Interfaces
{
    public interface ITraitsContainer
    {
        List<Trait> Traits { get; }
    }
}
