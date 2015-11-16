using HeresyCore.Descriptions.Learning;
using HeresyCore.Entities.Enums;
using System.Collections.Generic;

namespace ServiceTester.Utilities.Interfaces
{
    public interface ICharacterManagementContext : ICharacterBaseContext
    {
        bool SelectFreebie(int freebieId, int optionId);

        bool UpgradeStat(ECharacterStat stat, int points);

        IEnumerable<LearningPackageDescription> GetAvaibleLearningPackages();

        bool SelectLearningPackages(LearningPackageDescription package);
    }
}
