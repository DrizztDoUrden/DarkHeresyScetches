using HeresyCore.Entities.Data;
using HeresyCore.Entities.Enums;

namespace TestContent
{
    public class Races : Groups<Race>
    {
        private Races() : base(new Race
        {
            Id = "Race-Human",
            Name = "Человек",
            WoundsBase = new Dice(1, 5),
            FreeExp = 600,

            Skills =
            {
                ["Climb"] = ESkillMastery.Basic,
                ["Swim"] = ESkillMastery.Basic,
                ["Inquiry"] = ESkillMastery.Basic,
                ["Intimidate"] = ESkillMastery.Basic,
                ["Charm"] = ESkillMastery.Basic,
                [@"Lang\LowGothic"] = ESkillMastery.Common,
            },
        }) { }

        public static Races Collection = new Races();
    }
}
