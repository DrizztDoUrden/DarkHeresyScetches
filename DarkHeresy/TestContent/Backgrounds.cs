using HeresyCore.Entities.Data;

namespace TestContent
{
    public class Backgrounds : Groups<Background>
    {
        private Backgrounds() : base(new Background { Id = "Bg-Test", }) { }

        public static Backgrounds Collection = new Backgrounds();
    }
}
