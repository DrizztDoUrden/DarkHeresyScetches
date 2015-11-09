using HeresyCore.Entities.Data;

namespace TestContent
{
    public class Classes : Groups<Class>
    {
        private Classes() : base(new Class { Id = "Class-Test", }) { }

        public static Classes Collection = new Classes();
    }
}
