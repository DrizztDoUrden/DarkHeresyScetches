using HeresyCore.Entities.Data;
using HeresyCore.Entities.Enums;
using TestContent.Traits;

namespace TestContent
{
    public class Worlds : Groups<World>
    {
        private Worlds() : base(new World
        {
            Id = "World-Imperial",
            Name = "Имперский мир",
            WoundsBase = 8,
            FateRolls = {[0] = 1,[1] = 1,[2] = 1,[3] = 1,[4] = 1,[5] = 1,[6] = 1,[7] = 1,[8] = 1,[9] = 1, },
            Stats = {[ECharacterStat.Willpower] = 3, },
            Skills = {[@"CL\ImperialCreed"] = ESkillMastery.Common, },
            Traits = { new SoundConstitution(), },
        }) { }

        public static Worlds Collection = new Worlds();
    }
}
