using HeresyCore.Descriptions;

namespace ServiceTester.Utilities.Interfaces
{
    public interface ICharacterBaseContext
    {
        string Id { get; }
        CharacterDescription Character { get; }

        bool Update();
    }
}
