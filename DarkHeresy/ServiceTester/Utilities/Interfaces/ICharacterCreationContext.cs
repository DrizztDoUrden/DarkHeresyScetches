using HeresyCore.Descriptions;
using HeresyCore.Entities.Enums;

namespace ServiceTester.Utilities.Interfaces
{
    public interface ICharacterCreationContext : ICharacterBaseContext
    {
        bool SelectRace(RaceDescription race);
        bool RerollStat(ECharacterStat stat);
        bool SkipReroll();
        bool SelectWorld(WorldDescription world);
        bool SelectClass(ClassDescription cclass);
        bool SelectBackground(BackgroundDescription background);
        bool SkipBackground();
    }
}
