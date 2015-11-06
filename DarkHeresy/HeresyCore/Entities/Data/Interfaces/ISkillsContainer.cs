using HeresyCore.Entities.Enums;
using System.Collections.Generic;

namespace HeresyCore.Entities.Data.Interfaces
{
    public interface ISkillsContainer
    {
        IDictionary<string, ESkillMastery> Skills { get; }
    }
}
