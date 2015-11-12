using HeresyCore.Entities.Data;
using System.Runtime.Serialization;

namespace HeresyCore.Descriptions
{
    [DataContract]
    public class BackgroundDescription : GroupDescription
    {
        public BackgroundDescription() { }

        public BackgroundDescription(Background background) : base(background) { }

        public static implicit operator BackgroundDescription(Background bg) =>
            new BackgroundDescription(bg);
    }
}
