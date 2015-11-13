using HeresyCore.Entities.Enums;

namespace ServiceTester.Utilities.Interfaces
{
    public interface ICharacterManagementContext : ICharacterBaseContext
    {
        bool SelectFreebie(int freebieId, int optionId);

        bool UpgradeStat(ECharacterStat stat, int points);
    }
}
