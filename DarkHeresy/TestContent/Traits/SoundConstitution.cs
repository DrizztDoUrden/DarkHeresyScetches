using HeresyCore.Entities;
using HeresyCore.Entities.Data.Traits;
using System.Runtime.Serialization;

namespace TestContent.Traits
{
    [DataContract]
    public sealed class SoundConstitution : Trait<int>
    {
        public override string Id => nameof(SoundConstitution);

        public override string Name => nameof(SoundConstitution);

        protected override TraitData GetDefaultData() => new TraitData<int>(this, 1);

        public override void OnAdd(Character character, TraitData<int> traitData) =>
            character.MaxWounds.Moddifiers[Id] = traitData.Content;

        public override void OnRemove(Character character, TraitData<int> traitData) =>
            character.MaxWounds.Moddifiers.Remove(Id);

        public override bool OnStack(Character character, TraitData<int> traitData, TraitData<int> oldData)
        {
            oldData.Content += traitData.Content;
            return false;
        }
    }
}
