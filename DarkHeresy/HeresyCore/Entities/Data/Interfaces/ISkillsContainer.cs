using HeresyCore.Entities.Enums;
using System.Collections.Generic;

namespace HeresyCore.Entities.Data.Interfaces
{
    public interface ISkillsContainer
    {
        Dictionary<string, ESkillMastery> Skills { get; }
    }
}
